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

using System.ComponentModel;
using System.Media;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace UIconEdit.Maker
{
    /// <summary>
    /// Interaction logic for ErrorWindow.xaml
    /// </summary>
    partial class ErrorWindow
    {
        public ErrorWindow(MainWindow mainWindow, string message)
        {
            _mainWindow = mainWindow;
            _errorIcon = System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(System.Drawing.SystemIcons.Error.Handle,
               Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            _message = message;
            InitializeComponent();
        }

        private MainWindow _mainWindow;
        [Bindable(true)]
        public MainWindow MainWindow { get { return _mainWindow; } }

        private BitmapSource _errorIcon;
        [Bindable(true)]
        public BitmapSource ErrorIcon { get { return _errorIcon; } }

        private string _message;
        [Bindable(true)]
        public string Message { get { return _message; } }

        public static bool? Show(MainWindow mainWindow, string message)
        {
            return Show(mainWindow, mainWindow, message);
        }

        public static bool? Show(MainWindow mainWindow, Window owner, string message)
        {
            Mouse.OverrideCursor = null;
            ErrorWindow errorWindow = new ErrorWindow(mainWindow, message);
            errorWindow.Owner = owner;
            return errorWindow.ShowDialog();
        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            SystemSounds.Hand.Play();
        }
    }
}
