using System;
using System.Collections.Generic;
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
        /// <exception cref="ArgumentNullException">
        /// <paramref name="baseImage"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// <paramref name="baseImage"/> is disposed.
        /// </exception>
        public IconFrame(Image baseImage)
        {
            _setImage(baseImage, "image");
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

        private bool _alpha;
        /// <summary>
        /// Gets a 
        /// </summary>
        public bool Alpha1BPP
        {
            get { return _alpha; }
            set { _alpha = value; }
        }

        /// <summary>
        /// Gets the bit depth of the current instance.
        /// </summary>
        public BitDepth Depth
        {
            get
            {
                switch (_image.PixelFormat)
                {
                    case PixelFormat.Format1bppIndexed:
                        return BitDepth.Color2;
                    case PixelFormat.Format4bppIndexed:
                        return BitDepth.Color16;
                    case PixelFormat.Format8bppIndexed:
                        return BitDepth.Color256;
                    case PixelFormat.Format24bppRgb:
                        return BitDepth.Bit24;
                    default:
                        return BitDepth.Bit32;
                }
            }
        }

        /// <summary>
        /// Returns the number of bits per pixel of the current instance.
        /// </summary>
        public int BitsPerPixel
        {
            get
            {
                switch (Depth)
                {
                    case BitDepth.Color2:
                        return 1;
                    case BitDepth.Color16:
                        return 4;
                    case BitDepth.Color256:
                        return 8;
                    case BitDepth.Bit24:
                        return 24;
                    default:
                        return 32;
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
                switch (Depth)
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
    }

    internal struct IconFrameComparer : IEqualityComparer<IconFrame>, IComparer<IconFrame>
    {
        public int Compare(IconFrame x, IconFrame y)
        {
            int compare = x.Depth.CompareTo(y.Depth);
            if (compare != 0) return compare;

            compare = x.BaseImage.Width.CompareTo(y.BaseImage.Width);
            if (compare != 0) return compare;

            return x.BaseImage.Height.CompareTo(y.BaseImage.Height);
        }

        public bool Equals(IconFrame x, IconFrame y)
        {
            if (ReferenceEquals(x, y)) return true;
            return x.Depth == y.Depth && x.BaseImage.Width == y.BaseImage.Width && x.BaseImage.Height == y.BaseImage.Height;
        }

        public int GetHashCode(IconFrame obj)
        {
            if (obj == null) return 0;
            return (obj.BaseImage.Width) | (obj.BaseImage.Height << 16) | ((int)obj.Depth << 12);
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
