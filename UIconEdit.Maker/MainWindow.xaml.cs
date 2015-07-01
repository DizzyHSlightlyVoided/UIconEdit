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
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace UIconEdit.Maker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            _settings = new SettingsFile();
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                _settings.Load();
            }
            catch (Exception)
            {
                MessageBox.Show(this, _settings.LanguageFile.SettingsLoadError, _settings.LanguageFile.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        private SettingsFile _settings;
        [Bindable(true)]
        public SettingsFile SettingsFile { get { return _settings; } }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();
            args = new ArraySegment<string>(args, 1, args.Length - 1).ToArray();

            if (args.Length == 0) return;
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                LoadedFile = IconFileBase.Load(args[0]);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        #region LoadedFileProperty
        public static DependencyProperty LoadedFileProperty = DependencyProperty.Register("LoadedFile", typeof(IconFileBase), typeof(MainWindow),
            new PropertyMetadata(null, LoadedFileChanged));

        private static void LoadedFileChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(IsFileLoadedPropertyKey, e.NewValue != null);
        }
        #endregion

        #region LoadedFile
        /// <summary>
        /// Gets and sets the currently-loaded icon file.
        /// </summary>
        public IconFileBase LoadedFile
        {
            get { return (IconFileBase)GetValue(LoadedFileProperty); }
            set { SetValue(LoadedFileProperty, value); }
        }

        private static readonly DependencyPropertyKey IsFileLoadedPropertyKey = DependencyProperty.RegisterReadOnly("IsFileLoaded", typeof(bool), typeof(MainWindow),
            new PropertyMetadata());
        public static readonly DependencyProperty IsFileLoadedProperty = IsFileLoadedPropertyKey.DependencyProperty;

        public bool IsFileLoaded { get { return (bool)GetValue(IsFileLoadedProperty); } }
        #endregion
    }
}
