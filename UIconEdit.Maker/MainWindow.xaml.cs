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
using System.IO;
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
                FilePath = path;
                IsModified = false;
                listbox.SelectedIndex = 0;
            }
            catch (IconLoadException e)
            {
                _errorHandler(e);
                if (e.Code == IconErrorCode.ZeroValidEntries)
                {
                    if (e.TypeCode == IconTypeCode.Cursor)
                        LoadedFile = new CursorFile();
                    else
                        LoadedFile = new IconFile();
                    FilePath = path;
                    IsModified = false;
                    listbox.Focus();
                }
            }
            catch (Exception)
            {
                ErrorWindow.Show(this, string.Format(_settings.LanguageFile.ImageLoadError, path));
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        private void _errorHandler(IconLoadException e)
        {
            string message = _settings.LanguageFile.GetErrorMessage(e);

            ErrorWindow.Show(this, message);
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

        #region IsModified
        private static readonly DependencyPropertyKey IsModifiedPropertyKey = DependencyProperty.RegisterReadOnly("IsModified", typeof(bool), typeof(MainWindow),
            new PropertyMetadata());
        public static readonly DependencyProperty IsModifiedProperty = IsModifiedPropertyKey.DependencyProperty;

        public bool IsModified
        {
            get { return (bool)GetValue(IsModifiedProperty); }
            private set { SetValue(IsModifiedPropertyKey, value); }
        }
        #endregion

        #region FilePath
        public static readonly DependencyProperty FilePathProperty = DependencyProperty.Register("FilePath", typeof(string), typeof(MainWindow));

        public string FilePath
        {
            get { return (string)GetValue(FilePathProperty); }
            set { SetValue(FilePathProperty, value); }
        }
        #endregion

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            string filePath = FilePath;
            if (filePath != null)
            {
                dialog.InitialDirectory = Path.GetDirectoryName(filePath);
                dialog.FileName = Path.GetFileName(filePath);
            }

            dialog.Filter = string.Format("{0} (*.ico)|*.ico|{1} (*.cur)|*.cur|{2} (*.ico, *.cur)|*.ico;*.cur|{3} (*.*)|*",
                _settings.LanguageFile.TypeIco, _settings.LanguageFile.TypeCur, _settings.LanguageFile.TypeIcoCur, _settings.LanguageFile.TypeAll);

            var result = dialog.ShowDialog(this);
            if (!result.HasValue || !result.Value) return;

            _load(dialog.FileName);
        }

        private void window_Closed(object sender, EventArgs e)
        {
            _settings.Save();
        }

        private void _save(bool saveAs)
        {
            IconFileBase loadedFile = LoadedFile;
            if (loadedFile == null) return;
            string filePath = FilePath;
            if (saveAs)
            {
                SaveFileDialog dialog = new SaveFileDialog();
                if (!string.IsNullOrWhiteSpace(filePath))
                {
                    dialog.InitialDirectory = Path.GetDirectoryName(filePath);
                    dialog.FileName = Path.GetFileName(filePath);
                }
                dialog.Filter = string.Format("{0} (*.ico)|*.ico|{1} (*.cur)|*.cur", _settings.LanguageFile.TypeIco, _settings.LanguageFile.TypeCur);

                if (loadedFile.ID == IconTypeCode.Cursor)
                    dialog.FilterIndex = 2;

                var result = dialog.ShowDialog(this);
                if (!result.HasValue || !result.Value) return;

                if (loadedFile.ID == IconTypeCode.Cursor && dialog.FilterIndex == 1)
                    loadedFile = loadedFile.CloneAsCursorFile();
                else if (loadedFile.ID == IconTypeCode.Icon && dialog.FilterIndex == 2)
                    loadedFile = loadedFile.CloneAsIconFile();
                filePath = dialog.FileName;
            }
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                loadedFile.Save(filePath);
                System.Threading.Thread.Sleep(250);
            }
            catch (Exception)
            {
                ErrorWindow.Show(this, string.Format(_settings.LanguageFile.ImageSaveError, filePath));
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = IsModified && LoadedFile != null && LoadedFile.Entries.Count > 0 && LoadedFile.Entries.Count <= ushort.MaxValue;
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _save(string.IsNullOrWhiteSpace(FilePath));
        }

        private void SaveAs_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = LoadedFile != null && LoadedFile.Entries.Count > 0 && LoadedFile.Entries.Count <= ushort.MaxValue;
        }

        private void SaveAs_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _save(true);
        }

        private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }
    }
}
