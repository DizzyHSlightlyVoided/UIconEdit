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

        private void WriteImage(BinaryWriter writer, IIconFrame frame, ref int offset, out Stream writeStream)
        {
            var image = frame.Image;
            bool needPng;
            if (needPng = (image.Width > byte.MaxValue)) writer.Write(byte.MinValue);
            else writer.Write((byte)image.Width); //1
            if (needPng |= (image.Height > byte.MaxValue)) writer.Write(byte.MinValue);
            else writer.Write((byte)image.Height); //2

            if (needPng |= (image.Palette == null || image.Palette.Entries.Length == 0))
                writer.Write(byte.MinValue);
            else
                writer.Write(image.Palette.Entries.Length); //3

            writer.Write(byte.MinValue); //4

            writer.Write(frame.X); //6
            writer.Write(frame.Y); //8
            int length;

            MemoryStream ms = new MemoryStream();
            if (needPng)
            {
                image.Save(ms, ImageFormat.Png);
                writeStream = ms;
            }
            else
            {
                image.Save(ms, ImageFormat.Bmp);
                writeStream = new OffsetStream(ms, bmpHeaderSize, (int)ms.Length, false);
            }
            length = (int)writeStream.Length;
            writer.Write(length); //12
            writer.Write(offset); //16
            offset += length;
        }
    }

    /// <summary>
    /// Interface for base classes.
    /// </summary>
    public interface IIconFrame
    {
        /// <summary>
        /// Gets and sets the image associated with the current instance.
        /// </summary>
        Image Image { get; set; }

        /// <summary>
        /// Gets the 16-bit X component.
        /// In ICO format, specifies the color panes; in CUR format, specifies the horizontal offset in pixels of the hotspot from the left.
        /// </summary>
        short X { get; }
        /// <summary>
        /// Gets the 16-bit Y component.
        /// In ICO format, specifies bits per pixel; in CUR format, specifies the vertical offset in pixels of the hotspot from the top.
        /// </summary>
        short Y { get; }
    }
}
