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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace UIconEdit.Maker
{
    /// <summary>
    /// Interaction logic for ExtractWindow.xaml
    /// </summary>
    partial class ExtractWindow : IDisposable
    {
        public ExtractWindow(MainWindow owner, string path, int iconCount, int cursorCount)
        {
            Owner = owner;

            _path = path;
            _iconCount = iconCount;
            _cursorCount = cursorCount;

            SetValue(HasIconsPropertyKey, (iconCount != 0));
            SetValue(HasCursorsPropertyKey, (cursorCount != 0));

            InitializeComponent();
        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            var settingsFile = SettingsFile;

            if (HasIcons)
            {
                try
                {
                    IconExtraction.ExtractIconsForEach(_path, GetCallback<IconFile>(_icons, settingsFile, 0), _handler, _handler);
                    SetValue(HasIconsPropertyKey, _icons.Count != 0);
                }
                catch
                {
                    SetValue(HasIconsPropertyKey, false);
                }
            }

            if (HasCursors)
            {
                try
                {
                    IconExtraction.ExtractCursorsForEach(_path, GetCallback<CursorFile>(_cursors, settingsFile, _iconCount), _handler, _handler);
                    SetValue(HasCursorsPropertyKey, _cursors.Count != 0);
                }
                catch
                {
                    SetValue(HasCursorsPropertyKey, false);
                }
            }

            Mouse.OverrideCursor = null;

            if (HasIcons)
                listIcons.SelectedIndex = 0;
            else if (!HasCursors)
            {
                ErrorWindow.Show((MainWindow)Owner, this, string.Format(settingsFile.LanguageFile.IconExtractNone, _path));
                DialogResult = false;
                return;
            }
            else tabControl.SelectedIndex = 1;

            if (HasCursors)
                listCursors.SelectedIndex = 0;
        }

        private static IconExtractCallback<TIconFile> GetCallback<TIconFile>(ObservableCollection<FileToken> collection, SettingsFile settingsFile, int add)
            where TIconFile : IconFileBase
        {
            return delegate (int index, TIconFile file)
            {
                collection.Add(new FileToken(file, settingsFile, index));
                file.Dispose();
            };
        }

        private string _path;

        private int _iconCount, _cursorCount;
        [Bindable(true)]
        public SettingsFile SettingsFile { get { return ((MainWindow)Owner).SettingsFile; } }

        private static void _handler(IconExtractException e) { System.Diagnostics.Debug.WriteLine("{0}: {1}", e.GetType(), e.Message); }

        #region IsFullyLoaded
        private static readonly DependencyPropertyKey IsFullyLoadedPropertyKey = DependencyProperty.RegisterReadOnly("IsFullyLoaded", typeof(bool), typeof(ExtractWindow),
            new PropertyMetadata(false));
        public static DependencyProperty IsFullyLoadedProperty = IsFullyLoadedPropertyKey.DependencyProperty;

        public bool IsFullyLoaded { get { return (bool)GetValue(IsFullyLoadedProperty); } }
        #endregion

        #region HasIcons
        private static readonly DependencyPropertyKey HasIconsPropertyKey = DependencyProperty.RegisterReadOnly("HasIcons", typeof(bool), typeof(ExtractWindow),
            new PropertyMetadata(false));
        public static DependencyProperty HasIconsProperty = HasIconsPropertyKey.DependencyProperty;

        public bool HasIcons { get { return (bool)GetValue(HasIconsProperty); } }
        #endregion

        #region HasCursors
        private static readonly DependencyPropertyKey HasCursorsPropertyKey = DependencyProperty.RegisterReadOnly("HasCursors", typeof(bool), typeof(ExtractWindow),
            new PropertyMetadata(false));
        public static DependencyProperty HasCursorsProperty = HasCursorsPropertyKey.DependencyProperty;

        public bool HasCursors { get { return (bool)GetValue(HasCursorsProperty); } }
        #endregion

        private static ObservableCollection<FileToken> _icons = new ObservableCollection<FileToken>();
        [Bindable(true)]
        public ObservableCollection<FileToken> IconFiles { get { return _icons; } }

        private static ObservableCollection<FileToken> _cursors = new ObservableCollection<FileToken>();
        [Bindable(true)]
        public ObservableCollection<FileToken> CursorFiles { get { return _cursors; } }

        public struct FileToken : IDisposable
        {
            private static readonly EntryKey _baseKey = new EntryKey(48, 48, BitDepth.Depth32BitsPerPixel);

            public FileToken(IconFileBase file, SettingsFile settings, int index)
            {
                _settings = settings;
                _index = index;
                _count = file.Entries.Count;
                var entries = file.Entries.OrderBy(i => Math.Abs(i.EntryKey.CompareTo(_baseKey)));
                IconEntry curEntry = entries.Where(i => i.Width >= _baseKey.Width && i.Height >= _baseKey.Height).FirstOrDefault();
                if (curEntry == null)
                    curEntry = entries.FirstOrDefault();

                _image = new WriteableBitmap(curEntry.CombineAlpha());
            }

            public BitmapSource _image;
            [Bindable(true)]
            public BitmapSource Image { get { return _image; } }

            public SettingsFile _settings;
            [Bindable(true)]
            public SettingsFile Settings { get { return _settings; } }

            public int _index;
            [Bindable(true)]
            public int Index { get { return _index; } }

            public int _count;
            [Bindable(true)]
            public int Count { get { return _count; } }

            public override string ToString()
            {
                string format;
                if (_settings == null) format = "#{0} ({1})";
                else format = _settings.LanguageFile.ExtractFrameCount;
                return string.Format(format, _index + 1, _count);
            }

            public void Dispose()
            {
                _image = null;
                _settings = null;
            }
        }

        public IconFileBase GetFileAndDispose()
        {
            FileToken token;
            if (tabCur.IsSelected)
            {
                token = (FileToken)listCursors.SelectedItem;
                Dispose();
                return IconExtraction.ExtractCursorSingle(_path, token.Index, _handler);
            }

            token = (FileToken)listIcons.SelectedItem;
            Dispose();
            return IconExtraction.ExtractIconSingle(_path, token.Index, _handler);
        }

        private static void _handler(IconLoadException e) { System.Diagnostics.Debug.WriteLine("{0}: {1}", e.GetType(), e.Message); }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void tab_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        public void Dispose()
        {
            foreach (FileToken curToken in _cursors.Concat(_icons))
                curToken.Dispose();
            _icons.Clear();
            _cursors.Clear();
        }
    }
}
