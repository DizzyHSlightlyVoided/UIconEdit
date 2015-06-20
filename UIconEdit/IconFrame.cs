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
        /// <param name="width">The width of the new image.</param>
        /// <param name="height">The height of the new image.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="baseImage"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidEnumArgumentException">
        /// <paramref name="bitDepth"/> is not a valid <see cref="UIconEdit.BitDepth"/> value.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="width"/> or <paramref name="height"/> is less than <see cref="MinDimension"/> or is greater than <see cref="MaxDimension"/>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// <paramref name="baseImage"/> is disposed.
        /// </exception>
        public IconFrame(Image baseImage, BitDepth bitDepth, short width, short height, byte alphaThreshold)
        {
            _setImage(baseImage, "image");
            _setDepth(bitDepth, "bitDepth");
            _setSize(ref _width, width, "width");
            _setSize(ref _height, height, "height");
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

        private void _setSize(ref short dim, short value, string paramName)
        {
            if (value < MinDimension || value > MaxDimension)
                throw new ArgumentOutOfRangeException(paramName, value, "The specified value is out of bounds.");
            dim = value;
        }

        private short _width;
        /// <summary>
        /// Gets and sets the resampled width of the icon.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// In a set operation, the specified value is less than <see cref="MinDimension"/> or is greater than <see cref="MaxDimension"/>.
        /// </exception>
        public short Width
        {
            get { return _width; }
            set { _setSize(ref _width, value, null); }
        }

        private short _height;
        /// <summary>
        /// Gets and sets the resampled height of the icon.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// In a set operation, the specified value is less than <see cref="MinDimension"/> or is greater than <see cref="MaxDimension"/>.
        /// </exception>
        public short Height
        {
            get { return _height; }
            set { _setSize(ref _height, value, null); }
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

        /// <summary>
        /// Gets the pixel format of the resulting image file.
        /// </summary>
        public PixelFormat PixelFormat
        {
            get
            {
                switch (_depth)
                {
                    case BitDepth.Color2:
                        return PixelFormat.Format1bppIndexed;
                    case BitDepth.Color16:
                        return PixelFormat.Format4bppIndexed;
                    case BitDepth.Color256:
                        return PixelFormat.Format8bppIndexed;
                    //case BitDepth.Bit24:
                    //    return PixelFormat.Format24bppRgb;
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
                switch (_depth)
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

        internal short BitsPerPixel
        {
            get
            {
                switch (_depth)
                {
                    default:
                        return 32;
                    case BitDepth.Bit24:
                        return 24;
                    case BitDepth.Color2:
                        return 1;
                    case BitDepth.Color16:
                        return 4;
                    case BitDepth.Color256:
                        return 8;
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

            if (_image is Bitmap && _image.PixelFormat == PixelFormat.Format32bppArgb && _depth == BitDepth.Bit32 && _image.Width == _width && _image.Height == _height)
            {
                alphaMask = null;
                return (Bitmap)_image;
            }

            Bitmap fullColor = new Bitmap(_width, _height, fullFormat);

            using (Graphics g = Graphics.FromImage(fullColor))
                g.DrawImage(_image, 0, 0, _width, _height);

            if (_depth == BitDepth.Bit32)
            {
                alphaMask = null;
                return fullColor;
            }

            Rectangle fullRect = new Rectangle(0, 0, _width, _height);

            alphaMask = new Bitmap(_width, _height, alphaFormat);

            unsafe
            {
                BitmapData fullData = fullColor.LockBits(fullRect, ImageLockMode.ReadOnly, fullFormat);
                BitmapData alphaData = alphaMask.LockBits(fullRect, ImageLockMode.WriteOnly, alphaFormat);
                int offWidth = fullRect.Width / 8;

                byte* pAlpha = (byte*)alphaData.Scan0;
                uint* pFull = (uint*)alphaData.Scan0;

                for (int y = 0; y < _height; y++)
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

            Bitmap quantized = new Bitmap(_width, _height, PixelFormat);
            if (_depth == BitDepth.Bit24)
            {
                using (Graphics g = Graphics.FromImage(quantized))
                {
                    g.FillRectangle(Brushes.Black, fullRect);
                    g.DrawImage(fullColor, fullRect);
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

            compare = x.Width.CompareTo(y.Width);
            if (compare != 0) return compare;

            return x.Height.CompareTo(y.Height);
        }

        public bool Equals(IconFrame x, IconFrame y)
        {
            if (ReferenceEquals(x, y)) return true;
            return x.BitDepth == y.BitDepth && x.Width == y.Width && x.Height == y.Height;
        }

        public int GetHashCode(IconFrame obj)
        {
            if (obj == null) return 0;
            return (int)obj.Width | (obj.Height << 16) | ((int)obj.BitDepth << 12);
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
