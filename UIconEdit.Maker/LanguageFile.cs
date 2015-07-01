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
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using UIconEdit.Maker.Properties;

namespace UIconEdit.Maker
{
    internal class LanguageFile
    {
        private static LanguageFile _default = new LanguageFile();
        /// <summary>
        /// Gets a default language file.
        /// </summary>
        public static LanguageFile Default { get { return _default; } }

        private Dictionary<string, string> _text;

        private LanguageFile()
        {
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(Resources.en_US)))
                Load(ms, true);
            _shortName = string.Empty;
        }

        /// <summary>
        /// Creates a new instance with the specified language name.
        /// </summary>
        /// <param name="langName">The short language name to load.</param>
        public LanguageFile(string langName)
        {
            _text = new Dictionary<string, string>(_default._text, StringComparer.OrdinalIgnoreCase);
            int dex = langName.IndexOf('-');
            if (dex > 0)
            {
                string shortPath = GetPath(langName.Substring(0, dex));
                if (File.Exists(shortPath))
                    Load(shortPath);
            }
            Load(GetPath(langName));
            _shortName = langName;
        }

        private static string GetPath(string langName)
        {
            return Path.Combine(Path.GetDirectoryName(typeof(LanguageFile).Assembly.Location), "Languages", langName + ".json");
        }

        private void Load(string path)
        {
            using (FileStream fs = File.OpenRead(path))
                Load(fs, false);
        }

        private void Load(Stream stream, bool initial)
        {
            XDocument xml;
            using (XmlDictionaryReader reader = JsonReaderWriterFactory.CreateJsonReader(stream, new XmlDictionaryReaderQuotas()))
                xml = XDocument.Load(reader);
            if (initial)
            {
                _text = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                foreach (XElement curNode in xml.Elements().FirstOrDefault().Elements())
                    _text.Add(curNode.Name.LocalName, curNode.Value);
            }
            else
            {
                if (_text == null) _text = new Dictionary<string, string>(_default._text, StringComparer.OrdinalIgnoreCase);
                foreach (XElement curNode in xml.Elements().FirstOrDefault().Elements())
                {
                    if (curNode.HasElements) throw new InvalidDataException();

                    if (_text.ContainsKey(curNode.Name.LocalName))
                        _text[curNode.Name.LocalName] = curNode.Value;
                }
            }
            _textRO = new ReadOnlyDictionary<string, string>(_text);
        }

        private ReadOnlyDictionary<string, string> _textRO;
        /// <summary>
        /// Gets a dictionary containing localized text.
        /// </summary>
        public ReadOnlyDictionary<string, string> Text { get { return _textRO; } }

        private string _shortName;
        public string ShortName { get { return _shortName; } }

        public string LangName { get { return _text["LangName"]; } }
        public string Title { get { return _text["Title"]; } }

        public string Error { get { return _text["Error"]; } }
        public string LanguageLoadError { get { return _text["LanguageLoadError"]; } }
        public string SettingsLoadError { get { return _text["SettingsLoadError"]; } }

        public string BitsPerPixelFormat { get { return _text["BitsPerPixelFormat"]; } }
        public string SizeFormat { get { return _text["SizeFormat"]; } }
    }
}
