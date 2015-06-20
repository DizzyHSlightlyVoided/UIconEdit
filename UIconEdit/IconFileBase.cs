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
    public abstract class IconFileBase
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
            using (BinaryReader reader = new BinaryReader(input, new UTF8Encoding(), true))
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

                        Image loadedImage;

                        int dibSize = reader.ReadInt32();
                        if (dibSize < MinDibSize) throw new InvalidDataException();

                        const int pngLittleEndian = 0x474e5089; //"\u0089PNG"  in little-endian order.
                        BitDepth bitDepth;
                        if (dibSize == pngLittleEndian)
                        {
                            bitDepth = BitDepth.Bit32;
                            #region Load Png
                            using (OffsetStream ms = new OffsetStream(input, new byte[] { 0x89, 0x50, 0x4e, 0x47 }, entry.ResourceLength - 4, true))
                            {
                                loadedImage = Image.FromStream(ms);
                                ms.ReadToEnd();
                            }
                            #endregion
                        }
                        else
                        {
                            #region Load Bmp
                            using (OffsetStream ms = new OffsetStream(input, entry.ResourceLength - 4, true))
                            using (BinaryReader curReader = new BinaryReader(ms))
                            {
                                int width = curReader.ReadInt32();
                                int height = curReader.ReadInt32() / 2;
                                if (curReader.ReadInt16() != 1) throw new InvalidDataException();
                                short bitsPerPixel = curReader.ReadInt16();
                                if (loadedId == IconTypeCode.Icon && bitsPerPixel != entry.YBitsPerpixel)
                                    throw new InvalidDataException();
                                PixelFormat format;
                                int maxPalette;
                                switch (bitsPerPixel)
                                {
                                    case 1:
                                        format = PixelFormat.Format1bppIndexed;
                                        maxPalette = 2;
                                        bitDepth = BitDepth.Color2;
                                        break;
                                    case 4:
                                        format = PixelFormat.Format4bppIndexed;
                                        maxPalette = 16;
                                        bitDepth = BitDepth.Color16;
                                        break;
                                    case 8:
                                        format = PixelFormat.Format8bppIndexed;
                                        maxPalette = 256;
                                        bitDepth = BitDepth.Color256;
                                        break;
                                    case 24:
                                        format = PixelFormat.Format24bppRgb;
                                        maxPalette = 0;
                                        bitDepth = BitDepth.Bit24;
                                        break;
                                    case 32:
                                        format = PixelFormat.Format32bppArgb;
                                        maxPalette = 0;
                                        bitDepth = BitDepth.Bit32;
                                        break;
                                    default:
                                        throw new InvalidDataException();
                                }
                                if (curReader.ReadInt32() != 0) throw new InvalidDataException();
                                int imageSize = curReader.ReadInt32();
                                curReader.ReadInt64(); curReader.ReadInt64(); //Skip ahead 16 bytes
                                Bitmap loadedBmp = new Bitmap(width, height, format), alphaBmp = new Bitmap(width, height, PixelFormat.Format1bppIndexed);
                                Rectangle fullRect = new Rectangle(0, 0, width, height);

                                for (int i = 0; i < maxPalette; i++)
                                {
                                    byte r = curReader.ReadByte(), g = curReader.ReadByte(), b = curReader.ReadByte(), a = curReader.ReadByte();
                                    loadedBmp.Palette.Entries[i] = Color.FromArgb(a, r, g, b);
                                }

                                CopyData(curReader, ref alphaBmp, fullRect);
                                CopyData(curReader, ref loadedBmp, fullRect);

                                unsafe
                                {
                                    BitmapData loadedData = loadedBmp.LockBits(fullRect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                                    BitmapData alphaData = alphaBmp.LockBits(fullRect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

                                    long length = (long)width * height;

                                    uint* pLoaded = (uint*)loadedData.Scan0, pAlpha = (uint*)alphaData.Scan0;

                                    for (int i = 0; i < length; i++)
                                    {
                                        const uint noAlpha = 0xFFFFFF, alpha = ~noAlpha;

                                        if (pAlpha[i] == uint.MaxValue)
                                            pLoaded[i] |= alpha;
                                        else
                                            pLoaded[i] &= noAlpha;
                                    }
                                    loadedBmp.UnlockBits(loadedData);
                                    alphaBmp.UnlockBits(alphaData);
                                }

                                alphaBmp.Dispose();
                                loadedImage = loadedBmp;

                                ms.ReadToEnd();
                            }
                            #endregion
                        }

                        IconFrame frame;
                        if (id == IconTypeCode.Cursor)
                            frame = new CursorFrame(loadedImage, bitDepth, entry.XPlanes, entry.YBitsPerpixel);
                        else
                            frame = new IconFrame(loadedImage, bitDepth);
                        if (!returner.FrameCollection.Add(frame))
                        {
                            returner.FrameCollection.Remove(frame);
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

        internal static unsafe void CopyData(BinaryReader reader, ref Bitmap bitmap, Rectangle fullRect)
        {
            BitmapData bmpData = bitmap.LockBits(fullRect, ImageLockMode.WriteOnly, bitmap.PixelFormat);

            for (int y = bitmap.Height - 1; y >= 0; y--)
            {
                byte[] buffer = reader.ReadBytes(bmpData.Stride);
                if (buffer.Length < bmpData.Stride) throw new EndOfStreamException();
                Marshal.Copy(buffer, 0, bmpData.Scan0 + (y * bmpData.Stride), buffer.Length);
            }
            bitmap.UnlockBits(bmpData);

            if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
            {
                Bitmap newBmp = bitmap.Clone(fullRect, PixelFormat.Format32bppArgb);
                bitmap.Dispose();
                bitmap = newBmp;
            }
        }

        [System.Diagnostics.DebuggerDisplay("ImageOffset = {ImageOffset}, ResourceLength = {ResourceLength}, End = {End}")]
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
            using (BinaryWriter writer = new BinaryWriter(output, new UTF8Encoding(), true))
            {
                SortedSet<IconFrame> frames = new SortedSet<IconFrame>(frameCollection, new IconFrameComparer());

                writer.Write(ushort.MinValue);
                writer.Write((short)ID);
                writer.Write(frames.Count);

                uint offset = 6;

                List<MemoryStream> streamList = new List<MemoryStream>();

                foreach (IconFrame curFrame in frames)
                {
                    MemoryStream writeStream;
                    WriteImage(writer, curFrame, ref offset, out writeStream);
                    streamList.Add(writeStream);
                }

                foreach (MemoryStream ms in streamList)
                {
                    ms.CopyTo(output);
                    ms.Dispose();
                }
            }
        }

        /// <summary>
        /// Saves the icon file to the specified stream.
        /// </summary>
        /// <param name="output">The stream to which icon file will be written.</param>
        /// <exception cref="InvalidOperationException">
        /// The image contains zero frames.
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
            if (FrameCollection.Count == 0) throw new InvalidOperationException("No images set.");
            Save(output, FrameCollection);
        }

        const int MinDibSize = 40;

        private void WriteImage(BinaryWriter writer, IconFrame frame, ref uint offset, out MemoryStream writeStream)
        {
            var image = frame.BaseImage;
            if (frame.Width > byte.MaxValue) writer.Write(byte.MinValue);
            else writer.Write((byte)frame.Width); //1
            if (frame.Height > byte.MaxValue) writer.Write(byte.MinValue);
            else writer.Write((byte)frame.Height); //2

            int paletteCount;
            Bitmap alphaMask, quantized = frame.GetQuantized(out alphaMask, out paletteCount);

            if (alphaMask == null || quantized.Palette == null)
                writer.Write(byte.MinValue);
            else
                writer.Write((byte)(image.Palette.Entries.Length - 1)); //3

            writer.Write(byte.MinValue); //4

            writer.Write(GetImgX(frame)); //6
            writer.Write(GetImgY(frame)); //8

            uint length;
            writeStream = new MemoryStream();
            if (alphaMask == null)
            {
                image.Save(writeStream, ImageFormat.Png);
            }
            else
            {
                using (BinaryWriter msWriter = new BinaryWriter(writeStream, new UTF8Encoding(), true))
                {
                    msWriter.Write(MinDibSize);
                    msWriter.Write(quantized.Width);
                    msWriter.Write(quantized.Height + alphaMask.Height);
                    msWriter.Write((short)1);
                    msWriter.Write(frame.BitsPerPixel); //1, 4, 8, or 24
                    msWriter.Write(0); //Compression method = 0

                    Rectangle fullRect = new Rectangle(0, 0, quantized.Width, quantized.Height + alphaMask.Height);
                    BitmapData alphaData = alphaMask.LockBits(fullRect, ImageLockMode.ReadOnly, alphaMask.PixelFormat);
                    BitmapData imageData = quantized.LockBits(fullRect, ImageLockMode.ReadOnly, quantized.PixelFormat);

                    msWriter.Write((alphaData.Stride * fullRect.Height) + (imageData.Stride * fullRect.Height));

                    msWriter.Write(new byte[16]); //Skip resolution (8 bytes), palette count (4 bytes), and "important colors" (4 bytes)

                    if (quantized.Palette != null)
                    {
                        foreach (Color c in quantized.Palette.Entries)
                        {
                            msWriter.Write(c.R);
                            msWriter.Write(c.G);
                            msWriter.Write(c.B);
                            msWriter.Write(byte.MaxValue);
                        }
                    }

                    for (int y = fullRect.Height - 1; y >= 0; y--)
                    {
                        byte[] buffer = new byte[alphaData.Stride];

                        Marshal.Copy(alphaData.Scan0 + (y * alphaData.Stride), buffer, 0, buffer.Length);
                        msWriter.Write(buffer);
                    }

                    alphaMask.UnlockBits(alphaData);
                    alphaMask.Dispose();

                    for (int y = fullRect.Height - 1; y >= 0; y--)
                    {
                        byte[] buffer = new byte[imageData.Stride];

                        Marshal.Copy(imageData.Scan0 + (y * imageData.Stride), buffer, 0, buffer.Length);
                        msWriter.Write(buffer);
                    }

                    quantized.UnlockBits(imageData);
                    if (!ReferenceEquals(quantized, image)) //Hey, it could happen.
                        quantized.Dispose();
                }
            }

            length = (uint)writeStream.Length;
            writer.Write(length); //12
            writer.Write(offset); //16
            offset += length;
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
    public interface IFrameCollection : ICollection<IconFrame>
    {
        /// <summary>
        /// Adds the specified item to the collection.
        /// </summary>
        /// <param name="item">The item to add to the collection.</param>
        /// <returns><c>true</c> if <paramref name="item"/> was successfully added; <c>false</c> if <paramref name="item"/> or an icon frame like it
        /// already exists in the collection.</returns>
        new bool Add(IconFrame item);

        /// <summary>
        /// Returns an array containing elements copied from the current set.
        /// </summary>
        /// <returns>An array containing elements copied from the current set.</returns>
        IconFrame[] ToArray();
    }
}
