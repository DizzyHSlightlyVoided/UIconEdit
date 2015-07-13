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

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Input;

namespace UIconEdit.Maker
{
    /// <summary>
    /// Interaction logic for PreviewWindow.xaml
    /// </summary>
    partial class PreviewWindow
    {
        public PreviewWindow(AddWindow owner)
        {
            Owner = owner;
            SourceImage = owner.GetIconEntry().CombineAlpha();
            InitializeComponent();
        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.SelectZoom(this, scrollImage, SourceImage, ZoomProperty, ZoomedWidthPropertyKey, ZoomedHeightPropertyKey, ZoomScaleModePropertyKey);
            Mouse.OverrideCursor = null;
        }

        #region SourceImage
        public static readonly DependencyProperty SourceImageProperty = DependencyProperty.Register("SourceImage", typeof(BitmapSource), typeof(PreviewWindow));

        public BitmapSource SourceImage
        {
            get { return (BitmapSource)GetValue(SourceImageProperty); }
            set { SetValue(SourceImageProperty, value); }
        }
        #endregion

        private void _zoomSet()
        {
            MainWindow.ZoomSet(this, SourceImage, ZoomProperty, ZoomedWidthPropertyKey, ZoomedHeightPropertyKey, ZoomScaleModePropertyKey);
        }

        #region Zoom
        public static readonly DependencyProperty ZoomProperty = DependencyProperty.Register("Zoom", typeof(int), typeof(PreviewWindow),
            new PropertyMetadata(100, ZoomChanged), MainWindow.ZoomValidate);

        private static void ZoomChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PreviewWindow m = (PreviewWindow)d;
            m._zoomSet();
        }

        public int Zoom
        {
            get { return (int)GetValue(ZoomProperty); }
            set { SetValue(ZoomProperty, value); }
        }
        #endregion

        #region ZoomedWidth
        private static readonly DependencyPropertyKey ZoomedWidthPropertyKey = DependencyProperty.RegisterReadOnly("ZoomedWidth", typeof(double),
            typeof(PreviewWindow), new PropertyMetadata());
        public static readonly DependencyProperty ZoomedWidthProperty = ZoomedWidthPropertyKey.DependencyProperty;

        public double ZoomedWidth { get { return (double)GetValue(ZoomedWidthProperty); } }
        #endregion

        #region ZoomedHeight
        private static readonly DependencyPropertyKey ZoomedHeightPropertyKey = DependencyProperty.RegisterReadOnly("ZoomedHeight", typeof(double),
            typeof(PreviewWindow), new PropertyMetadata());
        public static readonly DependencyProperty ZoomedHeightProperty = ZoomedHeightPropertyKey.DependencyProperty;

        public double ZoomedHeight { get { return (double)GetValue(ZoomedHeightProperty); } }
        #endregion

        #region ZoomScaleMode
        private static readonly DependencyPropertyKey ZoomScaleModePropertyKey = DependencyProperty.RegisterReadOnly("ZoomScaleMode",
            typeof(BitmapScalingMode), typeof(PreviewWindow), new PropertyMetadata());
        public static readonly DependencyProperty ZoomScaleModeProperty = ZoomScaleModePropertyKey.DependencyProperty;

        public BitmapScalingMode ZoomScaleMode { get { return (BitmapScalingMode)GetValue(ZoomScaleModeProperty); } }
        #endregion

        private void cmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            SourceImage = ((AddWindow)Owner).GetIconEntry().CombineAlpha();
            Mouse.OverrideCursor = null;
        }
    }
}
