#region BSD license
/*
Copyright © 2015, KimikoMuffin.
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this
   list of conditions and the following disclaimer. 
2. Redistributions in binary form must reproduce the above copyright notice,
   this list of conditions and the following disclaimer in the documentation
   and/or other materials provided with the distribution.
3. The names of its contributors may not be used to endorse or promote 
   products derived from this software without specific prior written 
   permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace UIconEdit
{
    /// <summary>
    /// Base class for icon and cursor files.
    /// </summary>
    public abstract class IconFileBase : IDisposable, ICloneable
    {
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public IconFileBase()
        {
            _frames = new FrameList(this);
        }

        /// <summary>
        /// Loads an <see cref="IconFileBase"/> implementation from the specified stream.
        /// </summary>
        /// <param name="input">A stream containing an icon or cursor file.</param>
        /// <returns>An <see cref="IconFileBase"/> implementation loaded from <paramref name="input"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="input"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="input"/> is closed or does not support reading.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// <paramref name="input"/> is closed.
        /// </exception>
        /// <exception cref="InvalidDataException">
        /// <paramref name="input"/> does not contain a valid icon or cursor file.
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurred.
        /// </exception>
        public static IconFileBase Load(Stream input)
        {
            return Load(input, null);
        }

        /// <summary>
        /// Loads an <see cref="IconFileBase"/> implementation from the specified path.
        /// </summary>
        /// <param name="path">The path to a cursor or icon file.</param>
        /// <returns>An <see cref="IconFileBase"/> implementation loaded from <paramref name="path"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="path"/> is empty, contains only whitespace, or contains one or more invalid path characters as defined in <see cref="Path.GetInvalidPathChars()"/>.
        /// </exception>
        /// <exception cref="PathTooLongException">
        /// The specified path, filename, or both contain the system-defined maximum length.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// The specified path was not found.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        /// The specified path was invalid.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// <para><paramref name="path"/> specified a directory.</para>
        /// <para>-OR-</para>
        /// <para>The caller does not have the required permission.</para>
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// <paramref name="path"/> is in an invalid format.
        /// </exception>
        /// <exception cref="InvalidDataException">
        /// <paramref name="path"/> does not contain a valid icon or cursor file.
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurred.
        /// </exception>
        public static IconFileBase Load(string path)
        {
            using (FileStream fs = File.OpenRead(path))
                return Load(fs);
        }

        internal static IconFileBase Load(Stream input, IconTypeCode? id)
        {
#if LEAVEOPEN
            using (BinaryReader reader = new BinaryReader(input, new UTF8Encoding(), true))
#else
            BinaryReader reader = new BinaryReader(input, new UTF8Encoding());
#endif
            {
                if (reader.ReadInt16() != 0) throw new InvalidDataException();

                IconTypeCode loadedId = (IconTypeCode)reader.ReadInt16();
                if (id.HasValue && loadedId != id.Value) throw new InvalidDataException();

                IconFileBase returner;

                switch (loadedId)
                {
                    case IconTypeCode.Cursor:
                        returner = new CursorFile();
                        break;
                    case IconTypeCode.Icon:
                        returner = new IconFile();
                        break;
                    default:
                        throw new InvalidDataException();
                }

                ushort frameCount = reader.ReadUInt16();

                if (frameCount == 0) throw new InvalidDataException();

                IconDirEntry[] frameList = new IconDirEntry[frameCount];
                long offset = (16 * frameCount) + 6;

                for (int i = 0; i < frameCount; i++)
                {
                    IconDirEntry entry = new IconDirEntry();
                    entry.BWidth = reader.ReadByte();
                    entry.BHeight = reader.ReadByte();
                    entry.ColorCount = reader.ReadByte();
                    reader.ReadByte();
                    entry.XPlanes = reader.ReadUInt16();
                    entry.YBitsPerpixel = reader.ReadUInt16();
                    entry.ResourceLength = reader.ReadUInt32();
                    if (entry.ResourceLength <= sizeof(uint)) throw new InvalidDataException();
                    entry.ImageOffset = reader.ReadUInt32();
                    if (entry.ImageOffset < offset) throw new InvalidDataException();
                    frameList[i] = entry;
                }

                try
                {
                    Array.Sort(frameList);
                }
                catch (InvalidDataException) { throw; }
                catch (Exception e) { throw new InvalidDataException(e.Message, e); }

                const int bufferSize = 8192;
                try
                {
                    foreach (IconDirEntry entry in frameList)
                    {
                        long gapLength = entry.ImageOffset - offset;
                        byte[] curBuffer = new byte[bufferSize];
                        while (gapLength > 0)
                            gapLength -= input.Read(curBuffer, 0, (int)Math.Min(gapLength, bufferSize));

                        Bitmap loadedImage;

                        int dibSize = reader.ReadInt32();
                        if (dibSize < MinDibSize) throw new InvalidDataException();

                        const int pngLittleEndian = 0x474e5089; //"\u0089PNG"  in little-endian order.
                        bool isPng = false;
                        BitDepth bitDepth;
                        if (dibSize == pngLittleEndian)
                        {
                            #region Load Png
                            if (loadedId == IconTypeCode.Cursor)
                            {
                                bitDepth = BitDepth.Bit32;
                            }
                            else
                            {
                                switch (entry.YBitsPerpixel)
                                {
                                    case 1:
                                        bitDepth = BitDepth.Color2;
                                        break;
                                    case 4:
                                        bitDepth = BitDepth.Color16;
                                        break;
                                    case 8:
                                        bitDepth = BitDepth.Color256;
                                        break;
                                    case 24:
                                        bitDepth = BitDepth.Bit24;
                                        break;
                                    default: //32
                                        bitDepth = BitDepth.Bit32;
                                        break;
                                }
                            }
                            using (OffsetStream os = new OffsetStream(input, new byte[] { 0x89, 0x50, 0x4e, 0x47 }, entry.ResourceLength - 4, true))
                            using (MemoryStream ms = new MemoryStream())
                            {
                                os.CopyTo(ms);
                                using (Image img = Image.FromStream(ms))
                                {
                                    if (img.Width > IconFrame.MaxDimension || img.Height > IconFrame.MaxDimension) throw new InvalidDataException();

                                    loadedImage = new Bitmap(img.Width, img.Height, PixelFormat.Format32bppArgb);
                                    using (Graphics g = Graphics.FromImage(loadedImage))
                                        g.DrawImage(img, 0, 0, img.Width, img.Height);
                                }
                            }
                            #endregion
                            isPng = true;
                        }
                        else if (dibSize == MinDibSize)
                        {
                            #region Load Bmp
                            using (OffsetStream ms = new OffsetStream(input, entry.ResourceLength - 4, true))
                            using (BinaryReader curReader = new BinaryReader(ms))
                            {
                                int width = curReader.ReadInt32(); //8
                                int height = curReader.ReadInt32(); //12
                                if (width > IconFrame.MaxDimension || width < IconFrame.MinDimension ||
                                    height > (IconFrame.MaxDimension << 1) || height < (IconFrame.MinDimension << 1))
                                    throw new InvalidDataException();

                                ushort colorPanes = curReader.ReadUInt16(); //14
                                ushort bitsPerPixel = curReader.ReadUInt16(); //16

                                switch (bitsPerPixel)
                                {
                                    case 1:
                                        bitDepth = BitDepth.Color2;
                                        break;
                                    case 4:
                                        bitDepth = BitDepth.Color16;
                                        break;
                                    case 8:
                                        bitDepth = BitDepth.Color256;
                                        break;
                                    case 24:
                                        bitDepth = BitDepth.Bit24;
                                        break;
                                    case 32:
                                        bitDepth = BitDepth.Bit32;
                                        break;
                                    default:
                                        throw new InvalidDataException();
                                }
                                if (bitDepth != BitDepth.Bit32 && (height & 1) == 1)
                                    throw new InvalidDataException();

                                if (loadedId != IconTypeCode.Cursor && entry.YBitsPerpixel != 0 && bitsPerPixel != entry.YBitsPerpixel)
                                    throw new InvalidDataException();

                                MemoryStream bufferStream = new MemoryStream();
#if LEAVEOPEN
                                using (BinaryWriter bufferWriter = new BinaryWriter(bufferStream, new UTF8Encoding(), true))
#else
                                BinaryWriter bufferWriter = new BinaryWriter(bufferStream, new UTF8Encoding());
#endif
                                {
                                    bufferWriter.Write(ushort.MinValue); //2
                                    bufferWriter.Write((ushort)IconTypeCode.Icon); //4
                                    bufferWriter.Write((short)1); //6
                                    bufferWriter.Write(entry.BWidth); //7
                                    bufferWriter.Write(entry.BHeight); //8
                                    bufferWriter.Write(entry.ColorCount); //9
                                    bufferWriter.Write(byte.MinValue); //10
                                    bufferWriter.Write((short)1); //12
                                    bufferWriter.Write(bitsPerPixel); //14
                                    bufferWriter.Write(entry.ResourceLength); //18
                                    bufferWriter.Write(22); //22

                                    bufferWriter.Write(dibSize); //26
                                    bufferWriter.Write(width); //30
                                    bufferWriter.Write(height); //34
                                    height /= 2;
                                    bufferWriter.Write(colorPanes); //36
                                    bufferWriter.Write(bitsPerPixel); //38
                                }
#if !LEAVEOPEN
                                bufferWriter.Flush();
#endif
                                ms.CopyTo(bufferStream);
                                bufferStream.Seek(0, SeekOrigin.Begin);
                                Icon icon = new Icon(bufferStream);
                                loadedImage = new Bitmap(icon.Width, icon.Height, PixelFormat.Format32bppArgb);
                                using (Graphics g = Graphics.FromImage(loadedImage))
                                    g.DrawIcon(icon, new Rectangle(0, 0, width, height));
                            }
                            #endregion
                        }
                        else throw new InvalidDataException();

                        Debug.WriteLine("Reading type {0}, width:{1}, height:{2}, bit depth:{3}",
                            isPng ? "PNG" : "BMP", loadedImage.Width, loadedImage.Height, bitDepth);

                        IconFrame frame;

                        if (id == IconTypeCode.Cursor)
                            frame = new CursorFrame(bitDepth, loadedImage, entry.XPlanes, entry.YBitsPerpixel);
                        else
                            frame = new IconFrame(bitDepth, loadedImage);

                        if (!returner.Frames.Add(frame))
                        {
                            returner.Frames.RemoveAndDisposeSimilar(frame);
                            returner.Frames.Add(frame);
                        }
                        offset += entry.ResourceLength;
                    }
                }
                catch (InvalidDataException) { throw; }
                catch (ObjectDisposedException) { throw; }
                catch (IOException) { throw; }
                catch (Exception e) { throw new InvalidDataException(e.Message, e); }

                return returner;
            }
        }

        [DebuggerDisplay("ImageOffset = {ImageOffset}, ResourceLength = {ResourceLength}, End = {End}")]
        private class IconDirEntry : IComparable<IconDirEntry>
        {
            public byte BWidth;
            public byte BHeight;
            public byte ColorCount;
            public ushort XPlanes;
            public ushort YBitsPerpixel;
            public uint ResourceLength;
            public uint ImageOffset;
            public long End { get { return (long)ResourceLength + ImageOffset; } }

            public int CompareTo(IconDirEntry other)
            {
                if (ReferenceEquals(this, other)) return 0;
                if (ReferenceEquals(other, null)) return 1;

                if (End <= other.ImageOffset) return -1; //If this end is comes before the other's offset
                if (other.End <= ImageOffset) return 1; //If the other's end comes before this offset

                throw new InvalidDataException(); //If there's any kind of overlap, someone's wrong.
            }
        }

        /// <summary>
        /// Returns a duplicate of the current instance.
        /// </summary>
        /// <returns>A duplicate of the current instance, with copies of every icon frame and clones of each
        /// frame's <see cref="IconFrame.BaseImage"/> in <see cref="Frames"/>.</returns>
        public virtual IconFileBase Clone()
        {
            IconFileBase copy = (IconFileBase)MemberwiseClone();
            copy._frames = new FrameList(copy);
            foreach (IconFrame curFrame in _frames)
                copy._frames.Add(curFrame.Clone());
            return copy;
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        /// <summary>
        /// When overridden in a derived class, gets the 16-bit identifier for the file type.
        /// </summary>
        public abstract IconTypeCode ID { get; }

        private FrameList _frames;
        /// <summary>
        /// Gets a collection containing all frames in the icon file. 
        /// </summary>
        public FrameList Frames { get { return _frames; } }

        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether the specified value may be added to <see cref="Frames"/>.
        /// </summary>
        /// <param name="frame">The frame to check.</param>
        /// <returns><c>true</c> if <paramref name="frame"/> is not <c>null</c>; <c>false</c> otherwise.</returns>
        protected virtual bool IsValid(IconFrame frame)
        {
            return frame != null;
        }

        /// <summary>
        /// When overridden in a derived class, computes the 16-bit X component.
        /// </summary>
        /// <param name="frame">The image frame to calculate.</param>
        /// <returns>In icon files, the color panes. In cursor files, the horizontal offset of the hotspot from the left in pixels.</returns>
        protected abstract ushort GetImgX(IconFrame frame);

        /// <summary>
        /// When overridden in a derived class, computes the 16-bit Y component.
        /// </summary>
        /// <param name="frame">The image frame to calculate.</param>
        /// <returns>In icon files, the number of bits per pixel. In cursor files, the vertical offset of the hotspot from the top, in pixels.</returns>
        protected abstract ushort GetImgY(IconFrame frame);

        internal void Save(Stream output, IEnumerable<IconFrame> frameCollection)
        {
#if LEAVEOPEN
            using (BinaryWriter writer = new BinaryWriter(output, new UTF8Encoding(), true))
#else
            BinaryWriter writer = new BinaryWriter(output, new UTF8Encoding());
#endif
            {
                SortedSet<IconFrame> frames = new SortedSet<IconFrame>(frameCollection, new IconFrameComparer());

                writer.Write(ushort.MinValue);
                writer.Write((short)ID);
                writer.Write((short)frames.Count);

                uint offset = (uint)(6 + (frames.Count * 16));

                List<MemoryStream> streamList = new List<MemoryStream>();

                foreach (IconFrame curFrame in frames)
                {
                    MemoryStream writeStream;
                    WriteImage(writer, curFrame, ref offset, out writeStream);
                    streamList.Add(writeStream);
                }

                foreach (MemoryStream ms in streamList)
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    ms.CopyTo(output);
                    ms.Dispose();
                }
            }
#if !LEAVEOPEN
            writer.Flush();
#endif
        }

        /// <summary>
        /// Saves the file to the specified stream.
        /// </summary>
        /// <param name="output">The stream to which the file will be written.</param>
        /// <exception cref="InvalidOperationException">
        /// The current instance contains zero frames, or more than <see cref="ushort.MaxValue"/> frames.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="output"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="output"/> is closed or does not support writing.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// <paramref name="output"/> is closed.
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurred.
        /// </exception>
        public void Save(Stream output)
        {
            var frames = Frames;
            if (frames.Count == 0 || frames.Count > ushort.MaxValue) throw new InvalidOperationException();
            try
            {
                Save(output, frames);
            }
            catch (ObjectDisposedException) { throw; }
            catch (IOException) { throw; }
            catch (Exception e) { throw new IOException(e.Message, e); }
        }

        /// <summary>
        /// Saves the file to the specified file.
        /// </summary>
        /// <param name="path">The file to which the file will be written.</param>
        /// <exception cref="InvalidOperationException">
        /// The current instance contains zero frames, or more than <see cref="ushort.MaxValue"/> frames.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="path"/> is empty, contains only whitespace, or contains one or more invalid path characters as defined in <see cref="Path.GetInvalidPathChars()"/>.
        /// </exception>
        /// <exception cref="PathTooLongException">
        /// The specified path, filename, or both contain the system-defined maximum length.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        /// The specified path is invalid.
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurred.
        /// </exception>
        public void Save(string path)
        {
            var frames = Frames;
            if (frames.Count == 0) throw new InvalidOperationException("At least one frame is needed.");
            if (frames.Count > ushort.MaxValue) throw new InvalidOperationException("Two many frames!");
            using (FileStream fs = File.OpenWrite(path))
                try
                {
                    Save(fs, frames);
                }
                catch (ObjectDisposedException) { throw; }
                catch (IOException) { throw; }
                catch (Exception e) { throw new IOException(e.Message, e); }
        }

        const int MinDibSize = 40;

        private void WriteImage(BinaryWriter writer, IconFrame frame, ref uint offset, out MemoryStream writeStream)
        {
            var image = frame.BaseImage;

            bool isPng = (frame.Width > byte.MaxValue || frame.Height > byte.MaxValue);

            Debug.WriteLine("Writing type {0} - width:{1}, height:{2}, bit depth:{3} computed bits per pixel:{4}",
                isPng ? "PNG" : "BMP", frame.Width, frame.Height, frame.BitDepth, GetImgY(frame));

            if (isPng)
                writer.Write(ushort.MinValue); //2
            else
            {
                writer.Write((byte)frame.Width);
                writer.Write((byte)frame.Height);
            }

            int paletteCount;
            Bitmap alphaMask, quantized = frame.GetQuantized(out alphaMask, out paletteCount);

            if (alphaMask == null || quantized.Palette == null || quantized.Palette.Entries.Length > byte.MaxValue)
                writer.Write(byte.MinValue);
            else
                writer.Write((byte)(quantized.Palette.Entries.Length - 1)); //3

            writer.Write(byte.MinValue); //4

            writer.Write(GetImgX(frame)); //6
            writer.Write(GetImgY(frame)); //8

            uint length;
            writeStream = new MemoryStream();
            if (isPng)
            {
                quantized.Save(writeStream, ImageFormat.Png);
            }
            else
            {
#if LEAVEOPEN
                using (BinaryWriter msWriter = new BinaryWriter(writeStream, new UTF8Encoding(), true))
#else
                BinaryWriter msWriter = new BinaryWriter(writeStream, new UTF8Encoding());
#endif
                {
                    ushort bitsPerPixel = frame.BitsPerPixel;
                    int height = quantized.Height;
                    if (alphaMask != null) height += alphaMask.Height; //Only if bit depth != 32
                    msWriter.Write(MinDibSize);
                    msWriter.Write(quantized.Width);
                    msWriter.Write(height);
                    msWriter.Write((short)1);
                    msWriter.Write(bitsPerPixel); //1, 4, 8, 24, or 32
                    msWriter.Write(0); //Compression method = 0

                    msWriter.Write(0);

                    msWriter.Write(new byte[16]); //Skip resolution (8 bytes), palette count (4 bytes), and "important colors" (4 bytes)

                    using (MemoryStream bmpStream = new MemoryStream())
                    using (BinaryReader bmpReader = new BinaryReader(bmpStream))
                    {
                        quantized.Save(bmpStream, ImageFormat.Bmp);
                        bmpStream.Seek(10, SeekOrigin.Begin);
                        uint streamOffset = bmpReader.ReadUInt32();
                        bmpStream.Seek(54, SeekOrigin.Begin);

                        for (int i = 0; i < quantized.Palette.Entries.Length; i++)
                        {
                            uint color = bmpReader.ReadUInt32();
                            msWriter.Write(color);
                        }

                        int dataLength = quantized.Height * 4 * (((bitsPerPixel * quantized.Width) + 31) / 32);

                        byte[] buffer = bmpReader.ReadBytes(dataLength);

                        msWriter.Write(buffer);
                    }

                    if (alphaMask != null)
                    {
                        using (MemoryStream alphaStream = new MemoryStream())
                        using (BinaryReader alphaReader = new BinaryReader(alphaStream))
                        {
                            alphaMask.Save(alphaStream, ImageFormat.Bmp);
                            alphaStream.Seek(10, SeekOrigin.Begin);
                            uint streamOffset = alphaReader.ReadUInt32();
                            alphaStream.Seek(streamOffset, SeekOrigin.Begin);
                            alphaStream.CopyTo(writeStream);
                        }
                    }
                }
#if !LEAVEOPEN
                writer.Flush();
#endif
            }

            if (quantized != image) quantized.Dispose();
            if (alphaMask != null) alphaMask.Dispose();

            length = (uint)writeStream.Length;
            writer.Write(length); //12
            writer.Write(offset); //16
            offset += length;
        }

        private bool isDisposed = false;
        /// <summary>
        /// Immediately releases all resources used by the current instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases all unmanaged resources used by the current instance, and optionally releases all managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed)
                return;

            Frames.ClearAndDispose();

            isDisposed = true;
        }

        /// <summary>
        /// Destructor.
        /// </summary>
        ~IconFileBase()
        {
            Dispose(false);
        }

        /// <summary>
        /// Represents a hash list of frames. This collection treats <see cref="IconFrame"/> objects with the same
        /// <see cref="IconFrame.Width"/>, <see cref="IconFrame.Height"/>, and <see cref="IconFrame.BitDepth"/> as though they were equal.
        /// </summary>
        [DebuggerDisplay("Count = {Count}")]
        [DebuggerTypeProxy(typeof(DebugView))]
        public class FrameList : IList<IconFrame>, IList
#if IREADONLY
            , IReadOnlyList<IconFrame>
#endif
        {
            private HashSet<IconFrame> _set;
            private List<IconFrame> _items;
            private IconFileBase _file;

            internal FrameList(IconFileBase file)
            {
                _file = file;
                _set = new HashSet<IconFrame>(new IconFrameComparer());
                _items = new List<IconFrame>();
            }

            private IconFrame _checkAdd(object value, string paramName)
            {
                if (value == null) throw new ArgumentNullException(paramName);
                IconFrame frame = value as IconFrame;
                if (frame == null) throw new ArgumentException("The specified value is the wrong type.", paramName);
                return frame;
            }

            /// <summary>
            /// Gets and sets the element at the specified index.
            /// </summary>
            /// <param name="index">The index of the element to get or set.</param>
            /// <exception cref="ArgumentOutOfRangeException">
            /// <para><paramref name="index"/> is less than 0 or is greater than or equal to <see cref="Count"/>.</para>
            /// <para>-OR-</para>
            /// <para>In a set operation, the specified value is <c>null</c>.</para>
            /// </exception>
            /// <exception cref="NotSupportedException">
            /// In a set operation, an element with the same <see cref="IconFrame.Width"/>, <see cref="IconFrame.Height"/>, and <see cref="IconFrame.BitDepth"/>
            /// already exists in the list at a different index, or the specified value is already associated with a different icon file.
            /// </exception>
            public IconFrame this[int index]
            {
                get { return _items[index]; }
                set
                {
                    if (_file.isDisposed) throw new ObjectDisposedException(null);
                    if (value == null) throw new ArgumentOutOfRangeException(null, new ArgumentNullException().Message);
                    if (!_setValue(index, value, true))
                        throw new NotSupportedException();
                }
            }

            object IList.this[int index]
            {
                get { return _items[index]; }
                set { this[index] = _checkAdd(value, null); }
            }

            /// <summary>
            /// Gets the number of elements in the list.
            /// </summary>
            public int Count { get { return _items.Count; } }

            /// <summary>
            /// Adds the specified icon frame to the list.
            /// </summary>
            /// <param name="item">The icon frame to add to the list.</param>
            /// <returns><c>true</c> if <paramref name="item"/> was successfully added; <c>false</c> if <paramref name="item"/> is <c>null</c>,
            /// is already associated with a different icon file, or if an element with the same <see cref="IconFrame.Width"/>, <see cref="IconFrame.Height"/>,
            /// and <see cref="IconFrame.BitDepth"/> already exists in the list.</returns>
            public bool Add(IconFrame item)
            {
                return Insert(_items.Count, item);
            }

            void ICollection<IconFrame>.Add(IconFrame item)
            {
                Add(item);
            }

            int IList.Add(object value)
            {
                if (Add(_checkAdd(value, "value"))) return _items.Count;
                throw new NotSupportedException();
            }

            /// <summary>
            /// Adds the specified icon frame to the list at the specified index.
            /// </summary>
            /// <param name="index">The index at which to insert the icon frame.</param>
            /// <param name="item">The icon frame to add to the list.</param>
            /// <returns><c>true</c> if <paramref name="item"/> was successfully added; <c>false</c> if <paramref name="item"/> is <c>null</c>,
            /// is already associated with a different icon file, or if an element with the same <see cref="IconFrame.Width"/>, <see cref="IconFrame.Height"/>,
            /// and <see cref="IconFrame.BitDepth"/> already exists in the list.</returns>
            /// <exception cref="ArgumentOutOfRangeException">
            /// <paramref name="index"/> is less than 0 or is greater than <see cref="Count"/>.
            /// </exception>
            public bool Insert(int index, IconFrame item)
            {
                if (index < 0 || index > _items.Count) throw new ArgumentOutOfRangeException("index");
                if (item == null || item.File != null || !_file.IsValid(item) || !_set.Add(item)) return false;
                _items.Insert(index, item);
                item.File = _file;
                return true;
            }

            void IList<IconFrame>.Insert(int index, IconFrame item)
            {
                Insert(index, item);
            }

            void IList.Insert(int index, object value)
            {
                Insert(index, _checkAdd(value, "value"));
            }

            private bool _setValue(int index, IconFrame value, bool setter)
            {
                if (setter && index == _items.Count)
                    return Add(value);
                var oldItem = _items[index];
                if (value == null || value.File != null || !_file.IsValid(value) || (_set.Contains(value) && !_set.Comparer.Equals(oldItem, value)))
                    return false;
                _set.Remove(oldItem);
                _set.Add(value);
                _items[index] = value;
                oldItem.File = null;
                value.File = _file;
                return true;
            }

            /// <summary>
            /// Sets the value at the specified index.
            /// </summary>
            /// <param name="index">The index of the value to set.</param>
            /// <param name="item">The item to set at the specified index.</param>
            /// <returns><c>true</c> if <paramref name="item"/> was successfully set; <c>false</c> if <paramref name="item"/> is <c>null</c>,
            /// is already associated with a different icon file, or if an element with the same <see cref="IconFrame.Width"/>, <see cref="IconFrame.Height"/>,
            /// and <see cref="IconFrame.BitDepth"/> already exists at a different index.</returns>
            public bool SetValue(int index, IconFrame item)
            {
                return _setValue(index, item, false);
            }

            private void _removeAt(int index, bool disposing)
            {
                IconFrame item = _items[index];
                _items.RemoveAt(index);
                _set.Remove(item);
                item.File = null;
                if (disposing)
                    item.Dispose();
            }

            /// <summary>
            /// Removes the element at the specified index.
            /// </summary>
            /// <param name="index">The element at the specified index.</param>
            /// <exception cref="ArgumentOutOfRangeException">
            /// <paramref name="index"/> is less than 0 or is greater than or equal to <see cref="Count"/>.
            /// </exception>
            public void RemoveAt(int index)
            {
                _removeAt(index, false);
            }

            /// <summary>
            /// Removes the element at the specified index and immediately calls <see cref="IconFrame.Dispose()"/>.
            /// </summary>
            /// <param name="index">The element at the specified index.</param>
            /// <exception cref="ArgumentOutOfRangeException">
            /// <paramref name="index"/> is less than 0 or is greater than or equal to <see cref="Count"/>.
            /// </exception>
            public void RemoveAndDisposeAt(int index)
            {
                _removeAt(index, true);
            }

            private bool _removeSimilar(IconFrame item, bool disposing)
            {
                for (int i = 0; i < _items.Count; i++)
                {
                    if (_set.Comparer.Equals(item, _items[i]))
                    {
                        _removeAt(i, disposing);
                        return true;
                    }
                }
                return false;
            }

            private bool _remove(IconFrame item, bool disposing)
            {
                if (!_items.Remove(item)) return false;
                _set.Remove(item);
                item.File = null;
                if (disposing)
                    item.Dispose();
                return true;
            }

            /// <summary>
            /// Removes the specified icon frame from the list.
            /// </summary>
            /// <param name="item">The icon frame to remove from the list.</param>
            /// <returns><c>true</c> if <paramref name="item"/> was found and successfully removed; <c>false</c> otherwise.</returns>
            public bool Remove(IconFrame item)
            {
                return _remove(item, false);
            }

            void IList.Remove(object value)
            {
                Remove(value as IconFrame);
            }

            /// <summary>
            /// Removes the specified icon frame from the list and immediately callse <see cref="IconFrame.Dispose()"/>.
            /// </summary>
            /// <param name="item">The icon frame to remove from the list.</param>
            /// <returns><c>true</c> if <paramref name="item"/> was found and successfully removed; <c>false</c> otherwise.</returns>
            public bool RemoveAndDispose(IconFrame item)
            {
                return _remove(item, true);
            }

            /// <summary>
            /// Removes an icon frame similar to the specified value from the list.
            /// </summary>
            /// <param name="item">The icon frame compare.</param>
            /// <returns><c>true</c> if an icon frame with the same <see cref="IconFrame.Width"/>, <see cref="IconFrame.Height"/>, and <see cref="IconFrame.BitDepth"/>
            /// as <paramref name="item"/> was successfully found and removed; <c>false</c> if no such icon frame was found in the list.</returns>
            public bool RemoveSimilar(IconFrame item)
            {
                return _removeSimilar(item, false);
            }

            /// <summary>
            /// Removes an icon frame similar to the specified value from the list
            /// and immediately calls <see cref="IconFrame.Dispose()"/>.
            /// </summary>
            /// <param name="item">The icon frame to search for.</param>
            /// <returns><c>true</c> if an icon frame with the same <see cref="IconFrame.Width"/>, <see cref="IconFrame.Height"/>, and <see cref="IconFrame.BitDepth"/>
            /// as <paramref name="item"/> was successfully found and removed; <c>false</c> if no such icon frame was found in the list.</returns>
            public bool RemoveAndDisposeSimilar(IconFrame item)
            {
                return _removeSimilar(item, true);
            }

            private void _clear(bool disposing)
            {
                foreach (IconFrame item in _items)
                {
                    item.File = null;
                    if (disposing)
                        item.Dispose();
                }
                _set.Clear();
                _items.Clear();
            }

            /// <summary>
            /// Removes all elements from the list.
            /// </summary>
            public void Clear()
            {
                _clear(false);
            }

            /// <summary>
            /// Removes all elements from the list and immediately calls <see cref="IconFrame.Dispose()"/> on each one.
            /// </summary>
            public void ClearAndDispose()
            {
                _clear(true);
            }

            /// <summary>
            /// Determines if the specified element exists in the list.
            /// </summary>
            /// <param name="item">The icon frame to search for in the list.</param>
            /// <returns><c>true</c> if <paramref name="item"/> was found; <c>false</c> otherwise.</returns>
            public bool Contains(IconFrame item)
            {
                return _file.IsValid(item) && _items.Contains(item);
            }

            bool IList.Contains(object value)
            {
                return Contains(value as IconFrame);
            }

            /// <summary>
            /// Determines if an element similar to the specified icon frame exists in the list.
            /// </summary>
            /// <param name="item">The icon frame to compare.</param>
            /// <returns><c>true</c> if an icon frame with the same with the same <see cref="IconFrame.Width"/>, <see cref="IconFrame.Height"/>, and <see cref="IconFrame.BitDepth"/>
            /// as <paramref name="item"/> exists in the list; <c>false</c> otherwise.</returns>
            public bool ContainsSimilar(IconFrame item)
            {
                return item != null && _set.Contains(item);
            }

            /// <summary>
            /// Gets the index of the specified item.
            /// </summary>
            /// <param name="item">The icon frame to search for in the list.</param>
            /// <returns>The index of <paramref name="item"/>, if found; otherwise, -1.</returns>
            public int IndexOf(IconFrame item)
            {
                if (!_file.IsValid(item)) return -1;
                return _items.IndexOf(item);
            }

            int IList.IndexOf(object value)
            {
                return IndexOf(value as IconFrame);
            }

            /// <summary>
            /// Gets the index of an element similar to the specified item.
            /// </summary>
            /// <param name="item">The icon frame to compare.</param>
            /// <returns>The index of an icon frame with the same <see cref="IconFrame.Width"/>, <see cref="IconFrame.Height"/>, and <see cref="IconFrame.BitDepth"/>
            /// as <paramref name="item"/>, if found; otherwise, -1.</returns>
            public int IndexOfSimilar(IconFrame item)
            {
                if (item == null) return -1;
                for (int i = 0; i < _items.Count; i++)
                    if (_set.Comparer.Equals(item, _items[i])) return i;
                return -1;
            }

            /// <summary>
            /// Copies all elements in the list to the specified array.
            /// </summary>
            /// <param name="array">The array to which all elements in the list will be copied.</param>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="array"/> is <c>null</c>.
            /// </exception>
            /// <exception cref="ArgumentException">
            /// The length of <paramref name="array"/> is less than <see cref="Count"/>.
            /// </exception>
            public void CopyTo(IconFrame[] array)
            {
                _items.CopyTo(array);
            }

            /// <summary>
            /// Copies all elements in the list to the specified array, starting at the specified index.
            /// </summary>
            /// <param name="array">The array to which all elements in the list will be copied.</param>
            /// <param name="arrayIndex">The index in <paramref name="array"/> at which copying begins.</param>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="array"/> is <c>null</c>.
            /// </exception>
            /// <exception cref="ArgumentOutOfRangeException">
            /// <paramref name="arrayIndex"/> is less than 0.
            /// </exception>
            /// <exception cref="ArgumentException">
            /// The length of <paramref name="array"/> minus <paramref name="arrayIndex"/> is less than <see cref="Count"/>.
            /// </exception>
            public void CopyTo(IconFrame[] array, int arrayIndex)
            {
                _items.CopyTo(array, arrayIndex);
            }

            /// <summary>
            /// Copies a range of elements in the list to the specified array.
            /// </summary>
            /// <param name="index">The index of the first item to copy.</param>
            /// <param name="array">The array to which all elements in the list will be copied.</param>
            /// <param name="arrayIndex">The index in <paramref name="array"/> at which copying begins.</param>
            /// <param name="count">The number of elements to copy.</param>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="array"/> is <c>null</c>.
            /// </exception>
            /// <exception cref="ArgumentOutOfRangeException">
            /// <paramref name="index"/>, <paramref name="arrayIndex"/>, or <paramref name="count"/> is less than 0.
            /// </exception>
            /// <exception cref="ArgumentException">
            /// <para><paramref name="index"/> and <paramref name="count"/> do not indicate a valid range of elements in the current instance.</para>
            /// <para>-OR-</para>
            /// <para><paramref name="arrayIndex"/> and <paramref name="count"/> do not indicate a valid range of elements in <paramref name="array"/>.</para>
            /// </exception>
            public void CopyTo(int index, IconFrame[] array, int arrayIndex, int count)
            {
                _items.CopyTo(index, array, arrayIndex, count);
            }

            /// <summary>
            /// Returns an enumerator which iterates through the list.
            /// </summary>
            /// <returns>An enumerator which iterates through the list.</returns>
            public Enumerator GetEnumerator()
            {
                return new Enumerator(this);
            }

            /// <summary>
            /// Returns an array containing all elements in the current list.
            /// </summary>
            /// <returns>An array containing elements copied from the current list.</returns>
            public IconFrame[] ToArray()
            {
                return _items.ToArray();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            IEnumerator<IconFrame> IEnumerable<IconFrame>.GetEnumerator()
            {
                return GetEnumerator();
            }

            private int _removeWhere(Predicate<IconFrame> match, bool disposing)
            {
                if (match == null) throw new ArgumentNullException("match");
                int removed = 0;
                for (int i = 0; i < _items.Count; i++)
                {
                    while (i < _items.Count && match(_items[i]))
                    {
                        removed++;
                        IconFrame oldItem = _items[i];
                        _set.Remove(oldItem);
                        _items.RemoveAt(i);
                        if (disposing)
                            oldItem.Dispose();
                    }
                }
                return removed;
            }

            /// <summary>
            /// Removes all elements matching the specified predicate.
            /// </summary>
            /// <param name="match">A predicate used to define the elements to remove.</param>
            /// <returns>The number of elements which were removed.</returns>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="match"/> is <c>null</c>.
            /// </exception>
            public int RemoveWhere(Predicate<IconFrame> match)
            {
                return _removeWhere(match, false);
            }

            /// <summary>
            /// Removes all elements matching the specified predicate and immediately calls <see cref="IconFrame.Dispose()"/>.
            /// </summary>
            /// <param name="match">A predicate used to define the elements to remove.</param>
            /// <returns>The number of elements which were removed.</returns>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="match"/> is <c>null</c>.
            /// </exception>
            public int RemoveAndDisposeWhere(Predicate<IconFrame> match)
            {
                return _removeWhere(match, true);
            }

            bool ICollection<IconFrame>.IsReadOnly
            {
                get { return true; }
            }

            bool ICollection.IsSynchronized
            {
                get { return false; }
            }

            bool IList.IsFixedSize
            {
                get { return false; }
            }

            bool IList.IsReadOnly
            {
                get { return false; }
            }

            object ICollection.SyncRoot
            {
                get { return ((ICollection)_items).SyncRoot; }
            }

            void ICollection.CopyTo(Array array, int index)
            {
                ((ICollection)_items).CopyTo(array, index);
            }


            /// <summary>
            /// An enumerator which iterates through the list.
            /// </summary>
            public struct Enumerator : IEnumerator<IconFrame>
            {
                private IconFrame _current;
                private IEnumerator<IconFrame> _enum;

                internal Enumerator(FrameList set)
                {
                    _current = null;
                    _enum = set._items.GetEnumerator();
                }

                /// <summary>
                /// Gets the element at the current position in the enumerator.
                /// </summary>
                public IconFrame Current
                {
                    get { return _current; }
                }

                object IEnumerator.Current
                {
                    get { return _current; }
                }

                /// <summary>
                /// Disposes of the current instance.
                /// </summary>
                public void Dispose()
                {
                    if (_enum == null) return;
                    _enum.Dispose();
                    _enum = null;
                    _current = null;
                }

                /// <summary>
                /// Advances the enumerator to the next position in the list.
                /// </summary>
                /// <returns><c>true</c> if the enumerator successfully advanced; <c>false</c> if the enumerator has passed the end of the list.</returns>
                public bool MoveNext()
                {
                    if (_enum == null) return false;
                    if (!_enum.MoveNext())
                    {
                        Dispose();
                        return false;
                    }
                    _current = _enum.Current;
                    return true;
                }

                void IEnumerator.Reset()
                {
                    _enum.Reset();
                }
            }

            private class DebugView
            {
                private FrameList _list;

                public DebugView(FrameList list)
                {
                    _list = list;
                }

                [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
                public IconFrame[] Items
                {
                    get { return _list._items.ToArray(); }
                }
            }
        }
    }

    /// <summary>
    /// The type code for an icon file.
    /// </summary>
    public enum IconTypeCode : short
    {
        /// <summary>
        /// Indicates an icon (.ICO) file.
        /// </summary>
        Icon = 1,
        /// <summary>
        /// Indicates a cursor (.CUR) file.
        /// </summary>
        Cursor = 2,
    }
}
