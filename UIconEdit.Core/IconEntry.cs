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
using System.Runtime.InteropServices;
#if DRAWING
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using BitmapSource = System.Drawing.Image;
using nQuant;

namespace UIconDrawing
#else
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using nQuantWpf;

using Bitmap = System.Drawing.Bitmap;
using BitmapData = System.Drawing.Imaging.BitmapData;
using DPixelFormat = System.Drawing.Imaging.PixelFormat;
using ImageLockMode = System.Drawing.Imaging.ImageLockMode;
using InterpolationMode = System.Drawing.Drawing2D.InterpolationMode;
using Graphics = System.Drawing.Graphics;
using Rectangle = System.Drawing.Rectangle;

namespace UIconEdit
#endif
{
    /// <summary>
    /// Represents a single entry in an icon.
    /// </summary>
    public class IconEntry :
#if DRAWING
        IDisposable, ICloneable
#else
        Freezable
#endif
    {
        /// <summary>
        /// The default <see cref="AlphaThreshold"/> value.
        /// </summary>
        public const byte DefaultAlphaThreshold = 96;
        /// <summary>
        /// The default <see cref="AlphaThreshold"/> value when <see cref="BitDepth"/> is <see cref="IconBitDepth.Depth32BitsPerPixel"/>.
        /// </summary>
        public const byte DefaultAlphaThreshold32 = 1;

        private void _initValues(int width, int height, IconBitDepth bitDepth)
        {
            if (width < MinDimension || width > MaxDimension)
                throw new ArgumentOutOfRangeException("width");

            if (height < MinDimension || height > MaxDimension)
                throw new ArgumentOutOfRangeException("height");

            if (!_validateBitDepth(bitDepth))
                throw new InvalidEnumArgumentException("bitDepth", (int)bitDepth, typeof(IconBitDepth));
        }

        private static bool _validateBitDepth(IconBitDepth value)
        {
            switch (value)
            {
                case IconBitDepth.Depth1BitsPerPixel:
                case IconBitDepth.Depth4BitsPerPixel:
                case IconBitDepth.Depth8BitsPerPixel:
                case IconBitDepth.Depth24BitsPerPixel:
                case IconBitDepth.Depth32BitsPerPixel:
                    return true;
            }
            return false;
        }

        private static byte _defaultAlphaThreshold(IconBitDepth bitDepth)
        {
            if (bitDepth == IconBitDepth.Depth32BitsPerPixel) return DefaultAlphaThreshold32;
            return DefaultAlphaThreshold;
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
        /// <paramref name="bitDepth"/> is not a valid <see cref="IconBitDepth"/> value.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="width"/> or <paramref name="height"/> is less than <see cref="MinDimension"/> or is greater than <see cref="MaxDimension"/>.
        /// </exception>
        public IconEntry(BitmapSource baseImage, int width, int height, IconBitDepth bitDepth, int hotspotX, int hotspotY, byte alphaThreshold)
        {
            if (baseImage == null) throw new ArgumentNullException("baseImage");
            _initValues(width, height, bitDepth);
            _width = width;
            _height = height;
            _depth = bitDepth;
#if DRAWING
            _isPng
#else
            IsPng
#endif
                = IsPngByDefault;

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
        /// <paramref name="bitDepth"/> is not a valid <see cref="IconBitDepth"/> value.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="width"/> or <paramref name="height"/> is less than <see cref="MinDimension"/> or is greater than <see cref="MaxDimension"/>.
        /// </exception>
        public IconEntry(BitmapSource baseImage, int width, int height, IconBitDepth bitDepth, byte alphaThreshold)
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
        /// <paramref name="bitDepth"/> is not a valid <see cref="IconBitDepth"/> value.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="width"/> or <paramref name="height"/> is less than <see cref="MinDimension"/> or is greater than <see cref="MaxDimension"/>.
        /// </exception>
        public IconEntry(BitmapSource baseImage, int width, int height, IconBitDepth bitDepth, int hotspotX, int hotspotY)
            : this(baseImage, width, height, bitDepth, hotspotX, hotspotY, _defaultAlphaThreshold(bitDepth))
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
        /// <paramref name="bitDepth"/> is not a valid <see cref="IconBitDepth"/> value.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="width"/> or <paramref name="height"/> is less than <see cref="MinDimension"/> or is greater than <see cref="MaxDimension"/>.
        /// </exception>
        public IconEntry(BitmapSource baseImage, int width, int height, IconBitDepth bitDepth)
            : this(baseImage, width, height, bitDepth, 0, 0, _defaultAlphaThreshold(bitDepth))
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
        /// <paramref name="bitDepth"/> is not a valid <see cref="IconBitDepth"/> value.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The width or height of <paramref name="baseImage"/> is less than <see cref="MinDimension"/> or is greater than <see cref="MaxDimension"/>.
        /// </exception>
        public IconEntry(BitmapSource baseImage, IconBitDepth bitDepth, int hotspotX, int hotspotY, byte alphaThreshold)
        {
            if (baseImage == null) throw new ArgumentNullException("baseImage");
#if DRAWING
            if (baseImage.Width < MinDimension || baseImage.Width > MaxDimension || baseImage.Height < MinDimension || baseImage.Height > MaxDimension)
#else
            if (baseImage.PixelWidth < MinDimension || baseImage.PixelWidth > MaxDimension || baseImage.PixelHeight < MinDimension || baseImage.PixelHeight > MaxDimension)
#endif
                throw new ArgumentException("The image size is out of the supported range.", "baseImage");
            if (!_validateBitDepth(bitDepth))
                throw new InvalidEnumArgumentException("bitDepth", (int)bitDepth, typeof(IconBitDepth));
            BaseImage = baseImage;

            _depth = bitDepth;
#if DRAWING
            _width = baseImage.Width;
            _height = baseImage.Height;
            _isPng
#else
            _width = baseImage.PixelWidth;
            _height = baseImage.PixelHeight;
            IsPng
#endif
                = IsPngByDefault;

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
        /// <paramref name="bitDepth"/> is not a valid <see cref="IconBitDepth"/> value.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The width or height of <paramref name="baseImage"/> is less than <see cref="MinDimension"/> or is greater than <see cref="MaxDimension"/>.
        /// </exception>
        public IconEntry(BitmapSource baseImage, IconBitDepth bitDepth, int hotspotX, int hotspotY)
            : this(baseImage, bitDepth, hotspotX, hotspotY, _defaultAlphaThreshold(bitDepth))
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
        /// <paramref name="bitDepth"/> is not a valid <see cref="IconBitDepth"/> value.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The width or height of <paramref name="baseImage"/> is less than <see cref="MinDimension"/> or is greater than <see cref="MaxDimension"/>.
        /// </exception>
        public IconEntry(BitmapSource baseImage, IconBitDepth bitDepth, byte alphaThreshold)
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
        /// <paramref name="bitDepth"/> is not a valid <see cref="IconBitDepth"/> value.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The width or height of <paramref name="baseImage"/> is less than <see cref="MinDimension"/> or is greater than <see cref="MaxDimension"/>.
        /// </exception>
        public IconEntry(BitmapSource baseImage, IconBitDepth bitDepth)
            : this(baseImage, bitDepth, 0, 0, _defaultAlphaThreshold(bitDepth))
        {
        }

        internal IconEntry(BitmapSource baseImage, BitmapSource alphaImage, IconBitDepth bitDepth, int hotspotX, int hotspotY, bool isPng)
        {
            BaseImage = baseImage;
            AlphaImage = alphaImage;
#if DRAWING
            _width = baseImage.Width;
            _height = baseImage.Height;
            _isQuantizedImage = _isQuantizedAlpha = true;
            _isPng = isPng;
#else
            _width = baseImage.PixelWidth;
            _height = baseImage.PixelHeight;
            SetValue(IsQuantizedPropertyKey, true);
            IsPng = isPng;
#endif
            _depth = bitDepth;
            HotspotX = hotspotX;
            HotspotY = hotspotY;
        }

        internal IconEntry(BitmapSource baseImage, BitmapSource alphaImage, IconBitDepth bitDepth, bool isPng)
            : this(baseImage, alphaImage, bitDepth, 0, 0, isPng)
        {
        }

#if DRAWING
        /// <summary>
        /// Returns a duplicate of the current instance.
        /// </summary>
        /// <returns>A duplicate of the current instance.</returns>
        public IconEntry Clone()
        {
            IconEntry entry = (IconEntry)MemberwiseClone();
            entry.File = null;
            entry._baseImage = (BitmapSource)_baseImage.Clone();
            if (_alphaImage != null)
                entry._alphaImage = (BitmapSource)_alphaImage.Clone();

            return entry;
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        #region Disposal
        private bool _isDisposed;
        /// <summary>
        /// Gets a value indicating whether the current instance has been disposed.
        /// </summary>
        public bool IsDisposed { get { return _isDisposed; } }

        /// <summary>
        /// Immediately releases all resources used by the current instance.
        /// </summary>
        public void Dispose()
        {
            if (_isDisposed) return;
            Dispose(true);
            _isDisposed = true;
            if (Disposed != null)
            {
                Disposed(this, EventArgs.Empty);
                Disposed = null;
            }
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (File != null)
                    File.Entries.Remove(this);

                if (_isQuantizedImage)
                    _baseImage.Dispose();

                if (_isQuantizedAlpha && _alphaImage != null)
                    _alphaImage.Dispose();
            }
        }

        /// <summary>
        /// Destructor.
        /// </summary>
        ~IconEntry()
        {
            Dispose(false);
        }

        /// <summary>
        /// Raised when the current instance is disposed.
        /// </summary>
        public event EventHandler Disposed;
        #endregion
#else
        #region Freezable
        /// <summary>
        /// Creates a new default <see cref="IconEntry"/> instance.
        /// </summary>
        /// <returns>A default <see cref="IconEntry"/> instance.</returns>
        /// <seealso cref="Freezable.CreateInstanceCore()"/>
        protected override Freezable CreateInstanceCore()
        {
            return new IconEntry((BitmapSource)BaseImageProperty.DefaultMetadata.DefaultValue, 0);
        }

        private void _copy(IconEntry sourceEntry)
        {
            _width = sourceEntry._width;
            _height = sourceEntry._height;
            _depth = sourceEntry._depth;
            SetValue(IsQuantizedPropertyKey, sourceEntry.IsQuantized);
        }

        /// <summary>
        /// Makes the current instance a deep copy of the specified other object.
        /// </summary>
        /// <param name="sourceFreezable">The object to clone.</param>
        protected override void CloneCore(Freezable sourceFreezable)
        {
            _copy((IconEntry)sourceFreezable);
            base.CloneCore(sourceFreezable);
        }

        /// <summary>
        /// Makes the current instance a deep copy of the specified other object's value.
        /// </summary>
        /// <param name="sourceFreezable">The object to clone.</param>
        protected override void CloneCurrentValueCore(Freezable sourceFreezable)
        {
            _copy((IconEntry)sourceFreezable);
            base.CloneCurrentValueCore(sourceFreezable);
        }

        /// <summary>
        /// Makes the current instance a frozen copy of the specified other object.
        /// </summary>
        /// <param name="sourceFreezable">The object to clone.</param>
        protected override void GetAsFrozenCore(Freezable sourceFreezable)
        {
            _copy((IconEntry)sourceFreezable);
            base.GetAsFrozenCore(sourceFreezable);
        }

        /// <summary>
        /// Makes the current instance a frozen copy of the specified other object's value.
        /// </summary>
        /// <param name="sourceFreezable">The object to clone.</param>
        protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
        {
            _copy((IconEntry)sourceFreezable);
            base.GetCurrentValueAsFrozenCore(sourceFreezable);
        }

        /// <summary>
        /// Returns a modifiable copy of the current instance.
        /// When copying this object's dependency properties, this method copies expressions (which might no longer resolve), but not animations or their current values.
        /// </summary>
        /// <returns>A modifiable copy of the current instance.</returns>
        /// <seealso cref="Freezable"/>
        /// <seealso cref="Freezable.Clone()"/>
        public new IconEntry Clone()
        {
            return (IconEntry)base.Clone();
        }

        /// <summary>
        /// Returns a modifiable copy of the current instance using its curent values.
        /// </summary>
        /// <returns>A modifiable copy of the current instance.</returns>
        /// <seealso cref="Freezable"/>
        /// <seealso cref="Freezable.CloneCurrentValue()"/>
        public new IconEntry CloneCurrentValue()
        {
            return (IconEntry)base.CloneCurrentValue();
        }

        /// <summary>
        /// Creates a frozen copy of the current instance, using base (non-animated) property values.
        /// </summary>
        /// <returns>A frozen copy of the current instance.</returns>
        /// <seealso cref="Freezable"/>
        /// <seealso cref="Freezable.GetAsFrozen()"/>
        public new IconEntry GetAsFrozen()
        {
            return (IconEntry)base.GetAsFrozen();
        }

        /// <summary>
        /// Creates a frozen copy of the current instance, using current property values.
        /// </summary>
        /// <returns>A frozen copy of the current instance.</returns>
        /// <seealso cref="Freezable"/>
        /// <seealso cref="Freezable.GetCurrentValueAsFrozen()"/>
        public new IconEntry GetCurrentValueAsFrozen()
        {
            return (IconEntry)base.GetCurrentValueAsFrozen();
        }
        #endregion
#endif
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
        /// <see cref="IconBitDepth.Depth32BitsPerPixel"/>; all entries with a width or height greater than this will be saved as PNG.
        /// 96 pixels.
        /// </summary>
        public const short MaxBmp32 = 96;
        /// <summary>
        /// Gets and sets the maximum width or height at which an icon entry will be saved as a BMP file when <see cref="BitDepth"/> is any value except
        /// <see cref="IconBitDepth.Depth32BitsPerPixel"/>; all entries with a width or height greater than this will be saved as PNG.
        /// 255 pixels.
        /// </summary>
        public const short MaxBmp = byte.MaxValue;

#if DRAWING
        internal static readonly Color[] AlphaPalette = new Color[] { Color.White, Color.Black };
#else
        internal static readonly BitmapPalette AlphaPalette = new BitmapPalette(new Color[] { Colors.White, Colors.Black });
#endif

        #region IsQuantized
#if DRAWING
        private bool _isQuantizedImage, _isQuantizedAlpha;
#else
        private static readonly DependencyPropertyKey IsQuantizedPropertyKey = DependencyProperty.RegisterReadOnly("IsQuantized", typeof(bool), typeof(IconEntry),
            new PropertyMetadata(false));
        /// <summary>
        /// The dependency property for the read-only <see cref="IsQuantized"/> property.
        /// </summary>
        public static readonly DependencyProperty IsQuantizedProperty = IsQuantizedPropertyKey.DependencyProperty;
#endif
        /// <summary>
        /// Gets a value indicating whether <see cref="BaseImage"/> and <see cref="AlphaImage"/> are both known to be already quantized.
        /// </summary>
        public bool IsQuantized
        {
#if DRAWING
            get { return _isQuantizedImage && _isQuantizedAlpha; }
#else
            get { return (bool)GetValue(IsQuantizedProperty); }
#endif
        }
        #endregion

        #region BaseImage
#if DRAWING
        private BitmapSource _baseImage;

        /// <summary>
        /// Gets and sets the image associated with the current instance.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// In a set operation, the specified value is <c>null</c>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The current instance is disposed.
        /// </exception>
        public BitmapSource BaseImage
        {
            get
            {
                if (_isDisposed) throw new ObjectDisposedException(null);
                return _baseImage;
            }
            set
            {
                if (_isDisposed) throw new ObjectDisposedException(null);
                if (value == null) throw new ArgumentNullException();
                if (_isQuantizedImage && !ReferenceEquals(value, _baseImage))
                {
                    _baseImage.Dispose();
                    _isQuantizedImage = false;
                }
                _baseImage = value;
            }
        }
#else
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
#endif
        #endregion

        #region AlphaImage
#if DRAWING
        private BitmapSource _alphaImage;

        /// <summary>
        /// Gets and sets an image to be used as the alpha mask, or <c>null</c> to derive the alpha mask from <see cref="BaseImage"/>.
        /// Black pixels are transparent; white pixels are opaque.
        /// </summary>
        /// <exception cref="ObjectDisposedException">
        /// The current image is disposed.
        /// </exception>
        public BitmapSource AlphaImage
        {
            get
            {
                if (_isDisposed) throw new ObjectDisposedException(null);
                return _alphaImage;
            }
            set
            {
                if (_isDisposed) throw new ObjectDisposedException(null);
                if (_isQuantizedAlpha && !ReferenceEquals(_alphaImage, value))
                {
                    if (_alphaImage != null) _alphaImage.Dispose();
                    _isQuantizedAlpha = false;
                }
                _alphaImage = value;
            }
        }
#else
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
#endif
        #endregion

        /// <summary>
        /// Gets a value indicating whether the current instance will be saved as a PNG image within the icon structure by default.
        /// </summary>
#if !DRAWING
        [Bindable(true, BindingDirection.OneWay)]
#endif
        public bool IsPngByDefault
        {
            get
            {
                if (_depth == IconBitDepth.Depth32BitsPerPixel)
                    return _width > MaxBmp32 || _height > MaxBmp32;
                return _width > MaxBmp || _height > MaxBmp;
            }
        }

#if DRAWING
        private bool _isPng;
        /// <summary>
        /// Gets and sets a value indicating whether the current instance will be saved as a PNG image.
        /// <c>true</c> is not recommended for <see cref="BitDepth"/> values other than <see cref="IconBitDepth.Depth32BitsPerPixel"/>, and <c>false</c>
        /// is not recommended for <see cref="Width"/> and <see cref="Height"/> greater than or equal to 256.
        /// </summary>
        /// <exception cref="ObjectDisposedException">
        /// In a set operation, the current instance is disposed.
        /// </exception>
        public bool IsPng
        {
            get { return _isPng; }
            set
            {
                if (_isDisposed) throw new ObjectDisposedException(null);
                _isPng = value;
            }
        }
#else
        /// <summary>
        /// Dependency property for the <see cref="IsPng"/> property.
        /// </summary>
        public static readonly DependencyProperty IsPngProperty = DependencyProperty.Register("IsPng", typeof(bool), typeof(IconEntry),
            new PropertyMetadata(false, null));

        /// <summary>
        /// Gets and sets a value indicating whether the current instance will be saved as a PNG image.
        /// A value of <c>true</c> is not recommended for <see cref="BitDepth"/> values other than <see cref="IconBitDepth.Depth32BitsPerPixel"/>, and <c>false</c>
        /// is not recommended for <see cref="Width"/> and <see cref="Height"/> greater than or equal to 256.
        /// </summary>
        public bool IsPng
        {
            get { return (bool)GetValue(IsPngProperty); }
            set { SetValue(IsPngProperty, value); }
        }
#endif

        /// <summary>
        /// Gets a key for the icon entry.
        /// </summary>
#if !DRAWING
        [Bindable(true, BindingDirection.OneWay)]
#endif
        public IconEntryKey EntryKey { get { return new IconEntryKey(_width, _height, _depth); } }

        #region Width
        private int _width;
        /// <summary>
        /// Gets the resampled width of the icon.
        /// </summary>
#if !DRAWING
        [Bindable(true, BindingDirection.OneWay)]
#endif
        public int Width
        {
            get { return _width; }
        }
        #endregion

        #region Height
        private int _height;
        /// <summary>
        /// Gets the resampled height of the icon.
        /// </summary>
#if !DRAWING
        [Bindable(true, BindingDirection.OneWay)]
#endif
        public int Height
        {
            get { return _height; }
        }
        #endregion

#if DRAWING
        /// <summary>
        /// Gets the size of the current instance.
        /// </summary>
        public Size Size { get { return new Size(_width, _height); } }
#endif

        #region BitDepth
        private IconBitDepth _depth;
        /// <summary>
        /// Gets the bit depth of the current instance.
        /// </summary>
#if !DRAWING
        [Bindable(true, BindingDirection.OneWay)]
#endif
        public IconBitDepth BitDepth
        {
            get { return _depth; }
        }
        #endregion

        #region AlphaThreshold
#if DRAWING
        private byte _alphaThreshold;

        /// <summary>
        /// Gets and sets a value indicating the threshold of alpha values at <see cref="BitDepth"/>s below <see cref="IconBitDepth.Depth32BitsPerPixel"/>.
        /// Alpha values less than this value will be fully transparent; alpha values greater than or equal to this value will be fully opaque.
        /// </summary>
        /// <exception cref="ObjectDisposedException">
        /// In a set operation, the current instance is disposed.
        /// </exception>
        public byte AlphaThreshold
        {
            get { return _alphaThreshold; }
            set
            {
                if (_isDisposed) throw new ObjectDisposedException(null);
                _alphaThreshold = value;
            }
        }
#else
        /// <summary>
        /// The dependency property for the <see cref="AlphaThreshold"/> property.
        /// </summary>
        public static readonly DependencyProperty AlphaThresholdProperty = DependencyProperty.Register("AlphaThreshold", typeof(byte), typeof(BitmapImage));

        /// <summary>
        /// Gets and sets a value indicating the threshold of alpha values at <see cref="BitDepth"/>s below <see cref="IconBitDepth.Depth32BitsPerPixel"/>.
        /// Alpha values less than this value will be fully transparent; alpha values greater than or equal to this value will be fully opaque.
        /// </summary>
        public byte AlphaThreshold
        {
            get { return (byte)GetValue(AlphaThresholdProperty); }
            set { SetValue(AlphaThresholdProperty, value); }
        }
#endif
        #endregion

        #region HotspotX
#if DRAWING
        private int _hotspotX;

        /// <summary>
        /// In a cursor, gets the horizontal offset in pixels of the cursor's hotspot from the left side.
        /// Constrained to greater than or equal to 0 and less than or equal to <see cref="Width"/>.
        /// </summary>
        /// <exception cref="ObjectDisposedException">
        /// In a set operation, the current instance is disposed.
        /// </exception>
        public int HotspotX
        {
            get { return _hotspotX; }
            set
            {
                if (_isDisposed) throw new ObjectDisposedException(null);
                if (value < 0) _hotspotX = 0;
                else if (value > _width) _hotspotX = _width;
                else _hotspotX = value;
            }
        }
#else
        /// <summary>
        /// The dependency property for the <see cref="HotspotX"/> property.
        /// </summary>
        public static readonly DependencyProperty HotspotXProperty = DependencyProperty.Register("HotspotX", typeof(int), typeof(IconEntry),
            new PropertyMetadata(0, null, HotspotXCoerce));

        private static object HotspotXCoerce(DependencyObject d, object baseValue)
        {
            IconEntry i = (IconEntry)d;

            int value = (int)baseValue;
            if (value < 0) return 0;
            if (value > i._width) return i._width;

            return value;
        }

        /// <summary>
        /// In a cursor, gets the horizontal offset in pixels of the cursor's hotspot from the left side.
        /// Constrained to greater than or equal to 0 and less than or equal to <see cref="Width"/>.
        /// </summary>
        public int HotspotX
        {
            get { return (int)GetValue(HotspotXProperty); }
            set { SetValue(HotspotXProperty, value); }
        }
#endif
        #endregion

        #region HotspotY
#if DRAWING
        private int _hotspotY;

        /// <summary>
        /// In a cursor, gets the vertical offset in pixels of the cursor's hotspot from the top side.
        /// Constrained to greater than or equal to 0 and less than or equal to <see cref="Height"/>.
        /// </summary>
        /// <exception cref="ObjectDisposedException">
        /// In a set operation, the current instance is disposed.
        /// </exception>
        public int HotspotY
        {
            get { return _hotspotY; }
            set
            {
                if (_isDisposed) throw new ObjectDisposedException(null);
                if (value < 0) _hotspotY = 0;
                else if (value > _height) _hotspotY = _height;
                else _hotspotY = value;
            }
        }
#else
        /// <summary>
        /// The dependency property for the <see cref="HotspotY"/> property.
        /// </summary>
        public static readonly DependencyProperty HotspotYProperty = DependencyProperty.Register("HotspotY", typeof(int), typeof(IconEntry),
            new PropertyMetadata(0, null, HotspotYCoerce));

        private static object HotspotYCoerce(DependencyObject d, object baseValue)
        {
            IconEntry e = (IconEntry)d;

            int value = (int)baseValue;
            if (value < 0) return 0;
            if (value > e._height) return e._height;

            return value;
        }
        /// <summary>
        /// In a cursor, gets the vertical offset in pixels of the cursor's hotspot from the top side.
        /// Constrained to greater than or equal to 0 and less than or equal to <see cref="Height"/>.
        /// </summary>
        public int HotspotY
        {
            get { return (int)GetValue(HotspotYProperty); }
            set { SetValue(HotspotYProperty, value); }
        }
#endif
        #endregion

        #region ScalingFilter
#if DRAWING
        private IconScalingFilter _scalingFilter;

        private static bool ScalingFilterValidate(IconScalingFilter value)
        {
            switch (value)
            {
#else
        /// <summary>
        /// The dependency property for the <see cref="IconScalingFilter"/> property.
        /// </summary>
        public static readonly DependencyProperty IconScalingFilterProperty = DependencyProperty.Register("ScalingFilter", typeof(IconScalingFilter), typeof(IconEntry),
            new PropertyMetadata(IconScalingFilter.Matrix), ScalingFilterValidate);
        private static bool ScalingFilterValidate(object value)
        {
            switch ((IconScalingFilter)value)
            {
                case IconScalingFilter.Matrix:
#endif
                case IconScalingFilter.Bicubic:
                case IconScalingFilter.Bilinear:
                case IconScalingFilter.HighQualityBicubic:
                case IconScalingFilter.HighQualityBilinear:
                case IconScalingFilter.NearestNeighbor:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Gets and sets the scaling mode used to resize <see cref="BaseImage"/> and <see cref="AlphaImage"/> when quantizing.
        /// </summary>
        /// <exception cref="InvalidEnumArgumentException">
        /// In a set operation, the specified value is not a valid <see cref="IconScalingFilter"/> value.
        /// </exception>
        public IconScalingFilter ScalingFilter
        {
#if DRAWING
            get { return _scalingFilter; }
#else
            get { return (IconScalingFilter)GetValue(IconScalingFilterProperty); }
#endif
            set
            {
#if DRAWING
                if (_isDisposed) throw new ObjectDisposedException(null);
#endif
                if (!ScalingFilterValidate(value))
                    throw new InvalidEnumArgumentException(null, (int)value, typeof(IconScalingFilter));
#if DRAWING
                _scalingFilter = value;
#else
                SetValue(IconScalingFilterProperty, value);
#endif
            }
        }
        #endregion

        /// <summary>
        /// Gets the number of bits per pixel specified by <see cref="BitDepth"/>.
        /// </summary>
        [Bindable(true, BindingDirection.OneWay)]
        public int BitsPerPixel
        {
            get { return GetBitsPerPixel(_depth); }
        }

        /// <summary>
        /// Returns the number of bits per pixel associated with the specified <see cref="IconBitDepth"/> value.
        /// </summary>
        /// <param name="bitDepth">The <see cref="IconBitDepth"/> to check.</param>
        /// <returns>1 for <see cref="IconBitDepth.Depth1BitsPerPixel"/>; 4 for <see cref="IconBitDepth.Depth4BitsPerPixel"/>;
        /// 8 for <see cref="IconBitDepth.Depth8BitsPerPixel"/>; 24 for <see cref="IconBitDepth.Depth24BitsPerPixel"/>; or
        /// 32 for <see cref="IconBitDepth.Depth32BitsPerPixel"/>.</returns>
        /// <exception cref="InvalidEnumArgumentException">
        /// <paramref name="bitDepth"/> is not a valid <see cref="IconBitDepth"/> value.
        /// </exception>
        public static int GetBitsPerPixel(IconBitDepth bitDepth)
        {
            switch (bitDepth)
            {
                case IconBitDepth.Depth1BitPerPixel:
                    return 1;
                case IconBitDepth.Depth4BitsPerPixel:
                    return 4;
                case IconBitDepth.Depth8BitsPerPixel:
                    return 8;
                case IconBitDepth.Depth24BitsPerPixel:
                    return 24;
                case IconBitDepth.Depth32BitsPerPixel:
                    return 32;
                default:
                    throw new InvalidEnumArgumentException("bitDepth", (int)bitDepth, typeof(IconBitDepth));
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
        /// Gets the maximum color count associated with the specified <see cref="IconBitDepth"/>.
        /// </summary>
        /// <param name="bitDepth">The <see cref="IconBitDepth"/> to check.</param>
        /// <returns>21 for <see cref="IconBitDepth.Depth2Color"/>; 16 for <see cref="IconBitDepth.Depth16Color"/>;
        /// 256 for <see cref="IconBitDepth.Depth256Color"/>; 16777216 for <see cref="IconBitDepth.Depth24BitsPerPixel"/>; or
        /// 4294967296 for <see cref="IconBitDepth.Depth32BitsPerPixel"/>.</returns>
        /// <exception cref="InvalidEnumArgumentException">
        /// <paramref name="bitDepth"/> is not a valid <see cref="IconBitDepth"/> value.
        /// </exception>
        public static long GetColorCount(IconBitDepth bitDepth)
        {
            switch (bitDepth)
            {
                case IconBitDepth.Depth2Color:
                    return 2;
                case IconBitDepth.Depth16Color:
                    return 16;
                case IconBitDepth.Depth256Color:
                    return 256;
                case IconBitDepth.Depth24BitsPerPixel:
                    return 0x1000000;
                case IconBitDepth.Depth32BitsPerPixel:
                    return uint.MaxValue + 1L;
                default:
                    throw new InvalidEnumArgumentException("bitDepth", (int)bitDepth, typeof(IconBitDepth));
            }
        }

        /// <summary>
        /// Returns the <see cref="IconBitDepth"/> associated with the specified numeric value.
        /// </summary>
        /// <param name="value">The color count or number of bits per pixel to use.</param>
        /// <returns><see cref="IconBitDepth.Depth1BitPerPixel"/> if <paramref name="value"/> is 1 or 2;
        /// <see cref="IconBitDepth.Depth4BitsPerPixel"/> if <paramref name="value"/> is 4 or 16;
        /// <see cref="IconBitDepth.Depth8BitsPerPixel"/> if <paramref name="value"/> is 8 or 256;
        /// <see cref="IconBitDepth.Depth24BitsPerPixel"/> if <paramref name="value"/> is 24 or 16777216; or
        /// <see cref="IconBitDepth.Depth32BitsPerPixel"/> if <paramref name="value"/> is 32 or 4294967296.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="value"/> is not one of the specified parameter values.
        /// </exception>
        public static IconBitDepth GetBitDepth(long value)
        {
            switch (value)
            {
                case 1:
                case 2:
                    return IconBitDepth.Depth1BitPerPixel;
                case 4:
                case 16:
                    return IconBitDepth.Depth4BitsPerPixel;
                case 8:
                case 256:
                    return IconBitDepth.Depth8BitsPerPixel;
                case 24:
                case 0x1000000:
                    return IconBitDepth.Depth24BitsPerPixel;
                case 32:
                case uint.MaxValue + 1L:
                    return IconBitDepth.Depth32BitsPerPixel;
                default:
                    throw new ArgumentException("Not a valid color count or bits-per-pixel value.", "value");
            }
        }

#if DRAWING
        /// <summary>
        /// Returns the <see cref="IconBitDepth"/> associated with the specified <see cref="PixelFormat"/> value.
        /// </summary>
        /// <param name="pFormat">A <see cref="PixelFormat"/> from which to derive the <see cref="IconBitDepth"/> value.</param>
        /// <returns>The <see cref="IconBitDepth"/> associated with <paramref name="pFormat"/>, based on <see cref="BitmapSource.GetPixelFormatSize(PixelFormat)"/>;
        /// <see cref="IconBitDepth.Depth32BitsPerPixel"/> if the number of bits per pixel is not 1, 4, 8, 24, or 32; or
        /// <see cref="IconBitDepth.Depth24BitsPerPixel"/> if the pixel format is <see cref="PixelFormat.Format32bppRgb"/>
        /// (because only RGB data is stored in this pixel format, just as with <see cref="PixelFormat.Format24bppRgb"/>).</returns>
        public static IconBitDepth GetBitDepth(PixelFormat pFormat)
#else
        /// <summary>
        /// Returns the <see cref="IconBitDepth"/> associated with the specified <see cref="PixelFormat"/> value.
        /// </summary>
        /// <param name="pFormat">A <see cref="PixelFormat"/> from which to derive the <see cref="IconBitDepth"/> value.</param>
        /// <returns>The <see cref="IconBitDepth"/> associated with <paramref name="pFormat"/>, based on <see cref="PixelFormat.BitsPerPixel"/>;
        /// <see cref="IconBitDepth.Depth32BitsPerPixel"/> if the number of bits per pixel is not 1, 4, 8, 24, or 32; or
        /// <see cref="IconBitDepth.Depth24BitsPerPixel"/> if the pixel format is <see cref="PixelFormats.Bgr32"/>
        /// (because only RGB data is stored in this pixel format, just as with <see cref="PixelFormats.Bgr24"/>).</returns>
        public static IconBitDepth GetBitDepth(PixelFormat pFormat)
#endif
        {
#if DRAWING
            if (pFormat == PixelFormat.Format32bppRgb)
#else
            if (pFormat == PixelFormats.Bgr32)
#endif
                return IconBitDepth.Depth24BitsPerPixel;

            int bpp =
#if DRAWING
                BitmapSource.GetPixelFormatSize(pFormat);
#else
                pFormat.BitsPerPixel;
#endif
            switch (bpp)
            {
                case 1:
                    return IconBitDepth.Depth1BitPerPixel;
                case 4:
                    return IconBitDepth.Depth4BitsPerPixel;
                case 8:
                    return IconBitDepth.Depth8BitsPerPixel;
                case 24:
                    return IconBitDepth.Depth24BitsPerPixel;
                default:
                    return IconBitDepth.Depth32BitsPerPixel;
            }
        }

        /// <summary>
        /// Returns the <see cref="PixelFormat"/> associated with the specified <see cref="IconBitDepth"/>.
        /// </summary>
        /// <param name="depth">The bit depth from which to get the pixel format.</param>
        /// <returns>The pixel format associated with <paramref name="depth"/>.</returns>
        /// <exception cref="InvalidEnumArgumentException">
        /// <paramref name="depth"/> is not a valid <see cref="IconBitDepth"/> value.
        /// </exception>
        public static PixelFormat GetPixelFormat(IconBitDepth depth)
        {
            switch (depth)
            {
                case IconBitDepth.Depth1BitPerPixel:
#if DRAWING
                    return PixelFormat.Format1bppIndexed;
#else
                    return PixelFormats.Indexed1;
#endif
                case IconBitDepth.Depth4BitsPerPixel:
#if DRAWING
                    return PixelFormat.Format4bppIndexed;
#else
                    return PixelFormats.Indexed4;
#endif
                case IconBitDepth.Depth8BitsPerPixel:
#if DRAWING
                    return PixelFormat.Format8bppIndexed;
#else
                    return PixelFormats.Indexed8;
#endif
                case IconBitDepth.Depth24BitsPerPixel:
#if DRAWING
                    return PixelFormat.Format24bppRgb;
#else
                    return PixelFormats.Bgr24;
#endif
                case IconBitDepth.Depth32BitsPerPixel:
#if DRAWING
                    return PixelFormat.Format32bppArgb;
#else
                    return PixelFormats.Bgra32;
#endif
                default:
                    throw new InvalidEnumArgumentException("depth", (int)depth, typeof(IconBitDepth));
            }
        }

        internal IconFileBase File;

        #region GetQuantized
        private void SetScalingFilter(Graphics g)
        {
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
#if DRAWING
            switch (_scalingFilter)
#else
            switch (ScalingFilter)
#endif
            {
                case IconScalingFilter.Bicubic:
                    g.InterpolationMode = InterpolationMode.Bicubic;
                    break;
                case IconScalingFilter.Bilinear:
                    g.InterpolationMode = InterpolationMode.Bilinear;
                    break;
                case IconScalingFilter.HighQualityBicubic:
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    break;
                case IconScalingFilter.HighQualityBilinear:
                    g.InterpolationMode = InterpolationMode.HighQualityBilinear;
                    break;
                case IconScalingFilter.NearestNeighbor:
                    g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    break;
            }
        }

        #region ColorValue
        [StructLayout(LayoutKind.Explicit)]
        private struct ColorValue : IEquatable<ColorValue>
        {
            public ColorValue(int value)
            {
                this = new ColorValue() { Value = value };
            }

            public ColorValue(byte a, byte r, byte g, byte b)
            {
                this = new ColorValue() { A = a, R = r, G = g, B = b };
            }

            public ColorValue(byte r, byte g, byte b)
                : this(byte.MaxValue, r, g, b)
            {
            }

            public ColorValue(Color value)
            {
#if DRAWING
                this = new ColorValue() { Value = value.ToArgb() };
#else
                this = new ColorValue() { A = value.A, R = value.R, G = value.G, B = value.B };
#endif
            }

            [FieldOffset(0)]
            public int Value;

            [FieldOffset(0)]
            public byte B;
            [FieldOffset(1)]
            public byte G;
            [FieldOffset(2)]
            public byte R;
            [FieldOffset(3)]
            public byte A;

            public static readonly ColorValue White = new ColorValue(~0), Black = new ColorValue(~0xFFFFFF);

            public double GetDistance(ColorValue other)
            {
                if (Value == other.Value) return 0;

                double distance = Math.Abs(A - other.A) << 8;
                distance *= distance;
                double r = Math.Abs(R - other.R);
                distance += r * r;
                double g = Math.Abs(G - other.G);
                distance += g * g;
                double b = Math.Abs(B - other.B);
                return distance = b * b;
            }

            public byte IndexOfMinDist(Color[] colors)
            {
                int minDistDex = -1;
                double minDistance = double.MaxValue;

                for (int i = 0; i < colors.Length; i++)
                {
                    double newDistance = GetDistance(new ColorValue(colors[i]));
                    if (newDistance == 0) return (byte)i;
                    if (newDistance < minDistance)
                    {
                        minDistance = newDistance;
                        minDistDex = i;
                    }
                }
                return (byte)minDistDex;
            }

            public override string ToString()
            {
                return string.Format("#{0:x8}: A:{1} R:{2} B:{3} G:{4}", Value, A, R, B, G);
            }

            public override int GetHashCode()
            {
                return Value;
            }

            public Color GetColor()
            {
                return Color.FromArgb(A, R, G, B);
            }

            public bool Equals(ColorValue other)
            {
                return Value == other.Value;
            }
        }
        #endregion

#if DRAWING
        /// <summary>
        /// Applies <see cref="AlphaImage"/> to <see cref="BaseImage"/>.
        /// </summary>
        /// <returns>A new <see cref="Bitmap"/>, sized according to <see cref="Width"/> and <see cref="Height"/>, consisting of
        /// <see cref="AlphaImage"/> applied to <see cref="BaseImage"/> and with a 32-bit pixel format.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The current instance is disposed.
        /// </exception>
        public Bitmap GetCombinedAlpha()
        {
            if (_isDisposed) throw new ObjectDisposedException(null);

            Bitmap alphaMask, quantized = GetQuantized(true, IconBitDepth.Depth32BitsPerPixel, out alphaMask);

            if (alphaMask != null) alphaMask.Dispose();

            return quantized;
        }
#else
        /// <summary>
        /// Applies <see cref="AlphaImage"/> to <see cref="BaseImage"/>.
        /// </summary>
        /// <returns>A new <see cref="BitmapSource"/>, sized according to <see cref="Width"/> and <see cref="Height"/>, consisting of
        /// <see cref="AlphaImage"/> applied to <see cref="BaseImage"/> and with a 32-bit pixel format.</returns>
        public BitmapSource GetCombinedAlpha()
        {
            BitmapSource alphaMask;

            return GetQuantized(true, IconBitDepth.Depth32BitsPerPixel, out alphaMask);
        }
#endif

#if DRAWING
        /// <summary>
        /// Sets <see cref="BaseImage"/> and <see cref="AlphaImage"/> equal to their quantized equivalent,
        /// in a form indicated by the specified value.
        /// </summary>
        /// <param name="isPng">If <c>true</c>, <see cref="AlphaImage"/> will be set <c>null</c> and <see cref="BaseImage"/> will be quantized
        /// as if it was a PNG icon entry. If <c>false</c>, <see cref="BaseImage"/> and <see cref="AlphaImage"/> will be quantized
        /// as if for a BMP entry.</param>
        /// <exception cref="ObjectDisposedException">
        /// The current instance is disposed.
        /// </exception>
        public void SetQuantized(bool isPng)
        {
            Bitmap alphaMask, baseImage = GetQuantized(isPng, out alphaMask);

            BaseImage = baseImage;
            AlphaImage = alphaMask;
            _isQuantizedImage = _isQuantizedAlpha = true;
        }
#else
        /// <summary>
        /// Sets <see cref="BaseImage"/> and <see cref="AlphaImage"/> equal to their quantized equivalent,
        /// in a form indicated by the specified value.
        /// </summary>
        /// <param name="isPng">If <c>true</c>, <see cref="AlphaImage"/> will be set <c>null</c> and <see cref="BaseImage"/> will be quantized
        /// as if it was a PNG icon entry. If <c>false</c>, <see cref="BaseImage"/> and <see cref="AlphaImage"/> will be quantized
        /// as if for a BMP entry.</param>
        /// <exception cref="InvalidOperationException">
        /// The current instance is frozen.
        /// </exception>
        public void SetQuantized(bool isPng)
        {
            if (IsFrozen) throw new InvalidOperationException("The current instance is frozen.");
            BitmapSource alphaMask, baseImage = GetQuantized(isPng, out alphaMask);
            BaseImage = baseImage;
            AlphaImage = alphaMask;
            SetValue(IsQuantizedPropertyKey, true);
        }
#endif

#if DRAWING
        /// <summary>
        /// Sets <see cref="BaseImage"/> and <see cref="AlphaImage"/> equal to their quantized equivalent,
        /// in a form indicated by <see cref="IsPng"/>.
        /// </summary>
        /// <remarks>
        /// Performs the same action as <see cref="SetQuantized(bool)"/>, with <see cref="IsPng"/> passed as the parameter.
        /// </remarks>
        /// <exception cref="ObjectDisposedException">
        /// The current instance is disposed.
        /// </exception>
        public void SetQuantized()
#else
        /// <summary>
        /// Sets <see cref="BaseImage"/> and <see cref="AlphaImage"/> equal to their quantized equivalent,
        /// in a form indicated by <see cref="IsPng"/>.
        /// </summary>
        /// <remarks>
        /// Performs the same action as <see cref="SetQuantized(bool)"/>, with <see cref="IsPng"/> passed as the parameter.
        /// </remarks>
        /// <exception cref="InvalidOperationException">
        /// The current instance is frozen.
        /// </exception>
        public void SetQuantized()
#endif
        {
            SetQuantized(IsPng);
        }

#if DRAWING
        /// <summary>
        /// Returns color quantization of the current instance as it would appear for a PNG entry.
        /// </summary>
        /// <returns>A <see cref="Bitmap"/> containing the quantized image.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The current instance is disposed.
        /// </exception>
        /// <remarks>
        /// If <see cref="BaseImage"/> is already quantized and <see cref="AlphaImage"/> is <c>null</c>, this method returns a clone of
        /// <see cref="BaseImage"/> which must be disposed when you're done with it.
        /// </remarks>
        public Bitmap GetQuantizedPng()
        {
            Bitmap alphaMask, quantized = GetQuantized(true, out alphaMask);
            if (alphaMask != null) alphaMask.Dispose();
            return quantized;
        }
#else
        /// <summary>
        /// Returns color quantization of the current instance as it would appear for a PNG entry.
        /// </summary>
        /// <returns>A <see cref="BitmapSource"/> containing the quantized image.</returns>
        public BitmapSource GetQuantizedPng()
        {
            BitmapSource alphaMask;
            return GetQuantized(true, out alphaMask);
        }
#endif

#if DRAWING
        /// <summary>
        /// Returns color quantization of the current instance as it would appear for a BMP entry.
        /// </summary>
        /// <param name="alphaMask">When this method returns, contains the quantized alpha mask generated using <see cref="AlphaThreshold"/>.
        /// Black pixels are transparent; white pixels are opaque.
        /// This parameter is passed uninitialized.</param>
        /// <returns>A <see cref="Bitmap"/> containing the quantized image without the alpha mask.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The current instance is disposed.
        /// </exception>
        /// <remarks>
        /// If <see cref="BaseImage"/> and <see cref="AlphaImage"/> are already quantized, this method returns clones of both objects
        /// which must be disposed after you're done with them.
        /// </remarks>
        public Bitmap GetQuantized(out Bitmap alphaMask)
#else
        /// <summary>
        /// Returns color quantization of the current instance as it would appear for a BMP entry.
        /// </summary>
        /// <param name="alphaMask">When this method returns, contains the quantized alpha mask generated using <see cref="AlphaThreshold"/>.
        /// Black pixels are transparent; white pixels are opaque.
        /// This parameter is passed uninitialized.</param>
        /// <returns>A <see cref="BitmapSource"/> containing the quantized image without the alpha mask.</returns>
        public BitmapSource GetQuantized(out BitmapSource alphaMask)
#endif
        {
            return GetQuantized(false, out alphaMask);
        }

#if DRAWING
        internal Bitmap GetQuantized(bool isPng, out Bitmap alphaMask)
#else
        internal WriteableBitmap GetQuantized(bool isPng, out BitmapSource alphaMask)
#endif
        {
            return GetQuantized(isPng, _depth, out alphaMask);
        }

#if DRAWING
        internal unsafe Bitmap GetQuantized(bool isPng, IconBitDepth _depth, out Bitmap alphaMask)
        {
            if (_isDisposed) throw new ObjectDisposedException(null);
#else
        private WriteableBitmap GetQuantized(bool isPng, IconBitDepth _depth, out BitmapSource alphaMask)
        {
            BitmapSource _alphaImage = AlphaImage;
#endif

            if (IsQuantized && isPng == (_alphaImage == null))
            {
                if (_alphaImage == null)
                    alphaMask = null;
                else
#if DRAWING
                    alphaMask = (Bitmap)_alphaImage.Clone();
                return (Bitmap)_baseImage.Clone();
#else
                {
                    alphaMask = new WriteableBitmap(_alphaImage);
                    alphaMask.Freeze();
                }
                var resultImage = new WriteableBitmap(BaseImage);
                resultImage.Freeze();
                return resultImage;
#endif
            }

            ColorValue[] pixels =
#if DRAWING
                _getPixels(_baseImage);
#else
                _getPixels(BaseImage);
            byte _alphaThreshold = AlphaThreshold;
#endif

            if (isPng)
            {
                alphaMask = null;
                if (_alphaImage != null && _depth == IconBitDepth.Depth32BitsPerPixel && this._depth != IconBitDepth.Depth32BitsPerPixel)
                {
                    ColorValue[] alphaPixels = _getPixels(_alphaImage);

                    for (int i = 0; i < alphaPixels.Length; i++)
                    {
                        ColorValue curAlpha = alphaPixels[i];
                        double whiteDistance = curAlpha.GetDistance(ColorValue.White);
                        if (curAlpha.GetDistance(ColorValue.Black) < whiteDistance || curAlpha.GetDistance(default(ColorValue)) < whiteDistance)
                            pixels[i].A = 0;
                    }
                }
            }
            else
            {
                ColorValue[] alphaPixels;
                if (_alphaImage == null)
                {
                    alphaPixels = new ColorValue[pixels.Length];

                    for (int i = 0; i < pixels.Length; i++)
                    {
                        ColorValue curVal = pixels[i];
                        if (curVal.A < _alphaThreshold)
                            alphaPixels[i] = ColorValue.Black;
                        else
                            alphaPixels[i] = ColorValue.White;
                    }
                }
                else
                {
                    alphaPixels = _getPixels(_alphaImage);

                    for (int i = 0; i < alphaPixels.Length; i++)
                    {
                        ColorValue curAlpha = alphaPixels[i];
                        double whiteDistance = curAlpha.GetDistance(ColorValue.White);
                        if (curAlpha.GetDistance(ColorValue.Black) < whiteDistance || curAlpha.GetDistance(default(ColorValue)) < whiteDistance)
                            alphaPixels[i] = ColorValue.Black;
                        else
                            alphaPixels[i] = ColorValue.White;
                    }
                }

#if DRAWING
                alphaMask = GetBitmap(alphaPixels, PixelFormat.Format1bppIndexed, AlphaPalette, 2);
#else
                alphaMask = GetBitmap(alphaPixels, PixelFormats.Indexed1, AlphaPalette, 2);
                alphaMask.Freeze();
#endif
            }

            if (_depth == IconBitDepth.Depth32BitsPerPixel)
#if DRAWING
                return GetBitmap(pixels);
#else
            {
                var bmpResult = GetBitmap(pixels);
                bmpResult.Freeze();
                return bmpResult;
            }
#endif

            for (int i = 0; i < pixels.Length; i++)
            {
                ColorValue curPixel = pixels[i];
                if (curPixel.A < _alphaThreshold)
                {
                    if (isPng)
                        curPixel.A = 0;
                    else
                        curPixel = ColorValue.Black;
                }
                else curPixel.A = byte.MaxValue;

                pixels[i] = curPixel;
            }

            var quantized = GetBitmap(pixels, isPng ?
#if DRAWING
                PixelFormat.Format8bppIndexed
#else
                PixelFormats.Indexed8
#endif
                : GetPixelFormat(_depth), null, (ushort)GetColorCount(_depth));

            if (!isPng)
            {
#if !DRAWING
                quantized.Freeze();
#endif
                return quantized;
            }

            ColorValue[] otherPixels = _getPixels(quantized);

#if DRAWING
            quantized.Dispose();
#endif

            for (int i = 0; i < otherPixels.Length; i++)
            {
                var curPixel = pixels[i];
                var otherPixel = otherPixels[i];
                curPixel.R = otherPixel.R;
                curPixel.G = otherPixel.G;
                curPixel.B = otherPixel.B;
                pixels[i] = curPixel;
            }

#if DRAWING
            return GetBitmap(pixels);
#else
            quantized = GetBitmap(pixels);
            quantized.Freeze();
            return quantized;
#endif
        }

        private ColorValue[] _getPixels(BitmapSource image)
#if DRAWING
        {
            using (Bitmap bmp = new Bitmap(_width, _height, PixelFormat.Format32bppArgb))
            using (Graphics g = Graphics.FromImage(bmp))
            {
                SetScalingFilter(g);
                g.DrawImage(image, 0, 0, _width, _height);

                BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

                ColorValue[] pixels = new ColorValue[bmp.Width * bmp.Height];

                unsafe
                {
                    ColorValue* pColors = (ColorValue*)bmpData.Scan0;

                    for (int i = 0; i < pixels.Length; i++)
                        pixels[i] = pColors[i];
                }

                bmp.UnlockBits(bmpData);

                return pixels;
            }
        }
#else
        {
            ColorValue[] pixels = new ColorValue[_width * _height];

            Int32Rect fullRect = new Int32Rect(0, 0, _width, _height);

            FormatConvertedBitmap formatBmp = new FormatConvertedBitmap(image, PixelFormats.Bgra32, null, 0);
            if (formatBmp.PixelWidth == _width && formatBmp.PixelHeight == _height)
            {
                unsafe
                {
                    fixed (ColorValue* pPixels = pixels)
                        formatBmp.CopyPixels(fullRect, (IntPtr)pPixels, pixels.Length * sizeof(ColorValue), _width * sizeof(ColorValue));
                }
                return pixels;
            }
            else if (ScalingFilter == IconScalingFilter.Matrix)
            {
                TransformedBitmap transBmp = new TransformedBitmap(formatBmp, new ScaleTransform((double)_width / formatBmp.PixelWidth,
                    (double)_height / formatBmp.PixelHeight));
                unsafe
                {
                    fixed (ColorValue* pPixels = pixels)
                        transBmp.CopyPixels(fullRect, (IntPtr)pPixels, pixels.Length * sizeof(ColorValue), _width * sizeof(ColorValue));
                }
                return pixels;
            }

            using (Bitmap dBitmap = new Bitmap(image.PixelWidth, image.PixelHeight, DPixelFormat.Format32bppArgb))
            {
                BitmapData dData = dBitmap.LockBits(new Rectangle(0, 0, image.PixelWidth, image.PixelHeight),
                    ImageLockMode.WriteOnly, DPixelFormat.Format32bppArgb);

                image.CopyPixels(new Int32Rect(0, 0, image.PixelWidth, image.PixelHeight), dData.Scan0, image.PixelHeight * dData.Stride, dData.Stride);

                dBitmap.UnlockBits(dData);

                using (Bitmap sizeBmp = new Bitmap(_width, _height, DPixelFormat.Format32bppArgb))
                {
                    using (Graphics g = Graphics.FromImage(sizeBmp))
                    {
                        SetScalingFilter(g);

                        g.DrawImage(dBitmap, 0, 0, _width, _height);
                    }

                    BitmapData sizeData = sizeBmp.LockBits(new Rectangle(0, 0, _width, _height), ImageLockMode.ReadOnly, DPixelFormat.Format32bppArgb);

                    unsafe
                    {
                        ColorValue* pData = (ColorValue*)sizeData.Scan0;
                        for (int i = 0; i < pixels.Length; i++)
                            pixels[i] = pData[i];
                    }

                    sizeBmp.UnlockBits(sizeData);
                }
            }
            return pixels;
        }
#endif

#if DRAWING
        private Bitmap GetBitmap(ColorValue[] pixels, PixelFormat pFormat, Color[] palette, ushort maxColors)
        {
            var fullRect = new Rectangle(0, 0, _width, _height);
            if (pFormat == PixelFormat.Format24bppRgb || pFormat == PixelFormat.Format32bppArgb || palette == null)
            {
                Bitmap baseBmp;

                unsafe
                {
                    fixed (ColorValue* pVals = pixels)
                        baseBmp = new Bitmap(_width, _height, _width * sizeof(int), PixelFormat.Format32bppArgb, (IntPtr)pVals);
                }

                if (pFormat == PixelFormat.Format32bppArgb) return baseBmp;
                Bitmap returner;
                if (pFormat == PixelFormat.Format24bppRgb)
                    returner = baseBmp.Clone(fullRect, PixelFormat.Format24bppRgb);
                else
                {
                    switch (pFormat)
                    {
                        case PixelFormat.Format1bppIndexed:
                            maxColors = Math.Min((ushort)2, maxColors);
                            break;
                        case PixelFormat.Format4bppIndexed:
                            maxColors = Math.Min((ushort)16, maxColors);
                            break;
                        default:
                            maxColors = Math.Min((ushort)256, maxColors);
                            break;
                    }

                    WuQuantizer quant = new WuQuantizer();

                    returner = GetSmaller(Quantize(baseBmp, pixels, maxColors), pFormat);
                }
                baseBmp.Dispose();

                return returner;
            }

            byte[] indices = new byte[pixels.Length];
            Dictionary<ColorValue, byte> _getVals = new Dictionary<ColorValue, byte>();
            for (int i = 0; i < indices.Length; i++)
            {
                var curPixel = pixels[i];
                byte curDex;
                if (_getVals.TryGetValue(curPixel, out curDex))
                {
                    indices[i] = curDex;
                    continue;
                }

                _getVals[curPixel] = indices[i] = curDex = curPixel.IndexOfMinDist(palette);
            }

            Bitmap bmp8;
            unsafe
            {
                fixed (byte* pVals = indices)
                    bmp8 = new Bitmap(_width, _height, _width, PixelFormat.Format8bppIndexed, (IntPtr)pVals);
            }

            var bmp8Palette = bmp8.Palette;
            for (int i = 0; i < palette.Length; i++)
                bmp8Palette.Entries[i] = palette[i];

            for (int i = palette.Length; i < bmp8Palette.Entries.Length; i++)
                bmp8Palette.Entries[i] = default(Color);
            bmp8.Palette = bmp8Palette;

            return GetSmaller(bmp8, pFormat);
        }

        private Bitmap GetBitmap(ColorValue[] pixels)
        {
            return GetBitmap(pixels, PixelFormat.Format32bppArgb, null, ushort.MaxValue);
        }

        private Bitmap GetSmaller(Bitmap source, PixelFormat pFormat)
        {
            if (pFormat == PixelFormat.Format8bppIndexed)
                return source;

            Rectangle fullRect = new Rectangle(0, 0, _width, _height);

            int colorsPerByte, resultStride, shift, top;

            if (pFormat == PixelFormat.Format1bppIndexed)
            {
                colorsPerByte = 8;
                resultStride = (_width + 7) >> 3;
                shift = 3;
                top = 7;
            }
            else
            {
                colorsPerByte = 2;
                resultStride = (_width + 1) >> 1;
                shift = 1;
                top = 4;
            }

            Dictionary<byte, byte> indices = new Dictionary<byte, byte>();
            List<Color> colorList = new List<Color>();
            var sourceEntries = source.Palette.Entries;

            Bitmap result = new Bitmap(_width, _height, pFormat);

            BitmapData sourceData = source.LockBits(fullRect, ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);
            BitmapData resultData = result.LockBits(fullRect, ImageLockMode.WriteOnly, pFormat);

            {
                int sourceDataStride = sourceData.Stride;
                int resultDataStride = resultData.Stride;
                byte[] pSource = new byte[_width * sourceDataStride], pResult = new byte[_height * resultDataStride];
                Marshal.Copy(sourceData.Scan0, pSource, 0, pSource.Length);
                source.UnlockBits(sourceData);

                for (int y = 0; y < _height; y++)
                {
                    int yOffSource = (y * sourceDataStride), yOffResult = (y * resultDataStride);

                    for (int xRow = 0; xRow < _width; xRow++)
                    {
                        byte curSource = pSource[yOffSource + xRow], curDex;

                        if (!indices.TryGetValue(curSource, out curDex))
                        {
                            curDex = (byte)colorList.Count;
                            colorList.Add(sourceEntries[curSource]);
                            indices[curSource] = curDex;
                        }

                        pResult[yOffResult + (xRow >> shift)] |= (byte)(curDex << (top - (xRow % colorsPerByte)));
                    }
                }
                Marshal.Copy(pResult, 0, resultData.Scan0, pResult.Length);
                result.UnlockBits(resultData);
            }

            source.UnlockBits(sourceData);
            result.UnlockBits(resultData);

            var resultPalette = result.Palette;

            for (int i = 0; i < colorList.Count; i++)
                resultPalette.Entries[i] = colorList[i];
            for (int i = colorList.Count; i < resultPalette.Entries.Length; i++)
                resultPalette.Entries[i] = Color.Empty;
            result.Palette = resultPalette;

            source.Dispose();
            return result;
        }
#else
        private unsafe WriteableBitmap GetBitmap(ColorValue[] pixels, PixelFormat pFormat, BitmapPalette palette, ushort maxColors)
        {
            WriteableBitmap wBmp = new WriteableBitmap(_width, _height, 0, 0, PixelFormats.Bgra32, null);
            if (pFormat.BitsPerPixel == 24)
            {
                for (int i = 0; i < pixels.Length; i++)
                    pixels[i].A = byte.MaxValue;
            }

            wBmp.WritePixels(new Int32Rect(0, 0, _width, _height), pixels, _width * 4, 0);
            if (pFormat.BitsPerPixel == 32)
                return wBmp;
            else if (pFormat.BitsPerPixel == 24)
                return new WriteableBitmap(new FormatConvertedBitmap(wBmp, pFormat, null, 0));

            if (palette != null)
                return new WriteableBitmap(new FormatConvertedBitmap(wBmp, pFormat, palette, 0));

            WriteableBitmap quantized = Quantize(wBmp, pixels, maxColors);

            if (pFormat.BitsPerPixel == quantized.Format.BitsPerPixel)
                return quantized;

            byte[] indices = new byte[pixels.Length];

            quantized.CopyPixels(indices, _width, 0);

            var baseColors = quantized.Palette.Colors;
            List<Color> colors = new List<Color>();
            HashSet<byte> indexSet = new HashSet<byte>();

            foreach (byte b in indices)
            {
                if (indexSet.Contains(b)) continue;

                indexSet.Add(b);
                colors.Add(baseColors[b]);

                if (colors.Count == maxColors)
                    break;
            }

            return new WriteableBitmap(new FormatConvertedBitmap(quantized, pFormat, new BitmapPalette(colors), 1));
        }

        private WriteableBitmap GetBitmap(ColorValue[] pixels, PixelFormat format)
        {
            return GetBitmap(pixels, format, null, ushort.MaxValue);
        }

        private WriteableBitmap GetBitmap(ColorValue[] pixels)
        {
            return GetBitmap(pixels, PixelFormats.Bgra32, null, ushort.MaxValue);
        }
#endif

#if DRAWING
        private Bitmap Quantize(Bitmap source, ColorValue[] pixels, int colorCount)
#else
        private WriteableBitmap Quantize(BitmapSource source, ColorValue[] pixels, int colorCount)
#endif
        {
            byte[] data = new byte[pixels.Length];
            Dictionary<ColorValue, byte> indices = new Dictionary<ColorValue, byte>();

            bool smaller = true;

            for (int i = 0; i < pixels.Length; i++)
            {
                ColorValue c = pixels[i];
                byte dex;

                if (!indices.TryGetValue(c, out dex))
                {
                    if (indices.Count == colorCount)
                    {
                        smaller = false;
                        break;
                    }

                    dex = (byte)indices.Count;
                    indices.Add(c, dex);
                }
                data[i] = dex;
            }

            if (!smaller)
            {
                WuQuantizer quant = new WuQuantizer();
#if DRAWING
                return (Bitmap)quant.QuantizeImage(source, _alphaThreshold,
#else
                return (WriteableBitmap)quant.QuantizeImage(source, AlphaThreshold,
#endif
                    70, colorCount);
            }

#if DRAWING
            Bitmap bmp = new Bitmap(source.Width, source.Height, PixelFormat.Format8bppIndexed);
            Rectangle fullRect = new Rectangle(Point.Empty, source.Size);
            Color[] colors = indices.Keys.Select(i => i.GetColor()).ToArray();
            var palette = bmp.Palette;

            for (int i = 0; i < colors.Length; i++)
                palette.Entries[i] = colors[i];

            bmp.Palette = palette;

            BitmapData bmpData = bmp.LockBits(fullRect, ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            for (int y = 0; y < source.Height; y++)
                Marshal.Copy(data, y * source.Width, bmpData.Scan0 + (y * bmpData.Stride), source.Width);

            bmp.UnlockBits(bmpData);
#else
            WriteableBitmap bmp = new WriteableBitmap(source.PixelWidth, source.PixelHeight, 0, 0, PixelFormats.Indexed8,
                new BitmapPalette(indices.Keys.Select(i => i.GetColor()).ToArray()));

            bmp.WritePixels(new Int32Rect(0, 0, source.PixelWidth, source.PixelHeight), data, source.PixelWidth, 0);
#endif
            return bmp;
        }
        #endregion

        /// <summary>
        /// Returns a string representation of the current instance.
        /// </summary>
        /// <returns>A string representation of the current instance.</returns>
        public override string ToString()
        {
            return string.Format("{0}, BaseImage:{1}, IsPng:{2}", EntryKey,
#if DRAWING
                _baseImage, _isPng);
#else
                BaseImage, IsPng);
#endif
        }

        /// <summary>
        /// Parses the specified string as a <see cref="IconBitDepth"/> value.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <returns>The parsed <see cref="IconBitDepth"/> value.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <para><paramref name="value"/> is an empty string or contains only whitespace.</para>
        /// <para>-OR-</para>
        /// <para><paramref name="value"/> does not translate to a valid <see cref="IconBitDepth"/> value.</para>
        /// </exception>
        /// <remarks>
        /// <para><paramref name="value"/> is parsed in a case-insensitive manner which works differently from <see cref="Enum.Parse(Type, string, bool)"/>.</para>
        /// <para>First of all, all non-alphanumeric characters are stripped. If <paramref name="value"/> is entirely numeric, or begins with "Depth"
        /// followed by an entirely numeric value, it is parsed according to the number of colors or the number of bits per pixel, rather than the
        /// integer <see cref="IconBitDepth"/> value. There is fortunately no overlap; 1, 4, 8, 24, and 32 always refer to the number of bits
        /// per pixel, whereas 2, 16, 256, 16777216, and 4294967296 always refer to the number of colors.</para>
        /// <para>Otherwise, "Depth" is prepended to the beginning, and it attempts to ensure that the value ends with either "Color" or "BitsPerPixel"
        /// (or "BitPerPixel" in the case of <see cref="IconBitDepth.Depth1BitPerPixel"/>).</para>
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
        public static IconBitDepth ParseBitDepth(string value)
        {
            if (value == null) throw new ArgumentNullException("value");
            IconBitDepth result;
            TryParseBitDepth(value, true, out result);
            return result;
        }

        /// <summary>
        /// Parses the specified string as a <see cref="IconBitDepth"/> value.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <param name="result">When this method returns, contains the parsed <see cref="IconBitDepth"/> result, or
        /// the default value for type <see cref="IconBitDepth"/> if <paramref name="value"/> could not be parsed.
        /// This parameter is passed uninitialized.</param>
        /// <returns><c>true</c> if <paramref name="value"/> was successfully parsed; <c>false</c> otherwise.</returns>
        /// <remarks>
        /// <para><paramref name="value"/> is parsed in a case-insensitive manner which works differently from <see cref="Enum.TryParse{TEnum}(string, bool, out TEnum)"/>.</para>
        /// <para>First of all, all non-alphanumeric characters are stripped. If <paramref name="value"/> is entirely numeric, or begins with "Depth"
        /// followed by an entirely numeric value, it is parsed according to the number of colors or the number of bits per pixel, rather than the
        /// integer <see cref="IconBitDepth"/> value. There is fortunately no overlap; 1, 4, 8, 24, and 32 always refer to the number of bits
        /// per pixel, whereas 2, 16, 256, 16777216, and 4294967296 always refer to the number of colors.</para>
        /// <para>Otherwise, "Depth" is prepended to the beginning, and it attempts to ensure that the value ends with either "Color" or "BitsPerPixel"
        /// (or "BitPerPixel" in the case of <see cref="IconBitDepth.Depth1BitPerPixel"/>).</para>
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
        public static bool TryParseBitDepth(string value, out IconBitDepth result)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                result = 0;
                return false;
            }

            return TryParseBitDepth(value, false, out result);
        }

        private static bool TryParseBitDepth(string value, bool throwError, out IconBitDepth result)
        {
            if (throwError && string.IsNullOrWhiteSpace(value))
                Enum.Parse(typeof(IconBitDepth), value, true);

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
                        result = IconBitDepth.Depth1BitsPerPixel;
                        return true;
                    case 2:
                        result = IconBitDepth.Depth2Color;
                        return true;
                    case 4:
                        result = IconBitDepth.Depth4BitsPerPixel;
                        return true;
                    case 8:
                        result = IconBitDepth.Depth8BitsPerPixel;
                        return true;
                    case 16:
                        result = IconBitDepth.Depth16Color;
                        return true;
                    case 24:
                        result = IconBitDepth.Depth24BitsPerPixel;
                        return true;
                    case 32:
                        result = IconBitDepth.Depth32BitsPerPixel;
                        return true;
                    case 256:
                        result = IconBitDepth.Depth256Color;
                        return true;
                    case 16777216:
                        result = IconBitDepth.Depth24BitsPerPixel;
                        return true;
                    case uint.MaxValue + 1L:
                        result = IconBitDepth.Depth32BitsPerPixel;
                        return true;
                    default:
                        if (throwError)
                        {
                            stripValue += "!!+";

                            try
                            {
                                result = (IconBitDepth)Enum.Parse(typeof(IconBitDepth), stripValue, true);
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
                result = IconBitDepth.Depth24BitsPerPixel;
                return true;
            }
            else if (stripValue.Equals("depth4294967296color", StringComparison.OrdinalIgnoreCase))
            {
                result = IconBitDepth.Depth32BitsPerPixel;
                return true;
            }

            if (Enum.TryParse(stripValue, true, out result))
                return true;

            if (throwError)
            {
                try
                {
                    stripValue += "!!+";

                    result = (IconBitDepth)Enum.Parse(typeof(IconBitDepth), stripValue, true);
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
    public struct IconEntryKey : IEquatable<IconEntryKey>, IComparable<IconEntryKey>
    {
        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="width">The width of the icon entry.</param>
        /// <param name="height">The height of the icon entry.</param>
        /// <param name="bitDepth">The bit depth of the icon entry.</param>
        public IconEntryKey(int width, int height, IconBitDepth bitDepth)
        {
            Width = width;
            Height = height;
            BitDepth = bitDepth;
        }

        private static bool _isValid(IconBitDepth bitDepth)
        {
            switch (bitDepth)
            {
                case IconBitDepth.Depth32BitsPerPixel:
                case IconBitDepth.Depth24BitsPerPixel:
                case IconBitDepth.Depth8BitsPerPixel:
                case IconBitDepth.Depth4BitsPerPixel:
                case IconBitDepth.Depth1BitPerPixel:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Indicates the width of the icon entry.
        /// </summary>
        public int Width;

        /// <summary>
        /// Indicates the height of the icon entry.
        /// </summary>
        public int Height;

        /// <summary>
        /// Indicates the bit depth of the icon entry.
        /// </summary>
        public IconBitDepth BitDepth;

        /// <summary>
        /// Compares the current instance to the specified other <see cref="IconEntryKey"/> object. First
        /// <see cref="BitDepth"/> is compared, then <see cref="Height"/>, then <see cref="Width"/> (with
        /// higher color-counts and larger elements first).
        /// </summary>
        /// <param name="other">The other <see cref="IconEntryKey"/> to compare.</param>
        /// <returns>A value less than 0 if the current value comes before <paramref name="other"/>; 
        /// a value greater than 0 if the current value comes after <paramref name="other"/>; or
        /// 0 if the current instance is equal to <paramref name="other"/>.</returns>
        public int CompareTo(IconEntryKey other)
        {
            int compare = (int)BitDepth - (int)other.BitDepth;
            if (compare != 0) return compare;

            compare = other.Height - Height;
            if (compare != 0) return compare;

            return other.Width - Width;
        }

        /// <summary>
        /// Returns an invalid <see cref="IconEntryKey"/> with all values equal to 0.
        /// </summary>
        public static readonly IconEntry Empty;

        /// <summary>
        /// Gets a value indicating whether the current value would actually appear in an icon entry.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return Width >= IconEntry.MinDimension && Width <= IconEntry.MaxDimension
                    && Height >= IconEntry.MinDimension && Height <= IconEntry.MaxDimension && _isValid(BitDepth);
            }
        }

        /// <summary>
        /// Returns a string representation of the current value.
        /// </summary>
        /// <returns>A string representation of the current value.</returns>
        public override string ToString()
        {
            return string.Format("Width:{0}, Height:{1}, BitDepth:{2}", Width, Height, BitDepth);
        }

        /// <summary>
        /// Compares two <see cref="IconEntryKey"/> objects.
        /// </summary>
        /// <param name="f1">An <see cref="IconEntryKey"/> to compare.</param>
        /// <param name="f2">An <see cref="IconEntryKey"/> to compare.</param>
        /// <returns><c>true</c> if <paramref name="f1"/> is less than <paramref name="f2"/>; <c>false</c> otherwise.</returns>
        public static bool operator <(IconEntryKey f1, IconEntryKey f2)
        {
            return f1.CompareTo(f2) < 0;
        }

        /// <summary>
        /// Compares two <see cref="IconEntryKey"/> objects.
        /// </summary>
        /// <param name="f1">An <see cref="IconEntryKey"/> to compare.</param>
        /// <param name="f2">An <see cref="IconEntryKey"/> to compare.</param>
        /// <returns><c>true</c> if <paramref name="f1"/> is greater than <paramref name="f2"/>; <c>false</c> otherwise.</returns>
        public static bool operator >(IconEntryKey f1, IconEntryKey f2)
        {
            return f1.CompareTo(f2) > 0;
        }

        /// <summary>
        /// Compares two <see cref="IconEntryKey"/> objects.
        /// </summary>
        /// <param name="f1">An <see cref="IconEntryKey"/> to compare.</param>
        /// <param name="f2">An <see cref="IconEntryKey"/> to compare.</param>
        /// <returns><c>true</c> if <paramref name="f1"/> is less than or equal to <paramref name="f2"/>; <c>false</c> otherwise.</returns>
        public static bool operator <=(IconEntryKey f1, IconEntryKey f2)
        {
            return f1.CompareTo(f2) <= 0;
        }

        /// <summary>
        /// Compares two <see cref="IconEntryKey"/> objects.
        /// </summary>
        /// <param name="f1">An <see cref="IconEntryKey"/> to compare.</param>
        /// <param name="f2">An <see cref="IconEntryKey"/> to compare.</param>
        /// <returns><c>true</c> if <paramref name="f1"/> is less than or equal to <paramref name="f2"/>; <c>false</c> otherwise.</returns>
        public static bool operator >=(IconEntryKey f1, IconEntryKey f2)
        {
            return f1.CompareTo(f2) >= 0;
        }


        /// <summary>
        /// Determines if the current instance is equal to the specified other <see cref="IconEntryKey"/> value.
        /// </summary>
        /// <param name="other">The other <see cref="IconEntryKey"/> to compare.</param>
        /// <returns><c>true</c> if the current instance is equal to <paramref name="other"/>; <c>false</c> otherwise.</returns>
        public bool Equals(IconEntryKey other)
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
            return obj is IconEntryKey && Equals((IconEntryKey)obj);
        }

        /// <summary>
        /// Returns a hash code for the current value.
        /// </summary>
        /// <returns>A hash code for the current value.</returns>
        public override int GetHashCode()
        {
            return Width + Height + (int)BitDepth;
        }

        /// <summary>
        /// Determines equality of two <see cref="IconEntryKey"/> objects.
        /// </summary>
        /// <param name="f1">An <see cref="IconEntryKey"/> to compare.</param>
        /// <param name="f2">An <see cref="IconEntryKey"/> to compare.</param>
        /// <returns><c>true</c> if <paramref name="f1"/> is equal to <paramref name="f2"/>; <c>false</c> otherwise.</returns>
        public static bool operator ==(IconEntryKey f1, IconEntryKey f2)
        {
            return f1.Equals(f2);
        }

        /// <summary>
        /// Determines inequality of two <see cref="IconEntryKey"/> objects.
        /// </summary>
        /// <param name="f1">An <see cref="IconEntryKey"/> to compare.</param>
        /// <param name="f2">An <see cref="IconEntryKey"/> to compare.</param>
        /// <returns><c>true</c> if <paramref name="f1"/> is not equal to <paramref name="f2"/>; <c>false</c> otherwise.</returns>
        public static bool operator !=(IconEntryKey f1, IconEntryKey f2)
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

            int count = x.EntryKey.CompareTo(y.EntryKey);
            if (count != 0) return count;

            return x.GetHashCode().CompareTo(y.GetHashCode());
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
    public enum IconBitDepth : short
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
    public enum IconScalingFilter
    {
#if !DRAWING
        /// <summary>
        /// Resizes using a transformation matrix.
        /// </summary>
        Matrix,
#endif
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
