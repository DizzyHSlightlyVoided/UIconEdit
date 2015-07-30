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
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace UIconEdit.Maker
{
    /// <summary>
    /// Interaction logic for ExtractWindow.xaml
    /// </summary>
    partial class ExtractWindow
    {
        public ExtractWindow(MainWindow owner, string path, int iconCount)
        {
            Mouse.OverrideCursor = null;
            Owner = owner;
            {
                _task = new ThreadTask(this, iconCount);
                Binding taskBinding = new Binding("Task.IsFinished");
                taskBinding.ElementName = "window";
                SetBinding(IsFullyLoadedProperty, taskBinding);
            }
            _path = path;
            InitializeComponent();
        }

        private ThreadTask _task;
        [Bindable(true, BindingDirection.OneWay)]
        public ThreadTask Task { get { return _task; } }

        internal class ThreadTask : INotifyPropertyChanged
        {
            private ExtractWindow _owner;

            internal ThreadTask(ExtractWindow owner, int iconCount)
            {
                _owner = owner;
                _iconCount = iconCount;
                _backgroundWorker = new BackgroundWorker();
                _backgroundWorker.WorkerSupportsCancellation = true;
                _backgroundWorker.DoWork += _backgroundWorker_DoWork;
            }

            private double _transformX, _transformY;

            private void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
            {
                try
                {
                    IconExtraction.ExtractIconsForEach(_owner._path, delegate (int dex, IconFile iconFile, CancelEventArgs cE)
                    {
                        if (_owner._cancelled)
                        {
                            cE.Cancel = true;
                            return;
                        }
                        try
                        {
                            _icons.Add(new FileToken(iconFile, dex, _transformX, _transformY));
                        }
                        finally
                        {
                            curIndex = dex;
                            OnPropertyChanged("Value");
                        }
                    }, _handler, _handler);
                }
                catch { }
                finally
                {
                    _finished = true;
                    OnPropertyChanged("IsFinished");
                    _iconArray = _icons.ToArray();
                    OnPropertyChanged("Icons");
                }
            }

            private BackgroundWorker _backgroundWorker;

            private int _iconCount;
            [Bindable(true, BindingDirection.OneWay)]
            public int Maximum { get { return _iconCount; } }

            private int curIndex;
            [Bindable(true, BindingDirection.OneWay)]
            public int Value { get { return curIndex; } }

            private bool _finished;
            [Bindable(true, BindingDirection.OneWay)]
            public bool IsFinished { get { return _finished; } }

            private List<FileToken> _icons = new List<FileToken>();

            private FileToken[] _iconArray = null;
            [Bindable(true, BindingDirection.OneWay)]
            public FileToken[] Icons { get { return _iconArray; } }

            public event PropertyChangedEventHandler PropertyChanged;

            private void OnPropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

            public void Start()
            {
                PresentationSource source = PresentationSource.FromVisual(_owner);
                _transformX = source.CompositionTarget.TransformFromDevice.M11;
                _transformY = source.CompositionTarget.TransformFromDevice.M22;

                _backgroundWorker.RunWorkerAsync();
            }
        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            _task.Start();
        }

        private bool _cancelled;

        private string _path;

        [Bindable(true)]
        public SettingsFile SettingsFile { get { return ((MainWindow)Owner).SettingsFile; } }

        private static void _handler(IconExtractException e) { }

        #region IsFullyLoaded
        public static DependencyProperty IsFullyLoadedProperty = DependencyProperty.Register("IsFullyLoaded", typeof(bool), typeof(ExtractWindow),
            new PropertyMetadata(false, IsFullyLoadedChanged));

        private static void IsFullyLoadedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(bool)e.NewValue) return;

            ExtractWindow w = (ExtractWindow)d;

            if (w._task.Icons.Length == 0)
            {
                ErrorWindow.Show((MainWindow)w.Owner, w, string.Format(w.SettingsFile.LanguageFile.IconExtractNone, w._path));
                w.DialogResult = false;
                w.Close();
                return;
            }
            else
            {
                w.listIcons.SelectedIndex = 0;
            }
        }

        public bool IsFullyLoaded { get { return (bool)GetValue(IsFullyLoadedProperty); } }
        #endregion

        public int IconIndex { get { return listIcons.SelectedIndex; } }

        public struct FileToken
        {
            public FileToken(IconFileBase iconFile, int index, double transformX, double transformY)
            {
                _index = index;
                _count = iconFile.Entries.Count;

                const double size = 48;
                double width = size * transformX;
                double height = size * transformY;

                var entries = iconFile.Entries.OrderBy(delegate (IconEntry e)
                {
                    double distance = Math.Abs(e.BitsPerPixel - 32) << 8;

                    distance += Math.Abs(e.Width - width);
                    distance += Math.Abs(e.Height - height);

                    return Math.Abs(distance);
                });

                IconEntry entry = entries.Where(e => e.Width >= width && e.Height >= height).FirstOrDefault();

                if (entry == null)
                    entry = entries.FirstOrDefault();

                if (entry == null)
                    throw new InvalidOperationException();
                _image = (BitmapSource)entry.CombineAlpha().GetAsFrozen();
            }

            public BitmapSource _image;
            [Bindable(true)]
            public BitmapSource Image { get { return _image; } }

            public int _index;
            [Bindable(true)]
            public int Index { get { return _index; } }

            public int _count;
            [Bindable(true)]
            public int Count { get { return _count; } }
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

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            _cancelled = true;
            DialogResult = null;
            Close();
        }
    }
}
