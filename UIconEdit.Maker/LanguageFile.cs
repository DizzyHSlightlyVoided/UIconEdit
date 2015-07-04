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
using System.Globalization;
using System.IO;

using Newtonsoft.Json;

using UIconEdit.Maker.Properties;

namespace UIconEdit.Maker
{
    internal class LanguageFile : IEquatable<LanguageFile>
    {
        private static LanguageFile _default = new LanguageFile();
        /// <summary>
        /// Gets a default language file.
        /// </summary>
        public static LanguageFile Default { get { return _default; } }

        private Dictionary<string, string> _text;

        private static Dictionary<string, Dictionary<string, string>> _cache = new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase);

        private LanguageFile()
        {
            _shortName = string.Empty;
            using (StringReader sr = new StringReader(Resources.en_US))
                Load(sr, true);
            _textRO = new ReadOnlyDictionary<string, string>(_text);
        }

        /// <summary>
        /// Creates a new instance with the specified language name.
        /// </summary>
        /// <param name="langName">The short language name to load.</param>
        /// <param name="useCache"><c>true</c> to use the cache; <c>false</c> to load from the file no matter what.</param>
        public LanguageFile(string langName, bool useCache)
        {
            langName = langName.Trim();
            int dex = langName.IndexOf('-');
            if (!useCache || !_cache.TryGetValue(langName, out _text))
            {
                if (dex > 0)
                {
                    string shortPath = GetPath(langName.Substring(0, dex));
                    if (File.Exists(shortPath))
                        Load(shortPath);
                }
                Load(GetPath(langName));
                _shortName = langName;
                _cache[langName] = _text;
            }
            _textRO = new ReadOnlyDictionary<string, string>(_text);
        }

        private static string GetPath(string langName)
        {
            return Path.Combine(Path.GetDirectoryName(typeof(LanguageFile).Assembly.Location), "Languages", langName + ".json");
        }

        private void Load(string path)
        {
            using (StreamReader sr = new StreamReader(path, true))
                Load(sr, false);
        }

        JsonSerializer jSer = new JsonSerializer();

        private void Load(TextReader textReader, bool initial)
        {
            Dictionary<string, string> loadedText;
            using (JsonTextReader reader = new JsonTextReader(textReader))
                loadedText = jSer.Deserialize<Dictionary<string, string>>(reader);

            if (initial)
            {
                _text = new Dictionary<string, string>(loadedText, StringComparer.OrdinalIgnoreCase);

            }
            else
            {
                if (_text == null) _text = new Dictionary<string, string>(_default._text, StringComparer.OrdinalIgnoreCase);
                foreach (var curKVP in loadedText)
                {
                    if (_text.ContainsKey(curKVP.Key))
                        _text[curKVP.Key] = curKVP.Value;
                }
            }
        }

        public bool Equals(LanguageFile other)
        {
            return other != null && other.ShortName.Equals(ShortName, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as LanguageFile);
        }

        public override int GetHashCode()
        {
            if (_shortName == null) return 0;
            return _shortName.ToUpper().GetHashCode();
        }

        public static bool operator ==(LanguageFile l1, LanguageFile l2)
        {
            if (ReferenceEquals(l1, l2)) return true;
            if (ReferenceEquals(l1, null) ^ ReferenceEquals(l2, null)) return false;

            return l1.Equals(l2);
        }

        public static bool operator !=(LanguageFile l1, LanguageFile l2)
        {
            return !(l1 == l2);
        }

        private ReadOnlyDictionary<string, string> _textRO;
        /// <summary>
        /// Gets a dictionary containing localized text.
        /// </summary>
        public ReadOnlyDictionary<string, string> Text { get { return _textRO; } }

        private string _shortName;
        public string ShortName { get { return _shortName; } }

        public const string DefaultShortName = "en-US";

        public string LangName { get { return _text["LangName"]; } }
        public string Title { get { return _text["Title"]; } }

        public string Error { get { return _text["Error"]; } }
        public string LanguageLoadError { get { return _text["LanguageLoadError"]; } }
        public string SettingsSaveError { get { return _text["SettingsSaveError"]; } }
        public string ImageLoadError { get { return _text["ImageLoadError"]; } }
        public string ImageSaveError { get { return _text["ImageSaveError"]; } }
        public string ImageAddError { get { return _text["ImageAddError"]; } }

        public string MenuFile { get { return _text["MenuFile"]; } }
        public string MenuFileNew { get { return _text["MenuFileNew"]; } }
        public string MenuFileOpen { get { return _text["MenuFileOpen"]; } }
        public string MenuFileSave { get { return _text["MenuFileSave"]; } }
        public string MenuFileSaveAs { get { return _text["MenuFileSaveAs"]; } }
        public string MenuFileQuit { get { return _text["MenuFileQuit"]; } }

        public string MenuEdit { get { return _text["MenuEdit"]; } }
        public string MenuEditAdd { get { return _text["MenuEditAdd"]; } }
        public string MenuEditDup { get { return _text["MenuEditDup"]; } }
        public string MenuEditRem { get { return _text["MenuEditRem"]; } }

        public string ButtonTipNew { get { return _text["ButtonTipNew"]; } }
        public string ButtonTipOpen { get { return _text["ButtonTipOpen"]; } }
        public string ButtonTipSave { get { return _text["ButtonTipSave"]; } }

        public string ButtonTipAdd { get { return _text["ButtonTipAdd"]; } }
        public string ButtonTipDup { get { return _text["ButtonTipDup"]; } }
        public string ButtonTipRem { get { return _text["ButtonTipRem"]; } }

        public string ModifiedCaption { get { return _text["ModifiedCaption"]; } }
        public string ModifiedMessage { get { return _text["ModifiedMessage"]; } }

        public string RemoveCaption { get { return _text["RemoveCaption"]; } }
        public string RemoveMessage { get { return _text["RemoveMessage"]; } }

        public string ButtonOK { get { return _text["ButtonOK"]; } }
        public string ButtonCancel { get { return _text["ButtonCancel"]; } }
        public string ButtonYes { get { return _text["ButtonYes"]; } }
        public string ButtonNo { get { return _text["ButtonNo"]; } }
        public string ButtonNoSave { get { return _text["ButtonNoSave"]; } }

        public string FormatBitsPerPixel { get { return _text["FormatBitsPerPixel"]; } }
        public string FormatSize { get { return _text["FormatSize"]; } }
        public string CustomSize { get { return _text["CustomSize"]; } }
        public string ExtendedSize { get { return _text["ExtendedSize"]; } }
        public string SizeWidth { get { return _text["SizeWidth"]; } }
        public string SizeHeight { get { return _text["SizeHeight"]; } }

        public string AlphaThreshold { get { return _text["AlphaThreshold"]; } }

        public string GroupImage { get { return _text["GroupImage"]; } }
        public string GroupSize { get { return _text["GroupSize"]; } }
        public string GroupBitsPerPixel { get { return _text["GroupBitsPerPixel"]; } }

        public string AddTitle { get { return _text["AddTitle"]; } }
        public string DuplicateTitle { get { return _text["DuplicateTitle"]; } }

        public string TypeIco { get { return _text["TypeIco"]; } }
        public string TypeCur { get { return _text["TypeCur"]; } }
        public string TypeIcoCur { get { return _text["TypeIcoCur"]; } }
        public string TypeAll { get { return _text["TypeAll"]; } }
        public string TypeImage { get { return _text["TypeImage"]; } }

        public string GetErrorMessage(IconLoadException e)
        {
            IconErrorCode eCode;

            switch (e.Code)
            {
                case IconErrorCode.InvalidFormat:
                case IconErrorCode.ResourceTooSmall:
                case IconErrorCode.ResourceTooEarly:
                case IconErrorCode.ResourceOverlap:
                    eCode = IconErrorCode.InvalidFormat;
                    break;
                case IconErrorCode.ZeroEntries:
                case IconErrorCode.ZeroValidEntries:
                    eCode = IconErrorCode.ZeroEntries;
                    break;
                case IconErrorCode.WrongType:
                    eCode = IconErrorCode.WrongType;
                    break;
                case IconErrorCode.InvalidEntryType:
                case IconErrorCode.InvalidPngFile:
                case IconErrorCode.PngSizeMismatch:
                case IconErrorCode.InvalidBmpFile:
                case IconErrorCode.BmpSizeMismatch:
                case IconErrorCode.InvalidBmpHeightOdd:
                case IconErrorCode.BmpBitDepthMismatch:
                    eCode = IconErrorCode.InvalidEntryType;
                    break;
                case IconErrorCode.InvalidBitDepth:
                case IconErrorCode.InvalidBmpBitDepth:
                    eCode = IconErrorCode.InvalidBitDepth;
                    break;
                case IconErrorCode.InvalidBmpSize:
                case IconErrorCode.InvalidPngSize:
                    eCode = IconErrorCode.InvalidBmpSize;
                    break;
                default:
                    eCode = IconErrorCode.Unknown;
                    break;
            }

            string s = ((int)eCode).ToString("X", NumberFormatInfo.InvariantInfo);

            const string errorPrefix = "IconError";

            s = errorPrefix + s;

            string message;
            if (e.TypeCode == IconTypeCode.Cursor && _text.TryGetValue(s + "cursor", out message))
                return message;
            if (e.TypeCode == IconTypeCode.Icon && _text.TryGetValue(s + "icon", out message))
                return message;

            return _text[s];
        }
    }
}
