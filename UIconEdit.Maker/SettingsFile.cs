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

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UIconEdit.Maker
{
    class SettingsFile : DependencyObject
    {
        private static string SettingsPath =
            Path.Combine(Path.GetDirectoryName(typeof(SettingsFile).Assembly.Location), "settings.json");

        public SettingsFile(MainWindow owner)
        {
            _owner = owner;
        }

        public void Load()
        {
            try
            {
                if (!File.Exists(SettingsPath))
                    return;

                JObject root;
                using (StreamReader sr = new StreamReader(SettingsPath, Encoding.UTF8, true))
                using (JsonTextReader reader = new JsonTextReader(sr))
                    root = JToken.ReadFrom(reader) as JObject;

                if (root == null) throw new InvalidDataException();

                var jLang = root.GetValue(LanguageNameName) as JValue;

                if (jLang != null)
                    LanguageName = jLang.Value.ToString();
            }
            catch (Exception)
            {
            }
            finally
            {
                _loaded = true;
            }
        }

        private MainWindow _owner;
        [Bindable(true, BindingDirection.OneWay)]
        public MainWindow Owner { get { return _owner; } }

        private bool _loaded;

        public void Save()
        {
            if (!_loaded) return;
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (StreamWriter sw = new StreamWriter(ms, Encoding.UTF8, 4096, true))
                    using (var writer = new JsonTextWriter(sw))
                    {
                        writer.Formatting = Formatting.Indented;

                        writer.WriteStartObject();
                        writer.WritePropertyName(LanguageNameName); writer.WriteValue(LanguageName);
                        writer.WriteEndObject();
                    }
                    ms.Seek(0, SeekOrigin.Begin);
                    using (FileStream fs = File.Open(SettingsPath, FileMode.Create))
                        ms.CopyTo(fs);
                }
            }
            catch
            {
                MessageBox.Show(_owner, LanguageFile.SettingsSaveError, LanguageFile.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

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
                if ((string)e.NewValue == string.Empty)
                    s.LanguageFile = LanguageFile.Default;
                else
                    s.LanguageFile = new LanguageFile((string)e.NewValue, true);

                if (s._loaded && !s._dirty.ContainsKey(e.Property))
                    s._dirty.Add(e.Property, e.OldValue);
            }
            catch
            {
                d.SetValue(LanguageNameProperty, e.OldValue);
                MessageBox.Show(s._owner, string.Format(s.LanguageFile.LanguageLoadError, e.NewValue), s.LanguageFile.Error, MessageBoxButton.OK, MessageBoxImage.Error);
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
        }

        public LanguageFile LanguageFile
        {
            get { return (LanguageFile)GetValue(LanguageFileProperty); }
            set { SetValue(LanguageFileProperty, value); }
        }
        #endregion

        private Dictionary<DependencyProperty, object> _dirty = new Dictionary<DependencyProperty, object>();

        public void Reset()
        {
            foreach (var curKVP in _dirty)
                SetValue(curKVP.Key, curKVP.Value);
            _dirty.Clear();
        }
    }

    class Localizable<T>
    {
        public Localizable(SettingsFile settings)
        {
            Settings = settings;
        }

        public Localizable(SettingsFile settings, T value)
        {
            Settings = settings;
            Value = value;
        }

        public T Value { get; set; }

        public SettingsFile Settings { get; private set; }
    }

    internal static class LocExtensions
    {
        internal static int IndexOf<T>(this IList<Localizable<T>> source, T value)
        {
            int dex = 0;
            foreach (Localizable<T> item in source)
            {
                if (EqualityComparer<T>.Default.Equals(value, item.Value)) return dex;
                dex++;
            }
            return -1;
        }

        internal static bool Contains<T>(this IEnumerable<Localizable<T>> source, T value)
        {
            foreach (Localizable<T> item in source)
            {
                if (EqualityComparer<T>.Default.Equals(value, item.Value))
                    return true;
            }
            return false;
        }

        internal static bool Remove<T>(this ICollection<Localizable<T>> source, T value)
        {
            foreach (Localizable<T> item in source)
            {
                if (EqualityComparer<T>.Default.Equals(value, item.Value))
                {
                    source.Remove(item);
                    return true;
                }
            }
            return false;
        }
    }
}
