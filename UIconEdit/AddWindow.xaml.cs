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
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace UIconEdit.Maker
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    partial class AddWindow
    {
        private static readonly IconScalingFilter[] _filters = (IconScalingFilter[])Enum.GetValues(typeof(IconScalingFilter));
        public static IconScalingFilter[] Filters { get { return _filters; } }

        public AddWindow(MainWindow mainWindow, bool duplicated, bool newFile, BitmapSource image, IconBitDepth bitDepth)
        {
            Owner = _mainWindow = mainWindow;
            LoadedImage = image;

            const string prefix = "Owner.SettingsFile.LanguageFile.";
            Binding binding = new Binding(prefix + (duplicated ? "DuplicateTitle" : "AddTitle"));
            binding.ElementName = "window";
            binding.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(this, TitleProperty, binding);

            SetValue(NewFilePropertyKey, newFile);

            short width = 32, height = 32;
            if (image.PixelWidth > IconEntry.MaxDimension)
                width = IconEntry.MaxDimension;
            else if (image.PixelWidth >= IconEntry.MinDimension)
                width = (short)image.PixelWidth;

            if (image.PixelHeight > IconEntry.MaxDimension)
                height = IconEntry.MaxDimension;
            else if (image.PixelHeight >= IconEntry.MinDimension)
                height = (short)image.PixelHeight;

            EntryWidth = width;
            EntryHeight = height;

            InitializeComponent();

            bool initSize = false;

            if (width == height)
            {
                switch (width)
                {
                    case 16:
                        initSize = true;
                        sz16.IsChecked = true;
                        break;
                    case 24:
                        initSize = true;
                        sz24.IsChecked = true;
                        break;
                    case 32:
                        initSize = true;
                        sz32.IsChecked = true;
                        break;
                    case 48:
                        initSize = true;
                        sz48.IsChecked = true;
                        break;
                    case 256:
                        initSize = true;
                        sz256.IsChecked = true;
                        break;
                    case 768:
                        initSize = true;
                        sz768.IsChecked = chkExtended.IsChecked = true;
                        break;
                    case 20:
                        initSize = true;
                        sz20.IsChecked = chkExtended.IsChecked = true;
                        break;
                    case 40:
                        initSize = true;
                        sz40.IsChecked = chkExtended.IsChecked = true;
                        break;
                    case 64:
                        initSize = true;
                        sz64.IsChecked = chkExtended.IsChecked = true;
                        break;
                    case 80:
                        initSize = true;
                        sz80.IsChecked = chkExtended.IsChecked = true;
                        break;
                    case 96:
                        initSize = true;
                        sz96.IsChecked = chkExtended.IsChecked = true;
                        break;
                    case 128:
                        initSize = true;
                        sz128.IsChecked = chkExtended.IsChecked = true;
                        break;
                    case 512:
                        initSize = true;
                        sz512.IsChecked = chkExtended.IsChecked = true;
                        break;
                }
            }
            if (!initSize)
            {
                CustomWidth = width;
                CustomHeight = height;
                szCust.IsChecked = true;
            }

            switch (bitDepth)
            {
                default: //Depth32BitsPerPixel
                    rad32bit.IsChecked = true;
                    SetValue(UseAlphaThresholdPropertyKey, false);
                    break;
                case IconBitDepth.Depth24BitsPerPixel:
                    rad24bit.IsChecked = true;
                    break;
                case IconBitDepth.Depth8BitsPerPixel:
                    rad8bit.IsChecked = true;
                    break;
                case IconBitDepth.Depth4BitsPerPixel:
                    rad4bit.IsChecked = true;
                    break;
                case IconBitDepth.Depth1BitPerPixel:
                    rad1bit.IsChecked = true;
                    break;
            }

            Mouse.OverrideCursor = null;
        }

        private MainWindow _mainWindow;
        public SettingsFile Settings { get { return _mainWindow.SettingsFile; } }

        public IconEntry GetIconEntry(bool quantize)
        {
            IconEntry entry;
            if (_savedEntry == null)
                entry = new IconEntry(LoadedImage, EntryWidth, EntryHeight, BitDepth);
            else if (_savedEntry.Width != EntryWidth || _savedEntry.Height != EntryHeight || _savedEntry.BitDepth != BitDepth)
                entry = _savedEntry.Clone(EntryWidth, EntryHeight, BitDepth);
            else if (quantize)
                entry = _savedEntry.Clone();
            else
                entry = _savedEntry;

            entry.AlphaThreshold = AlphaThreshold;
            entry.AlphaThresholdMode = AlphaThresholdMode;

            entry.ScalingFilter = (IconScalingFilter)cmbFilter.SelectedValue;
            if (quantize)
                entry.SetQuantized();
            return entry;
        }

        #region NewFile
        private static readonly DependencyPropertyKey NewFilePropertyKey = DependencyProperty.RegisterReadOnly(nameof(NewFile), typeof(bool), typeof(AddWindow),
            new PropertyMetadata());
        public static readonly DependencyProperty NewFileProperty = NewFilePropertyKey.DependencyProperty;

        public bool NewFile { get { return (bool)GetValue(NewFileProperty); } }
        #endregion

        #region UseAlphaThreshold
        private static readonly DependencyPropertyKey UseAlphaThresholdPropertyKey = DependencyProperty.RegisterReadOnly(nameof(UseAlphaThreshold), typeof(bool),
            typeof(AddWindow), new PropertyMetadata(true, UseAlphaThresholdChanged));

        private static void UseAlphaThresholdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                AddWindow a = (AddWindow)d;
                a.SetValue(AlphaThresholdProperty, a._curAlphaThreshold);
            }
            else
            {
                d.SetValue(AlphaThresholdProperty, IconEntry.DefaultAlphaThreshold32);
            }
        }

        public static readonly DependencyProperty UseAlphaThresholdProperty = UseAlphaThresholdPropertyKey.DependencyProperty;

        public bool UseAlphaThreshold { get { return (bool)GetValue(UseAlphaThresholdProperty); } }
        #endregion

        #region AlphaThreshold
        public static readonly DependencyProperty AlphaThresholdProperty = DependencyProperty.Register(nameof(AlphaThreshold), typeof(byte), typeof(AddWindow),
            new PropertyMetadata(IconEntry.DefaultAlphaThreshold, AlphaThresholdChanged));

        private static void AlphaThresholdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AddWindow a = (AddWindow)d;

            if (a.UseAlphaThreshold)
                a._curAlphaThreshold = (byte)e.NewValue;
        }

        private byte _curAlphaThreshold = IconEntry.DefaultAlphaThreshold;

        public byte AlphaThreshold
        {
            get { return (byte)GetValue(AlphaThresholdProperty); }
            set { SetValue(AlphaThresholdProperty, value); }
        }
        #endregion

        #region AlphaThresholdMode
        public static readonly DependencyProperty AlphaThresholdModePoperty = DependencyProperty.Register(nameof(AlphaThresholdMode), typeof(IconAlphaThresholdMode),
            typeof(AddWindow), new PropertyMetadata(IconAlphaThresholdMode.Darken));

        public IconAlphaThresholdMode AlphaThresholdMode
        {
            get { return (IconAlphaThresholdMode)GetValue(AlphaThresholdModePoperty); }
            set { SetValue(AlphaThresholdModePoperty, value); }
        }
        #endregion

        #region LoadedImage
        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register(nameof(LoadedImage), typeof(BitmapSource), typeof(AddWindow));

        public BitmapSource LoadedImage
        {
            get { return (BitmapSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }
        #endregion

        #region BitDepth
        private static readonly DependencyPropertyKey BitDepthPropertyKey = DependencyProperty.RegisterReadOnly(nameof(BitDepth), typeof(IconBitDepth), typeof(AddWindow),
            new PropertyMetadata(IconBitDepth.Depth32BitsPerPixel, BitDepthChanged));

        private static void BitDepthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AddWindow a = (AddWindow)d;

            switch ((IconBitDepth)e.NewValue)
            {
                default: //Depth32BitsPerPixel
                    a.rad32bit.IsChecked = true;
                    a.SetValue(UseAlphaThresholdPropertyKey, false);
                    break;
                case IconBitDepth.Depth24BitsPerPixel:
                    a.rad24bit.IsChecked = true;
                    a.SetValue(UseAlphaThresholdPropertyKey, true);
                    break;
                case IconBitDepth.Depth8BitsPerPixel:
                    a.rad8bit.IsChecked = true;
                    a.SetValue(UseAlphaThresholdPropertyKey, true);
                    break;
                case IconBitDepth.Depth4BitsPerPixel:
                    a.rad4bit.IsChecked = true;
                    a.SetValue(UseAlphaThresholdPropertyKey, true);
                    break;
                case IconBitDepth.Depth1BitsPerPixel:
                    a.rad1bit.IsChecked = true;
                    a.SetValue(UseAlphaThresholdPropertyKey, true);
                    break;
            }
        }

        public static readonly DependencyProperty BitDepthProperty = BitDepthPropertyKey.DependencyProperty;

        public IconBitDepth BitDepth
        {
            get { return (IconBitDepth)GetValue(BitDepthProperty); }
            private set { SetValue(BitDepthPropertyKey, value); }
        }
        #endregion

        #region ExtendedSizes
        public static readonly DependencyProperty ExtendedSizesProperty = DependencyProperty.Register(nameof(ExtendedSizes), typeof(bool), typeof(AddWindow),
            new PropertyMetadata(false));

        public bool ExtendedSizes
        {
            get { return (bool)GetValue(ExtendedSizesProperty); }
            set { SetValue(ExtendedSizesProperty, value); }
        }
        #endregion

        #region MatrixSelectedIndex
        public static readonly DependencyProperty MatrixSelectedIndexProperty = DependencyProperty.Register(nameof(MatrixSelectedIndex), typeof(int), typeof(AddWindow),
            new PropertyMetadata(0));

        public int MatrixSelectedIndex
        {
            get { return (int)GetValue(MatrixSelectedIndexProperty); }
            set { SetValue(MatrixSelectedIndexProperty, value); }
        }
        #endregion

        #region DifferentSize
        private static readonly DependencyPropertyKey DifferentSizePropertyKey = DependencyProperty.RegisterReadOnly(nameof(DifferentSize), typeof(bool), typeof(AddWindow),
            new PropertyMetadata(true, null, DifferentSizeCoerce));
        public static readonly DependencyProperty DifferentSizeProperty = DifferentSizePropertyKey.DependencyProperty;

        private static object DifferentSizeCoerce(DependencyObject d, object baseValue)
        {
            AddWindow a = (AddWindow)d;
            var image = a.LoadedImage;
            return image.PixelWidth != a.EntryWidth || image.PixelHeight != a.EntryHeight;
        }

        public bool DifferentSize { get { return (bool)GetValue(DifferentSizeProperty); } }
        #endregion

        private static bool SizeValidate(object value)
        {
            short val = (short)value;
            return val >= IconEntry.MinDimension && val <= IconEntry.MaxDimension;
        }

        private static void EntrySizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.CoerceValue(DifferentSizeProperty);
        }

        #region EntryWidth
        public static readonly DependencyProperty EntryWidthProperty = DependencyProperty.Register(nameof(EntryWidth), typeof(short), typeof(AddWindow),
            new PropertyMetadata((short)32), SizeValidate);

        public short EntryWidth
        {
            get { return (short)GetValue(EntryWidthProperty); }
            set { SetValue(EntryWidthProperty, value); }
        }
        #endregion

        #region EntryHeight
        public static readonly DependencyProperty EntryHeightProperty = DependencyProperty.Register(nameof(EntryHeight), typeof(short), typeof(AddWindow),
            new PropertyMetadata((short)32, EntrySizeChanged), SizeValidate);

        public short EntryHeight
        {
            get { return (short)GetValue(EntryHeightProperty); }
            set { SetValue(EntryHeightProperty, value); }
        }
        #endregion

        private static void CustomSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AddWindow a = (AddWindow)d;
            if (a.szCust.IsChecked.HasValue && a.szCust.IsChecked.Value)
            {
                a.EntryWidth = a.CustomWidth;
                a.EntryHeight = a.CustomHeight;
            }
        }

        #region CustomWidth
        public static readonly DependencyProperty CustomWidthProperty = DependencyProperty.Register(nameof(CustomWidth), typeof(short), typeof(AddWindow),
            new PropertyMetadata((short)32, CustomSizeChanged), SizeValidate);

        public short CustomWidth
        {
            get { return (short)GetValue(CustomWidthProperty); }
            set { SetValue(CustomWidthProperty, value); }
        }
        #endregion

        #region CustomHeight
        public static readonly DependencyProperty CustomHeightProperty = DependencyProperty.Register(nameof(CustomHeight), typeof(short), typeof(AddWindow),
            new PropertyMetadata((short)32, CustomSizeChanged), SizeValidate);

        public short CustomHeight
        {
            get { return (short)GetValue(CustomHeightProperty); }
            set { SetValue(CustomHeightProperty, value); }
        }
        #endregion

        private void bit_Checked(object sender, RoutedEventArgs e)
        {
            if (sender == rad24bit)
                BitDepth = IconBitDepth.Depth24BitsPerPixel;
            else if (sender == rad8bit)
                BitDepth = IconBitDepth.Depth8BitsPerPixel;
            else if (sender == rad4bit)
                BitDepth = IconBitDepth.Depth4BitsPerPixel;
            else if (sender == rad1bit)
                BitDepth = IconBitDepth.Depth1BitPerPixel;
            else
                BitDepth = IconBitDepth.Depth32BitsPerPixel;
        }

        private void size_Checked(object sender, RoutedEventArgs e)
        {
            SizeRadioButton rad = sender as SizeRadioButton;
            if (rad == null) return;
            EntryWidth = rad.EntryWidth;
            EntryHeight = rad.EntryHeight;
        }

        private void szCust_Checked(object sender, RoutedEventArgs e)
        {
            if (!szCust.IsChecked.HasValue || !szCust.IsChecked.Value)
                return;

            EntryWidth = CustomWidth;
            EntryHeight = CustomHeight;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            var file = _mainWindow.LoadedFile;

            var width = EntryWidth;
            var height = EntryHeight;
            var depth = BitDepth;

            if (depth != IconBitDepth.Depth32BitsPerPixel && (width > byte.MaxValue || height > byte.MaxValue))
            {
                ErrorWindow.Show(_mainWindow, this, _mainWindow.SettingsFile.LanguageFile.TooBigNonPng);
                return;
            }

            if (!NewFile && file != null && file.Entries.ContainsSimilar(width, height, depth))
            {
                ErrorWindow.Show(_mainWindow, this, string.Format(_mainWindow.SettingsFile.LanguageFile.ImageAddError,
                    IconEntry.GetBitsPerPixel(BitDepth), width, height));
                return;
            }

            DialogResult = true;
            Close();
        }

        #region Preview
        public static readonly RoutedCommand PreviewCommand = new RoutedCommand("Preview", typeof(AddWindow));

        private void Preview_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void Preview_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            PreviewWindow pWindow = new PreviewWindow(this);
            pWindow.ShowDialog();
        }
        #endregion

        #region SetAlpha
        public static readonly RoutedCommand SetAlphaCommand = new RoutedCommand("SetAlpha", typeof(AddWindow));

        private IconEntry _savedEntry;

        private void SetAlpha_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format(MainWindow.OpenImageFilter, _mainWindow.SettingsFile.LanguageFile.TypeImage,
                _mainWindow.SettingsFile.LanguageFile.TypeAll);

            bool? result = dialog.ShowDialog(this);
            if (!result.HasValue || !result.Value) return;

            BitmapSource alphaImage = MainWindow.LoadImage(dialog.FileName, _mainWindow, this);
            if (alphaImage == null)
                return;

            PreviewWindow prevWindow = new PreviewWindow(this, alphaImage);
            result = prevWindow.ShowDialog();
            if (!result.HasValue || !result.Value)
                return;

            _savedEntry = prevWindow.SourceEntry;
            LoadedImage = _savedEntry.GetCombinedAlpha();
        }
        #endregion
    }
}
