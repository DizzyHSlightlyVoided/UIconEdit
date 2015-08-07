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
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;

namespace UIconEdit.Maker
{
    class SettingsFile : DependencyObject
    {
        private static string SettingsPath = Path.Combine(
#if DEBUG
            Path.GetDirectoryName(typeof(SettingsFile).Assembly.Location),
#else
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "UIconEdit",
#endif
            "settings.xml");

        public SettingsFile(MainWindow owner)
        {
            _owner = owner;
        }

#if !DEBUG
        private void _dirVerify()
        {
            string dir = Path.GetDirectoryName(SettingsPath);

            if (File.Exists(SettingsPath) || Directory.Exists(dir)) return;

            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData, Environment.SpecialFolderOption.Create);

            string[] dirParts = dir.Substring(path.Length).Split(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < dirParts.Length; i++)
            {
                path = Path.Combine(path, dirParts[i]);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
            }
        }
#endif
        public void Load()
        {
#if !DEBUG
            _dirVerify();
#endif
            try
            {
                if (!File.Exists(SettingsPath))
                    return;

                XDocument doc;
                using (StreamReader sr = new StreamReader(SettingsPath, Encoding.UTF8, true))
                    doc = XDocument.Load(sr);

                XElement root = doc.Root;

                if (root == null || root.Name.LocalName != SettingsName)
                    return;

                var jLang = root.Element(LanguageNameName);

                if (jLang != null && !jLang.HasElements)
                    LanguageName = jLang.Value.ToString();

                var jKeep = root.Element(KeepHotspotCheckedName);
                if (jKeep != null && !jKeep.HasElements)
                {
                    bool jVal;
                    if (bool.TryParse(jKeep.Value, out jVal))
                        KeepHotspotChecked = jVal;
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                _loaded = true;

                SetValue(IsModifiedPropertyKey, false);
                _dirty.Clear();
            }
        }

        private MainWindow _owner;
        [Bindable(true, BindingDirection.OneWay)]
        public MainWindow Owner { get { return _owner; } }

        private bool _loaded;

        public void Save(Window window)
        {
            if (!_loaded) return;
            var oldOverride = Mouse.OverrideCursor;
            try
            {
#if !DEBUG
                _dirVerify();
#endif
                Mouse.OverrideCursor = Cursors.Wait;
                using (MemoryStream ms = new MemoryStream())
                {
                    using (StreamWriter sw = new StreamWriter(ms, Encoding.UTF8, 4096, true))
                    using (var writer = XmlWriter.Create(sw))
                    {
                        writer.WriteStartDocument();
                        writer.WriteStartElement(SettingsName);

                        writer.WriteElementString(LanguageNameName, LanguageName);
                        writer.WriteElementString(KeepHotspotCheckedName, KeepHotspotChecked.ToString());
                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                    }
                    ms.Seek(0, SeekOrigin.Begin);
                    using (FileStream fs = File.Open(SettingsPath, FileMode.Create))
                        ms.CopyTo(fs);

                    SetValue(IsModifiedPropertyKey, false);
                    _dirty.Clear();
                }
            }
            catch
            {
                ErrorWindow.Show(_owner, window, LanguageFile.SettingsSaveError);
            }
            finally
            {
                Mouse.OverrideCursor = oldOverride;
            }
        }

        public void Save()
        {
            Save(_owner);
        }

        private static void SaveableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SettingsFile s = (SettingsFile)d;

            if (s._loaded && !s._dirty.ContainsKey(e.Property))
                s._dirty.Add(e.Property, e.OldValue);

            s.SetValue(IsModifiedPropertyKey, true);
        }

        const string SettingsName = "Settings";

        #region LanguageName
        const string LanguageNameName = "LanguageName";
        public static readonly DependencyProperty LanguageNameProperty = DependencyProperty.Register(LanguageNameName, typeof(string), typeof(SettingsFile),
            new PropertyMetadata("", LanguageNameChanged), LanguageNameValidate);

        private static bool LanguageNameValidate(object value)
        {
            return value != null;
        }

        private static void LanguageNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SettingsFile s = (SettingsFile)d;

            try
            {
                var file = s.LanguageFile;
                if (file == null || !file.ShortName.Equals((string)e.NewValue, StringComparison.OrdinalIgnoreCase))
                {
                    if ((string)e.NewValue == string.Empty)
                        s.LanguageFile = LanguageFile.Default;
                    else
                        s.LanguageFile = new LanguageFile((string)e.NewValue, true);
                }
            }
            catch
            {
                d.SetValue(LanguageNameProperty, e.OldValue);
                ErrorWindow.Show(s._owner, string.Format(s.LanguageFile.LanguageLoadError, e.NewValue));
            }
        }

        public string LanguageName
        {
            get { return (string)GetValue(LanguageNameProperty); }
            set { SetValue(LanguageNameProperty, value); }
        }
        #endregion

        #region LanguageFile
        const string LanguageFileName = "LanguageFile";
        public static readonly DependencyProperty LanguageFileProperty = DependencyProperty.Register(LanguageFileName, typeof(LanguageFile),
            typeof(SettingsFile), new PropertyMetadata(LanguageFile.Default, LanguageFileChanged));

        private static void LanguageFileChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null)
                d.SetValue(LanguageNameProperty, null);
            else
                d.SetValue(LanguageNameProperty, ((LanguageFile)e.NewValue).ShortName);

            SaveableChanged(d, e);
        }

        public LanguageFile LanguageFile
        {
            get { return (LanguageFile)GetValue(LanguageFileProperty); }
            set { SetValue(LanguageFileProperty, value); }
        }
        #endregion

        #region KeepHotspotChecked
        const string KeepHotspotCheckedName = "KeepHotspotChecked";
        public static readonly DependencyProperty KeepHotspotCheckedProperty = DependencyProperty.Register(KeepHotspotCheckedName, typeof(bool), typeof(SettingsFile),
            new PropertyMetadata(false, SaveableChanged));

        public bool KeepHotspotChecked
        {
            get { return (bool)GetValue(KeepHotspotCheckedProperty); }
            set { SetValue(KeepHotspotCheckedProperty, value); }
        }
        #endregion

        #region IsModified 
        private static readonly DependencyPropertyKey IsModifiedPropertyKey = DependencyProperty.RegisterReadOnly("IsModified", typeof(bool), typeof(SettingsFile),
            new PropertyMetadata(false));
        public static readonly DependencyProperty IsModifiedProperty = IsModifiedPropertyKey.DependencyProperty;

        public bool IsModified { get { return (bool)GetValue(IsModifiedProperty); } }
        #endregion

        private Dictionary<DependencyProperty, object> _dirty = new Dictionary<DependencyProperty, object>();

        public void Reset()
        {
            foreach (var curKVP in _dirty)
                SetValue(curKVP.Key, curKVP.Value);
            _dirty.Clear();
            SetValue(IsModifiedPropertyKey, false);
        }
    }
}
