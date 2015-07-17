#region BSD License
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
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using BitmapData = System.Drawing.Imaging.BitmapData;
using DBitmap = System.Drawing.Bitmap;
using DPixelFormat = System.Drawing.Imaging.PixelFormat;
using DRectangle = System.Drawing.Rectangle;
using ImageLockMode = System.Drawing.Imaging.ImageLockMode;
using InterpolationMode = System.Drawing.Drawing2D.InterpolationMode;

namespace UIconEdit
{
    /// <summary>
    /// Represents a single entry in an icon.
    /// </summary>
    public class IconEntry : DependencyObject
    {
        /// <summary>
        /// The default <see cref="AlphaThreshold"/> value.
        /// </summary>
        public const byte DefaultAlphaThreshold = 96;

        private void _initValues(short width, short height, BitDepth bitDepth)
        {
            if (width < MinDimension || width > MaxDimension)
                throw new ArgumentOutOfRangeException("width");

            if (height < MinDimension || height > MaxDimension)
                throw new ArgumentOutOfRangeException("height");

            if (!_validateBitDepth(bitDepth))
                throw new InvalidEnumArgumentException("bitDepth", (int)bitDepth, typeof(BitDepth));
        }

        private static bool _validateBitDepth(BitDepth value)
        {
            switch (value)
            {
                case BitDepth.Depth1BitsPerPixel:
                case BitDepth.Depth4BitsPerPixel:
                case BitDepth.Depth8BitsPerPixel:
                case BitDepth.Depth24BitsPerPixel:
                case BitDepth.Depth32BitsPerPixel:
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Creates a new instance with the specified image.
        /// </summary>
        /// <param name="baseImage">The image associated with the current instance.</param>
        /// <param name="width">The width of the new image.</param>
        /// <param name="height">The height of the new image.</param>
        /// <param name="bitDepth">Indicates the bit depth of the resulting image.</param>
        /// <param name="hotspotX">In a cursor file, the horizontal offset in pixels of the cursor's hotspot from the left side.
        /// Constrained to be less than <paramref name="width"/>.</param>
        /// <param name="hotspotY">In a cursor file, the horizontal offset in pixels of the cursor's hotspot from the top.
        /// Constrained to be less than <paramref name="height"/>.</param>
        /// <param name="alphaThreshold">If the alpha value of a given pixel is below this value, that pixel will be fully transparent.
        /// If the alpha value is greater than or equal to this value, the pixel will be fully opaque.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="baseImage"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidEnumArgumentException">
        /// <paramref name="bitDepth"/> is not a valid <see cref="UIconEdit.BitDepth"/> value.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="width"/> or <paramref name="height"/> is less than <see cref="MinDimension"/> or is greater than <see cref="MaxDimension"/>.
        /// </exception>
        public IconEntry(BitmapSource baseImage, short width, short height, BitDepth bitDepth, ushort hotspotX, ushort hotspotY, byte alphaThreshold)
        {
            if (baseImage == null) throw new ArgumentNullException("baseImage");
            _initValues(width, height, bitDepth);
            _depth = bitDepth;
            _width = width;
            _height = height;
            BaseImage = baseImage;
            AlphaThreshold = alphaThreshold;
            HotspotX = hotspotX;
            HotspotY = hotspotY;
        }

        /// <summary>
        /// Creates a new instance with the specified image.
        /// </summary>
        /// <param name="baseImage">The image associated with the current instance.</param>
        /// <param name="width">The width of the new image.</param>
        /// <param name="height">The height of the new image.</param>
        /// <param name="bitDepth">Indicates the bit depth of the resulting image.</param>
        /// <param name="alphaThreshold">If the alpha value of a given pixel is below this value, that pixel will be fully transparent.
        /// If the alpha value is greater than or equal to this value, the pixel will be fully opaque.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="baseImage"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidEnumArgumentException">
        /// <paramref name="bitDepth"/> is not a valid <see cref="UIconEdit.BitDepth"/> value.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="width"/> or <paramref name="height"/> is less than <see cref="MinDimension"/> or is greater than <see cref="MaxDimension"/>.
        /// </exception>
        public IconEntry(BitmapSource baseImage, short width, short height, BitDepth bitDepth, byte alphaThreshold)
            : this(baseImage, width, height, bitDepth, 0, 0, alphaThreshold)
        {
        }

        /// <summary>
        /// Creates a new instance with the specified image.
        /// </summary>
        /// <param name="baseImage">The image associated with the current instance.</param>
        /// <param name="width">The width of the new image.</param>
        /// <param name="height">The height of the new image.</param>
        /// <param name="bitDepth">Indicates the bit depth of the resulting image.</param>
        /// <param name="hotspotX">In a cursor file, the horizontal offset in pixels of the cursor's hotspot from the left side.
        /// Constrained to be less than <paramref name="width"/>.</param>
        /// <param name="hotspotY">In a cursor file, the horizontal offset in pixels of the cursor's hotspot from the top.
        /// Constrained to be less than <paramref name="height"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="baseImage"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidEnumArgumentException">
        /// <paramref name="bitDepth"/> is not a valid <see cref="UIconEdit.BitDepth"/> value.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="width"/> or <paramref name="height"/> is less than <see cref="MinDimension"/> or is greater than <see cref="MaxDimension"/>.
        /// </exception>
        public IconEntry(BitmapSource baseImage, short width, short height, BitDepth bitDepth, ushort hotspotX, ushort hotspotY)
            : this(baseImage, width, height, bitDepth, hotspotX, hotspotY, DefaultAlphaThreshold)
        {
        }

        /// <summary>
        /// Creates a new instance with the specified image.
        /// </summary>
        /// <param name="baseImage">The image associated with the current instance.</param>
        /// <param name="width">The width of the new image.</param>
        /// <param name="height">The height of the new image.</param>
        /// <param name="bitDepth">Indicates the bit depth of the resulting image.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="baseImage"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidEnumArgumentException">
        /// <paramref name="bitDepth"/> is not a valid <see cref="UIconEdit.BitDepth"/> value.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="width"/> or <paramref name="height"/> is less than <see cref="MinDimension"/> or is greater than <see cref="MaxDimension"/>.
        /// </exception>
        public IconEntry(BitmapSource baseImage, short width, short height, BitDepth bitDepth)
            : this(baseImage, width, height, bitDepth, 0, 0, DefaultAlphaThreshold)
        {
        }

        /// <summary>
        /// Creates a new instance with the specified image.
        /// </summary>
        /// <param name="baseImage">The image associated with the current instance.</param>
        /// <param name="bitDepth">Indicates the bit depth of the resulting image.</param>
        /// <param name="hotspotX">In a cursor file, the horizontal offset in pixels of the cursor's hotspot from the left side.
        /// Constrained to be less than the width of <paramref name="baseImage"/>.</param>
        /// <param name="hotspotY">In a cursor file, the horizontal offset in pixels of the cursor's hotspot from the top.
        /// Constrained to be less than the height of <paramref name="baseImage"/>.</param>
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
        public IconEntry(BitmapSource baseImage, BitDepth bitDepth, ushort hotspotX, ushort hotspotY, byte alphaThreshold)
        {
            if (baseImage == null) throw new ArgumentNullException("baseImage");
            if (baseImage.PixelWidth < MinDimension || baseImage.PixelWidth > MaxDimension || baseImage.PixelHeight < MinDimension || baseImage.PixelHeight > MaxDimension)
                throw new ArgumentException("The image size is out of the supported range.", "baseImage");
            if (!_validateBitDepth(bitDepth))
                throw new InvalidEnumArgumentException("bitDepth", (int)bitDepth, typeof(BitDepth));
            BaseImage = baseImage;
            _depth = bitDepth;
            _width = (short)baseImage.PixelWidth;
            _height = (short)baseImage.PixelHeight;
            AlphaThreshold = alphaThreshold;
            HotspotX = hotspotX;
            HotspotY = hotspotY;
        }

        /// <summary>
        /// Creates a new instance with the specified image.
        /// </summary>
        /// <param name="baseImage">The image associated with the current instance.</param>
        /// <param name="bitDepth">Indicates the bit depth of the resulting image.</param>
        /// <param name="hotspotX">In a cursor file, the horizontal offset in pixels of the cursor's hotspot from the left side.
        /// Constrained to be less than the width of <paramref name="baseImage"/>.</param>
        /// <param name="hotspotY">In a cursor file, the horizontal offset in pixels of the cursor's hotspot from the top.
        /// Constrained to be less than the height of <paramref name="baseImage"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="baseImage"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidEnumArgumentException">
        /// <paramref name="bitDepth"/> is not a valid <see cref="UIconEdit.BitDepth"/> value.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The width or height of <paramref name="baseImage"/> is less than <see cref="MinDimension"/> or is greater than <see cref="MaxDimension"/>.
        /// </exception>
        public IconEntry(BitmapSource baseImage, BitDepth bitDepth, ushort hotspotX, ushort hotspotY)
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
        /// <paramref name="bitDepth"/> is not a valid <see cref="UIconEdit.BitDepth"/> value.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The width or height of <paramref name="baseImage"/> is less than <see cref="MinDimension"/> or is greater than <see cref="MaxDimension"/>.
        /// </exception>
        public IconEntry(BitmapSource baseImage, BitDepth bitDepth, byte alphaThreshold)
            : this(baseImage, bitDepth, 0, 0, alphaThreshold)
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
        /// <paramref name="bitDepth"/> is not a valid <see cref="UIconEdit.BitDepth"/> value.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The width or height of <paramref name="baseImage"/> is less than <see cref="MinDimension"/> or is greater than <see cref="MaxDimension"/>.
        /// </exception>
        public IconEntry(BitmapSource baseImage, BitDepth bitDepth)
            : this(baseImage, bitDepth, 0, 0, DefaultAlphaThreshold)
        {
        }

        internal IconEntry(BitmapSource baseImage, BitmapSource alphaImage, BitDepth bitDepth, ushort hotspotX, ushort hotspotY)
            : this(baseImage, bitDepth, hotspotX, hotspotY, DefaultAlphaThreshold)
        {
            AlphaImage = alphaImage;
            SetValue(IsQuantizedPropertyKey, true);
        }

        internal IconEntry(BitmapSource baseImage, BitmapSource alphaImage, BitDepth bitDepth)
            : this(baseImage, alphaImage, bitDepth, 0, 0)
        {
        }


        /// <summary>
        /// The minimum dimensions of an icon. 1 pixels.
        /// </summary>
        public const short MinDimension = 1;
        /// <summary>
        /// The maximum dimensions of an icon. 768 pixels as of Windows 10.
        /// </summary>
        public const short MaxDimension = 768;
        /// <summary>
        /// Gets and sets the maximum width or height at which an icon entry will be saved as a BMP file when <see cref="BitDepth"/> is
        /// <see cref="UIconEdit.BitDepth.Depth32BitsPerPixel"/>; all entries with a width or height greater than this will be saved as PNG.
        /// 96 pixels.
        /// </summary>
        public const short MaxBmp32 = 96;
        /// <summary>
        /// Gets and sets the maximum width or height at which an icon entry will be saved as a BMP file when <see cref="BitDepth"/> is any value except
        /// <see cref="UIconEdit.BitDepth.Depth32BitsPerPixel"/>; all entries with a width or height greater than this will be saved as PNG.
        /// 255 pixels.
        /// </summary>
        public const short MaxBmp = byte.MaxValue;

        internal static readonly BitmapPalette AlphaPalette = new BitmapPalette(new Color[] { Colors.White, Colors.Black });

        #region IsQuantized
        private static readonly DependencyPropertyKey IsQuantizedPropertyKey = DependencyProperty.RegisterReadOnly("IsQuantized", typeof(bool), typeof(IconEntry),
            new PropertyMetadata(false));
        /// <summary>
        /// The dependency property for the read-only <see cref="IsQuantized"/> property.
        /// </summary>
        public static readonly DependencyProperty IsQuantizedProperty = IsQuantizedPropertyKey.DependencyProperty;

        /// <summary>
        /// Gets a value indicating whether <see cref="BaseImage"/> and <see cref="AlphaImage"/> are known to be already quantized.
        /// </summary>
        public bool IsQuantized { get { return (bool)GetValue(IsQuantizedProperty); } }
        #endregion

        #region BaseImage
        /// <summary>
        /// The dependency property for the <see cref="BaseImage"/> property.
        /// </summary>
        public static readonly DependencyProperty BaseImageProperty = DependencyProperty.Register("BaseImage", typeof(BitmapSource), typeof(IconEntry),
            new PropertyMetadata(new WriteableBitmap(1, 1, 0, 0, PixelFormats.Indexed1, AlphaPalette), BaseImageChanged), BaseImageValidate);

        private static void BaseImageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(IsQuantizedPropertyKey, false);
        }

        private static bool BaseImageValidate(object value)
        {
            return value != null;
        }

        /// <summary>
        /// Gets and sets the image associated with the current instance.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// In a set operation, the specified value is <c>null</c>.
        /// </exception>
        public BitmapSource BaseImage
        {
            get { return (BitmapSource)GetValue(BaseImageProperty); }
            set
            {
                if (value == null) throw new ArgumentNullException();
                SetValue(BaseImageProperty, value);
            }
        }
        #endregion

        #region AlphaImage
        /// <summary>
        /// The dependency property for the <see cref="AlphaImage"/> property.
        /// </summary>
        public static readonly DependencyProperty AlphaImageProperty = DependencyProperty.Register("AlphaImage", typeof(BitmapSource), typeof(IconEntry),
            new PropertyMetadata(null, BaseImageChanged));

        /// <summary>
        /// Gets and sets an image to be used as the alpha mask, or <c>null</c> to derive the alpha mask from <see cref="BaseImage"/>.
        /// Black pixels are transparent; white pixels are opaque.
        /// </summary>
        public BitmapSource AlphaImage
        {
            get { return (BitmapSource)GetValue(AlphaImageProperty); }
            set { SetValue(AlphaImageProperty, value); }
        }
        #endregion

        /// <summary>
        /// Gets a value indicating whether the current instance will be saved as a PNG image within the icon structure by default.
        /// </summary>
        [Bindable(true, BindingDirection.OneWay)]
        public bool IsPng
        {
            get
            {
                if (_depth == BitDepth.Depth32BitsPerPixel)
                    return _width > 96 || _height > 96;
                return _width > MaxBmp32 || _height > MaxBmp32;
            }
        }

        /// <summary>
        /// Gets a key for the icon entry.
        /// </summary>
        [Bindable(true, BindingDirection.OneWay)]
        public EntryKey EntryKey { get { return new EntryKey(_width, _height, _depth); } }

        private readonly short _width;
        /// <summary>
        /// Gets the resampled width of the icon.
        /// </summary>
        [Bindable(true, BindingDirection.OneWay)]
        public short Width
        {
            get { return _width; }
        }

        private readonly short _height;
        /// <summary>
        /// Gets the resampled height of the icon.
        /// </summary>
        [Bindable(true, BindingDirection.OneWay)]
        public short Height
        {
            get { return _height; }
        }

        #region BitDepth
        private readonly BitDepth _depth;
        /// <summary>
        /// Gets the bit depth of the current instance.
        /// </summary>
        [Bindable(true, BindingDirection.OneWay)]
        public BitDepth BitDepth
        {
            get { return _depth; }
        }
        #endregion

        #region AlphaThreshold
        /// <summary>
        /// The dependency property for the <see cref="AlphaThreshold"/> property.
        /// </summary>
        public static readonly DependencyProperty AlphaThresholdProperty = DependencyProperty.Register("AlphaThreshold", typeof(byte), typeof(BitmapImage));

        /// <summary>
        /// Gets and sets a value indicating the threshold of alpha values at <see cref="BitDepth"/>s below <see cref="UIconEdit.BitDepth.Depth32BitsPerPixel"/>.
        /// Alpha values less than this value will be fully transparent; alpha values greater than or equal to this value will be fully opaque.
        /// </summary>
        public byte AlphaThreshold
        {
            get { return (byte)GetValue(AlphaThresholdProperty); }
            set { SetValue(AlphaThresholdProperty, value); }
        }
        #endregion

        #region HotspotX
        /// <summary>
        /// The dependency property for the <see cref="HotspotX"/> property.
        /// </summary>
        public static readonly DependencyProperty HotspotXProperty = DependencyProperty.Register("HotspotX", typeof(ushort), typeof(IconEntry),
            new PropertyMetadata(ushort.MinValue, null, HotspotXCoerce));

        private static object HotspotXCoerce(DependencyObject d, object baseValue)
        {
            IconEntry i = (IconEntry)d;

            ushort value = (ushort)baseValue;
            if (value > i._width) return (ushort)i._width;

            return baseValue;
        }

        /// <summary>
        /// In a cursor, gets the horizontal offset in pixels of the cursor's hotspot from the left side.
        /// Constrained to greater than or equal to 0 and less than or equal to <see cref="Width"/>.
        /// </summary>
        public ushort HotspotX
        {
            get { return (ushort)GetValue(HotspotXProperty); }
            set { SetValue(HotspotXProperty, value); }
        }
        #endregion

        #region HotspotY
        /// <summary>
        /// The dependency property for the <see cref="HotspotY"/> property.
        /// </summary>
        public static readonly DependencyProperty HotspotYProperty = DependencyProperty.Register("HotspotY", typeof(ushort), typeof(IconEntry),
            new PropertyMetadata(ushort.MinValue, null, HotspotYCoerce));

        private static object HotspotYCoerce(DependencyObject d, object baseValue)
        {
            IconEntry e = (IconEntry)d;

            ushort value = (ushort)baseValue;
            if (value > e._height) return (ushort)e._height;

            return baseValue;
        }

        /// <summary>
        /// In a cursor, gets the vertical offset in pixels of the cursor's hotspot from the top side.
        /// Constrained to greater than or equal to 0 and less than or equal to <see cref="Height"/>.
        /// </summary>
        public ushort HotspotY
        {
            get { return (ushort)GetValue(HotspotYProperty); }
            set { SetValue(HotspotYProperty, value); }
        }
        #endregion

        #region ScalingFilter
        /// <summary>
        /// The dependency property for the <see cref="ScalingFilter"/> property.
        /// </summary>
        public static readonly DependencyProperty ScalingFilterProperty = DependencyProperty.Register("ScalingFilter", typeof(ScalingFilter), typeof(IconEntry),
            new PropertyMetadata(ScalingFilter.Matrix), ScalingFilterValidate);

        private static bool ScalingFilterValidate(object value)
        {
            switch ((ScalingFilter)value)
            {
                case ScalingFilter.Matrix:
                case ScalingFilter.Bicubic:
                case ScalingFilter.Bilinear:
                case ScalingFilter.HighQualityBicubic:
                case ScalingFilter.HighQualityBilinear:
                case ScalingFilter.NearestNeighbor:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Gets and sets the scaling mode used to resize <see cref="BaseImage"/> and <see cref="AlphaImage"/> when quantizing.
        /// </summary>
        /// <exception cref="InvalidEnumArgumentException">
        /// In a set operation, the specified value is not a valid <see cref="UIconEdit.ScalingFilter"/> value.
        /// </exception>
        public ScalingFilter ScalingFilter
        {
            get { return (ScalingFilter)GetValue(ScalingFilterProperty); }
            set
            {
                if (!ScalingFilterValidate(value))
                    throw new InvalidEnumArgumentException(null, (int)value, typeof(ScalingFilter));
                SetValue(ScalingFilterProperty, value);
            }
        }
        #endregion

        /// <summary>
        /// Gets the number of bits per pixel specified by <see cref="BitDepth"/>.
        /// </summary>
        [Bindable(true, BindingDirection.OneWay)]
        public ushort BitsPerPixel
        {
            get { return GetBitsPerPixel(_depth); }
        }

        /// <summary>
        /// Returns the number of bits per pixel associated with the specified <see cref="UIconEdit.BitDepth"/> value.
        /// </summary>
        /// <param name="bitDepth">The <see cref="UIconEdit.BitDepth"/> to check.</param>
        /// <returns>1 for <see cref="BitDepth.Depth1BitsPerPixel"/>; 4 for <see cref="BitDepth.Depth4BitsPerPixel"/>;
        /// 8 for <see cref="BitDepth.Depth8BitsPerPixel"/>; 24 for <see cref="BitDepth.Depth24BitsPerPixel"/>; or
        /// 32 for <see cref="BitDepth.Depth32BitsPerPixel"/>.</returns>
        /// <exception cref="InvalidEnumArgumentException">
        /// <paramref name="bitDepth"/> is not a valid <see cref="UIconEdit.BitDepth"/> value.
        /// </exception>
        public static ushort GetBitsPerPixel(BitDepth bitDepth)
        {
            switch (bitDepth)
            {
                case BitDepth.Depth1BitPerPixel:
                    return 1;
                case BitDepth.Depth4BitsPerPixel:
                    return 4;
                case BitDepth.Depth8BitsPerPixel:
                    return 8;
                case BitDepth.Depth24BitsPerPixel:
                    return 24;
                case BitDepth.Depth32BitsPerPixel:
                    return 32;
                default:
                    throw new InvalidEnumArgumentException("bitDepth", (int)bitDepth, typeof(BitDepth));
            }
        }

        /// <summary>
        /// Gets the maximum color count specified by <see cref="BitDepth"/>.
        /// </summary>
        [Bindable(true, BindingDirection.OneWay)]
        public long ColorCount
        {
            get { return GetColorCount(_depth); }
        }

        /// <summary>
        /// Gets the maximum color count associated with the specified <see cref="UIconEdit.BitDepth"/>.
        /// </summary>
        /// <param name="bitDepth">The <see cref="UIconEdit.BitDepth"/> to check.</param>
        /// <returns>21 for <see cref="BitDepth.Depth2Color"/>; 16 for <see cref="BitDepth.Depth16Color"/>;
        /// 256 for <see cref="BitDepth.Depth256Color"/>; 16777216 for <see cref="BitDepth.Depth24BitsPerPixel"/>; or
        /// 4294967296 for <see cref="BitDepth.Depth32BitsPerPixel"/>.</returns>
        /// <exception cref="InvalidEnumArgumentException">
        /// <paramref name="bitDepth"/> is not a valid <see cref="UIconEdit.BitDepth"/> value.
        /// </exception>
        public static long GetColorCount(BitDepth bitDepth)
        {
            switch (bitDepth)
            {
                case BitDepth.Depth2Color:
                    return 2;
                case BitDepth.Depth16Color:
                    return 16;
                case BitDepth.Depth256Color:
                    return 256;
                case BitDepth.Depth24BitsPerPixel:
                    return 0x1000000;
                case BitDepth.Depth32BitsPerPixel:
                    return uint.MaxValue + 1L;
                default:
                    throw new InvalidEnumArgumentException("bitDepth", (int)bitDepth, typeof(BitDepth));
            }
        }

        /// <summary>
        /// Returns the <see cref="UIconEdit.BitDepth"/> associated with the specified numeric value.
        /// </summary>
        /// <param name="value">The color count or number of bits per pixel to use.</param>
        /// <returns><see cref="UIconEdit.BitDepth.Depth1BitPerPixel"/> if <paramref name="value"/> is 1 or 2;
        /// <see cref="UIconEdit.BitDepth.Depth4BitsPerPixel"/> if <paramref name="value"/> is 4 or 16;
        /// <see cref="UIconEdit.BitDepth.Depth8BitsPerPixel"/> if <paramref name="value"/> is 8 or 256;
        /// <see cref="UIconEdit.BitDepth.Depth24BitsPerPixel"/> if <paramref name="value"/> is 24 or 16777216; or
        /// <see cref="UIconEdit.BitDepth.Depth32BitsPerPixel"/> if <paramref name="value"/> is 32 or 4294967296.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="value"/> is not one of the specified parameter values.
        /// </exception>
        public static BitDepth GetBitDepth(long value)
        {
            switch (value)
            {
                case 1:
                case 2:
                    return BitDepth.Depth1BitPerPixel;
                case 4:
                case 16:
                    return BitDepth.Depth4BitsPerPixel;
                case 8:
                case 256:
                    return BitDepth.Depth8BitsPerPixel;
                case 24:
                case 0x1000000:
                    return BitDepth.Depth24BitsPerPixel;
                case 32:
                case uint.MaxValue + 1L:
                    return BitDepth.Depth32BitsPerPixel;
                default:
                    throw new ArgumentException("Not a valid color count or bits-per-pixel value.", "value");
            }
        }

        /// <summary>
        /// Returns the <see cref="PixelFormat"/> associated with the specified <see cref="UIconEdit.BitDepth"/>.
        /// </summary>
        /// <param name="depth">The bit depth from which to get the pixel format.</param>
        /// <returns>The pixel format associated with <paramref name="depth"/>.</returns>
        /// <exception cref="InvalidEnumArgumentException">
        /// <paramref name="depth"/> is not a valid <see cref="UIconEdit.BitDepth"/> value.
        /// </exception>
        public static PixelFormat GetPixelFormat(BitDepth depth)
        {
            switch (depth)
            {
                case BitDepth.Depth1BitPerPixel:
                    return PixelFormats.Indexed1;
                case BitDepth.Depth4BitsPerPixel:
                    return PixelFormats.Indexed4;
                case BitDepth.Depth8BitsPerPixel:
                    return PixelFormats.Indexed8;
                case BitDepth.Depth24BitsPerPixel:
                    return PixelFormats.Bgr24;
                case BitDepth.Depth32BitsPerPixel:
                    return PixelFormats.Bgra32;
                default:
                    throw new InvalidEnumArgumentException("depth", (int)depth, typeof(BitDepth));
            }
        }

        /// <summary>
        /// Applies <see cref="AlphaImage"/> to <see cref="BaseImage"/>.
        /// </summary>
        /// <returns>A new <see cref="BitmapSource"/>, sized according to <see cref="Width"/> and <see cref="Height"/>, consisting of
        /// <see cref="AlphaImage"/> applied to <see cref="BaseImage"/> and with pixel format <see cref="PixelFormats.Bgra32"/></returns>
        public BitmapSource CombineAlpha()
        {
            BitmapSource alphaMask;

            return GetQuantized(true, BitDepth.Depth32BitsPerPixel, out alphaMask);
        }

        internal IconFileBase File;

        private static readonly Dictionary<string, DependencyPropertyKey> propertyKeys =
            typeof(IconEntry).GetFields(BindingFlags.Static | BindingFlags.NonPublic).Where(i => i.FieldType == typeof(DependencyPropertyKey))
            .ToDictionary(k => k.Name, v => (DependencyPropertyKey)v.GetValue(null));

        /// <summary>
        /// Returns a duplicate of the current instance.
        /// </summary>
        /// <returns>A duplicate of the current instance, with its own clone of <see cref="BaseImage"/>.</returns>
        public IconEntry Clone()
        {
            IconEntry copy = (IconEntry)MemberwiseClone();

            LocalValueEnumerator enumerator = GetLocalValueEnumerator();
            while (enumerator.MoveNext())
            {
                LocalValueEntry curEntry = enumerator.Current;

                object value = curEntry.Value;
                ICloneable cloneable = value as ICloneable;
                if (cloneable != null) value = cloneable.Clone();

                DependencyPropertyKey key;
                if (propertyKeys.TryGetValue(curEntry.Property.Name + "PropertyKey", out key))
                    copy.SetValue(key, value);
                else
                    copy.SetValue(curEntry.Property, value);
            }

            copy.File = null;
            return copy;
        }

        /// <summary>
        /// Returns color quantization of the current instance as it would appear for a PNG entry.
        /// </summary>
        /// <returns>A <see cref="BitmapSource"/> containing the quantized image.</returns>
        public BitmapSource GetQuantizedPng()
        {
            BitmapSource alphaMask;
            return GetQuantized(true, out alphaMask);
        }

        /// <summary>
        /// Sets <see cref="BaseImage"/> and <see cref="AlphaImage"/> equal to their quantized equivalent,
        /// in a form indicated by the specified value.
        /// </summary>
        /// <param name="isPng">If <c>true</c>, <see cref="AlphaImage"/> will be set <c>null</c> and <see cref="BaseImage"/> will be quantized
        /// as if it was a PNG icon entry. If <c>false</c>, <see cref="BaseImage"/> and <see cref="AlphaImage"/> will be quantized
        /// as if for a BMP entry.</param>
        public void SetQuantized(bool isPng)
        {
            BitmapSource alphaMask, baseImage = GetQuantized(isPng, out alphaMask);
            BaseImage = baseImage;
            AlphaImage = alphaMask;
            SetValue(IsQuantizedPropertyKey, true);
        }

        /// <summary>
        /// Sets <see cref="BaseImage"/> and <see cref="AlphaImage"/> equal to their quantized equivalent,
        /// in a form indicated by <see cref="IsPng"/>.
        /// </summary>
        /// <remarks>
        /// Performs the same action as <see cref="SetQuantized(bool)"/>, with <see cref="IsPng"/> passed as the parameter.
        /// </remarks>
        public void SetQuantized()
        {
            SetQuantized(IsPng);
        }

        #region Get Quantized
        /// <summary>
        /// Returns color quantization of the current instance as it would appear for a BMP entry.
        /// </summary>
        /// <param name="alphaMask">When this method returns, contains the quantized alpha mask generated using <see cref="AlphaThreshold"/>.
        /// Black pixels are transparent; white pixels are opaque.
        /// This parameter is passed uninitialized.</param>
        /// <returns>A <see cref="BitmapSource"/> containing the quantized image without the alpha mask.</returns>
        public BitmapSource GetQuantized(out BitmapSource alphaMask)
        {
            return GetQuantized(false, out alphaMask);
        }

        internal WriteableBitmap GetQuantized(bool isPng, out BitmapSource alphaMask)
        {
            return GetQuantized(isPng, _depth, out alphaMask);
        }

        private WriteableBitmap GetQuantized(bool isPng, BitDepth _depth, out BitmapSource alphaMask)
        {
            bool isQuantized = IsQuantized;
            BitmapSource alphaImage = AlphaImage;

            if (isQuantized && (isPng == (alphaImage == null)))
            {
                alphaMask = alphaImage;
                return new WriteableBitmap(BaseImage);
            }

            ScalingFilter scaleMode = ScalingFilter;

            uint[] pixels = _scaleBitmap(scaleMode, BaseImage);
            const uint opaqueAlpha = 0xFF000000u;

            byte _alphaThreshold = AlphaThreshold;

            if (isPng)
            {
                alphaMask = null;
                if (alphaImage != null)
                {
                    alphaImage = new FormatConvertedBitmap(alphaImage, PixelFormats.Bgra32, null, 0);

                    if (alphaImage.PixelWidth != _width || alphaImage.PixelHeight != _height)
                    {
                        alphaImage = new TransformedBitmap(alphaImage, new ScaleTransform((double)_width / alphaImage.PixelWidth,
                            (double)_height / alphaImage.PixelHeight));
                    }

                    uint[] alphaPixels = new uint[_width * _height];
                    alphaImage.CopyPixels(alphaPixels, _width * sizeof(uint), 0);

                    for (int i = 0; i < alphaPixels.Length; i++)
                    {
                        if (alphaPixels[i] == opaqueAlpha)
                            pixels[i] &= ~opaqueAlpha;
                    }
                }
            }
            else if (alphaImage == null)
            {
                uint[] alphaPixels = new uint[pixels.Length];

                for (int i = 0; i < pixels.Length; i++)
                {
                    uint curVal = pixels[i] >> 24;
                    if (curVal < _alphaThreshold)
                        alphaPixels[i] = opaqueAlpha;
                    else
                        alphaPixels[i] = uint.MaxValue;
                }

                alphaMask = new FormatConvertedBitmap(GetBitmap(_width, _height, alphaPixels), PixelFormats.Indexed1, AlphaPalette, 0);
            }
            else
            {
                if (alphaImage.PixelWidth != _width || alphaImage.PixelHeight != _height)
                {
                    if (scaleMode == ScalingFilter.Matrix)
                    {
                        alphaImage = new TransformedBitmap(alphaImage, new ScaleTransform((double)_width / alphaImage.PixelWidth,
                            (double)_height / alphaImage.PixelHeight));
                    }
                    else
                    {
                        uint[] alphaPixels = new uint[_width * _height];
                        _scaleBitmap(scaleMode, new FormatConvertedBitmap(alphaImage, PixelFormats.Bgra32, null, 0), alphaPixels);
                        alphaMask = GetBitmap(_width, _height, alphaPixels);
                    }
                }
                alphaMask = new FormatConvertedBitmap(alphaImage, PixelFormats.Indexed1, AlphaPalette, 0);
            }

            if (_depth == BitDepth.Depth32BitsPerPixel)
                return GetBitmap(_width, _height, pixels);

            for (int i = 0; i < pixels.Length; i++)
            {
                uint value = pixels[i] >> 24;
                if (value < _alphaThreshold)
                    pixels[i] = isPng ? (pixels[i] & ~opaqueAlpha) : opaqueAlpha;
                else
                    pixels[i] |= opaqueAlpha;
            }

            DPixelFormat pFormat;
            ushort paletteCount;
            switch (_depth)
            {
                case BitDepth.Depth24BitsPerPixel:
                    if (isPng) return GetBitmap(_width, _height, pixels);
                    return GetBitmap(_width, _height, pixels, DPixelFormat.Format24bppRgb);
                case BitDepth.Depth2Color:
                    paletteCount = 2;
                    pFormat = isPng ? DPixelFormat.Format8bppIndexed : DPixelFormat.Format1bppIndexed;
                    break;
                case BitDepth.Depth16Color:
                    paletteCount = 16;
                    pFormat = isPng ? DPixelFormat.Format8bppIndexed : DPixelFormat.Format4bppIndexed;
                    break;
                default: //Depth256Color
                    paletteCount = 256;
                    pFormat = DPixelFormat.Format8bppIndexed;
                    break;
            }

            WriteableBitmap quantized = GetBitmap(_width, _height, pixels, pFormat, paletteCount);

            if (!isPng) return quantized;

            byte[] quantBytes = new byte[pixels.Length];
            quantized.CopyPixels(quantBytes, _width, 0);

            for (int i = 0; i < pixels.Length; i++)
            {
                Color curColor = quantized.Palette.Colors[quantBytes[i]];

                uint bgra = curColor.B | ((uint)curColor.G << 8) | ((uint)curColor.R << 16);

                pixels[i] = (pixels[i] & opaqueAlpha) | bgra;
            }

            return GetBitmap(_width, _height, pixels);
        }

        private uint[] _scaleBitmap(ScalingFilter scaleMode, BitmapSource image)
        {
            uint[] pixels = new uint[_width * _height];
            FormatConvertedBitmap formatBmp = new FormatConvertedBitmap(image, PixelFormats.Bgra32, null, 0);
            if (formatBmp.PixelWidth == _width && formatBmp.PixelHeight == _height)
            {
                formatBmp.CopyPixels(pixels, _width * sizeof(uint), 0);
            }
            else if (scaleMode == ScalingFilter.Matrix)
            {
                TransformedBitmap transBmp = new TransformedBitmap(formatBmp, new ScaleTransform((double)_width / formatBmp.PixelWidth,
                    (double)_height / formatBmp.PixelHeight));
                transBmp.CopyPixels(pixels, _width * sizeof(uint), 0);
            }
            else _scaleBitmap(scaleMode, formatBmp, pixels);

            return pixels;
        }

        private void _scaleBitmap(ScalingFilter scaleMode, FormatConvertedBitmap image, uint[] pixels)
        {
            using (DBitmap dBitmap = new DBitmap(image.PixelWidth, image.PixelHeight, DPixelFormat.Format32bppArgb))
            {
                BitmapData dData = dBitmap.LockBits(new DRectangle(0, 0, image.PixelWidth, image.PixelHeight),
                    ImageLockMode.WriteOnly, DPixelFormat.Format32bppArgb);

                image.CopyPixels(new Int32Rect(0, 0, image.PixelWidth, image.PixelHeight), dData.Scan0, image.PixelHeight * dData.Stride, dData.Stride);

                dBitmap.UnlockBits(dData);

                using (DBitmap sizeBmp = new DBitmap(_width, _height, DPixelFormat.Format32bppArgb))
                {
                    using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(sizeBmp))
                    {
                        g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                        switch (scaleMode)
                        {
                            case ScalingFilter.Bicubic:
                                g.InterpolationMode = InterpolationMode.Bicubic;
                                break;
                            case ScalingFilter.Bilinear:
                                g.InterpolationMode = InterpolationMode.Bilinear;
                                break;
                            case ScalingFilter.HighQualityBicubic:
                                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                break;
                            case ScalingFilter.HighQualityBilinear:
                                g.InterpolationMode = InterpolationMode.HighQualityBilinear;
                                break;
                            case ScalingFilter.NearestNeighbor:
                                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                                break;
                        }

                        g.DrawImage(dBitmap, 0, 0, _width, _height);
                    }

                    BitmapData sizeData = sizeBmp.LockBits(new DRectangle(0, 0, _width, _height), ImageLockMode.ReadOnly, DPixelFormat.Format32bppArgb);

                    unsafe
                    {
                        uint* pData = (uint*)sizeData.Scan0;
                        for (int i = 0; i < pixels.Length; i++)
                            pixels[i] = pData[i];
                    }

                    sizeBmp.UnlockBits(sizeData);
                }
            }
        }

        internal unsafe WriteableBitmap GetBitmap(int pixelWidth, int pixelHeight, uint[] pixels, DPixelFormat format, ushort maxColors)
        {
            if (maxColors > 256 && (format == DPixelFormat.Format32bppArgb || format == DPixelFormat.Format24bppRgb))
            {
                WriteableBitmap wBmp = new WriteableBitmap(pixelWidth, pixelHeight, 0, 0, PixelFormats.Bgra32, null);
                if (format == DPixelFormat.Format24bppRgb)
                {
                    for (int i = 0; i < pixels.Length; i++)
                        pixels[i] |= 0xFF000000;
                }

                wBmp.WritePixels(new Int32Rect(0, 0, pixelWidth, pixelHeight), pixels, pixelWidth * 4, 0);
                if (format == DPixelFormat.Format32bppArgb)
                    return wBmp;

                return new WriteableBitmap(new FormatConvertedBitmap(wBmp, PixelFormats.Bgr24, null, 0));
            }
            var fullRect = new DRectangle(0, 0, pixelWidth, pixelHeight);
            DBitmap resultBmp;
            {
                DBitmap baseBmp = new DBitmap(pixelWidth, pixelHeight, DPixelFormat.Format32bppArgb);

                BitmapData bData = baseBmp.LockBits(fullRect, ImageLockMode.WriteOnly, DPixelFormat.Format32bppArgb);

                uint* pData = (uint*)bData.Scan0;

                for (int i = 0; i < pixels.Length; i++)
                    pData[i] = pixels[i];

                baseBmp.UnlockBits(bData);

                if (maxColors > 256)
                {
                    if (format == DPixelFormat.Format32bppArgb)
                        resultBmp = baseBmp;
                    else
                    {
                        resultBmp = baseBmp.Clone(fullRect, format);
                        baseBmp.Dispose();
                    }
                }
                else
                {
                    nQuant.WuQuantizer quant = new nQuant.WuQuantizer();
                    resultBmp = (DBitmap)quant.QuantizeImage(baseBmp, 0, 0, maxColors);
                    baseBmp.Dispose();
                }
            }

            BitmapPalette palette = null;
            if (resultBmp.Palette != null && resultBmp.Palette.Entries.Length != 0)
            {
                var palCollection = resultBmp.Palette.Entries.Select(c => Color.FromArgb(c.A, c.R, c.G, c.B));

                switch (format)
                {
                    case DPixelFormat.Format32bppArgb:
                    case DPixelFormat.Format24bppRgb:
                    case DPixelFormat.Format8bppIndexed:
                        break;
                    default:
                        palCollection = palCollection.Take(maxColors);
                        break;
                }

                palette = new BitmapPalette(palCollection.ToArray());
            }

            PixelFormat rFormat;

            switch (resultBmp.PixelFormat)
            {
                default: //DPixelFormat.Format32bppArgb:
                    rFormat = PixelFormats.Bgra32;
                    break;
                case DPixelFormat.Format24bppRgb:
                    rFormat = PixelFormats.Bgr24;
                    break;
                case DPixelFormat.Format8bppIndexed:
                    rFormat = PixelFormats.Indexed8;
                    break;
                case DPixelFormat.Format4bppIndexed:
                    rFormat = PixelFormats.Indexed4;
                    break;
                case DPixelFormat.Format1bppIndexed:
                    rFormat = PixelFormats.Indexed1;
                    break;
            }

            BitmapData rData = resultBmp.LockBits(fullRect, ImageLockMode.ReadOnly, resultBmp.PixelFormat);

            BitmapSource source = BitmapSource.Create(pixelWidth, pixelHeight, 0, 0, rFormat, palette, rData.Scan0, rData.Stride * rData.Height, rData.Stride);

            switch (format)
            {
                default: //DPixelFormat.Format32bppArgb:
                    rFormat = PixelFormats.Bgra32;
                    break;
                case DPixelFormat.Format24bppRgb:
                    rFormat = PixelFormats.Bgr24;
                    break;
                case DPixelFormat.Format8bppIndexed:
                    rFormat = PixelFormats.Indexed8;
                    break;
                case DPixelFormat.Format4bppIndexed:
                    rFormat = PixelFormats.Indexed4;
                    break;
                case DPixelFormat.Format1bppIndexed:
                    rFormat = PixelFormats.Indexed1;
                    break;
            }

            WriteableBitmap result = new WriteableBitmap(new FormatConvertedBitmap(source, rFormat, source.Palette, 0));

            resultBmp.UnlockBits(rData);
            resultBmp.Dispose();

            return result;
        }

        internal WriteableBitmap GetBitmap(int pixelWidth, int pixelHeight, uint[] pixels, DPixelFormat format)
        {
            return GetBitmap(pixelWidth, pixelHeight, pixels, format, ushort.MaxValue);
        }

        internal WriteableBitmap GetBitmap(int pixelWidth, int pixelHeight, uint[] pixels)
        {
            return GetBitmap(pixelWidth, pixelHeight, pixels, DPixelFormat.Format32bppArgb, ushort.MaxValue);
        }
        #endregion

        /// <summary>
        /// Returns a string representation of the current instance.
        /// </summary>
        /// <returns>A string representation of the current instance.</returns>
        public override string ToString()
        {
            return string.Format("{0}, BaseImage:{1}", EntryKey, BaseImage);
        }


        /// <summary>
        /// Parses the specified string as a <see cref="UIconEdit.BitDepth"/> value.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <returns>The parsed <see cref="UIconEdit.BitDepth"/> value.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <para><paramref name="value"/> is an empty string or contains only whitespace.</para>
        /// <para>-OR-</para>
        /// <para><paramref name="value"/> does not translate to a valid <see cref="UIconEdit.BitDepth"/> value.</para>
        /// </exception>
        /// <remarks>
        /// <para><paramref name="value"/> is parsed in a case-insensitive manner which works differently from <see cref="Enum.Parse(Type, string, bool)"/>.</para>
        /// <para>First of all, all non-alphanumeric characters are stripped. If <paramref name="value"/> is entirely numeric, or begins with "Depth"
        /// followed by an entirely numeric value, it is parsed according to the number of colors or the number of bits per pixel, rather than the
        /// integer <see cref="UIconEdit.BitDepth"/> value. There is fortunately no overlap; 1, 4, 8, 24, and 32 always refer to the number of bits
        /// per pixel, whereas 2, 16, 256, 16777216, and 4294967296 always refer to the number of colors.</para>
        /// <para>Otherwise, "Depth" is prepended to the beginning, and it attempts to ensure that the value ends with either "Color" or "BitsPerPixel"
        /// (or "BitPerPixel" in the case of <see cref="UIconEdit.BitDepth.Depth1BitPerPixel"/>).</para>
        /// </remarks>
        /// <example>
        /// <code>
        /// BitDepth result;
        /// if (IconEntry.TryParse("32", out result)) Console.WriteLine("Succeeded: " + result);
        /// else Console.WriteLine("Failed!");
        /// //Succeeded: BitDepth.Depth32BitsPerPixel
        /// 
        /// if (IconEntry.TryParse("32Bit", out result)) Console.WriteLine("Succeeded: " + result);
        /// else Console.WriteLine("Failed");
        /// //Succeeded: BitDepth.Depth32BitsPerPixel
        /// 
        /// if (IconEntry.TryParse("32Color", out result)) Console.WriteLine("Succeeded: " + result);
        /// else Console.WriteLine("Failed");
        /// //Failed
        /// 
        /// if (IconEntry.TryParse("Depth256", out result)) Console.WriteLine("Succeeded: " + result);
        /// else Console.WriteLine("Failed");
        /// //Succeeded: BitDepth.Depth256Color
        /// </code>
        /// </example>
        public static BitDepth ParseBitDepth(string value)
        {
            if (value == null) throw new ArgumentNullException("value");
            BitDepth result;
            TryParseBitDepth(value, true, out result);
            return result;
        }

        /// <summary>
        /// Parses the specified string as a <see cref="UIconEdit.BitDepth"/> value.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <param name="result">When this method returns, contains the parsed <see cref="UIconEdit.BitDepth"/> result, or
        /// the default value for type <see cref="UIconEdit.BitDepth"/> if <paramref name="value"/> could not be parsed.
        /// This parameter is passed uninitialized.</param>
        /// <returns><c>true</c> if <paramref name="value"/> was successfully parsed; <c>false</c> otherwise.</returns>
        /// <remarks>
        /// <para><paramref name="value"/> is parsed in a case-insensitive manner which works differently from <see cref="Enum.TryParse{TEnum}(string, bool, out TEnum)"/>.</para>
        /// <para>First of all, all non-alphanumeric characters are stripped. If <paramref name="value"/> is entirely numeric, or begins with "Depth"
        /// followed by an entirely numeric value, it is parsed according to the number of colors or the number of bits per pixel, rather than the
        /// integer <see cref="UIconEdit.BitDepth"/> value. There is fortunately no overlap; 1, 4, 8, 24, and 32 always refer to the number of bits
        /// per pixel, whereas 2, 16, 256, 16777216, and 4294967296 always refer to the number of colors.</para>
        /// <para>Otherwise, "Depth" is prepended to the beginning, and it attempts to ensure that the value ends with either "Color" or "BitsPerPixel"
        /// (or "BitPerPixel" in the case of <see cref="UIconEdit.BitDepth.Depth1BitPerPixel"/>).</para>
        /// </remarks>
        /// <example>
        /// <code>
        /// BitDepth result;
        /// if (IconEntry.TryParse("32", out result)) Console.WriteLine("Succeeded: " + result);
        /// else Console.WriteLine("Failed!");
        /// //Succeeded: BitDepth.Depth32BitsPerPixel
        /// 
        /// if (IconEntry.TryParse("32Bit", out result)) Console.WriteLine("Succeeded: " + result);
        /// else Console.WriteLine("Failed");
        /// //Succeeded: BitDepth.Depth32BitsPerPixel
        /// 
        /// if (IconEntry.TryParse("32Color", out result)) Console.WriteLine("Succeeded: " + result);
        /// else Console.WriteLine("Failed");
        /// //Failed
        /// 
        /// if (IconEntry.TryParse("Depth256", out result)) Console.WriteLine("Succeeded: " + result);
        /// else Console.WriteLine("Failed");
        /// //Succeeded: BitDepth.Depth256Color
        /// </code>
        /// </example>
        public static bool TryParseBitDepth(string value, out BitDepth result)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                result = 0;
                return false;
            }

            return TryParseBitDepth(value, false, out result);
        }

        private static bool TryParseBitDepth(string value, bool throwError, out BitDepth result)
        {
            if (throwError && string.IsNullOrWhiteSpace(value))
                Enum.Parse(typeof(BitDepth), value, true);

            value = value.Trim();

            long intVal;

            const NumberStyles numStyles = NumberStyles.Integer & ~NumberStyles.AllowLeadingSign;

            string stripValue = new string(value.Where(char.IsLetterOrDigit).Select(char.ToLowerInvariant).ToArray());

            if (long.TryParse(stripValue, numStyles, NumberFormatInfo.InvariantInfo, out intVal) || (stripValue.StartsWith("depth") &&
                long.TryParse(stripValue.Substring(5), numStyles, NumberFormatInfo.InvariantInfo, out intVal)))
            {
                switch (intVal)
                {
                    case 1:
                        result = BitDepth.Depth1BitsPerPixel;
                        return true;
                    case 2:
                        result = BitDepth.Depth2Color;
                        return true;
                    case 4:
                        result = BitDepth.Depth4BitsPerPixel;
                        return true;
                    case 8:
                        result = BitDepth.Depth8BitsPerPixel;
                        return true;
                    case 16:
                        result = BitDepth.Depth16Color;
                        return true;
                    case 24:
                        result = BitDepth.Depth24BitsPerPixel;
                        return true;
                    case 32:
                        result = BitDepth.Depth32BitsPerPixel;
                        return true;
                    case 256:
                        result = BitDepth.Depth256Color;
                        return true;
                    case 16777216:
                        result = BitDepth.Depth24BitsPerPixel;
                        return true;
                    case uint.MaxValue + 1L:
                        result = BitDepth.Depth32BitsPerPixel;
                        return true;
                    default:
                        if (throwError)
                        {
                            stripValue += "!!+";

                            try
                            {
                                result = (BitDepth)Enum.Parse(typeof(BitDepth), stripValue, true);
                            }
                            catch (ArgumentException e)
                            {
                                throw new ArgumentException(e.Message.Replace(stripValue, value), "value");
                            }
                            return false;
                        }

                        result = 0;
                        return false;
                }
            }

            if (!stripValue.StartsWith("depth"))
                stripValue = "depth" + stripValue;
            if (stripValue.EndsWith("bit"))
                stripValue += "sPerPixel";
            else if (stripValue.EndsWith("bits"))
                stripValue += "PerPixel";
            else if (stripValue.EndsWith("colors", StringComparison.OrdinalIgnoreCase))
                stripValue = stripValue.Substring(value.Length - 1);
            else if (!stripValue.Equals("depth1bitperpixel", StringComparison.OrdinalIgnoreCase))
                stripValue = stripValue.Replace("bitperpixel", "BitsPerPixel");

            if (stripValue.Equals("depth16777216color", StringComparison.OrdinalIgnoreCase))
            {
                result = BitDepth.Depth24BitsPerPixel;
                return true;
            }
            else if (stripValue.Equals("depth4294967296color", StringComparison.OrdinalIgnoreCase))
            {
                result = BitDepth.Depth32BitsPerPixel;
                return true;
            }

            if (Enum.TryParse(stripValue, true, out result))
                return true;

            if (throwError)
            {
                try
                {
                    stripValue += "!!+";

                    result = (BitDepth)Enum.Parse(typeof(BitDepth), stripValue, true);
                }
                catch (ArgumentException e)
                {
                    throw new ArgumentException(e.Message.Replace(stripValue, value), "value");
                }
            }

            return false;
        }
    }

    /// <summary>
    /// Represents a simplified key for an icon entry.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct EntryKey : IEquatable<EntryKey>, IComparable<EntryKey>
    {
        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="width">The width of the icon entry.</param>
        /// <param name="height">The height of the icon entry.</param>
        /// <param name="bitDepth">The bit depth of the icon entry.</param>
        public EntryKey(short width, short height, BitDepth bitDepth)
        {
            Width = width;
            Height = height;
            BitDepth = bitDepth;
        }

        /// <summary>
        /// Indicates the width of the icon entry.
        /// </summary>
        public short Width;
        /// <summary>
        /// Indicates the height of the icon entry.
        /// </summary>
        public short Height;
        /// <summary>
        /// Indicates the bit depth of the icon entry.
        /// </summary>
        public BitDepth BitDepth;

        /// <summary>
        /// Gets a value indicating whether <see cref="Width"/>, <see cref="Height"/>, and <see cref="BitDepth"/> are all 0.
        /// </summary>
        public bool IsEmpty { get { return Width == 0 && Height == 0 && BitDepth == 0; } }

        /// <summary>
        /// Gets a value indicating whether the current instance contains valid values which would actually occur in an <see cref="IconEntry"/>.
        /// </summary>
        public bool IsValid
        {
            get
            {
                if (Width < IconEntry.MinDimension || Width > IconEntry.MaxDimension || Height < IconEntry.MinDimension || Height > IconEntry.MaxDimension)
                    return false;

                switch (BitDepth)
                {
                    case BitDepth.Depth1BitPerPixel:
                    case BitDepth.Depth4BitsPerPixel:
                    case BitDepth.Depth8BitsPerPixel:
                    case BitDepth.Depth24BitsPerPixel:
                    case BitDepth.Depth32BitsPerPixel:
                        return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Compares the current instance to the specified other <see cref="EntryKey"/> object. First
        /// <see cref="BitDepth"/> is compared, then <see cref="Height"/>, then <see cref="Width"/> (with
        /// higher color-counts and larger elements first).
        /// </summary>
        /// <param name="other">The other <see cref="EntryKey"/> to compare.</param>
        /// <returns>A value less than 0 if the current value comes before <paramref name="other"/>; 
        /// a value greater than 0 if the current value comes after <paramref name="other"/>; or
        /// 0 if the current instance is equal to <paramref name="other"/>.</returns>
        public int CompareTo(EntryKey other)
        {
            int compare = (int)BitDepth - (int)other.BitDepth;
            if (compare != 0) return compare;

            compare = other.Height - Height;
            if (compare != 0) return compare;

            return other.Width - Width;
        }

        /// <summary>
        /// An <see cref="EntryKey"/> with <see cref="IsEmpty"/> set to <c>true</c>.
        /// </summary>
        public static readonly EntryKey Empty;

        /// <summary>
        /// Returns a string representation of the current value.
        /// </summary>
        /// <returns>A string representation of the current value.</returns>
        public override string ToString()
        {
            return string.Format("Width:{0}, Height:{1}, BitDepth:{2}", Width, Height, BitDepth);
        }

        /// <summary>
        /// Compares two <see cref="EntryKey"/> objects.
        /// </summary>
        /// <param name="f1">An <see cref="EntryKey"/> to compare.</param>
        /// <param name="f2">An <see cref="EntryKey"/> to compare.</param>
        /// <returns><c>true</c> if <paramref name="f1"/> is less than <paramref name="f2"/>; <c>false</c> otherwise.</returns>
        public static bool operator <(EntryKey f1, EntryKey f2)
        {
            return f1.CompareTo(f2) < 0;
        }

        /// <summary>
        /// Compares two <see cref="EntryKey"/> objects.
        /// </summary>
        /// <param name="f1">An <see cref="EntryKey"/> to compare.</param>
        /// <param name="f2">An <see cref="EntryKey"/> to compare.</param>
        /// <returns><c>true</c> if <paramref name="f1"/> is greater than <paramref name="f2"/>; <c>false</c> otherwise.</returns>
        public static bool operator >(EntryKey f1, EntryKey f2)
        {
            return f1.CompareTo(f2) > 0;
        }

        /// <summary>
        /// Compares two <see cref="EntryKey"/> objects.
        /// </summary>
        /// <param name="f1">An <see cref="EntryKey"/> to compare.</param>
        /// <param name="f2">An <see cref="EntryKey"/> to compare.</param>
        /// <returns><c>true</c> if <paramref name="f1"/> is less than or equal to <paramref name="f2"/>; <c>false</c> otherwise.</returns>
        public static bool operator <=(EntryKey f1, EntryKey f2)
        {
            return f1.CompareTo(f2) <= 0;
        }

        /// <summary>
        /// Compares two <see cref="EntryKey"/> objects.
        /// </summary>
        /// <param name="f1">An <see cref="EntryKey"/> to compare.</param>
        /// <param name="f2">An <see cref="EntryKey"/> to compare.</param>
        /// <returns><c>true</c> if <paramref name="f1"/> is less than or equal to <paramref name="f2"/>; <c>false</c> otherwise.</returns>
        public static bool operator >=(EntryKey f1, EntryKey f2)
        {
            return f1.CompareTo(f2) >= 0;
        }


        /// <summary>
        /// Determines if the current instance is equal to the specified other <see cref="EntryKey"/> value.
        /// </summary>
        /// <param name="other">The other <see cref="EntryKey"/> to compare.</param>
        /// <returns><c>true</c> if the current instance is equal to <paramref name="other"/>; <c>false</c> otherwise.</returns>
        public bool Equals(EntryKey other)
        {
            return Width == other.Width && Height == other.Height && BitDepth == other.BitDepth;
        }

        /// <summary>
        /// Determines if the current instance is equal to the specified other object.
        /// </summary>
        /// <param name="obj">The other object to compare.</param>
        /// <returns><c>true</c> if the current instance is equal to <paramref name="obj"/>; <c>false</c> otherwise.</returns>
        public override bool Equals(object obj)
        {
            return obj is EntryKey && Equals((EntryKey)obj);
        }

        /// <summary>
        /// Returns a hash code for the current value.
        /// </summary>
        /// <returns>A hash code for the current value.</returns>
        public override int GetHashCode()
        {
            return ((ushort)Width | ((ushort)Height << 16)) ^ ((int)BitDepth << 12);
        }

        /// <summary>
        /// Determines equality of two <see cref="EntryKey"/> objects.
        /// </summary>
        /// <param name="f1">An <see cref="EntryKey"/> to compare.</param>
        /// <param name="f2">An <see cref="EntryKey"/> to compare.</param>
        /// <returns><c>true</c> if <paramref name="f1"/> is equal to <paramref name="f2"/>; <c>false</c> otherwise.</returns>
        public static bool operator ==(EntryKey f1, EntryKey f2)
        {
            return f1.Equals(f2);
        }

        /// <summary>
        /// Determines inequality of two <see cref="EntryKey"/> objects.
        /// </summary>
        /// <param name="f1">An <see cref="EntryKey"/> to compare.</param>
        /// <param name="f2">An <see cref="EntryKey"/> to compare.</param>
        /// <returns><c>true</c> if <paramref name="f1"/> is not equal to <paramref name="f2"/>; <c>false</c> otherwise.</returns>
        public static bool operator !=(EntryKey f1, EntryKey f2)
        {
            return !f1.Equals(f2);
        }
    }

    internal struct IconEntryComparer : IEqualityComparer<IconEntry>, IComparer<IconEntry>
    {
        public int Compare(IconEntry x, IconEntry y)
        {
            if (ReferenceEquals(x, y))
                return 0;

            if (ReferenceEquals(x, null)) return -1;
            else if (ReferenceEquals(y, null)) return 1;

            return x.EntryKey.CompareTo(y.EntryKey);
        }

        public bool Equals(IconEntry x, IconEntry y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null) ^ ReferenceEquals(y, null)) return false;
            return x.EntryKey == y.EntryKey;
        }

        public int GetHashCode(IconEntry obj)
        {
            if (obj == null) return 0;
            return obj.EntryKey.GetHashCode();
        }
    }

    /// <summary>
    /// Indicates the bit depth of an icon entry.
    /// </summary>
    public enum BitDepth : short
    {
        /// <summary>
        /// Indicates that the entry is full color with alpha (32 bits per pixel).
        /// </summary>
        Depth32BitsPerPixel = 0,
        /// <summary>
        /// Indicates that the entry is full color without alpha (24 bits per pixel).
        /// </summary>
        Depth24BitsPerPixel = 1,
        /// <summary>
        /// Indicates that the entry is 256-color (8 bits per pixel). Same value as <see cref="Depth8BitsPerPixel"/>.
        /// </summary>
        Depth256Color = 2,
        /// <summary>
        /// Indicates that the entry is 16-color (4 bits per pixel). Same value as <see cref="Depth4BitsPerPixel"/>.
        /// </summary>
        Depth16Color = 3,
        /// <summary>
        /// Indicates that the entry is 2-color (1 bit per pixel). Same value as <see cref="Depth1BitPerPixel"/>.
        /// </summary>
        Depth2Color = 4,
        /// <summary>
        /// Indicates that the entry is 256-color (8 bits per pixel). Same value as <see cref="Depth256Color"/>.
        /// </summary>
        Depth8BitsPerPixel = Depth256Color,
        /// <summary>
        /// Indicates that the entry is 16-color (4 bits per pixel). Same value as <see cref="Depth16Color"/>.
        /// </summary>
        Depth4BitsPerPixel = Depth16Color,
        /// <summary>
        /// Indicates that the entry is 2-color (1 bit per pixel). Same value as <see cref="Depth2Color"/>.
        /// </summary>
        Depth1BitPerPixel = Depth2Color,
        /// <summary>
        /// Indicates that the entry is 2-color (1 bit per pixel). Same value as <see cref="Depth2Color"/>.
        /// </summary>
        Depth1BitsPerPixel = Depth2Color,
    }

    /// <summary>
    /// Indicates options for resizing <see cref="IconEntry.BaseImage"/> and <see cref="IconEntry.AlphaImage"/> when quantizing.
    /// </summary>
    public enum ScalingFilter
    {
        /// <summary>
        /// Resizes using a transformation matrix.
        /// </summary>
        Matrix,
        /// <summary>
        /// Specifies bilinear interpolation.
        /// </summary>
        Bilinear,
        /// <summary>
        /// Specifies bicubic interpolation.
        /// </summary>
        Bicubic,
        /// <summary>
        /// Specifies nearest-neighbor interpolation.
        /// </summary>
        NearestNeighbor,
        /// <summary>
        /// Specifies high-quality bilinear interpolation. Prefiltering is performed to ensure high-quality transformation.
        /// </summary>
        HighQualityBilinear,
        /// <summary>
        /// Specifies high-quality bicubic interpolation. Prefiltering is performed to ensure high-quality transformation.
        /// </summary>
        HighQualityBicubic
    }
}
