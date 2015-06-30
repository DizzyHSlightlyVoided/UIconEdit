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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using nQuant;

using PixelFormat = System.Drawing.Imaging.PixelFormat;
using Point = System.Windows.Point;

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
        public IconEntry(BitmapSource baseImage, short width, short height, BitDepth bitDepth, byte alphaThreshold)
        {
            if (baseImage == null) throw new ArgumentNullException("baseImage");
            _initValues(width, height, bitDepth);
            SetValue(BitDepthPropertyKey, bitDepth);
            SetValue(WidthPropertyKey, width);
            SetValue(HeightPropertyKey, height);
            SetBPS();
            BaseImage = baseImage;
            AlphaThreshold = alphaThreshold;
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
        public IconEntry(BitmapSource baseImage, short width, short height, BitDepth bitDepth)
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
        public IconEntry(BitmapSource baseImage, BitDepth bitDepth, byte alphaThreshold)
        {
            if (baseImage == null) throw new ArgumentNullException("baseImage");
            if (baseImage.PixelWidth < MinDimension || baseImage.PixelWidth > MaxDimension || baseImage.PixelHeight < MinDimension || baseImage.PixelHeight > MaxDimension)
                throw new ArgumentException("The image size is out of the supported range.", "baseImage");
            if (!_validateBitDepth(bitDepth))
                throw new InvalidEnumArgumentException("bitDepth", (int)bitDepth, typeof(BitDepth));
            BaseImage = baseImage;
            SetValue(WidthPropertyKey, (short)baseImage.PixelWidth);
            SetValue(HeightPropertyKey, (short)baseImage.PixelHeight);
            SetValue(BitDepthPropertyKey, bitDepth);
            SetBPS();
            AlphaThreshold = alphaThreshold;
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
            : this(baseImage, bitDepth, DefaultAlphaThreshold)
        {
        }

        /// <summary>
        /// The minimum dimensions of an icon. 1 pixel in size.
        /// </summary>
        public const short MinDimension = 1;
        /// <summary>
        /// The maximum dimensions of an icon. 768 as of Windows 10.
        /// </summary>
        public const short MaxDimension = 768;

        #region BaseImage
        /// <summary>
        /// The dependency property for the <see cref="BaseImage"/> property.
        /// </summary>
        public static readonly DependencyProperty BaseImageProperty = DependencyProperty.Register("BaseImage", typeof(BitmapSource), typeof(IconEntry),
            new PropertyMetadata(null));

        /// <summary>
        /// Gets and sets the image associated with the current instance.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// In a set operation, the specified value is <c>null</c>.
        /// </exception>
        public BitmapSource BaseImage
        {
            get { return (BitmapSource)GetValue(BaseImageProperty); }
            set { SetValue(BaseImageProperty, value); }
        }
        #endregion

        private void SetBPS()
        {
            ushort bps;
            long colorCount;
            switch (BitDepth)
            {
                case BitDepth.Depth1BitPerPixel:
                    bps = 1;
                    colorCount = 2;
                    break;
                case BitDepth.Depth4BitsPerPixel:
                    bps = 4;
                    colorCount = 16;
                    break;
                case BitDepth.Depth8BitsPerPixel:
                    bps = 8;
                    colorCount = 256;
                    break;
                case BitDepth.Depth24BitsPerPixel:
                    bps = 24;
                    colorCount = 0x1000000;
                    break;
                default: //Depth32BitsPerPixel
                    bps = 32;
                    colorCount = uint.MaxValue + 1L;
                    break;
            }
            SetValue(EntryKeyPropertyKey, new EntryKey(Width, Height, BitDepth));
            SetValue(BitsPerPixelPropertyKey, bps);
            SetValue(ColorCountPropertyKey, colorCount);
        }

        #region Key
        private static readonly DependencyPropertyKey EntryKeyPropertyKey = DependencyProperty.RegisterReadOnly("Key", typeof(EntryKey),
            typeof(IconEntry), new PropertyMetadata(default(EntryKey)));
        /// <summary>
        /// The dependency property for the read-only <see cref="EntryKey"/> property.
        /// </summary>
        public static readonly DependencyProperty EntryKeyProperty = EntryKeyPropertyKey.DependencyProperty;

        /// <summary>
        /// Gets a key for the icon entry.
        /// </summary>
        public EntryKey EntryKey { get { return (EntryKey)GetValue(EntryKeyProperty); } }
        #endregion

        #region Width
        private static readonly DependencyPropertyKey WidthPropertyKey = DependencyProperty.RegisterReadOnly("Width", typeof(short), typeof(IconEntry),
            new PropertyMetadata(MinDimension));
        /// <summary>
        /// The dependency property for the read-only <see cref="Width"/> property.
        /// </summary>
        public static readonly DependencyProperty WidthProperty = WidthPropertyKey.DependencyProperty;

        /// <summary>
        /// Gets the resampled width of the icon.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// In a set operation, the specified value is less than <see cref="MinDimension"/> or is greater than <see cref="MaxDimension"/>.
        /// </exception>
        public short Width
        {
            get { return (short)GetValue(WidthProperty); }
        }
        #endregion

        #region Height
        private static readonly DependencyPropertyKey HeightPropertyKey = DependencyProperty.RegisterReadOnly("Height", typeof(short), typeof(IconEntry),
            new PropertyMetadata(MinDimension));
        /// <summary>
        /// The dependency property for the read-only <see cref="Height"/> property.
        /// </summary>
        public static readonly DependencyProperty HeightProperty = HeightPropertyKey.DependencyProperty;

        /// <summary>
        /// Gets the resampled height of the icon.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// In a set operation, the specified value is less than <see cref="MinDimension"/> or is greater than <see cref="MaxDimension"/>.
        /// </exception>
        public short Height
        {
            get { return (short)GetValue(HeightProperty); }
        }
        #endregion

        #region BitDepth
        private static readonly DependencyPropertyKey BitDepthPropertyKey = DependencyProperty.RegisterReadOnly("BitDepth", typeof(BitDepth), typeof(IconEntry),
            new PropertyMetadata(BitDepth.Depth32BitsPerPixel));
        /// <summary>
        /// The dependency property for the read-only <see cref="BitDepth"/> property.
        /// </summary>
        public static readonly DependencyProperty BitDepthProperty = BitDepthPropertyKey.DependencyProperty;

        /// <summary>
        /// Gets the bit depth of the current instance.
        /// </summary>
        public BitDepth BitDepth
        {
            get { return (BitDepth)GetValue(BitDepthProperty); }
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

        #region BitsPerPixel
        private static readonly DependencyPropertyKey BitsPerPixelPropertyKey = DependencyProperty.RegisterReadOnly("BitsPerPixel", typeof(ushort), typeof(IconEntry),
            new PropertyMetadata((ushort)32));
        /// <summary>
        /// The dependency-property for the read-only <see cref="BitsPerPixel"/> property.
        /// </summary>
        public static readonly DependencyProperty BitsPerPixelProperty = BitsPerPixelPropertyKey.DependencyProperty;

        /// <summary>
        /// Gets the number of bits per pixel specified by <see cref="BitDepth"/>.
        /// </summary>
        public ushort BitsPerPixel
        {
            get { return (ushort)GetValue(BitsPerPixelProperty); }
        }
        #endregion

        #region ColorCount
        private static readonly DependencyPropertyKey ColorCountPropertyKey = DependencyProperty.RegisterReadOnly("ColorCount", typeof(long), typeof(IconEntry),
            new PropertyMetadata(uint.MaxValue + 1L));
        /// <summary>
        /// The dependency property for the read-only <see cref="ColorCount"/> property.
        /// </summary>
        public static readonly DependencyProperty ColorCountProperty = ColorCountPropertyKey.DependencyProperty;

        /// <summary>
        /// Gets the maximum color count specified by <see cref="BitDepth"/>.
        /// </summary>
        public long ColorCount
        {
            get { return (long)GetValue(ColorCountProperty); }
        }
        #endregion

        #region DrawInterpolationMode
        /// <summary>
        /// The dependency property for the <see cref="DrawInterpolationMode"/> property.
        /// </summary>
        public static readonly DependencyProperty DrawInterpolationModeProperty = DependencyProperty.Register("DrawInterpolationMode", typeof(InterpolationMode),
            typeof(IconEntry), new PropertyMetadata(InterpolationMode.Default), DrawInterpolationModeValidate);

        private static bool DrawInterpolationModeValidate(object value)
        {
            switch ((InterpolationMode)value)
            {
                case InterpolationMode.Bicubic:
                case InterpolationMode.Bilinear:
                case InterpolationMode.Default:
                case InterpolationMode.High:
                case InterpolationMode.HighQualityBicubic:
                case InterpolationMode.HighQualityBilinear:
                case InterpolationMode.Low:
                case InterpolationMode.NearestNeighbor:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Gets and sets the interpolation mode used by graphics objects when scaling.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// In a set operation, the specified value is not a valid <see cref="InterpolationMode"/> value.
        /// </exception>
        public InterpolationMode DrawInterpolationMode
        {
            get { return (InterpolationMode)GetValue(DrawInterpolationModeProperty); }
            set { SetValue(DrawInterpolationModeProperty, value); }
        }
        #endregion

        #region DrawPixelOffsetMode
        /// <summary>
        /// The dependency property for the <see cref="DrawPixelOffsetMode"/> property.
        /// </summary>
        public static DependencyProperty DrawPixelOffsetModeProperty = DependencyProperty.Register("DrawPixelOffsetMode", typeof(PixelOffsetMode), typeof(IconEntry),
            new PropertyMetadata(PixelOffsetMode.Default), DrawPixelOffsetModeValidate);

        private static bool DrawPixelOffsetModeValidate(object value)
        {
            switch ((PixelOffsetMode)value)
            {
                case PixelOffsetMode.Default:
                case PixelOffsetMode.Half:
                case PixelOffsetMode.HighQuality:
                case PixelOffsetMode.HighSpeed:
                case PixelOffsetMode.None:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Gets and sets the pixel offset mode used by graphics objects when rescaling the image.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// In a set operation, the specified value is not a valid <see cref="PixelOffsetMode"/> value.
        /// </exception>
        public PixelOffsetMode DrawPixelOffsetMode
        {
            get { return (PixelOffsetMode)GetValue(DrawPixelOffsetModeProperty); }
            set { SetValue(DrawPixelOffsetModeProperty, value); }
        }
        #endregion

        internal IconFileBase File;

        private Graphics GraphicsFromImage(Bitmap image)
        {
            Graphics g = Graphics.FromImage(image);
            g.InterpolationMode = DrawInterpolationMode;
            g.PixelOffsetMode = DrawPixelOffsetMode;
            return g;
        }

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

        internal unsafe Bitmap GetQuantized(out Bitmap alphaMask, out int paletteCount)
        {
            const PixelFormat formatFull = PixelFormat.Format32bppArgb, formatAlpha = PixelFormat.Format1bppIndexed;
            short _width = Width, _height = Height;
            var _depth = BitDepth;

            bool isPng = _width > byte.MaxValue || _height > byte.MaxValue;

            Bitmap fullColor = new Bitmap(_width, _height, formatFull);

            var image = BaseImage;

            using (Bitmap smallBmp = new Bitmap(image.PixelWidth, image.PixelHeight, formatFull))
            {
                FormatConvertedBitmap formatBmp = new FormatConvertedBitmap(image, PixelFormats.Bgra32, null, 0);

                BitmapData smallData = smallBmp.LockBits(new Rectangle(0, 0, smallBmp.Width, smallBmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

                formatBmp.CopyPixels(new Int32Rect(0, 0, smallData.Width, smallData.Height), smallData.Scan0,
                    Math.Abs(smallData.Height * smallData.Stride), smallData.Stride);

                smallBmp.UnlockBits(smallData);

                using (Graphics g = GraphicsFromImage(fullColor))
                    g.DrawImage(smallBmp, 0, 0, _width, _height);
            }
            Rectangle fullRect = new Rectangle(0, 0, _width, _height);
            const uint opaqueAlpha = 0xFF000000u;

            byte _alphaThreshold = AlphaThreshold;

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

            if (_depth == BitDepth.Depth32BitsPerPixel)
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
                    if (value < _alphaThreshold)
                        pFull[x] = isPng ? (pFull[x] & ~opaqueAlpha) : opaqueAlpha;
                    else
                        pFull[x] |= opaqueAlpha;
                }
            }

            fullColor.UnlockBits(bmpData);

            if (_depth == BitDepth.Depth24BitsPerPixel)
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
                case BitDepth.Depth2Color:
                    paletteCount = 2;
                    break;
                case BitDepth.Depth16Color:
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

            if (_depth == BitDepth.Depth256Color)
                return quantized;

            Bitmap quant2;

            BitmapData quant256data = quantized.LockBits(fullRect, ImageLockMode.ReadOnly, format8);

            if (_depth == BitDepth.Depth2Color)
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
    /// Represents a single entry of a cursor.
    /// </summary>
    public class CursorEntry : IconEntry
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
        /// <para><paramref name="width"/> or <paramref name="height"/> is less than <see cref="IconEntry.MinDimension"/> or is greater than
        ///  <see cref="IconEntry.MaxDimension"/>.</para>
        /// <para>-OR-</para>
        /// <para><paramref name="hotspotX"/> is greater than <paramref name="width"/>.</para>
        /// <para>-OR-</para>
        /// <para><paramref name="hotspotY"/> is greater than <paramref name="height"/>.</para>
        /// </exception>
        public CursorEntry(BitmapSource baseImage, short width, short height, BitDepth bitDepth, ushort hotspotX, ushort hotspotY, byte alphaThreshold)
            : base(baseImage, width, height, bitDepth, alphaThreshold)
        {
            HotspotX = hotspotX;
            HotspotY = hotspotY;
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
        /// <para><paramref name="width"/> or <paramref name="height"/> is less than <see cref="IconEntry.MinDimension"/> or is greater than
        /// <see cref="IconEntry.MaxDimension"/>.</para>
        /// <para>-OR-</para>
        /// <para><paramref name="hotspotX"/> is greater than <paramref name="width"/>.</para>
        /// <para>-OR-</para>
        /// <para><paramref name="hotspotY"/> is greater than <paramref name="height"/>.</para>
        /// </exception>
        public CursorEntry(BitmapSource baseImage, short width, short height, BitDepth bitDepth, ushort hotspotX, ushort hotspotY)
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
        /// <paramref name="width"/> or <paramref name="height"/> is less than <see cref="IconEntry.MinDimension"/> or is greater than <see cref="IconEntry.MaxDimension"/>.
        /// </exception>
        public CursorEntry(BitmapSource baseImage, short width, short height, BitDepth bitDepth, byte alphaThreshold)
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
        /// <paramref name="width"/> or <paramref name="height"/> is less than <see cref="IconEntry.MinDimension"/> or is greater than <see cref="IconEntry.MaxDimension"/>.
        /// </exception>
        public CursorEntry(BitmapSource baseImage, short width, short height, BitDepth bitDepth)
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// <para>-OR-</para>
        /// <para><paramref name="hotspotX"/> is greater than the width of <paramref name="baseImage"/>.</para>
        /// <para>-OR-</para>
        /// <para><paramref name="hotspotY"/> is greater than the height of <paramref name="baseImage"/>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The width or height of <paramref name="baseImage"/> is less than <see cref="IconEntry.MinDimension"/> or is greater than <see cref="IconEntry.MaxDimension"/>.
        /// </exception>
        public CursorEntry(BitmapSource baseImage, BitDepth bitDepth, ushort hotspotX, ushort hotspotY, byte alphaThreshold)
            : base(baseImage, bitDepth, alphaThreshold)
        {
            HotspotX = hotspotX;
            HotspotY = hotspotY;
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// <para>-OR-</para>
        /// <para><paramref name="hotspotX"/> is greater than the width of <paramref name="baseImage"/>.</para>
        /// <para>-OR-</para>
        /// <para><paramref name="hotspotY"/> is greater than the height of <paramref name="baseImage"/>.</para>
        /// </exception>
        /// <exception cref="InvalidEnumArgumentException">
        /// <paramref name="bitDepth"/> is not a valid <see cref="BitDepth"/> value.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The width or height of <paramref name="baseImage"/> is less than <see cref="IconEntry.MinDimension"/> or is greater than <see cref="IconEntry.MaxDimension"/>.
        /// </exception>
        public CursorEntry(BitmapSource baseImage, BitDepth bitDepth, ushort hotspotX, ushort hotspotY)
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
        /// The width or height of <paramref name="baseImage"/> is less than <see cref="IconEntry.MinDimension"/> or is greater than <see cref="IconEntry.MaxDimension"/>.
        /// </exception>
        public CursorEntry(BitmapSource baseImage, BitDepth bitDepth, byte alphaThreshold)
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
        /// The width or height of <paramref name="baseImage"/> is less than <see cref="IconEntry.MinDimension"/> or is greater than <see cref="IconEntry.MaxDimension"/>.
        /// </exception>
        public CursorEntry(BitmapSource baseImage, BitDepth bitDepth)
            : base(baseImage, bitDepth)
        {
        }

        #region HotspotX
        /// <summary>
        /// The dependency property for the <see cref="HotspotX"/> property.
        /// </summary>
        public static readonly DependencyProperty HotspotXProperty = DependencyProperty.Register("HotspotX", typeof(ushort), typeof(CursorEntry),
            new PropertyMetadata(ushort.MinValue, HotspotXChanged));

        private static void HotspotXChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CursorEntry c = (CursorEntry)d;
            if ((ushort)e.NewValue > c.Width)
            {
                c.SetValue(HotspotXProperty, e.OldValue);
                throw new ArgumentOutOfRangeException(null, e.NewValue, "The hotspot X position is greater than the width of the image.");
            }
        }

        /// <summary>
        /// Gets and sets the horizontal offset of the cursor's hotspot from the left of the cursor in pixels.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// In a set operation, the specified value is greater than <see cref="IconEntry.Width"/>.
        /// </exception>
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
        public static readonly DependencyProperty HotspotYProperty = DependencyProperty.Register("HotspotY", typeof(ushort), typeof(CursorEntry),
            new PropertyMetadata(ushort.MinValue, HotspotYChanged));

        private static void HotspotYChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CursorEntry c = (CursorEntry)d;
            if ((ushort)e.NewValue > c.Height)
            {
                c.SetValue(HotspotXProperty, e.OldValue);
                throw new ArgumentOutOfRangeException(null, e.NewValue, "The hotspot X position is greater than the width of the image.");
            }
        }

        /// <summary>
        /// Gets and sets the vertical offset of the cursor's hotspot from the top of the cursor in pixels.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// In a set operation, the specified value is greater than <see cref="IconEntry.Height"/>.
        /// </exception>
        public ushort HotspotY
        {
            get { return (ushort)GetValue(HotspotYProperty); }
            set { SetValue(HotspotYProperty, value); }
        }
        #endregion

        /// <summary>
        /// Gets the offset of the cursor's hotspot from the upper-left corner of the cursor in pixels.
        /// </summary>
        public Point Hotspot
        {
            get { return new Point(Width, Height); }
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
            if (BitDepth != other.BitDepth)
                return BitDepth.CompareTo(other.BitDepth);

            if (Height != other.Height)
                return other.Height.CompareTo(Height);

            return other.Width.CompareTo(Width);
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

    internal struct IconEntryComparer : IEqualityComparer<IconEntry>, IComparer<IconEntry>, IEqualityComparer<CursorEntry>, IComparer<CursorEntry>
    {
        public int Compare(CursorEntry x, CursorEntry y)
        {
            return Compare((IconEntry)x, y);
        }

        public int Compare(IconEntry x, IconEntry y)
        {
            if (ReferenceEquals(x, y))
                return 0;

            if (ReferenceEquals(x, null)) return -1;
            else if (ReferenceEquals(y, null)) return 1;

            return x.EntryKey.CompareTo(y.EntryKey);
        }

        public bool Equals(CursorEntry x, CursorEntry y)
        {
            return Equals((IconEntry)x, y);
        }

        public bool Equals(IconEntry x, IconEntry y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null) ^ ReferenceEquals(y, null)) return false;
            return x.EntryKey == y.EntryKey;
        }

        public int GetHashCode(CursorEntry obj)
        {
            return GetHashCode((IconEntry)obj);
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
    public enum BitDepth
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
}
