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
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace UIconEdit
{
    /// <summary>
    /// Base class for icon and cursor files.
    /// </summary>
    public abstract class IconFileBase : IDisposable
    {
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
                        //TODO: Cursor implementation
                        throw new NotImplementedException();
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
                catch { throw new InvalidDataException(); }

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

                                if (loadedId != IconTypeCode.Cursor && bitsPerPixel != entry.YBitsPerpixel)
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

                        if (!returner.FrameCollection.Add(frame))
                        {
                            returner.FrameCollection.RemoveAndDispose(frame);
                            returner.FrameCollection.Add(frame);
                        }
                        offset += entry.ResourceLength;
                    }
                }
                catch (InvalidDataException) { throw; }
                catch (ObjectDisposedException) { throw; }
                catch (IOException) { throw; }
                catch { throw new InvalidDataException(); }

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
        /// When overridden in a derived class, gets the 16-bit identifier for the file type.
        /// </summary>
        public abstract IconTypeCode ID { get; }

        /// <summary>
        /// When overridden in a derived class,
        /// gets a collection containing all frames in the icon file.
        /// </summary>
        protected abstract IFrameCollection FrameCollection { get; }

        /// <summary>
        /// Gets a collection containing all frames in the icon file. 
        /// </summary>
        public IFrameCollection Frames { get { return FrameCollection; } }

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

        internal void Save(Stream output, ICollection<IconFrame> frameCollection)
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
        /// Saves the icon file to the specified stream.
        /// </summary>
        /// <param name="output">The stream to which icon file will be written.</param>
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
            var frameCollection = FrameCollection;
            if (frameCollection.Count == 0 || frameCollection.Count > ushort.MaxValue) throw new InvalidOleVariantTypeException();
            Save(output, frameCollection);
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

            if (disposing)
            {
                foreach (IconFrame item in FrameCollection)
                    item.Dispose();
            }

            isDisposed = true;
        }

        /// <summary>
        /// Destructor.
        /// </summary>
        ~IconFileBase()
        {
            Dispose(false);
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

    /// <summary>
    /// Interface for <see cref="IconFrame"/> collections.
    /// </summary>
    public interface IFrameCollection : IList<IconFrame>
    {
        /// <summary>
        /// Adds the specified item to the list.
        /// </summary>
        /// <param name="item">The item to add to the collection.</param>
        /// <returns><c>true</c> if <paramref name="item"/> was successfully added; <c>false</c> if an <see cref="IconFrame"/> matching the specified criteria
        /// already exists in the list.</returns>
        new bool Add(IconFrame item);

        /// <summary>
        /// Inserts the specified item into the list at the specified index.
        /// </summary>
        /// <param name="index">The index at which to insert the item.</param>
        /// <param name="item">The item to insert into the list.</param>
        /// <returns><c>true</c> if <paramref name="item"/> was successfully added; <c>false</c> if an <see cref="IconFrame"/> matching the specified criteria
        /// already exists in the list.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> is less than 0 or is greater than the number of elements in the list.
        /// </exception>
        new bool Insert(int index, IconFrame item);

        /// <summary>
        /// Sets the value at the specified index.
        /// </summary>
        /// <param name="index">The index of the value to set.</param>
        /// <param name="item">The value to set.</param>
        /// <returns><c>true</c> if <paramref name="item"/> was successfully set; <c>false</c> if an <see cref="IconFrame"/> matching the specified criteria
        /// already exists in the list.</returns>
        bool SetValue(int index, IconFrame item);

        /// <summary>
        /// Removes the specified item from the collection and immediately calls <see cref="IconFrame.Dispose()"/>.
        /// </summary>
        /// <param name="item">The item to search for in the collection.</param>
        /// <returns><c>true</c> if <paramref name="item"/> was found and successfully removed; <c>false</c> otherwise.</returns>
        bool RemoveAndDispose(IconFrame item);

        /// <summary>
        /// Removes the item at the specified index and immediately calls <see cref="IconFrame.Dispose()"/>.
        /// </summary>
        /// <param name="index">The index of the item to remove.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> is less than 0 or is greater than 
        /// </exception>
        void RemoveAndDisposeAt(int index);

        /// <summary>
        /// Returns an array containing elements copied from the current list.
        /// </summary>
        /// <returns>An array containing elements copied from the current list.</returns>
        IconFrame[] ToArray();
    }
}
