using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;

namespace UIconEdit
{
    /// <summary>
    /// Represents a single frame in an icon.
    /// </summary>
    public class IconFrame
    {
        /// <summary>
        /// Creates a new instance with the specified image.
        /// </summary>
        /// <param name="baseImage">The image associated with the current instance.</param>
        /// <param name="bitDepth">Indicates the bit depth of the resulting image.</param>
        /// <param name="alphaThreshold">If the alpha value of a given pixel is below this value, that pixel will be fully transparent.
        /// If the alpha value is greater than or equal to this value, the pixel will be fully opaque.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="baseImage"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidEnumArgumentException">
        /// <paramref name="bitDepth"/> is not a valid <see cref="UIconEdit.BitDepth"/> value.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// <paramref name="baseImage"/> is disposed.
        /// </exception>
        public IconFrame(Image baseImage, BitDepth bitDepth, byte alphaThreshold)
        {
            _setImage(baseImage, "image");
            _setDepth(bitDepth, "bitDepth");
            _alphaThreshold = alphaThreshold;
        }

        /// <summary>
        /// The minimum dimensions of an icon. 16 as of Windows 10.
        /// </summary>
        public const int MinDimension = 16;
        /// <summary>
        /// The maximum dimensions of an icon. 768 as of Windows 10.
        /// </summary>
        public const int MaxDimension = 768;

        private void _setImage(Image image, string paramName)
        {
            if (image == null) throw new ArgumentNullException(paramName);
            int width, height;
            try
            {
                width = image.Width;
                height = image.Height;
            }
            catch (ObjectDisposedException)
            {
                throw;
            }
            catch
            {
                throw new ObjectDisposedException(paramName);
            }
            if (width < MinDimension || width > MaxDimension || height < MinDimension || height > MaxDimension)
                throw new ArgumentException("Width or height is out of bounds.", paramName);
            _image = image;
        }

        private Image _image;
        /// <summary>
        /// Gets and sets the image associated with the current instance.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// In a set operation, the specified value is <c>null</c>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// In a set operation, the specified value is disposed.
        /// </exception>
        public Image BaseImage
        {
            get { return _image; }
            set { _setImage(value, null); }
        }

        private void _setDepth(BitDepth depth, string paramName)
        {
            switch (depth)
            {
                case BitDepth.Bit24:
                case BitDepth.Bit32:
                case BitDepth.Color2:
                case BitDepth.Color16:
                case BitDepth.Color256:
                    break;
                default:
                    throw new InvalidEnumArgumentException(null, (int)depth, typeof(BitDepth));
            }
            _depth = depth;
        }

        private BitDepth _depth;
        /// <summary>
        /// Gets and sets the bit depth of the current instance. This property is ignored if the width or height of the image is greater than 255.
        /// </summary>
        /// <exception cref="InvalidEnumArgumentException">
        /// In a set operation, the specified value is not a valid <see cref="UIconEdit.BitDepth"/> value.
        /// </exception>
        public BitDepth BitDepth
        {
            get { return _depth; }
            set { _setDepth(value, null); }
        }

        internal BitDepth ActualBitDepth
        {
            get
            {
                if (_image.Width > byte.MaxValue || _image.Height > byte.MaxValue)
                    return BitDepth.Bit32;
                return _depth;
            }
        }

        /// <summary>
        /// Gets the pixel format of the resulting image file.
        /// </summary>
        public PixelFormat PixelFormat
        {
            get
            {
                switch (ActualBitDepth)
                {
                    case BitDepth.Color2:
                        return PixelFormat.Format1bppIndexed;
                    case BitDepth.Color16:
                        return PixelFormat.Format4bppIndexed;
                    case BitDepth.Color256:
                        return PixelFormat.Format8bppIndexed;
                    case BitDepth.Bit24:
                        return PixelFormat.Format24bppRgb;
                    default:
                        return PixelFormat.Format32bppArgb;
                }
            }
        }

        /// <summary>
        /// Gets the maximum index of the palette.
        /// </summary>
        public byte PaletteCount
        {
            get
            {
                switch (ActualBitDepth)
                {
                    case BitDepth.Color2:
                        return 1;
                    case BitDepth.Color16:
                        return 15;
                    case BitDepth.Color256:
                        return 255;
                    default:
                        return 0;
                }
            }
        }

        private byte _alphaThreshold;
        /// <summary>
        /// Gets and sets a value indicating the threshold of alpha values when <see cref="BitDepth"/> is any value other than
        /// <see cref="UIconEdit.BitDepth.Bit32"/>. Alpha values less than this value will be fully transparent; alpha values greater than or equal to this
        /// value will be fully opaque.
        /// </summary>
        public byte AlphaThreshold
        {
            get { return _alphaThreshold; }
            set { _alphaThreshold = value; }
        }

        internal Bitmap GetQuantized(out Bitmap alphaMask)
        {
            const PixelFormat fullFormat = PixelFormat.Format32bppArgb, alphaFormat = PixelFormat.Format1bppIndexed;

            BitDepth depth = ActualBitDepth;
            if (_image is Bitmap && _image.PixelFormat == PixelFormat.Format32bppArgb && depth == BitDepth.Bit32)
            {
                alphaMask = null;
                return (Bitmap)_image;
            }

            Bitmap fullColor = new Bitmap(_image.Width, _image.Height, fullFormat);

            using (Graphics g = Graphics.FromImage(fullColor))
                g.DrawImage(_image, 0, 0, fullColor.Width, fullColor.Height);


            if (depth == BitDepth.Bit32)
            {
                alphaMask = null;
                return fullColor;
            }
            Rectangle fullRect = new Rectangle(0, 0, fullColor.Width, fullColor.Height);

            alphaMask = new Bitmap(fullColor.Width, fullColor.Height, alphaFormat);

            unsafe
            {
                BitmapData fullData = fullColor.LockBits(fullRect, ImageLockMode.ReadOnly, fullFormat);
                BitmapData alphaData = alphaMask.LockBits(fullRect, ImageLockMode.WriteOnly, alphaFormat);
                int offWidth = fullRect.Width / 8;

                byte* pAlpha = (byte*)alphaData.Scan0;
                uint* pFull = (uint*)alphaData.Scan0;

                for (int y = 0; y < fullColor.Height; y++)
                {
                    for (int xAlpha = 0; xAlpha < offWidth; xAlpha++)
                    {
                        int offsetAlpha = (y * alphaData.Stride) + xAlpha;
                        int offsetFull = (y * fullData.Stride) + (xAlpha * 8);

                        byte curValue = 0;

                        for (int xFull = 0; xFull < 8; xFull++)
                        {
                            uint alpha = (pFull[offsetFull + xFull] >> 24) & 0xFF;
                            if (alpha >= _alphaThreshold)
                                curValue |= (byte)(1 << 7 - xFull);
                        }
                    }
                }
            }

            Bitmap quantized = new Bitmap(fullColor.Width, fullColor.Height, PixelFormat);
            if (depth == BitDepth.Bit24)
            {
                using (Graphics g = Graphics.FromImage(quantized))
                {
                    g.FillRectangle(Brushes.Black, 0, 0, fullColor.Width, fullColor.Height);
                    g.DrawImage(fullColor, 0, 0, fullColor.Width, fullColor.Height);
                }
            }
            else
            {
                //TODO: Quantize
            }
            fullColor.Dispose();
            return quantized;
        }
    }

    internal struct IconFrameComparer : IEqualityComparer<IconFrame>, IComparer<IconFrame>
    {
        public int Compare(IconFrame x, IconFrame y)
        {
            int compare = x.BitDepth.CompareTo(y.BitDepth);
            if (compare != 0) return compare;

            compare = x.BaseImage.Width.CompareTo(y.BaseImage.Width);
            if (compare != 0) return compare;

            return x.BaseImage.Height.CompareTo(y.BaseImage.Height);
        }

        public bool Equals(IconFrame x, IconFrame y)
        {
            if (ReferenceEquals(x, y)) return true;
            return x.BitDepth == y.BitDepth && x.BaseImage.Width == y.BaseImage.Width && x.BaseImage.Height == y.BaseImage.Height;
        }

        public int GetHashCode(IconFrame obj)
        {
            if (obj == null) return 0;
            return (obj.BaseImage.Width) | (obj.BaseImage.Height << 16) | ((int)obj.BitDepth << 12);
        }
    }

    /// <summary>
    /// Indicates the bit depth of an icon frame.
    /// </summary>
    public enum BitDepth
    {
        /// <summary>
        /// Indicates that the frame is full color with alpha (32 bits per pixel).
        /// </summary>
        Bit32,
        /// <summary>
        /// Indicates that the frame is full color without alpha (24 bits per pixel plus 1 bit per pixel alpha mask).
        /// </summary>
        Bit24,
        /// <summary>
        /// Indicates that the frame is 256-color (8 bits per pixel plus 1 bit per pixel alpha mask)
        /// </summary>
        Color256,
        /// <summary>
        /// Indicates that the frame is 16-color (4 bits per pixel plus 1 bit per pixel alpha mask).
        /// </summary>
        Color16,
        /// <summary>
        /// Indicates that the frame is 2-color (1 bit per pixel plus 1 bit per pixel alpha mask).
        /// </summary>
        Color2,
    }
}
