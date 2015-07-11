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
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace UIconEdit.Maker
{

    internal class SizeStringConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string format = (string)values[0];
            object width = values[1];
            object height = (values.Length < 3) ? width : values[2];
            return string.Format(format, width, height);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    internal class BPSStringConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string format = (string)values[0];
            return string.Format(format, values[1]);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    internal class AlphaImageConverter : IValueConverter
    {
        public static BitmapSource Convert(IconEntry entry)
        {
            if (entry == null) return null;
            if (entry.AlphaImage == null || entry.BitDepth == BitDepth.Depth32BitsPerPixel) return entry.BaseImage;

            uint[] bmpPixels = new uint[entry.Width * entry.Height];
            uint[] alphaPixels = new uint[bmpPixels.Length];

            new FormatConvertedBitmap(entry.BaseImage, PixelFormats.Bgra32, null, 0).CopyPixels(bmpPixels, entry.Width * 4, 0);
            new FormatConvertedBitmap(entry.AlphaImage, PixelFormats.Bgra32, null, 0).CopyPixels(alphaPixels, entry.Width * 4, 0);

            for (int i = 0; i < bmpPixels.Length; i++)
            {
                if (alphaPixels[i] != uint.MaxValue)
                    bmpPixels[i] = 0;
            }

            return BitmapSource.Create(entry.Width, entry.Height, 0, 0, PixelFormats.Bgra32, null, bmpPixels, entry.Width * 4);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value as IconEntry);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    internal class IsCursorConverter : IValueConverter
    {
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is CursorFile;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    internal class CanUseHotspotConverter : IsCursorConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)base.Convert(value, targetType, parameter, culture) ? Visibility.Visible : Visibility.Collapsed;
        }
    }

    internal class BooleanAndToVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 0) return Visibility.Collapsed;

            foreach (object curVal in values)
            {
                bool bVal = false;
                if (curVal is bool?)
                {
                    bool? nVal = (bool?)curVal;
                    if (nVal.HasValue) bVal = nVal.Value;
                }
                else if (curVal is bool)
                    bVal = (bool)curVal;

                if (!bVal) return Visibility.Collapsed;
            }
            return Visibility.Visible;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    internal class ZoomConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 0) return 0;

            double value = 1;

            foreach (object curVal in values)
            {
                if (curVal is IConvertible)
                    value *= ((IConvertible)curVal).ToDouble(culture);
            }

            if (values[0] is IConvertible)
            {
                double curVal = ((IConvertible)values[0]).ToDouble(null);
                if (curVal > 100)
                    value += (curVal / 2);
            }

            return Math.Round(value / 100);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    internal class ScaleFilterConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2 || !(values[0] is ScalingFilter))
                return "";

            ScalingFilter filter = (ScalingFilter)values[0];
            LanguageFile langFile = values[1] as LanguageFile;

            if (langFile == null) return filter.ToString();
            return langFile.GetScalingFilter(filter);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
