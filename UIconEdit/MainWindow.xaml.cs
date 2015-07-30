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
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using Microsoft.Win32;

namespace UIconEdit.Maker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    partial class MainWindow
    {
        private static int[] _zooms = new int[] { 3200, 2400, 2000, 1600, 1200, 1000, 800, 600, 400, 300, 200, 150, 100, 50, 25 };

        public static int[] Zooms { get { return _zooms; } }

        public MainWindow()
        {
            _settings = new SettingsFile(this);

            InitializeComponent();

            Mouse.OverrideCursor = Cursors.Wait;
            _settings.Load();

            if (_settings.LanguageName == "")
                _settings.LanguageName = LanguageFile.DefaultShortName;

            _settings.Save();

            SetValue(FilePathStatusPropertyKey, _settings.LanguageFile.FilePathNew);

            Mouse.OverrideCursor = null;
        }

        private SettingsFile _settings;
        [Bindable(true)]
        public SettingsFile SettingsFile { get { return _settings; } }

        private void window_ContentRendered(object sender, EventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();
            args = new ArraySegment<string>(args, 1, args.Length - 1).ToArray();

            if (args.Length == 0) return;
            _load(args[0]);
        }

        private void _load(string path)
        {
            bool oldModified = IsModified;
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                LoadedFile = IconFileBase.Load(path, _errorHandler);
                FilePath = path;
                listbox.SelectedIndex = 0;
                Mouse.OverrideCursor = null;
                scrollEntries.ScrollToTop();
                return;
            }
            catch (IconLoadException e)
            {
                if (e.Code != IconErrorCode.InvalidFormat)
                {
                    _errorHandler(e);
                    if (e.Code == IconErrorCode.ZeroValidEntries)
                    {
                        if (e.TypeCode == IconTypeCode.Cursor)
                            LoadedFile = new CursorFile();
                        else
                            LoadedFile = new IconFile();
                        FilePath = path;
                        listbox.SelectedIndex = 0;
                    }
                    else IsModified = oldModified;

                    Mouse.OverrideCursor = null;
                    scrollEntries.ScrollToTop();
                    return;
                }
            }
            catch (FileFormatException)
            {
            }
            catch (Exception)
            {
                ErrorWindow.Show(this, string.Format(_settings.LanguageFile.ImageLoadError, path));
                return;
            }

            try
            {
                BitmapSource bmpSource;
                using (FileStream fs = File.OpenRead(path))
                    bmpSource = new WriteableBitmap(BitmapFrame.Create(fs));

                Mouse.OverrideCursor = null;
                AddWindow addWindow = new AddWindow(this, false, true, bmpSource, IconBitDepth.Depth32BitsPerPixel);
                bool? result = addWindow.ShowDialog();

                if (result.HasValue && result.Value)
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    FilePath = null;
                    LoadedFile = new CursorFile();
                    LoadedFile.Entries.Add(addWindow.GetIconEntry());
                    listbox.SelectedIndex = 0;
                    IsModified = true;
                    scrollEntries.ScrollToTop();
                }
                Mouse.OverrideCursor = null;
                return;
            }
            catch (Exception)
            {
            }

            try
            {
                int iconCount = IconExtraction.ExtractIconCount(path);

                if (iconCount == 0)
                    throw new InvalidDataException();

                using (ExtractWindow extractWindow = new ExtractWindow(this, path, iconCount))
                {
                    bool? result = extractWindow.ShowDialog();
                    if (!result.HasValue || !result.Value) return;
                    Mouse.OverrideCursor = Cursors.Wait;
                    try
                    {
                        LoadedFile = IconExtraction.ExtractIconSingle(path, extractWindow.IconIndex);
                    }
                    catch
                    {
                        ErrorWindow.Show(this, string.Format(_settings.LanguageFile.IconExtractError, extractWindow.IconIndex, path));
                        return;
                    }
                }
                IsModified = true;
                FilePath = null;
                listbox.SelectedIndex = 0;
                scrollEntries.ScrollToTop();
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
            IsModified = true;
            ErrorWindow.Show(this, message);
            Mouse.OverrideCursor = Cursors.Wait;
        }

        #region LoadedFile
        public static readonly DependencyProperty LoadedFileProperty = DependencyProperty.Register("LoadedFile", typeof(IconFileBase), typeof(MainWindow),
            new PropertyMetadata(null, LoadedFileChanged));

        private static void LoadedFileChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MainWindow m = (MainWindow)d;
            if (e.OldValue != null)
                ((IDisposable)e.OldValue).Dispose();

            m.SetValue(IsFileLoadedPropertyKey, e.NewValue != null);
            m.SetValue(IsModifiedPropertyKey, false);
            if (e.NewValue == null || ((IconFileBase)e.NewValue).Entries.Count == 0)
            {
                m.listbox.SelectedIndex = -1;
                m.SetValue(IsLoadedAndSelectedPropertyKey, false);
            }
            else
            {
                m.listbox.SelectedIndex = 0;
                m.listbox.Focus();
            }
            GC.Collect();
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
        public static readonly DependencyProperty FilePathProperty = DependencyProperty.Register("FilePath", typeof(string), typeof(MainWindow),
            new PropertyMetadata(null, FilePathChanged));

        private static void FilePathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MainWindow w = (MainWindow)d;
            w.SetValue(FilePathStatusPropertyKey, (string)e.NewValue ?? w._settings.LanguageFile.FilePathNew);
        }

        public string FilePath
        {
            get { return (string)GetValue(FilePathProperty); }
            set { SetValue(FilePathProperty, value); }
        }
        #endregion

        #region FilePathStatus
        private static readonly DependencyPropertyKey FilePathStatusPropertyKey = DependencyProperty.RegisterReadOnly("FilePathStatus", typeof(string), typeof(MainWindow),
            new PropertyMetadata());
        public static DependencyProperty FilePathStatusProperty = FilePathStatusPropertyKey.DependencyProperty;

        public string FilePathStatus { get { return (string)GetValue(FilePathStatusProperty); } }
        #endregion

        #region IsLoadedAndSelected
        private static readonly DependencyPropertyKey IsLoadedAndSelectedPropertyKey = DependencyProperty.RegisterReadOnly("IsLoadedAndSelected",
            typeof(bool), typeof(MainWindow), new PropertyMetadata(false));
        public static readonly DependencyProperty IsLoadedAndSelectedProperty = IsLoadedAndSelectedPropertyKey.DependencyProperty;

        public bool IsLoadedAndSelected { get { return (bool)GetValue(IsLoadedAndSelectedProperty); } }
        #endregion

        #region ShowMouseCoords
        private static readonly DependencyPropertyKey ShowMouseCoordsPropertyKey = DependencyProperty.RegisterReadOnly("ShowMouseCoords",
            typeof(bool), typeof(MainWindow), new PropertyMetadata(false));
        public static readonly DependencyProperty ShowMouseCoordsProperty = ShowMouseCoordsPropertyKey.DependencyProperty;

        public bool ShowMouseCoords { get { return (bool)GetValue(ShowMouseCoordsProperty); } }
        #endregion

        #region MousePosX
        private static readonly DependencyPropertyKey MousePosXPropertyKey = DependencyProperty.RegisterReadOnly("MousePosX", typeof(int),
            typeof(MainWindow), new PropertyMetadata());
        public static readonly DependencyProperty MousePosXProperty = MousePosXPropertyKey.DependencyProperty;

        public int MousePosX { get { return (int)GetValue(MousePosXProperty); } }
        #endregion

        #region MousePosY
        private static readonly DependencyPropertyKey MousePosYPropertyKey = DependencyProperty.RegisterReadOnly("MousePosY", typeof(int),
            typeof(MainWindow), new PropertyMetadata());
        public static readonly DependencyProperty MousePosYProperty = MousePosYPropertyKey.DependencyProperty;

        public int MousePosY { get { return (int)GetValue(MousePosYProperty); } }
        #endregion

        internal static void ZoomSet(Window w, BitmapSource image, DependencyProperty ZoomProperty, DependencyPropertyKey ZoomedWidthPropertyKey,
            DependencyPropertyKey ZoomedHeightPropertyKey, DependencyPropertyKey ZoomScaleModePropertyKey)
        {
            int val = (int)w.GetValue(ZoomProperty);
            w.SetValue(ZoomedWidthPropertyKey, Math.Floor(image.PixelWidth * (val / 100.0)));
            w.SetValue(ZoomedHeightPropertyKey, Math.Floor(image.PixelHeight * (val / 100.0)));
            w.SetValue(ZoomScaleModePropertyKey, val >= 100 ? BitmapScalingMode.NearestNeighbor : BitmapScalingMode.HighQuality);
        }

        private void _zoomSet()
        {
            ZoomSet(this, ((IconEntry)listbox.SelectedItem).BaseImage, ZoomProperty, ZoomedWidthPropertyKey, ZoomedHeightPropertyKey, ZoomScaleModePropertyKey);
        }

        #region Zoom
        public static readonly DependencyProperty ZoomProperty = DependencyProperty.Register("Zoom", typeof(int), typeof(MainWindow),
            new PropertyMetadata(100, ZoomChanged), ZoomValidate);

        private static void ZoomChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MainWindow m = (MainWindow)d;
            m._zoomSet();
        }

        internal static bool ZoomValidate(object value)
        {
            return (int)value > 0;
        }

        public int Zoom
        {
            get { return (int)GetValue(ZoomProperty); }
            set { SetValue(ZoomProperty, value); }
        }
        #endregion

        #region ZoomedWidth
        private static readonly DependencyPropertyKey ZoomedWidthPropertyKey = DependencyProperty.RegisterReadOnly("ZoomedWidth", typeof(double),
            typeof(MainWindow), new PropertyMetadata());
        public static readonly DependencyProperty ZoomedWidthProperty = ZoomedWidthPropertyKey.DependencyProperty;

        public double ZoomedWidth { get { return (double)GetValue(ZoomedWidthProperty); } }
        #endregion

        #region ZoomedHeight
        private static readonly DependencyPropertyKey ZoomedHeightPropertyKey = DependencyProperty.RegisterReadOnly("ZoomedHeight", typeof(double),
            typeof(MainWindow), new PropertyMetadata());
        public static readonly DependencyProperty ZoomedHeightProperty = ZoomedHeightPropertyKey.DependencyProperty;

        public double ZoomedHeight { get { return (double)GetValue(ZoomedHeightProperty); } }
        #endregion

        #region ZoomScaleMode
        private static readonly DependencyPropertyKey ZoomScaleModePropertyKey = DependencyProperty.RegisterReadOnly("ZoomScaleMode",
            typeof(BitmapScalingMode), typeof(MainWindow), new PropertyMetadata());
        public static readonly DependencyProperty ZoomScaleModeProperty = ZoomScaleModePropertyKey.DependencyProperty;

        public BitmapScalingMode ZoomScaleMode { get { return (BitmapScalingMode)GetValue(ZoomScaleModeProperty); } }
        #endregion

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (_checkModified())
                return;

            OpenFileDialog dialog = new OpenFileDialog();
            string filePath = FilePath;
            if (filePath != null)
            {
                dialog.InitialDirectory = Path.GetDirectoryName(filePath);
                dialog.FileName = Path.GetFileName(filePath);
            }

            dialog.Filter = string.Format("{0} (*.ico)|*.ico|{1} (*.cur)|*.cur|{2} (*.ico, *.cur)|*.ico;*.cur|" +
                "{3}|*.gif;*.png;*.bmp;*.dib;*.tif;*.tiff;*.jpg;*.jpeg|{4} (*.*)|*",
                _settings.LanguageFile.TypeIco, _settings.LanguageFile.TypeCur, _settings.LanguageFile.TypeIcoCur, _settings.LanguageFile.TypeImage,
                _settings.LanguageFile.TypeAll);

            {
                var loadedFile = LoadedFile;
                if (filePath != null && loadedFile != null && loadedFile.ID == IconTypeCode.Cursor)
                    dialog.FilterIndex = 2;
            }

            var result = dialog.ShowDialog(this);
            if (!result.HasValue || !result.Value) return;

            _load(dialog.FileName);
        }

        private void window_Closed(object sender, EventArgs e)
        {
            _settings.Save();
            LoadedFile = null;
        }

        private bool _save(bool saveAs)
        {
            IconFileBase loadedFile = LoadedFile;
            if (loadedFile == null) return false;
            string filePath = FilePath;
            if (saveAs || string.IsNullOrWhiteSpace(filePath))
            {
                SaveFileDialog dialog = new SaveFileDialog();
                if (!string.IsNullOrWhiteSpace(filePath))
                {
                    dialog.InitialDirectory = Path.GetDirectoryName(filePath);
                    dialog.FileName = Path.GetFileName(filePath);
                }
                dialog.Filter = string.Format("{0} (*.ico)|*.ico|{1} (*.cur)|*.cur", _settings.LanguageFile.TypeIco, _settings.LanguageFile.TypeCur);

                if (loadedFile.ID == IconTypeCode.Cursor && filePath != null)
                    dialog.FilterIndex = 2;

                var result = dialog.ShowDialog(this);
                if (!result.HasValue || !result.Value) return false;

                if (loadedFile.ID == IconTypeCode.Cursor && dialog.FilterIndex == 1)
                    loadedFile = loadedFile.CloneAsIconFile();
                else if (loadedFile.ID == IconTypeCode.Icon && dialog.FilterIndex == 2)
                    loadedFile = loadedFile.CloneAsCursorFile();
                filePath = dialog.FileName;
            }
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                loadedFile.Save(filePath);
                if (loadedFile.ID != LoadedFile.ID)
                {
                    int dex = listbox.SelectedIndex;
                    LoadedFile = loadedFile;
                    listbox.SelectedIndex = dex;
                }
                System.Threading.Thread.Sleep(100);
                IsModified = false;
                FilePath = filePath;
                return true;
            }
            catch (Exception)
            {
                ErrorWindow.Show(this, string.Format(_settings.LanguageFile.ImageSaveError, filePath));
                return false;
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = IsModified && LoadedFile != null && LoadedFile.Entries.Count > 0 && LoadedFile.Entries.Count <= ushort.MaxValue;
            e.Handled = true;
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _save(false);
        }

        private void SaveAs_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = LoadedFile != null && LoadedFile.Entries.Count > 0 && LoadedFile.Entries.Count <= ushort.MaxValue;
            e.Handled = true;
        }

        private void SaveAs_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _save(true);
        }

        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (_checkModified()) return;
            LoadedFile = new CursorFile();
        }

        public static readonly RoutedCommand AddCommand = new RoutedCommand("Add", typeof(MainWindow));

        private void Add_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = IsFileLoaded;
            e.Handled = true;
        }

        private void _add(AddWindow addWindow)
        {
            bool? result = addWindow.ShowDialog();

            if (!result.HasValue || !result.Value) return;
            Mouse.OverrideCursor = Cursors.Wait;
            var newEntry = addWindow.GetIconEntry();
            int dex = ~LoadedFile.Entries.BinarySearchSimilar(newEntry);
            LoadedFile.Entries.Insert(dex, newEntry);
            listbox.SelectedIndex = dex;
            IsModified = true;
            listbox.ScrollIntoView(listbox.SelectedItem);
            Mouse.OverrideCursor = null;
        }

        private void Add_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format("{0}|*.gif;*.png;*.bmp;*.dib;*.tif;*.tiff;*.jpg;*.jpeg|{1} (*.*)|*",
                _settings.LanguageFile.TypeImage, _settings.LanguageFile.TypeAll);

            bool? result = dialog.ShowDialog(this);
            if (!result.HasValue || !result.Value) return;

            BitmapSource bmpSource;
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                using (FileStream fs = File.OpenRead(dialog.FileName))
                    bmpSource = new WriteableBitmap(BitmapFrame.Create(fs));
            }
            catch
            {
                ErrorWindow.Show(this, string.Format(_settings.LanguageFile.ImageLoadError, dialog.FileName));
                return;
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }

            _add(new AddWindow(this, false, false, bmpSource, IconBitDepth.Depth32BitsPerPixel));
        }

        public static readonly RoutedCommand DuplicateCommand = new RoutedCommand("Duplicate", typeof(MainWindow));

        private void Duplicate_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = IsLoadedAndSelected;
            e.Handled = true;
        }

        private void Duplicate_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IconEntry entry = (IconEntry)(listbox.SelectedItem);

            BitmapSource bmpSource = AlphaImageConverter.Convert(entry);

            _add(new AddWindow(this, true, false, bmpSource, entry.BitDepth));
        }

        public static readonly RoutedCommand RemoveCommand = new RoutedCommand("Duplicate", typeof(MainWindow));

        private void Remove_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            QuestionWindow window = new QuestionWindow(this, _settings.LanguageFile.RemoveMessage, _settings.LanguageFile.RemoveCaption);
            window.ButtonOKEnabled = false;
            window.ButtonCancelEnabled = false;
            window.ButtonYesEnabled = true;
            window.ButtonNoEnabled = true;
            bool? result = window.ShowDialog();
            if (!result.HasValue || !result.Value || window.Result == MessageBoxResult.No) return;
            LoadedFile.Entries.RemoveAt(listbox.SelectedIndex);
            IsModified = true;
        }

        public static readonly RoutedCommand ExportCommand = new RoutedCommand("Export", typeof(MainWindow));

        private void Export_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IconEntry entry = ((IconEntry)listbox.SelectedItem);
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = string.Format("{0} (*.png)|*.png", _settings.LanguageFile.TypePng);
            string filePath = FilePath;
            if (!string.IsNullOrWhiteSpace(filePath))
                dialog.FileName = Path.GetFileNameWithoutExtension(filePath) + "-";
            dialog.FileName += string.Format(_settings.LanguageFile.FilenameSuffix, entry.BitsPerPixel, entry.Width, entry.Height);

            bool? result = dialog.ShowDialog(this);

            if (!result.HasValue || !result.Value) return;

            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(AlphaImageConverter.Convert(entry)));

                using (FileStream fs = File.Open(dialog.FileName, FileMode.Create))
                    encoder.Save(fs);
                System.Threading.Thread.Sleep(100);
            }
            catch
            {
                ErrorWindow.Show(this, string.Format(_settings.LanguageFile.ImageSaveError, dialog.FileName));
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        public static readonly RoutedCommand ExportAllCommand = new RoutedCommand("ExportAll", typeof(MainWindow));

        private void ExportAll_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.AddExtension = false;
            dialog.Filter = string.Format("{0} (*.png)|*", _settings.LanguageFile.TypePngSuffix);
            string filePath = FilePath;
            if (!string.IsNullOrWhiteSpace(filePath))
                dialog.FileName = Path.GetFileNameWithoutExtension(filePath);

            Tuple<string, IconEntry>[] entries = { };

            bool unSelected = true;
            while (unSelected)
            {
                bool? result = dialog.ShowDialog(this);

                if (!result.HasValue || !result.Value) return;

                unSelected = false;

                const string png = ".png";

                filePath = dialog.FileName.Trim();
                if (filePath.EndsWith(png, StringComparison.OrdinalIgnoreCase))
                    filePath = filePath.Substring(0, filePath.Length - 4);

                Dictionary<IconEntryKey, int> entryCount = new Dictionary<IconEntryKey, int>();

                entries = LoadedFile.Entries.Select(delegate (IconEntry curEntry)
                {
                    string curPath = string.Format(_settings.LanguageFile.FilenameSuffix, curEntry.BitsPerPixel, curEntry.Width, curEntry.Height);
                    var curKey = curEntry.EntryKey;

                    int curCount;
                    if (entryCount.TryGetValue(curKey, out curCount))
                    {
                        curPath += string.Format(_settings.LanguageFile.FilenameSuffixEx, curCount);
                        entryCount[curKey] = curCount + 1;
                    }
                    else entryCount.Add(curKey, 0);

                    return new Tuple<string, IconEntry>(filePath + curPath + png, curEntry);
                }).ToArray();

                bool overwriteAll = false;
                foreach (var curTuple in entries)
                {
                    if (unSelected || overwriteAll) break;

                    if (File.Exists(curTuple.Item1))
                    {
                        QuestionWindow questionWindow = new QuestionWindow(this, string.Format(_settings.LanguageFile.OverwriteMessage, curTuple.Item1),
                            _settings.LanguageFile.OverwriteCaption);
                        questionWindow.ButtonYesEnabled = true;
                        questionWindow.ButtonOKEnabled = true;
                        questionWindow.ButtonOKMessage = _settings.LanguageFile.ButtonOverwrite;
                        questionWindow.ButtonNoEnabled = true;
                        questionWindow.ButtonCancelEnabled = true;

                        result = questionWindow.ShowDialog();

                        switch (questionWindow.Result)
                        {
                            default: //MessageBoxResult.Yes
                                break;
                            case MessageBoxResult.OK:
                                overwriteAll = true;
                                break;
                            case MessageBoxResult.No:
                                unSelected = true;
                                break;
                            case MessageBoxResult.Cancel:
                                return;
                        }
                    }
                }
            }

            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                foreach (var curTuple in entries)
                {
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(AlphaImageConverter.Convert(curTuple.Item2)));

                    filePath = curTuple.Item1;

                    using (FileStream fs = File.Open(filePath, FileMode.Create))
                        encoder.Save(fs);
                }

                System.Threading.Thread.Sleep(100);
            }
            catch
            {
                ErrorWindow.Show(this, string.Format(_settings.LanguageFile.ImageSaveError, filePath));
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        public static readonly RoutedCommand SettingsCommand = new RoutedCommand("Settings", typeof(MainWindow));

        public void Settings_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SettingsWindow window = new SettingsWindow(this);
            var result = window.ShowDialog();

            if (!result.HasValue || !result.Value)
                _settings.Reset();

            _settings.Save();
        }

        private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private bool _checkModified()
        {
            if (!IsModified) return false;

            QuestionWindow window = new QuestionWindow(this, _settings.LanguageFile.ModifiedMessage, _settings.LanguageFile.ModifiedCaption);
            window.ButtonYesEnabled = true;
            window.ButtonYesMessage = _settings.LanguageFile.MenuFileSave;
            if (FilePath == null) window.ButtonOKEnabled = false;
            else
            {
                window.ButtonOKEnabled = true;
                window.ButtonOKMessage = _settings.LanguageFile.MenuFileSaveAs;
            }
            window.ButtonNoEnabled = true;
            window.ButtonNoMessage = _settings.LanguageFile.ButtonNoSave;
            window.ButtonCancelEnabled = true;
            window.ShowDialog();
            if (window.Result == MessageBoxResult.Cancel) return true;
            else if (window.Result == MessageBoxResult.No) return false;

            return !_save(window.Result == MessageBoxResult.OK);
        }

        private void listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_settings.KeepHotspotChecked)
                chkHotspot.IsChecked = false;
            if (listbox.SelectedIndex < 0)
            {
                SetValue(IsLoadedAndSelectedPropertyKey, false);
                return;
            }

            SetValue(IsLoadedAndSelectedPropertyKey, true);
            var image = ((IconEntry)listbox.SelectedItem).BaseImage;

            SelectZoom(this, scrollImage, image, ZoomProperty, ZoomedWidthPropertyKey, ZoomedHeightPropertyKey, ZoomScaleModePropertyKey);
        }

        internal static void SelectZoom(Window window, ScrollViewer scrollImage, BitmapSource image, DependencyProperty ZoomProperty,
            DependencyPropertyKey ZoomedWidthPropertyKey, DependencyPropertyKey ZoomedHeightPropertyKey, DependencyPropertyKey ZoomScaleModePropertyKey)
        {
            const double padding = 24;

            double multiplier;
            double width = scrollImage.ActualWidth - padding, height = scrollImage.ActualHeight - padding;

            if (image.PixelWidth > width || image.PixelHeight > height)
            {
                multiplier = Math.Min(width / image.PixelWidth, height / image.PixelHeight);
            }
            else
            {
                multiplier = Math.Floor(Math.Min(width / image.PixelWidth, height / image.PixelHeight));
            }

            int newZoom = (int)(multiplier * 100);
            if ((int)window.GetValue(ZoomProperty) == newZoom)
                ZoomSet(window, image, ZoomProperty, ZoomedWidthPropertyKey, ZoomedHeightPropertyKey, ZoomScaleModePropertyKey);
            else
                window.SetValue(ZoomProperty, newZoom);
        }

        private void window_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = _checkModified();
        }

        private void NavBack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = IsFileLoaded && LoadedFile.Entries.Count != 0 && listbox.SelectedIndex != 0;
            e.Handled = true;
        }

        private void FirstPage_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            listbox.SelectedIndex = 0;
            listbox.ScrollIntoView(listbox.SelectedItem);
        }

        private void PreviousPage_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            listbox.SelectedIndex--;
            listbox.ScrollIntoView(listbox.SelectedItem);
        }

        private void NavForward_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = IsFileLoaded && LoadedFile.Entries.Count != 0 && listbox.SelectedIndex < listbox.Items.Count - 1;
            e.Handled = true;
        }

        private void NextPage_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            listbox.SelectedIndex++;
            listbox.ScrollIntoView(listbox.SelectedItem);
        }

        private void LastPage_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            listbox.SelectedIndex = listbox.Items.Count - 1;
            listbox.ScrollIntoView(listbox.SelectedIndex);
        }

        private void _setMousePos(MouseEventArgs e)
        {
            var position = e.GetPosition(imgIcon);

            IconEntry curEntry = listbox.SelectedItem as IconEntry;

            SetValue(MousePosXPropertyKey, (int)(position.X * curEntry.Width / ZoomedWidth));
            SetValue(MousePosYPropertyKey, (int)(position.Y * curEntry.Height / ZoomedHeight));

        }

        private void imgIcon_MouseMove(object sender, MouseEventArgs e)
        {
            _setMousePos(e);
            if (e.LeftButton == MouseButtonState.Released || !chkHotspot.IsEnabled || !chkHotspot.IsChecked.HasValue || !chkHotspot.IsChecked.Value)
            {
                listbox.Focus();
                return;
            }

            var position = e.GetPosition(imgIcon);

            IconEntry curEntry = listbox.SelectedItem as IconEntry;

            curEntry.HotspotX = (int)(position.X * curEntry.Width / ZoomedWidth);
            curEntry.HotspotY = (int)(position.Y * curEntry.Height / ZoomedHeight);

            IsModified = true;
            listbox.Focus();
        }

        private void imgIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            imgIcon_MouseMove(sender, e);
            listbox.Focus();
        }

        private void imgIcon_MouseEnter(object sender, MouseEventArgs e)
        {
            SetValue(ShowMouseCoordsPropertyKey, true);
            _setMousePos(e);
        }

        private void imgIcon_MouseLeave(object sender, MouseEventArgs e)
        {
            SetValue(ShowMouseCoordsPropertyKey, false);
        }

        private void listbox_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollEntries.ScrollToVerticalOffset(scrollEntries.VerticalOffset - e.Delta / 2);
            e.Handled = true;
        }
    }
}
