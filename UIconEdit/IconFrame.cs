#region
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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

using nQuant;

namespace UIconEdit
{
    /// <summary>
    /// Represents a single frame in an icon.
    /// </summary>
    public class IconFrame : IDisposable
    {
        internal const byte DefaultAlphaThreshold = 96;

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
        public IconFrame(Image baseImage, short width, short height, BitDepth bitDepth, byte alphaThreshold)
        {
            _setImage(baseImage, "baseImage");
            _setDepth(bitDepth, "bitDepth");
            _setSize(ref _width, width, "width");
            _setSize(ref _height, height, "height");
            _alphaThreshold = alphaThreshold;
        }

        /// <summary>
        /// Creates a new instance with the specified image.
        /// </summary>
        /// <param name="baseImage">The image associated with the current instance.</param>
        /// <param name="bitDepth">Indicates the bit depth of the resulting image.</param>
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
        public IconFrame(Image baseImage, short width, short height, BitDepth bitDepth)
            : this(baseImage, width, height, bitDepth, DefaultAlphaThreshold)
        {
        }

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
        /// <exception cref="ArgumentException">
        /// The width or height of <paramref name="baseImage"/> is less than <see cref="MinDimension"/> or is greater than <see cref="MaxDimension"/>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// <paramref name="baseImage"/> is disposed.
        /// </exception>
        public IconFrame(Image baseImage, BitDepth bitDepth, byte alphaThreshold)
        {
            _setImage(baseImage, "baseImage");
            if (baseImage.Width < MinDimension || baseImage.Width > MaxDimension || baseImage.Height < MinDimension || baseImage.Height > MaxDimension)
                throw new ArgumentException("The image size is out of the supported range.", "baseImage");
            _width = (short)baseImage.Width;
            _height = (short)baseImage.Height;
            _setDepth(bitDepth, "bitDepth");
            _alphaThreshold = alphaThreshold;
        }

        /// <summary>
        /// Creates a new instance with the specified image.
        /// </summary>
        /// <param name="baseImage">The image associated with the current instance.</param>
        /// <param name="bitDepth">Indicates the bit depth of the resulting image.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="baseImage"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidEnumArgumentException">
        /// <paramref name="bitDepth"/> is not a valid <see cref="UIconEdit.BitDepth"/> value.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The width or height of <paramref name="baseImage"/> is less than <see cref="MinDimension"/> or is greater than <see cref="MaxDimension"/>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// <paramref name="baseImage"/> is disposed.
        /// </exception>
        public IconFrame(Image baseImage, BitDepth bitDepth)
            : this(baseImage, bitDepth, DefaultAlphaThreshold)
        {
        }

        internal IconFrame(BitDepth bitDepth, Bitmap baseImage)
            : this(baseImage, bitDepth, DefaultAlphaThreshold)
        {
            _ownedImage = true;
        }

        /// <summary>
        /// The minimum dimensions of an icon. 1 pixel in size.
        /// </summary>
        public const int MinDimension = 1;
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
            _ownedImage = false;
        }

        private bool _ownedImage;
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
                throw new ArgumentOutOfRangeException(paramName);
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

        internal IconFileBase File;

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
                    case BitDepth.Bit24:
                        return PixelFormat.Format24bppRgb;
                    default:
                        return PixelFormat.Format32bppArgb;
                }
            }
        }

        internal ushort BitsPerPixel
        {
            get
            {
                switch (_depth)
                {
                    default: //Bit32
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
        /// Gets and sets a value indicating the threshold of alpha values at <see cref="BitDepth"/>s below <see cref="UIconEdit.BitDepth.Bit32"/>.
        /// Alpha values less than this value will be fully transparent; alpha values greater than or equal to this value will be fully opaque.
        /// </summary>
        public byte AlphaThreshold
        {
            get { return _alphaThreshold; }
            set { _alphaThreshold = value; }
        }

        private InterpolationMode _lerpMode;
        /// <summary>
        /// Gets and sets the interpolation mode used by graphics objects when scaling.
        /// </summary>
        /// <exception cref="InvalidEnumArgumentException">
        /// In a set operation, the specified value is not a valid <see cref="InterpolationMode"/> value.
        /// </exception>
        public InterpolationMode DrawInterpolationMode
        {
            get { return _lerpMode; }
            set
            {
                switch (value)
                {
                    case InterpolationMode.Bicubic:
                    case InterpolationMode.Bilinear:
                    case InterpolationMode.Default:
                    case InterpolationMode.High:
                    case InterpolationMode.HighQualityBicubic:
                    case InterpolationMode.HighQualityBilinear:
                    case InterpolationMode.Low:
                    case InterpolationMode.NearestNeighbor:
                        break;
                    default:
                        throw new InvalidEnumArgumentException(null, (int)value, typeof(InterpolationMode));
                }
                _lerpMode = value;
            }
        }

        private PixelOffsetMode _offMode;
        /// <summary>
        /// Gets and sets the pixel offset mode used by graphics objects when rescaling the image.
        /// </summary>
        /// <exception cref="InvalidEnumArgumentException">
        /// In a set operation, the specified value is not a valid <see cref="PixelOffsetMode"/> value.
        /// </exception>
        public PixelOffsetMode DrawPixelOffsetMode
        {
            get { return _offMode; }
            set
            {
                switch (value)
                {
                    case PixelOffsetMode.Default:
                    case PixelOffsetMode.Half:
                    case PixelOffsetMode.HighQuality:
                    case PixelOffsetMode.HighSpeed:
                    case PixelOffsetMode.None:
                        break;
                    default:
                        throw new InvalidEnumArgumentException(null, (int)value, typeof(PixelOffsetMode));
                }
                _offMode = value;
            }
        }

        private Graphics GraphicsFromImage(Image image)
        {
            Graphics g = Graphics.FromImage(image);
            g.InterpolationMode = _lerpMode;
            g.PixelOffsetMode = _offMode;
            return g;
        }

        /// <summary>
        /// Returns a duplicate of the current instance.
        /// </summary>
        /// <returns>A duplicate of the current instance, with its own clone of <see cref="BaseImage"/>.</returns>
        public virtual IconFrame Clone()
        {
            IconFrame copy = (IconFrame)MemberwiseClone();
            copy._image = (Image)this._image.Clone();
            copy._ownedImage = true;
            copy.File = null;
            return copy;
        }

        internal unsafe Bitmap GetQuantized(out Bitmap alphaMask, out int paletteCount)
        {
            const PixelFormat formatFull = PixelFormat.Format32bppArgb, formatAlpha = PixelFormat.Format1bppIndexed;

            bool isPng = _width > byte.MaxValue || _height > byte.MaxValue;

            if (_image is Bitmap && isPng && _depth == BitDepth.Bit32 && _image.PixelFormat == PixelFormat.Format32bppArgb
                && _image.Width == _width && _image.Height == _height)
            {
                paletteCount = 0;
                alphaMask = null;
                return (Bitmap)_image;
            }

            Bitmap fullColor = new Bitmap(_width, _height, formatFull);

            using (Graphics g = GraphicsFromImage(fullColor))
                g.DrawImage(_image, 0, 0, _width, _height);

            Rectangle fullRect = new Rectangle(0, 0, _width, _height);
            const uint opaqueAlpha = 0xFF000000u;

            using (Bitmap alphaTemp = new Bitmap(_width, _height, formatFull))
            {
                BitmapData fullData = fullColor.LockBits(fullRect, ImageLockMode.ReadOnly, formatFull);
                BitmapData alphaData = alphaTemp.LockBits(fullRect, ImageLockMode.WriteOnly, formatFull);
                int offWidth = fullRect.Width / 8;

                for (int y = 0; y < _height; y++)
                {
                    uint* pFull = (uint*)(fullData.Scan0 + (y * fullData.Stride));
                    uint* pAlpha = (uint*)(alphaData.Scan0 + (y * alphaData.Stride));

                    for (int x = 0; x < _width; x++)
                    {
                        uint value = pFull[x] >> 24;
                        if (value < _alphaThreshold)
                            pAlpha[x] = uint.MaxValue;
                        else
                            pAlpha[x] = opaqueAlpha;
                    }
                }
                fullColor.UnlockBits(fullData);
                alphaTemp.UnlockBits(alphaData);

                alphaMask = alphaTemp.Clone(fullRect, formatAlpha);
            }

            if (_depth == BitDepth.Bit32)
            {
                paletteCount = 0;
                return fullColor;
            }

            BitmapData bmpData = fullColor.LockBits(fullRect, ImageLockMode.ReadWrite, formatFull);

            for (int y = 0; y < _height; y++)
            {
                uint* pFull = (uint*)(bmpData.Scan0 + (y * bmpData.Stride));

                for (int x = 0; x < _width; x++)
                {
                    uint value = pFull[x] >> 24;
                    if (isPng && value < _alphaThreshold)
                        pFull[x] &= ~opaqueAlpha;
                    else
                        pFull[x] |= opaqueAlpha;
                }
            }

            fullColor.UnlockBits(bmpData);

            if (_depth == BitDepth.Bit24)
            {
                paletteCount = 0;

                if (isPng)
                    return fullColor;

                Bitmap full24 = new Bitmap(_width, _height, PixelFormat.Format24bppRgb);
                using (Graphics g = GraphicsFromImage(full24))
                    g.DrawImage(fullColor, 0, 0, _width, _height);

                fullColor.Dispose();
                return full24;
            }

            switch (_depth)
            {
                case BitDepth.Color2:
                    paletteCount = 2;
                    break;
                case BitDepth.Color16:
                    paletteCount = 16;
                    break;
                default:
                    paletteCount = 256;
                    break;
            }

            WuQuantizer quant = new WuQuantizer();

            Bitmap quantized = (Bitmap)quant.QuantizeImage(fullColor, 10, 70, ref paletteCount);
            const PixelFormat format8 = PixelFormat.Format8bppIndexed;

            if (isPng)
            {
                BitmapData quantData = quantized.LockBits(fullRect, ImageLockMode.ReadOnly, format8);
                BitmapData fullData = fullColor.LockBits(fullRect, ImageLockMode.ReadWrite, formatFull);

                for (int y = 0; y < _height; y++)
                {
                    byte* pQuant = (byte*)(quantData.Scan0 + (y * quantData.Stride));
                    uint* pData = (uint*)(fullData.Scan0 + (y * fullData.Stride));

                    for (int x = 0; x < _width; x++)
                        pData[x] = (pData[x] & opaqueAlpha) | (unchecked((uint)quantized.Palette.Entries[pQuant[x]].ToArgb()) & ~opaqueAlpha);
                }
                quantized.UnlockBits(quantData);
                fullColor.UnlockBits(fullData);
                return fullColor;
            }

            fullColor.Dispose();

            if (_depth == BitDepth.Color256)
                return quantized;

            Bitmap quant2;

            BitmapData quant256data = quantized.LockBits(fullRect, ImageLockMode.ReadOnly, format8);

            if (_depth == BitDepth.Color2)
            {
                quant2 = quantized.Clone(fullRect, PixelFormat.Format1bppIndexed);
            }
            else
            {
                const PixelFormat format4 = PixelFormat.Format4bppIndexed;
                quant2 = new Bitmap(_width, _height, format4);
                var palette = quant2.Palette;
                for (int i = 0; i < paletteCount; i++)
                    palette.Entries[i] = quantized.Palette.Entries[i];
                quant2.Palette = palette;

                BitmapData quant16data = quant2.LockBits(fullRect, ImageLockMode.ReadOnly, format4);

                for (int y = 0; y < _height; y++)
                {
                    byte* p256 = (byte*)(quant256data.Scan0 + (quant256data.Stride * y));
                    byte* p16 = (byte*)(quant16data.Scan0 + (quant16data.Stride * y));

                    for (int x = 0; x < quant16data.Stride; x++)
                    {
                        int x2 = x * 2;
                        p16[x] = (byte)((p256[x2] << 4) | p256[x2 + 1]);
                    }
                }
            }
            quantized.Dispose();
            return quant2;
        }

        private bool isDisposed;
        /// <summary>
        /// Releases all resources used by the current instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Releases all unmanaged resources used by the current instance, and optionally releases all managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed) return;
            if (_ownedImage)
                _image.Dispose();
            GC.SuppressFinalize(this);
            isDisposed = true;
        }

        /// <summary>
        /// Disposes of the current instance.
        /// </summary>
        ~IconFrame()
        {
            Dispose(false);
        }
    }

    /// <summary>
    /// Represents a single frame of a cursor.
    /// </summary>
    public class CursorFrame : IconFrame
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
        /// <param name="hotspotX">The horizontal offset of the cursor's hotspot from the left of the cursor in pixels.</param>
        /// <param name="hotspotY">The vertical offset of the cursor's hotspot from the top of the cursor in pixels.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="baseImage"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidEnumArgumentException">
        /// <paramref name="bitDepth"/> is not a valid <see cref="BitDepth"/> value.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="width"/> or <paramref name="height"/> is less than <see cref="IconFrame.MinDimension"/> or is greater than <see cref="IconFrame.MaxDimension"/>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// <paramref name="baseImage"/> is disposed.
        /// </exception>
        public CursorFrame(Image baseImage, short width, short height, BitDepth bitDepth, ushort hotspotX, ushort hotspotY, byte alphaThreshold)
            : base(baseImage, width, height, bitDepth, alphaThreshold)
        {
            _x = hotspotX;
            _y = hotspotY;
        }

        /// <summary>
        /// Creates a new instance with the specified image.
        /// </summary>
        /// <param name="baseImage">The image associated with the current instance.</param>
        /// <param name="bitDepth">Indicates the bit depth of the resulting image.</param>
        /// <param name="width">The width of the new image.</param>
        /// <param name="height">The height of the new image.</param>
        /// <param name="hotspotX">The horizontal offset of the cursor's hotspot from the left of the cursor in pixels.</param>
        /// <param name="hotspotY">The vertical offset of the cursor's hotspot from the top of the cursor in pixels.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="baseImage"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidEnumArgumentException">
        /// <paramref name="bitDepth"/> is not a valid <see cref="BitDepth"/> value.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="width"/> or <paramref name="height"/> is less than <see cref="IconFrame.MinDimension"/> or is greater than <see cref="IconFrame.MaxDimension"/>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// <paramref name="baseImage"/> is disposed.
        /// </exception>
        public CursorFrame(Image baseImage, short width, short height, BitDepth bitDepth, ushort hotspotX, ushort hotspotY)
            : this(baseImage, width, height, bitDepth, hotspotX, hotspotY, DefaultAlphaThreshold)
        {
        }

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
        /// <paramref name="bitDepth"/> is not a valid <see cref="BitDepth"/> value.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="width"/> or <paramref name="height"/> is less than <see cref="IconFrame.MinDimension"/> or is greater than <see cref="IconFrame.MaxDimension"/>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// <paramref name="baseImage"/> is disposed.
        /// </exception>
        public CursorFrame(Image baseImage, short width, short height, BitDepth bitDepth, byte alphaThreshold)
            : base(baseImage, width, height, bitDepth, alphaThreshold)
        {
        }

        /// <summary>
        /// Creates a new instance with the specified image.
        /// </summary>
        /// <param name="baseImage">The image associated with the current instance.</param>
        /// <param name="bitDepth">Indicates the bit depth of the resulting image.</param>
        /// <param name="width">The width of the new image.</param>
        /// <param name="height">The height of the new image.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="baseImage"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidEnumArgumentException">
        /// <paramref name="bitDepth"/> is not a valid <see cref="BitDepth"/> value.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="width"/> or <paramref name="height"/> is less than <see cref="IconFrame.MinDimension"/> or is greater than <see cref="IconFrame.MaxDimension"/>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// <paramref name="baseImage"/> is disposed.
        /// </exception>
        public CursorFrame(Image baseImage, short width, short height, BitDepth bitDepth)
            : base(baseImage, width, height, bitDepth)
        {
        }

        /// <summary>
        /// Creates a new instance with the specified image.
        /// </summary>
        /// <param name="baseImage">The image associated with the current instance.</param>
        /// <param name="bitDepth">Indicates the bit depth of the resulting image.</param>
        /// <param name="alphaThreshold">If the alpha value of a given pixel is below this value, that pixel will be fully transparent.
        /// If the alpha value is greater than or equal to this value, the pixel will be fully opaque.</param>
        /// <param name="hotspotX">The horizontal offset of the cursor's hotspot from the left of the cursor in pixels.</param>
        /// <param name="hotspotY">The vertical offset of the cursor's hotspot from the top of the cursor in pixels.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="baseImage"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidEnumArgumentException">
        /// <paramref name="bitDepth"/> is not a valid <see cref="BitDepth"/> value.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The width or height of <paramref name="baseImage"/> is less than <see cref="IconFrame.MinDimension"/> or is greater than <see cref="IconFrame.MaxDimension"/>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// <paramref name="baseImage"/> is disposed.
        /// </exception>
        public CursorFrame(Image baseImage, BitDepth bitDepth, ushort hotspotX, ushort hotspotY, byte alphaThreshold)
            : base(baseImage, bitDepth, alphaThreshold)
        {
            _x = hotspotX;
            _y = hotspotY;
        }

        /// <summary>
        /// Creates a new instance with the specified image.
        /// </summary>
        /// <param name="baseImage">The image associated with the current instance.</param>
        /// <param name="bitDepth">Indicates the bit depth of the resulting image.</param>
        /// <param name="hotspotX">The horizontal offset of the cursor's hotspot from the left of the cursor in pixels.</param>
        /// <param name="hotspotY">The vertical offset of the cursor's hotspot from the top of the cursor in pixels.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="baseImage"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidEnumArgumentException">
        /// <paramref name="bitDepth"/> is not a valid <see cref="BitDepth"/> value.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The width or height of <paramref name="baseImage"/> is less than <see cref="IconFrame.MinDimension"/> or is greater than <see cref="IconFrame.MaxDimension"/>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// <paramref name="baseImage"/> is disposed.
        /// </exception>
        public CursorFrame(Image baseImage, BitDepth bitDepth, ushort hotspotX, ushort hotspotY)
            : this(baseImage, bitDepth, hotspotX, hotspotY, DefaultAlphaThreshold)
        {
        }

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
        /// <paramref name="bitDepth"/> is not a valid <see cref="BitDepth"/> value.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The width or height of <paramref name="baseImage"/> is less than <see cref="IconFrame.MinDimension"/> or is greater than <see cref="IconFrame.MaxDimension"/>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// <paramref name="baseImage"/> is disposed.
        /// </exception>
        public CursorFrame(Image baseImage, BitDepth bitDepth, byte alphaThreshold)
            : base(baseImage, bitDepth, alphaThreshold)
        {
        }

        /// <summary>
        /// Creates a new instance with the specified image.
        /// </summary>
        /// <param name="baseImage">The image associated with the current instance.</param>
        /// <param name="bitDepth">Indicates the bit depth of the resulting image.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="baseImage"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidEnumArgumentException">
        /// <paramref name="bitDepth"/> is not a valid <see cref="BitDepth"/> value.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The width or height of <paramref name="baseImage"/> is less than <see cref="IconFrame.MinDimension"/> or is greater than <see cref="IconFrame.MaxDimension"/>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// <paramref name="baseImage"/> is disposed.
        /// </exception>
        public CursorFrame(Image baseImage, BitDepth bitDepth)
            : base(baseImage, bitDepth)
        {
        }

        internal CursorFrame(BitDepth bitDepth, Bitmap baseImage, ushort hotspotX, ushort hotspotY)
            : base(baseImage, bitDepth)
        {
            _x = hotspotX;
            _y = hotspotY;
        }

        private ushort _x;
        /// <summary>
        /// Gets and sets the horizontal offset of the cursor's hotspot from the left of the cursor in pixels.
        /// </summary>
        public ushort HotspotX
        {
            get { return _x; }
            set { _x = value; }
        }

        private ushort _y;
        /// <summary>
        /// Gets and sets the vertical offset of the cursor's hotspot from the top of the cursor in pixels.
        /// </summary>
        public ushort HotspotY
        {
            get { return _y; }
            set { _y = value; }
        }

        /// <summary>
        /// Gets the offset of the cursor's hotspot from the upper-left corner of the cursor in pixels.
        /// </summary>
        public Point Hotspot
        {
            get { return new Point(_x, _y); }
        }
    }

    internal struct IconFrameComparer : IEqualityComparer<IconFrame>, IComparer<IconFrame>, IEqualityComparer<CursorFrame>, IComparer<CursorFrame>
    {
        public int Compare(CursorFrame x, CursorFrame y)
        {
            return Compare((IconFrame)x, y);
        }

        public int Compare(IconFrame x, IconFrame y)
        {
            int compare = x.BitDepth.CompareTo(y.BitDepth);
            if (compare != 0) return compare;

            compare = y.Height.CompareTo(x.Height);
            if (compare != 0) return compare;

            return y.Width.CompareTo(x.Width);
        }

        public bool Equals(CursorFrame x, CursorFrame y)
        {
            return Equals((IconFrame)x, y);
        }

        public bool Equals(IconFrame x, IconFrame y)
        {
            if (ReferenceEquals(x, y)) return true;
            return x.BitDepth == y.BitDepth && x.Width == y.Width && x.Height == y.Height;
        }

        public int GetHashCode(CursorFrame obj)
        {
            return GetHashCode((IconFrame)obj);
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
