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

using Microsoft.Win32;

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

            _settings = new SettingsFile(this);

            Mouse.OverrideCursor = Cursors.Wait;
            _settings.Load();

            if (_settings.LanguageName == "")
                _settings.LanguageName = LanguageFile.DefaultShortName;

            _settings.Save();
            Mouse.OverrideCursor = null;
        }

        private SettingsFile _settings;
        [Bindable(true)]
        public SettingsFile SettingsFile { get { return _settings; } }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();
            args = new ArraySegment<string>(args, 1, args.Length - 1).ToArray();

            if (args.Length == 0) return;
            _load(args[0]);
        }

        private void _load(string path)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                LoadedFile = IconFileBase.Load(path, _errorHandler);
            }
            catch (IconLoadException e)
            {
                _errorHandler(e);
            }
            catch (Exception)
            {
                MessageBox.Show(this, string.Format(_settings.LanguageFile.ImageLoadError, path), _settings.LanguageFile.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        private void _errorHandler(IconLoadException e)
        {
            //TODO: List error codes.
            MessageBox.Show(this, _settings.LanguageFile.ImageLoadError, _settings.LanguageFile.Error, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #region LoadedFile
        public static DependencyProperty LoadedFileProperty = DependencyProperty.Register("LoadedFile", typeof(IconFileBase), typeof(MainWindow),
            new PropertyMetadata(null, LoadedFileChanged));

        private static void LoadedFileChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(IsFileLoadedPropertyKey, e.NewValue != null);
        }

        /// <summary>
        /// Gets and sets the currently-loaded icon file.
        /// </summary>
        public IconFileBase LoadedFile
        {
            get { return (IconFileBase)GetValue(LoadedFileProperty); }
            set { SetValue(LoadedFileProperty, value); }
        }
        #endregion

        #region IsFileLoaded
        private static readonly DependencyPropertyKey IsFileLoadedPropertyKey = DependencyProperty.RegisterReadOnly("IsFileLoaded", typeof(bool), typeof(MainWindow),
            new PropertyMetadata());
        public static readonly DependencyProperty IsFileLoadedProperty = IsFileLoadedPropertyKey.DependencyProperty;

        public bool IsFileLoaded { get { return (bool)GetValue(IsFileLoadedProperty); } }
        #endregion

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
        }

        private void window_Closed(object sender, EventArgs e)
        {
        }
    }
}
