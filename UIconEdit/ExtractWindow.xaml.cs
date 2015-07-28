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
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace UIconEdit.Maker
{
    /// <summary>
    /// Interaction logic for ExtractWindow.xaml
    /// </summary>
    partial class ExtractWindow : IDisposable
    {
        public ExtractWindow(MainWindow owner, string path, int iconCount)
        {
            Owner = owner;

            _path = path;
            InitializeComponent();
            progressBar.Maximum = iconCount;
        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            int curIndex = 0;
            ENUMRESNAMEPROC proc = delegate (IntPtr h, int type, IntPtr name, IntPtr param)
            {
                try
                {
                    if (_cancelled) return false;

                    using (MemoryStream ms = Win32Funcs.ExtractData(h, type, name))
                    using (BinaryReader br = new BinaryReader(ms))
                    {
                        ms.Seek(4, SeekOrigin.Begin);
                        _icons.Add(new FileToken(_path, curIndex, br.ReadUInt16()));
                    }
                }
                catch { }
                finally
                {
                    curIndex++;
                    progressBar.Value++;
                }
                return true;
            };

            IntPtr hModule = IntPtr.Zero;
            try
            {
                hModule = Win32Funcs.LoadLibraryEx(_path, IntPtr.Zero, Win32Funcs.LOAD_LIBRARY_AS_DATAFILE);
                if (hModule == IntPtr.Zero)
                    throw new Win32Exception();

                if (!Win32Funcs.EnumResourceNames(hModule, Win32Funcs.RT_GROUP_ICON, proc, 0))
                    throw new Win32Exception();
            }
            catch { }
            finally
            {
                if (hModule != IntPtr.Zero)
                    Win32Funcs.FreeLibrary(hModule);
                SetValue(IsFullyLoadedPropertyKey, true);
            }

            Mouse.OverrideCursor = null;

            if (_icons.Count != 0)
                listIcons.SelectedIndex = 0;
            else
            {
                ErrorWindow.Show((MainWindow)Owner, this, string.Format(SettingsFile.LanguageFile.IconExtractNone, _path));
                DialogResult = false;
                return;
            }
        }

        private bool _cancelled;

        private string _path;

        [Bindable(true)]
        public SettingsFile SettingsFile { get { return ((MainWindow)Owner).SettingsFile; } }

        private static void _handler(IconExtractException e) { }

        #region IsFullyLoaded
        private static readonly DependencyPropertyKey IsFullyLoadedPropertyKey = DependencyProperty.RegisterReadOnly("IsFullyLoaded", typeof(bool), typeof(ExtractWindow),
            new PropertyMetadata(false));
        public static DependencyProperty IsFullyLoadedProperty = IsFullyLoadedPropertyKey.DependencyProperty;

        public bool IsFullyLoaded { get { return (bool)GetValue(IsFullyLoadedProperty); } }
        #endregion

        private static ObservableCollection<FileToken> _icons = new ObservableCollection<FileToken>();
        [Bindable(true)]
        public ObservableCollection<FileToken> IconFiles { get { return _icons; } }

        private static ObservableCollection<FileToken> _cursors = new ObservableCollection<FileToken>();
        [Bindable(true)]
        public ObservableCollection<FileToken> CursorFiles { get { return _cursors; } }

        public int IconIndex { get { return listIcons.SelectedIndex; } }

        public struct FileToken : IDisposable
        {
            public FileToken(string path, int index, int count)
            {
                IntPtr hIcon = IntPtr.Zero;
                try
                {
                    hIcon = ExtractIcon(System.Diagnostics.Process.GetCurrentProcess().Handle, path, index);
                    _count = count;
                    if (hIcon == IntPtr.Zero) throw new Win32Exception();
                    _image = new WriteableBitmap(Imaging.CreateBitmapSourceFromHIcon(hIcon, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions()));
                }
                finally
                {
                    if (hIcon != IntPtr.Zero)
                        DestroyIcon(hIcon);
                }
                _index = index;
            }

            [DllImport("shell32.dll")]
            private static extern IntPtr ExtractIcon(IntPtr hInst, string lpszExeFileName, int nIconIndex);

            [DllImport("user32.dll")]
            private static extern bool DestroyIcon(IntPtr hIcon);

            public BitmapSource _image;
            [Bindable(true)]
            public BitmapSource Image { get { return _image; } }

            public int _index;
            [Bindable(true)]
            public int Index { get { return _index; } }

            public int _count;
            [Bindable(true)]
            public int Count { get { return _count; } }

            public void Dispose()
            {
                _image = null;
            }
        }

        private static void _handler(IconLoadException e) { }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void listIcons_MouseDoubleClick(object sender, MouseButtonEventArgs e)
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

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            _cancelled = true;
            DialogResult = null;
            Close();
        }
    }
}
