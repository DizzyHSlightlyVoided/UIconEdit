#region BSD license
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
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

#if DRAWING
namespace UIconDrawing
#else
using System.Windows;

namespace UIconEdit
#endif
{
    /// <summary>
    /// Represents an animated cursor file.
    /// </summary>
    public class AnimatedCursorFile :
#if DRAWING
        IDisposable, INotifyPropertyChanged
#else
        DependencyObject
#endif
    {
        /// <summary>
        /// Initializes a new <see cref="AnimatedCursorFile"/> instance.
        /// </summary>
        public AnimatedCursorFile()
        {
        }

        #region Load
        private const int _idBaseRiff = 0x46464952; //little-endian "RIFF"
        private const int _idBaseAcon = 0x4e4f4341; //little-endian "ACON"
        private const int _idChnkAnih = 0x68696e61; //little-endian "anih"
        private const int _idChnkList = 0x5453494c; //little-endian "LIST"
        private const int _idListInfo = 0x4f464e49; //little-endian "INFO"
        private const int _idListFram = 0x6d617266; //little-endian "fram"
        private const int _idItemIcon = 0x6e6f6369; //little-endian "icon"
        private const int _idItemInam = 0x4d414e49; //little-endian "INAM"
        private const int _idItemIart = 0x54524149; //little-endian "IART"
        private const int _idChnkRate = 0x65746172; //little-endian "rate"
        private const int _idChnkSeq = 0x20716573; //little-endian "seq "

        private const int sizeAnih = 36;

        /// <summary>
        /// Loads an animated cursor file from the specified stream.
        /// </summary>
        /// <param name="input">The stream containing the animated cursor file to load.</param>
        /// <param name="handler">An error handler for loading the individual cursor files, or <c>null</c> to always throw an exception.</param>
        /// <returns>A loaded <see cref="AnimatedCursorFile"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="input"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="input"/> is closed.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// <paramref name="input"/> is closed.
        /// </exception>
        /// <exception cref="IconExtractException">
        /// An error occurred when loading the animated cursor file.
        /// </exception>
        /// <exception cref="FileFormatException">
        /// An error occurred when loading the animated cursor file.
        /// </exception>
        public static AnimatedCursorFile Load(Stream input, IconExtractExceptionHandler handler)
        {
            AnimatedCursorFile file = new AnimatedCursorFile();

            Dictionary<int, MemoryStream> chunks = new Dictionary<int, MemoryStream>(), lists = new Dictionary<int, MemoryStream>();

            using (MemoryStream ms = new MemoryStream())
            using (BinaryReader reader = new BinaryReader(ms))
            {
                using (BinaryReader br = new BinaryReader(input, new System.Text.UTF8Encoding(), true))
                {
                    if (br.ReadUInt32() != _idBaseRiff) throw new FileFormatException();
                    _copyBuffer(br, null, ms);
                }

                if (reader.ReadUInt32() != _idBaseAcon)
                    throw new FileFormatException();

                while (ms.Position + 8 < ms.Length)
                {
                    int id = reader.ReadInt32();

                    MemoryStream curStream = null;
                    int length = reader.ReadInt32();

                    if (id == _idChnkList)
                    {
                        length -= 4;

                        id = reader.ReadInt32();

                        if ((id == _idListInfo || id == _idListFram) && !lists.ContainsKey(id))
                        {
                            curStream = new MemoryStream();

                            lists.Add(id, curStream);
                        }

                    }
                    else if ((id == _idChnkAnih || id == _idChnkRate || id == _idChnkSeq) && !chunks.ContainsKey(id))
                    {
                        curStream = new MemoryStream();

                        chunks.Add(id, curStream);
                    }

                    _copyBuffer(reader, length, curStream);
                }
            }

            try
            {
                int frameCount, steps, displayRate;

                bool hasSequences, rawData;

                MemoryStream ms;

                if (lists.TryGetValue(_idListInfo, out ms))
                {
                    string name = null, author = null;

                    using (BinaryReader reader = new BinaryReader(ms))
                    {
                        while (ms.Position + 8 < ms.Length)
                        {
                            int id = reader.ReadInt32();
                            int length = reader.ReadInt32();
                            if (length < 0) throw new FileFormatException();

                            byte[] data = reader.ReadBytes(length);
                            if (data.Length < length) throw new EndOfStreamException();

                            if ((length & 1) == 1)
                                reader.ReadByte();

                            try
                            {
                                if (id == _idItemInam)
                                    name = _getString(data);
                                else if (id == _idItemIart)
                                    author = _getString(data);
                            }
                            catch
                            {
                                string eName;
                                if (id == _idItemInam)
                                    eName = "INAM";
                                else
                                    eName = "IART";

                                throw new FileFormatException(string.Format("Invalid UTF-8 data under {0} info.", eName));
                            }
                        }

#if DRAWING
                        file._name = name;
                        file._author = author;
#else
                        file.CursorName = name;
                        file.CursorAuthor = author;
#endif
                    }
                }

                #region "anih" chunk
                if (!chunks.TryGetValue(_idChnkAnih, out ms))
                    throw new FileFormatException();

                using (BinaryReader reader = new BinaryReader(ms))
                {
                    if (ms.Length != sizeAnih || sizeAnih != reader.ReadUInt32())
                        throw new FileFormatException();

                    frameCount = reader.ReadInt32();
                    if (frameCount < 0) throw new FileFormatException();
                    steps = reader.ReadInt32();
                    if (steps < 0) throw new FileFormatException();

                    int width = reader.ReadInt32();
                    if (width < 0) throw new FileFormatException();
                    int height = reader.ReadInt32();
                    if (height < 0) throw new FileFormatException();

                    int bitsPerPixel = reader.ReadInt32();
                    switch (bitsPerPixel)
                    {
                        case 0:
                        case 1:
                        case 4:
                        case 8:
                        case 24:
                        case 32:
                            break;
                        default:
                            throw new FileFormatException("Invalid bits per pixel: " + bitsPerPixel);
                    }
                    int numPanes = reader.ReadInt32();
                    displayRate = reader.ReadInt32();

                    if (displayRate <= 0) throw new FileFormatException();

#if DRAWING
                    file._rate = displayRate;
#else
                    file.DisplayRateJiffies = displayRate;
#endif
                    uint flags = reader.ReadUInt32();

                    hasSequences = (flags & 2) != 0;
                    rawData = (flags & 1) == 0;

                    if (rawData) throw new NotSupportedException("Raw data is not supported."); //TODO: figure out how raw data works?
                }
                #endregion

                #region "fram" list (with "icon" chunks)
                AnimatedCursorFrame[] frames = new AnimatedCursorFrame[frameCount];

                if (!lists.TryGetValue(_idListFram, out ms))
                    throw new FileFormatException();

                using (BinaryReader reader = new BinaryReader(ms))
                {
                    for (int i = 0; i < frameCount; i++)
                    {
                        if (reader.ReadInt32() != _idItemIcon)
                            throw new FileFormatException();

                        using (MemoryStream iconStream = new MemoryStream())
                        {
                            _copyBuffer(reader, null, iconStream);

                            if (rawData)
                            {
                                //TODO: Figure out how raw data works?
                                throw new NotSupportedException("Raw data is not supported.");
                            }
                            else
                            {
                                try
                                {
                                    IconLoadExceptionHandler sHandler = null;
                                    if (handler != null)
                                        sHandler = e => handler(new IconExtractException(e, i));

                                    IconFileBase iconFile = IconFileBase.Load(iconStream, sHandler);

                                    CursorFile cursorFile = iconFile as CursorFile;

                                    if (cursorFile == null)
                                    {
                                        cursorFile = iconFile.CloneAsCursorFile();
#if DRAWING
                                        iconFile.Dispose();
#endif
                                    }
                                    frames[i] = new AnimatedCursorFrame(cursorFile);
                                }
                                //catch (IconLoadException e)
                                //{
                                //    throw new IconExtractException(e, i);
                                //}
                                //catch (Exception e)
                                //{
                                //    throw new IconExtractException(e, IconTypeCode.Unknown, i);
                                //}
                                finally
                                {
                                }
                            }
                        }
                    }
                }
                #endregion

                #region "seq " chunk
                int[] indices = null;
                if (hasSequences && chunks.TryGetValue(_idChnkSeq, out ms))
                {
                    if (ms.Length != steps * 4)
                        throw new FileFormatException("Invalid seq chunk size.");

                    indices = new int[steps];

                    using (BinaryReader reader = new BinaryReader(ms))
                    {
                        for (int i = 0; i < steps; i++)
                        {
                            int curVal = reader.ReadInt32();
                            if (curVal < 0 || curVal >= frameCount)
                                throw new FileFormatException(string.Format("Invalid sequence index at entry {0}: {1}", i, curVal));
                            indices[i] = curVal;
                        }
                    }
                }
                #endregion

                #region "rate" chunk
                if (chunks.TryGetValue(_idChnkRate, out ms))
                {
                    if (ms.Length != frameCount * 4)
                        throw new FileFormatException("Invalid rate chunk size.");

                    using (BinaryReader reader = new BinaryReader(ms))
                    {
                        for (int i = 0; i < frameCount; i++)
                        {
                            int curRate = reader.ReadInt32();
                            if (curRate <= 0)
                                throw new FileFormatException(string.Format("Invalid rate at entry {0}: {1}", i, curRate));
                            if (curRate != displayRate)
                                frames[i].LengthJiffies = curRate;
                        }
                    }
                }
                #endregion

                switch (AllEntriesSame(frames))
                {
                    case EntryKeyResult.NoEntries:
                        throw new InvalidDataException("No entries were loaded.");
                    case EntryKeyResult.Different:
                        throw new InvalidDataException("Mismatch between entries.");
                    case EntryKeyResult.EmptyList:
                        throw new InvalidDataException("At least one entry contains zero elements.");
                }

                foreach (AnimatedCursorFrame cFrame in frames)
                    file._entries.Add(cFrame);

                if (hasSequences)
                {
                    foreach (int i in indices)
                        file._frames.Add(i);
                }

                return file;
            }
            finally
            {
                foreach (MemoryStream ms in chunks.Values.Concat(lists.Values))
                    ms.Close();
            }
        }

        /// <summary>
        /// Loads an animated cursor file from the specified stream.
        /// </summary>
        /// <param name="input">The stream containing the animated cursor file to load.</param>
        /// <returns>A loaded <see cref="AnimatedCursorFile"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="input"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="input"/> is closed.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// <paramref name="input"/> is closed.
        /// </exception>
        /// <exception cref="IconExtractException">
        /// An error occurred when loading the animated cursor file.
        /// </exception>
        /// <exception cref="FileFormatException">
        /// An error occurred when loading the animated cursor file.
        /// </exception>
        public static AnimatedCursorFile Load(Stream input)
        {
            return Load(input, null);
        }

        /// <summary>
        /// Loads an <see cref="AnimatedCursorFile"/> from the specified path.
        /// </summary>
        /// <param name="path">The path to a cursor file.</param>
        /// <param name="handler">An error handler for loading the individual cursor files, or <c>null</c> to always throw an exception.</param>
        /// <returns>An <see cref="IconFileBase"/> implementation loaded from <paramref name="path"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="path"/> is empty, contains only whitespace, or contains one or more invalid path characters as defined in
        ///  <see cref="Path.GetInvalidPathChars()"/>.
        /// </exception>
        /// <exception cref="PathTooLongException">
        /// The specified path, filename, or both contain the system-defined maximum length.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// The specified path was not found.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        /// The specified path was invalid.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// <para><paramref name="path"/> specified a directory.</para>
        /// <para>-OR-</para>
        /// <para>The caller does not have the required permission.</para>
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// <paramref name="path"/> is in an invalid format.
        /// </exception>
        /// <exception cref="IconExtractException">
        /// An error occurred when loading the animated cursor file.
        /// </exception>
        /// <exception cref="FileFormatException">
        /// An error occurred when loading the animated cursor file.
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurred.
        /// </exception>
        public static AnimatedCursorFile Load(string path, IconExtractExceptionHandler handler)
        {
            using (FileStream fs = File.OpenRead(path))
                return Load(fs, handler);
        }

        /// <summary>
        /// Loads an <see cref="AnimatedCursorFile"/> from the specified path.
        /// </summary>
        /// <param name="path">The path to a cursor file.</param>
        /// <returns>An <see cref="IconFileBase"/> implementation loaded from <paramref name="path"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="path"/> is empty, contains only whitespace, or contains one or more invalid path characters as defined in
        ///  <see cref="Path.GetInvalidPathChars()"/>.
        /// </exception>
        /// <exception cref="PathTooLongException">
        /// The specified path, filename, or both contain the system-defined maximum length.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// The specified path was not found.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        /// The specified path was invalid.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// <para><paramref name="path"/> specified a directory.</para>
        /// <para>-OR-</para>
        /// <para>The caller does not have the required permission.</para>
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// <paramref name="path"/> is in an invalid format.
        /// </exception>
        /// <exception cref="IconLoadException">
        /// An error occurred when processing the cursor file's format.
        /// </exception>
        /// <exception cref="FileFormatException">
        /// An error occurred when processing the cursor file's format.
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurred.
        /// </exception>
        public static AnimatedCursorFile Load(string path)
        {
            return Load(path, null);
        }

        private static void _copyBuffer(BinaryReader reader, int? getLength, MemoryStream output)
        {
            int length;

            if (getLength.HasValue)
                length = getLength.Value;
            else
                length = reader.ReadInt32();
            if (length < 0) throw new FileFormatException();

            const int bufferSize = 8192;

            byte[] buffer = new byte[bufferSize];

            while (length > 0)
            {
                int read = reader.Read(buffer, 0, Math.Min(length, bufferSize));

                if (read == 0) throw new EndOfStreamException();

                if (output != null)
                    output.Write(buffer, 0, read);

                length -= read;
            }

            if (output != null)
                output.Seek(0, SeekOrigin.Begin);
        }

        private static string _getString(byte[] buffer)
        {
            string s = Encoding.UTF8.GetString(buffer);

            int nullDex = s.IndexOf('\0');

            if (nullDex >= 0)
                return s.Substring(0, nullDex);

            return s;
        }
        #endregion

        private enum EntryKeyResult
        {
            AllSame,
            Different,
            EmptyList,
            NoEntries,
        }

        private static EntryKeyResult AllEntriesSame(ICollection<AnimatedCursorFrame> entries)
        {
            IconEntryKey[] keys = null;

            foreach (AnimatedCursorFrame frame in entries)
            {
                var iconEntries = frame.File.Entries;
                IconEntryKey[] curKeys = new IconEntryKey[iconEntries.Count];

                if (curKeys.Length == 0) return EntryKeyResult.EmptyList;

                for (int i = 0; i < curKeys.Length; i++)
                    curKeys[i] = iconEntries[i].EntryKey;
                Array.Sort(curKeys);

                if (keys == null)
                    keys = curKeys;
                else
                {
                    if (keys.Length != curKeys.Length) return EntryKeyResult.Different;

                    for (int i = 0; i < keys.Length; i++)
                        if (keys[i] != curKeys[i]) return EntryKeyResult.Different;
                }
            }

            if (keys == null) return EntryKeyResult.NoEntries;

            return EntryKeyResult.AllSame;
        }

        #region Entries
        private ObservableCollection<AnimatedCursorFrame> _entries = new ObservableCollection<AnimatedCursorFrame>();
        /// <summary>
        /// Gets a list of <see cref="IconFileBase"/> objects containing all entries in the animated cursor file.
        /// </summary>
#if !DRAWING
        [Bindable(true)]
#endif
        public ObservableCollection<AnimatedCursorFrame> Entries { get { return _entries; } }
        #endregion

        #region FrameIndices
        private ObservableCollection<int> _frames = new ObservableCollection<int>();
        /// <summary>
        /// Gets the ordering of the frames, as indices within <see cref="Entries"/>.
        /// </summary>
#if !DRAWING
        [Bindable(true)]
#endif
        public ObservableCollection<int> FrameIndices { get { return _frames; } }
        #endregion

        internal static TimeSpan JiffiesToTime(int jiffies, string paramName)
        {
            if (jiffies < 1) throw new ArgumentOutOfRangeException(paramName);
            return new TimeSpan(TimeSpan.TicksPerSecond * jiffies / 60);
        }

        /// <summary>
        /// Converts the specified number of "jiffies" (1/60 of a second) to its corresponding <see cref="TimeSpan"/> value.
        /// </summary>
        /// <param name="jiffies">The number of jiffies to convert.</param>
        /// <returns>A <see cref="TimeSpan"/> with a <see cref="TimeSpan.TotalSeconds"/> value equal to <paramref name="jiffies"/> divided by 60.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="jiffies"/> is less than or equal to 0.
        /// </exception>
        public static TimeSpan JiffiesToTime(int jiffies)
        {
            return JiffiesToTime(jiffies, "jiffies");
        }

        internal static int TimeToJiffies(TimeSpan value, string paramName)
        {
            if (value <= TimeSpan.Zero) throw new ArgumentOutOfRangeException(paramName);
            double jiffies = Math.Floor(value.TotalSeconds * 60);

            if (jiffies <= 0 || jiffies > int.MaxValue)
                throw new ArgumentOutOfRangeException(paramName);

            return (int)jiffies;
        }

        /// <summary>
        /// Converts the specified <see cref="TimeSpan"/> to its equivalent number of "jiffies" (1/60 of a second).
        /// </summary>
        /// <param name="value">The <see cref="TimeSpan"/> to convert.</param>
        /// <returns>A number of jiffies equal to <paramref name="value"/>'s <see cref="TimeSpan.TotalSeconds"/> multiplied by 60.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> translates to a number of jiffies less than or equal to 0, or greater than <see cref="int.MaxValue"/>.
        /// </exception>
        public static int TimeToJiffies(TimeSpan value)
        {
            return TimeToJiffies(value, "value");
        }

        #region DisplayRateJiffies
#if DRAWING
        private int _rate = 10;
#else
        /// <summary>
        /// The dependency property for the <see cref="DisplayRateJiffies"/> property.
        /// </summary>
        public static readonly DependencyProperty DisplayRateJiffiesProperty = DependencyProperty.Register("DisplayRateJiffies", typeof(int), typeof(AnimatedCursorFile),
            new PropertyMetadata(10, DisplayRateJiffiesChanged, JiffiesCoerce));

        private static void DisplayRateJiffiesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(DisplayRateTimeProperty, JiffiesToTime((int)e.NewValue));
        }

        internal static object JiffiesCoerce(DependencyObject d, object baseValue)
        {
            int value = (int)baseValue;
            if (value <= 0) return 1;
            return baseValue;
        }
#endif
        /// <summary>
        /// Gets and sets the default delay before displaying the next frame, in "jiffies" (1/60 of a second).
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// In a set operation, the specified value is less than or equal to 0.
        /// </exception>
        public int DisplayRateJiffies
        {
#if DRAWING
            get { return _rate; }
#else
            get { return (int)GetValue(DisplayRateJiffiesProperty); }
#endif
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException();
#if DRAWING
                _rate = value;

                OnPropertyChanged("DisplayRateJiffies");
                OnPropertyChanged("DisplayRateTime");
#else
                SetValue(DisplayRateJiffiesProperty, value);
#endif
            }
        }
        #endregion

#if !DRAWING
        /// <summary>
        /// The dependency property for the <see cref="DisplayRateTime"/> property.
        /// </summary>
        public static readonly DependencyProperty DisplayRateTimeProperty = DependencyProperty.Register("DisplayRateTime", typeof(TimeSpan), typeof(AnimatedCursorFile),
            new PropertyMetadata(new TimeSpan(TimeSpan.TicksPerSecond * 10 / 60), DisplayRateTimeChanged, TimeCoerce));

        private static void DisplayRateTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(DisplayRateJiffiesProperty, TimeToJiffies((TimeSpan)e.NewValue));
        }

        internal static object TimeCoerce(DependencyObject d, object baseValue)
        {
            TimeSpan value = (TimeSpan)baseValue;

            var seconds = (long)(value.TotalSeconds * 60);

            if (seconds < 1)
                return new TimeSpan(TimeSpan.TicksPerSecond / 60);

            if (seconds > int.MaxValue)
                return new TimeSpan(TimeSpan.TicksPerSecond * int.MaxValue / 60);

            return new TimeSpan(TimeSpan.TicksPerSecond * seconds / 60);
        }
#endif
        /// <summary>
        /// Gets and sets the default delay before displaying the next frame. Fitted to the nearest "jiffy" (1/60 of a second).
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// In a set operation, the specified value translates to a number of jiffies less than or equal to 0 or greater than <see cref="int.MaxValue"/>
        /// </exception>
        public TimeSpan DisplayRateTime
        {
#if DRAWING
            get { return JiffiesToTime(_rate); }
            set { DisplayRateJiffies = TimeToJiffies(value, null); }
#else
            get { return (TimeSpan)GetValue(DisplayRateTimeProperty); }
            set
            {
                TimeToJiffies(value, null);
                SetValue(DisplayRateTimeProperty, value);
            }
#endif
        }

        #region CursorName
#if DRAWING
        private string _name;
#else
        /// <summary>
        /// The dependency property for the <see cref="CursorName"/> property.
        /// </summary>
        public static readonly DependencyProperty CursorNameProperty = DependencyProperty.Register("CursorName", typeof(string), typeof(AnimatedCursorFile));
#endif
        /// <summary>
        /// Gets the name of the animated cursor file.
        /// </summary>
        public string CursorName
        {
#if DRAWING
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("CursorName");
            }
#else
            get { return (string)GetValue(CursorNameProperty); }
            set { SetValue(CursorNameProperty, value); }
#endif
        }
        #endregion

        #region CursorAuthor
#if DRAWING
        private string _author;
#else
        /// <summary>
        /// The dependency property for the <see cref="CursorAuthor"/> property.
        /// </summary>
        public static readonly DependencyProperty CursorAuthorProperty = DependencyProperty.Register("CursorAuthor", typeof(string), typeof(AnimatedCursorFile));
#endif
        /// <summary>
        /// Gets the author of the animated cursor file.
        /// </summary>
        public string CursorAuthor
        {
#if DRAWING
            get { return _author; }
            set
            {
                _author = value;
                OnPropertyChanged("CursorAuthor");
            }
#else
            get { return (string)GetValue(CursorAuthorProperty); }
            set { SetValue(CursorAuthorProperty, value); }
#endif
        }
        #endregion

#if DRAWING
        /// <summary>
        /// Raised when a property on the current instance changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the property which was changed.</param>
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Disposal
        /// <summary>
        /// Raised when the current instance is disposed.
        /// </summary>
        public event EventHandler Disposed;

        private bool _isDisposed;
        /// <summary>
        /// Gets a value indicating whether the current instance has been disposed.
        /// </summary>
        public bool IsDisposed { get { return _isDisposed; } }

        /// <summary>
        /// Immediately releases all resources used by the current instance.
        /// </summary>
        public void Dispose()
        {
            if (!_isDisposed) return;

            Dispose(true);
            _isDisposed = true;
            if (Disposed != null)
                Disposed(this, EventArgs.Empty);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (AnimatedCursorFrame curFile in _entries)
                    curFile.File.Dispose();
            }
            _entries.Clear();
        }

        /// <summary>
        /// Destructor.
        /// </summary>
        ~AnimatedCursorFile()
        {
            Dispose(false);
        }
        #endregion
#endif
    }

    /// <summary>
    /// Represents rate information for a single frame of an animated cursor.
    /// </summary>
    public sealed class AnimatedCursorFrame :
#if DRAWING
        INotifyPropertyChanged
#else
        DependencyObject
#endif
    {
        /// <summary>
        /// Creates a new instance with the specified cursor file.
        /// </summary>
        /// <param name="file">The cursor file associated with the current instance.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="file"/> is <c>null</c>.
        /// </exception>
        public AnimatedCursorFrame(CursorFile file)
        {
            if (file == null) throw new ArgumentNullException("file");
            _file = file;
        }

        /// <summary>
        /// Creates a new instance with the specified values.
        /// </summary>
        /// <param name="file">The cursor file associated with the current instance.</param>
        /// <param name="jiffies">The delay before displaying the next frame in the animated cursor, in "jiffies" (1/60 of a second).</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="file"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="jiffies"/> is less than 0.
        /// </exception>
        public AnimatedCursorFrame(CursorFile file, int jiffies)
        {
            if (file == null) throw new ArgumentNullException("file");
            if (jiffies < 0) throw new ArgumentOutOfRangeException("jiffies");
            _file = file;
#if DRAWING
            _jiffies = jiffies;
#else
            SetValue(LengthJiffiesProperty, jiffies);
#endif
        }

        /// <summary>
        /// Creates a new instance with the specified values.
        /// </summary>
        /// <param name="file">The cursor file associated with the current instance.</param>
        /// <param name="length">The delay before displaying the next frame in the animated cursor.
        /// Fitted to the nearest "jiffy" (1/60 of a second).</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="file"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="length"/> is less than <see cref="TimeSpan.Zero"/>, or represents a number of "jiffies" (1/60 of a second)
        /// greater than <see cref="int.MaxValue"/>.
        /// </exception>
        public AnimatedCursorFrame(CursorFile file, TimeSpan length)
        {
            if (file == null) throw new ArgumentNullException("file");
            int jiffies = AnimatedCursorFile.TimeToJiffies(length, "length");
            _file = file;
#if DRAWING
            _jiffies = jiffies;
#else
            SetValue(LengthJiffiesProperty, jiffies);
#endif
        }

#if DRAWING
        /// <summary>
        /// Raised when a property on the current instance changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
#endif

        #region File
        private readonly CursorFile _file;
        /// <summary>
        /// Gets and sets the cursor file associated with the current instance.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// In a set operation, the specified value is <c>null</c>.
        /// </exception>
#if !DRAWING
        [Bindable(true)]
#endif
        public CursorFile File
        {
            get { return _file; }
        }
        #endregion

        #region Jiffies
#if DRAWING
        private int? _jiffies;
#else
        /// <summary>
        /// Dependency property for the <see cref="LengthJiffies"/> property.
        /// </summary>
        public static readonly DependencyProperty LengthJiffiesProperty = DependencyProperty.Register("LengthJiffies", typeof(int?), typeof(AnimatedCursorFrame),
            new PropertyMetadata(default(int?), LengthJiffiesChanged, LengthJiffiesCoerce));

        private static void LengthJiffiesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            int? newVal = (int)e.NewValue;
            d.SetValue(LengthProperty, newVal.HasValue ? AnimatedCursorFile.JiffiesToTime(newVal.Value) : default(TimeSpan?));
        }

        private static object LengthJiffiesCoerce(DependencyObject d, object baseValue)
        {
            int? value = (int?)baseValue;

            if (!value.HasValue) return value;

            value = (int)AnimatedCursorFile.JiffiesCoerce(d, value.Value);

            return value;
        }
#endif
        /// <summary>
        /// Gets and sets the delay before displaying the next frame in the animated cursor, in "jiffies" (1/60 of a second),
        /// or <c>null</c> to use the animated cursor file's <see cref="AnimatedCursorFile.DisplayRateJiffies"/> value.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// In a set operation, the specified value is not <c>null</c> and is less than 0.
        /// </exception>
        public int? LengthJiffies
        {
#if DRAWING
            get { return _jiffies; }
#else
            get { return (int?)GetValue(LengthJiffiesProperty); }
#endif
            set
            {
                if (value.HasValue && value.Value < 0) throw new ArgumentOutOfRangeException();
#if DRAWING
                _jiffies = value;
                OnPropertyChanged("Jiffies");
                OnPropertyChanged("Length");
#else
                SetValue(LengthJiffiesProperty, value);
#endif
            }
        }
        #endregion

        #region Length
#if !DRAWING
        /// <summary>
        /// Dependency property for the <see cref="LengthTime"/> property.
        /// </summary>
        public static readonly DependencyProperty LengthProperty = DependencyProperty.Register("LengthTime", typeof(TimeSpan?), typeof(AnimatedCursorFrame),
            new PropertyMetadata(default(TimeSpan?), LengthTimeChanged, LengthTimeCoerce));

        private static void LengthTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimeSpan? newVal = (TimeSpan?)e.NewValue;

            d.SetValue(LengthJiffiesProperty, newVal.HasValue ? AnimatedCursorFile.TimeToJiffies(newVal.Value) : default(int?));
        }

        private static object LengthTimeCoerce(DependencyObject d, object baseValue)
        {
            TimeSpan? value = (TimeSpan?)baseValue;

            if (!value.HasValue) return value;

            value = (TimeSpan)AnimatedCursorFile.TimeCoerce(d, value.Value);

            return value;
        }
#endif
        /// <summary>
        /// Gets and sets the delay before displaying the next frame in the animated cursor, or <c>null</c> to use the
        /// animated cursor file's <see cref="AnimatedCursorFile.DisplayRateTime"/> property.
        /// Fitted to the nearest "jiffy" (1/60 of a second).
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// In a set operation, the specified value is not <c>null</c>, and is less than <see cref="TimeSpan.Zero"/>
        /// or represents a number of <see cref="LengthJiffies"/> greater than <see cref="int.MaxValue"/>.
        /// </exception>
        public TimeSpan? LengthTime
        {
#if DRAWING
            get
            {
                if (_jiffies.HasValue)
                    return new TimeSpan(TimeSpan.TicksPerSecond * _jiffies.Value / 60);
                return null;
            }
            set { LengthJiffies = value.HasValue ? AnimatedCursorFile.TimeToJiffies(value.Value, null) : default(int?); }
#else
            get { return (TimeSpan?)GetValue(LengthJiffiesProperty); }
            set
            {
                if (value.HasValue)
                    AnimatedCursorFile.TimeToJiffies(value.Value, null);
                SetValue(LengthJiffiesProperty, value);
            }
#endif
        }
        #endregion
    }
}