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
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UIconEdit.Maker
{
    /// <summary>
    /// Interaction logic for ErrorWindow.xaml
    /// </summary>
    partial class ErrorWindow
    {
        public ErrorWindow(MainWindow mainWindow, string message)
        {
            _errorIcon = System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(System.Drawing.SystemIcons.Error.Handle,
               Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            _message = message;
            _mainWindow = mainWindow;
            InitializeComponent();
            Owner = mainWindow;
        }

        private MainWindow _mainWindow;

        [Bindable(true)]
        public LanguageFile LanguageFile { get { return _mainWindow.SettingsFile.LanguageFile; } }

        private BitmapSource _errorIcon;
        [Bindable(true)]
        public BitmapSource ErrorIcon { get { return _errorIcon; } }

        private string _message;
        [Bindable(true)]
        public string Message { get { return _message; } }

        public static bool? Show(MainWindow mainWindow, string message)
        {
            ErrorWindow errorWindow = new ErrorWindow(mainWindow, message);
            return errorWindow.ShowDialog();
        }
    }
}
