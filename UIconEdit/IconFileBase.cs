using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace UIconEdit
{
    /// <summary>
    /// Base class for icon and cursor files.
    /// </summary>
    public abstract class IconFileBase
    {
        const int bmpHeaderSize = 14;

        /// <summary>
        /// When overridden in a derived class, gets the 16-bit identifier for the file type.
        /// </summary>
        public abstract IconTypeCode ID { get; }

        /// <summary>
        /// When overridden in a derived class, computes the 16-bit X component.
        /// </summary>
        /// <param name="frame">The image frame to calculate.</param>
        /// <returns>In icon files, the color panes. In cursor files, the horizontal offset of the hotspot from the left in pixels.</returns>
        protected abstract short GetImgX(IconFrame frame);

        /// <summary>
        /// When overridden in a derived class, computes the 16-bit Y component.
        /// </summary>
        /// <param name="frame">The image frame to calculate.</param>
        /// <returns>In icon files, the number of bits per pixel. In cursor files, the vertical offset of the hotspot from the top, in pixels.</returns>
        protected abstract short GetImgY(IconFrame frame);

        private void WriteImage(BinaryWriter writer, IconFrame frame, ref int offset, out Stream writeStream)
        {
            var image = frame.BaseImage;
            bool needPng;
            if (needPng = (image.Width > byte.MaxValue)) writer.Write(byte.MinValue);
            else writer.Write((byte)image.Width); //1
            if (image.Height > byte.MaxValue)
            {
                needPng = true;
                writer.Write(byte.MinValue);
            }
            else writer.Write((byte)image.Height); //2

            writer.Write(frame.PaletteCount);
            if (frame.PaletteCount == 0) needPng = true;

            writer.Write(byte.MinValue); //4

            writer.Write(GetImgX(frame)); //6
            writer.Write(GetImgY(frame)); //8

            Image quantized;
            if (frame.Depth == BitDepth.Bit32 && image.PixelFormat != PixelFormat.Format32bppArgb)
            {
                quantized = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);
                using (Graphics g = Graphics.FromImage(quantized))
                    g.DrawImage(image, image.Width, image.Height);
            }
            else quantized = image;

            int length;
            MemoryStream ms = new MemoryStream();
            if (needPng)
            {
                quantized.Save(ms, ImageFormat.Png);
                writeStream = ms;
            }
            else
            {
                quantized.Save(ms, ImageFormat.Bmp);
                writeStream = new OffsetStream(ms, bmpHeaderSize, (int)ms.Length, false);
            }
            if (!ReferenceEquals(quantized, image)) quantized.Dispose();

            length = (int)writeStream.Length;
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
}
