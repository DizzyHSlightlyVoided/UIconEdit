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
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace UIconEdit.Maker
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    partial class SettingsWindow
    {
        public SettingsWindow(MainWindow owner)
        {
            Owner = owner;

            LinkedList<LanguageFile> languages = new LinkedList<LanguageFile>();

            int dex = -1;
            var settingsFile = SettingsFile;
            var oldFile = settingsFile.LanguageFile;

            int cIndex = 0;
            foreach (string curPath in Directory.EnumerateFiles(Path.Combine(Path.GetDirectoryName(typeof(SettingsWindow).Assembly.Location), "Languages"), "*.json")
                .Select(Path.GetFileNameWithoutExtension))
            {
                try
                {
                    var curLang = new LanguageFile(curPath, false);

                    if (curPath.Equals(oldFile.ShortName, StringComparison.OrdinalIgnoreCase))
                    {
                        dex = cIndex;
                        settingsFile.LanguageFile = curLang;
                    }
                    cIndex++;
                    languages.AddLast(curLang);
                }
                catch { }
            }

            if (dex < 0)
            {
                languages.AddFirst(oldFile);
                dex = 0;
            }

            _languages = languages.ToArray();

            InitializeComponent();

            cmbLang.SelectedIndex = dex;
            settingsFile.Reset();
        }

        private LanguageFile[] _languages;
        [Bindable(true)]
        public LanguageFile[] Languages { get { return _languages; } }

        [Bindable(true)]
        public SettingsFile SettingsFile { get { return ((MainWindow)Owner).SettingsFile; } }

        private void Apply_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = SettingsFile.IsModified;
            e.Handled = true;
        }

        private void Apply_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SettingsFile.Save();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            SettingsFile.Save();
            DialogResult = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            SettingsFile.Reset();
        }
    }
}
