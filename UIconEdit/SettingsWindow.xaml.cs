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
using Microsoft.Win32;

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
            try
            {
                using (RegistryKey key = Registry.ClassesRoot.OpenSubKey(regKey2, false))
                {
                    if (key != null)
                    {
                        object value = key.GetValue(null);

                        IsInRegistry = value != null && key.GetValueKind(null) == RegistryValueKind.String && value.ToString() == AppLoc();
                        SetValue(CanSavePropertyKey, false);
                    }
                }
            }
            catch { }

            InitializeComponent();

            settingsFile.Reset();
        }

        const string regKey1 = @"*\shell\UIconEdit", regKey2 = regKey1 + @"\command";

        private LanguageFile[] _languages;
        [Bindable(true)]
        public LanguageFile[] Languages { get { return _languages; } }

        [Bindable(true)]
        public SettingsFile SettingsFile { get { return ((MainWindow)Owner).SettingsFile; } }

        #region IsInRegistry
        public static readonly DependencyProperty IsInRegistryProperty = DependencyProperty.Register("IsInRegistry", typeof(bool), typeof(SettingsWindow),
            new PropertyMetadata(false, IsInRegistryChanged));

        private static void IsInRegistryChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(CanSavePropertyKey, true);
        }

        public bool IsInRegistry
        {
            get { return (bool)GetValue(IsInRegistryProperty); }
            set { SetValue(IsInRegistryProperty, value); }
        }
        #endregion

        #region CanSave
        private static readonly DependencyPropertyKey CanSavePropertyKey = DependencyProperty.RegisterReadOnly("CanSave", typeof(bool), typeof(SettingsWindow),
            new PropertyMetadata(false));
        public static readonly DependencyProperty CanSaveProperty = CanSavePropertyKey.DependencyProperty;

        public bool CanSave { get { return (bool)GetValue(CanSaveProperty); } }
        #endregion

        private void Apply_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = SettingsFile.IsModified || CanSave;
            e.Handled = true;
        }

        private void Apply_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _save(false);
        }

        private static string AppLoc()
        {
            return string.Format("\"{0}\" \"%1\"", typeof(SettingsWindow).Assembly.Location);
        }

        private static RegistryKey GetOrCreateRegKey()
        {
            RegistryKey key = Registry.ClassesRoot.OpenSubKey(regKey2, RegistryKeyPermissionCheck.ReadWriteSubTree);
            if (key == null) return Registry.ClassesRoot.CreateSubKey(regKey2, RegistryKeyPermissionCheck.ReadWriteSubTree);

            return key;
        }

        private bool _save(bool closing)
        {
            SettingsFile.Save(this);
            bool repeating = false;
            do
            {
                try
                {
                    if (chkRegistry.IsChecked.HasValue && chkRegistry.IsChecked.Value)
                    {
                        using (RegistryKey key = GetOrCreateRegKey())
                        {
                            key.SetValue(null, AppLoc());
                        }
                    }
                    else
                    {
                        Registry.ClassesRoot.DeleteSubKeyTree(regKey1);
                    }
                    SetValue(CanSavePropertyKey, false);
                    return true;
                }
                catch
                {
                    if (closing)
                    {
                        var langFile = SettingsFile.LanguageFile;
                        QuestionWindow qWindow = new QuestionWindow((MainWindow)Owner, langFile.RegistryError, SettingsFile.LanguageFile.Error);
                        qWindow.Owner = this;

                        qWindow.ButtonOKEnabled = true;
                        qWindow.ButtonOKMessage = langFile.ButtonRetry;
                        qWindow.ButtonYesEnabled = false;
                        qWindow.ButtonNoEnabled = true;
                        qWindow.ButtonNoMessage = langFile.ButtonNoSave;
                        qWindow.ButtonCancelEnabled = true;

                        qWindow.Show();

                        switch (qWindow.Result)
                        {
                            case MessageBoxResult.OK:
                                repeating = true;
                                continue;
                            case MessageBoxResult.No:
                                return true;
                            default:
                                return false;
                        }
                    }
                    else
                    {
                        ErrorWindow.Show((MainWindow)Owner, this, SettingsFile.LanguageFile.RegistryError);
                    }
                }
            }
            while (repeating);
            return false;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (_save(true))
            {
                DialogResult = true;
                Close();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            SettingsFile.Reset();
        }
    }
}
