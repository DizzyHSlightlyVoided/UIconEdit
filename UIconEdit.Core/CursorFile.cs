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
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace UIconEdit
{
    /// <summary>
    /// Represents a cursor file.
    /// </summary>
    public class CursorFile : IconFileBase
    {
        /// <summary>
        /// Creates a new <see cref="CursorFile"/> instance.
        /// </summary>
        public CursorFile()
        {
            _entries = new EntryList(this);
        }

        /// <summary>
        /// Loads a <see cref="CursorFile"/> from the specified stream.
        /// </summary>
        /// <param name="input">A stream containing an cursor file.</param>
        /// <returns>A <see cref="CursorFile"/> loaded from <paramref name="input"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="input"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="input"/> is closed or does not support reading.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// <paramref name="input"/> is closed.
        /// </exception>
        /// <exception cref="IconLoadException">
        /// An error occurred when processing the cursor file's format.
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurred.
        /// </exception>
        public static new CursorFile Load(Stream input)
        {
            return (CursorFile)Load(input, IconTypeCode.Cursor, null);
        }

        /// <summary>
        /// Loads a <see cref="CursorFile"/> from the specified stream.
        /// </summary>
        /// <param name="input">A stream containing an cursor file.</param>
        /// <param name="handler">A delegate used to process <see cref="IconLoadException"/>s thrown when processing individual cursor entries,
        /// or <c>null</c> to throw an exception in those cases.</param>
        /// <returns>A <see cref="CursorFile"/> loaded from <paramref name="input"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="input"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="input"/> is closed or does not support reading.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// <paramref name="input"/> is closed.
        /// </exception>
        /// <exception cref="IconLoadException">
        /// An error occurred when processing the cursor file's format.
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurred.
        /// </exception>
        public static new CursorFile Load(Stream input, Action<IconLoadException> handler)
        {
            return (CursorFile)Load(input, IconTypeCode.Cursor, handler);
        }

        /// <summary>
        /// Loads an <see cref="IconFileBase"/> implementation from the specified path.
        /// </summary>
        /// <param name="path">The path to a cursor file.</param>
        /// <param name="handler">A delegate used to process <see cref="IconLoadException"/>s thrown when processing individual cursor entries,
        /// or <c>null</c> to throw an exception in those cases.</param>
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
        /// <exception cref="IOException">
        /// An I/O error occurred.
        /// </exception>
        public static new CursorFile Load(string path, Action<IconLoadException> handler)
        {
            using (FileStream fs = File.OpenRead(path))
                return (CursorFile)Load(fs, IconTypeCode.Cursor, handler);
        }

        /// <summary>
        /// Loads an <see cref="IconFileBase"/> implementation from the specified path.
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
        /// <exception cref="IOException">
        /// An I/O error occurred.
        /// </exception>
        public static new CursorFile Load(string path)
        {
            return Load(path, null);
        }

        /// <summary>
        /// Gets the 16-bit type code for the current instance.
        /// </summary>
        public override IconTypeCode ID
        {
            get { return IconTypeCode.Cursor; }
        }

        private EntryList _entries;
        /// <summary>
        /// Gets a collection containing all entries in the cursor file.
        /// </summary>
        [Bindable(true)]
        public new EntryList Entries { get { return _entries; } }

        /// <summary>
        /// Gets a valid indicating whether the specified instance is a valid <see cref="CursorEntry"/> object.
        /// </summary>
        /// <param name="entry">The cursor entry to test.</param>
        /// <returns><c>true</c> if <paramref name="entry"/> is a <see cref="CursorEntry"/> instance; <c>false</c> otherwise.</returns>
        protected override bool IsValid(IconEntry entry)
        {
            return entry is CursorEntry;
        }

        /// <summary>
        /// Returns a duplicate of the current instance.
        /// </summary>
        /// <returns>A duplicate of the current instance, with copies of every cursor entry and clones of each
        /// entry's <see cref="IconEntry.BaseImage"/> in <see cref="Entries"/>.</returns>
        public override IconFileBase Clone()
        {
            CursorFile copy = (CursorFile)base.Clone();
            copy._entries = new EntryList(copy);
            return copy;
        }

        /// <summary>
        /// Gets the horizontal offset of the hotspot in the specified entry from the left of the specified cursor entry.
        /// </summary>
        /// <param name="entry">The entry from which to get the horizontal offset.</param>
        /// <returns>The horizontal offset of the hotspot from the left in pixels.</returns>
        protected override ushort GetImgX(IconEntry entry)
        {
            return ((CursorEntry)entry).HotspotX;
        }

        /// <summary>
        /// Gets the vertical offset of the hotspot in the specified entry from the top of the specified cursor entry.
        /// </summary>
        /// <param name="entry">The entry from which to get the horizontal offset.</param>
        /// <returns>The vertical offset of the hotspot from the top in pixels.</returns>
        protected override ushort GetImgY(IconEntry entry)
        {
            return ((CursorEntry)entry).HotspotY;
        }

        /// <summary>
        /// Represents a list of cursor entries.
        /// </summary>
        [DebuggerDisplay("Count = {Count}")]
        [DebuggerTypeProxy(typeof(DebugView))]
        public new class EntryList : IList<CursorEntry>, IList, INotifyCollectionChanged, INotifyPropertyChanged
#if IREADONLY
            , IReadOnlyList<CursorEntry>
#endif
        {
            internal EntryList(CursorFile file)
            {
                _file = file;
                _entries = ((IconFileBase)file).Entries;
                _entries.CollectionChanged += _entries_CollectionChanged;
                ((INotifyPropertyChanged)_entries).PropertyChanged += _entries_PropertyChanged;
            }

            private IconFileBase _file;
            private IconFileBase.EntryList _entries;

            /// <summary>
            /// Raised when elements are added to or removed from the list.
            /// </summary>
            public event NotifyCollectionChangedEventHandler CollectionChanged;
            /// <summary>
            /// Raised when a property on the current instance changes.
            /// </summary>
            protected event PropertyChangedEventHandler PropertyChanged;
            event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
            {
                add { PropertyChanged += value; }
                remove { PropertyChanged -= value; }
            }

            private void _entries_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
            {
                if (CollectionChanged != null)
                    CollectionChanged(this, e);
            }

            private void _entries_PropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, e);
            }

            /// <summary>
            /// Gets the number of entries contained in the list.
            /// </summary>
            public int Count { get { return _entries.Count; } }

            /// <summary>
            /// Gets and sets the cursor entry at the specified index.
            /// </summary>
            /// <param name="index">The cursor entry at the specified index.</param>
            /// <exception cref="ArgumentOutOfRangeException">
            /// <para><paramref name="index"/> is less than 0 or is greater than or equal to <see cref="Count"/>.</para>
            /// <para>-OR-</para>
            /// <para>In a set operation, the specified value is <c>null</c>.</para>
            /// </exception>
            /// <exception cref="NotSupportedException">
            /// In a set operation, the specified value is <c>null</c> or is already associated with a different cursor file.
            /// </exception>
            public CursorEntry this[int index]
            {
                get { return (CursorEntry)_entries[index]; }
                set { _entries[index] = value; }
            }

            object IList.this[int index]
            {
                get { return _entries[index]; }
                set { ((IList)_entries)[index] = value; }
            }

            /// <summary>
            /// Sets the value at the specified index.
            /// </summary>
            /// <param name="index">The index of the value to set.</param>
            /// <param name="item">The value to set.</param>
            /// <returns><c>true</c> if <paramref name="item"/> was successfully set; <c>false</c> if <paramref name="item"/> is <c>null</c>,
            /// is already associated with a different icon file, or if an element with the same <see cref="IconEntry.Width"/>, <see cref="IconEntry.Height"/>,
            /// and <see cref="IconEntry.BitDepth"/> already exists in the list at a different index.</returns>
            /// <exception cref="ArgumentOutOfRangeException">
            /// <paramref name="index"/> is less than 0 or is greater than <see cref="Count"/>.
            /// </exception>
            public bool SetValue(int index, CursorEntry item)
            {
                return _entries.SetValue(index, item);
            }

            /// <summary>
            /// Adds the specified cursor entry to the list.
            /// </summary>
            /// <param name="item">The cursor entry to add to the list.</param>
            /// <returns><c>true</c> if <paramref name="item"/> was successfully added; <c>false</c> if <paramref name="item"/> is <c>null</c>,
            /// is already associated with a different icon file, <see cref="Count"/> is equal to <see cref="ushort.MaxValue"/>, or if an element with the same
            /// <see cref="IconEntry.Width"/>, <see cref="IconEntry.Height"/>, and <see cref="IconEntry.BitDepth"/> already exists in the list.</returns>
            public bool Add(CursorEntry item)
            {
                return _entries.Add(item);
            }

            void ICollection<CursorEntry>.Add(CursorEntry item)
            {
                Add(item);
            }

            int IList.Add(object value)
            {
                return ((IList)_entries).Add(value);
            }

            /// <summary>
            /// Inserts the specified cursor entry into the list at the specified index.
            /// </summary>
            /// <param name="index">The index at which the cursor entry will be inserted.</param>
            /// <param name="item">The cursor entry to add to the list.</param>
            /// <returns><c>true</c> if <paramref name="item"/> was successfully added; <c>false</c> if <paramref name="item"/> is <c>null</c>,
            /// is already associated with a different icon file, <see cref="Count"/> is equal to <see cref="ushort.MaxValue"/>, or if an element with the same
            /// <see cref="IconEntry.Width"/>, <see cref="IconEntry.Height"/>, and <see cref="IconEntry.BitDepth"/> already exists in the list.</returns>
            public bool Insert(int index, CursorEntry item)
            {
                return _entries.Insert(index, item);
            }

            void IList<CursorEntry>.Insert(int index, CursorEntry item)
            {
                Insert(index, item);
            }

            void IList.Insert(int index, object value)
            {
                ((IList)_entries).Insert(index, value);
            }

            /// <summary>
            /// Removes the element at the specified index.
            /// </summary>
            /// <param name="index">The index of the cursor entry to remove.</param>
            /// <exception cref="ArgumentOutOfRangeException">
            /// <paramref name="index"/> is less than 0 or is greater than or equal to <see cref="Count"/>.
            /// </exception>
            public void RemoveAt(int index)
            {
                _entries.RemoveAt(index);
            }

            /// <summary>
            /// Removes the specified cursor entry from the list.
            /// </summary>
            /// <param name="item">The cursor entry to to remove from the list.</param>
            /// <returns><c>true</c> if <paramref name="item"/> was found and successfully removed; <c>false</c> otherwise.</returns>
            public bool Remove(CursorEntry item)
            {
                return _entries.Remove(item);
            }

            void IList.Remove(object value)
            {
                ((IList)_entries).Remove(value);
            }

            /// <summary>
            /// Removes an element similar to the specified cursor entry from the list.
            /// </summary>
            /// <param name="item">The cursor entry to to compare.</param>
            /// <returns><c>true</c> if a cursor entry with the same <see cref="IconEntry.Width"/>, <see cref="IconEntry.Height"/>, and <see cref="IconEntry.BitDepth"/>
            /// as <paramref name="item"/> was successfully found and removed; <c>false</c> if no such cursor entry was found in the list.</returns>
            public bool RemoveSimilar(CursorEntry item)
            {
                return _entries.RemoveSimilar(item);
            }

            /// <summary>
            /// Removes a cursor entry similar to the specified value from the list.
            /// </summary>
            /// <param name="key">The entry key to compare.</param>
            /// <returns><c>true</c> if a cursor entry with the same <see cref="IconEntry.Width"/>, <see cref="IconEntry.Height"/>, and <see cref="IconEntry.BitDepth"/>
            /// as <paramref name="key"/> was successfully found and removed; <c>false</c> if no such cursor entry was found in the list.</returns>
            public bool RemoveSimilar(EntryKey key)
            {
                return _entries.RemoveSimilar(key);
            }

            /// <summary>
            /// Removes a cursor entry similar to the specified values from the list.
            /// </summary>
            /// <param name="width">The width of the cursor entry to search for.</param>
            /// <param name="height">The height of the cursor entry to search for.</param>
            /// <param name="bitDepth">The bit depth of the cursor entry to search for.</param>
            /// <returns><c>true</c> if a cursor entry with the same <see cref="IconEntry.Width"/> as <paramref name="width"/>, the same <see cref="IconEntry.Height"/>
            /// as <paramref name="height"/>, and the same <see cref="IconEntry.BitDepth"/> as <paramref name="bitDepth"/>  was successfully found and removed;
            /// <c>false</c> if no such cursor entry was found in the list.</returns>
            public bool RemoveSimilar(short width, short height, BitDepth bitDepth)
            {
                return _entries.RemoveSimilar(width, height, bitDepth);
            }

            /// <summary>
            /// Copies all elements in the list to the specified array, starting at the specified index.
            /// </summary>
            /// <param name="array">The array to which all elements in the list will be copied.</param>
            /// <param name="arrayIndex">The index in <paramref name="array"/> at which copying begins.</param>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="array"/> is <c>null</c>.
            /// </exception>
            /// <exception cref="ArgumentOutOfRangeException">
            /// <paramref name="arrayIndex"/> is less than 0.
            /// </exception>
            /// <exception cref="ArgumentException">
            /// The length of <paramref name="array"/> minus <paramref name="arrayIndex"/> is less than <see cref="Count"/>.
            /// </exception>
            public void CopyTo(CursorEntry[] array, int arrayIndex)
            {
                _entries.CopyTo(array, arrayIndex);
            }

            void ICollection.CopyTo(Array array, int index)
            {
                ((IList)_entries).CopyTo(array, index);
            }

            /// <summary>
            /// Removes all elements from the collection.
            /// </summary>
            public void Clear()
            {
                _entries.Clear();
            }

            /// <summary>
            /// Determines if the specified cursor entry exists in the list.
            /// </summary>
            /// <param name="item">The cursor entry to search for in the list.</param>
            /// <returns><c>true</c> if <paramref name="item"/> was found; <c>false</c> otherwise.</returns>
            public bool Contains(CursorEntry item)
            {
                return _entries.Contains(item);
            }

            bool IList.Contains(object item)
            {
                return Contains(item as CursorEntry);
            }

            /// <summary>
            /// Determines if an element similar to the specified cursor entry exists in the list.
            /// </summary>
            /// <param name="item">The cursor entry to compare.</param>
            /// <returns><c>true</c> if a cursor entry with the same with the same <see cref="IconEntry.Width"/>, <see cref="IconEntry.Height"/>, and <see cref="IconEntry.BitDepth"/>
            /// as <paramref name="item"/> exists in the list; <c>false</c> otherwise.</returns>
            public bool ContainsSimilar(CursorEntry item)
            {
                return _entries.ContainsSimilar(item);
            }

            /// <summary>
            /// Determines if an element similar to the specified value exists in the list.
            /// </summary>
            /// <param name="key">The entry key to compare.</param>
            /// <returns><c>true</c> if a cursor entry with the same with the same <see cref="IconEntry.Width"/>, <see cref="IconEntry.Height"/>, and <see cref="IconEntry.BitDepth"/>
            /// as <paramref name="key"/> exists in the list; <c>false</c> otherwise.</returns>
            public bool ContainsSimilar(EntryKey key)
            {
                return _entries.ContainsSimilar(key);
            }

            /// <summary>
            /// Determines if an element similar to the specified values exists in the list.
            /// </summary>
            /// <param name="width">The width of the cursor entry to search for.</param>
            /// <param name="height">The height of the cursor entry to search for.</param>
            /// <param name="bitDepth">The bit depth of the cursor entry to search for.</param>
            /// <returns><c>true</c> if a cursor entry with the same <see cref="IconEntry.Width"/> as <paramref name="width"/>, the same <see cref="IconEntry.Height"/>
            /// as <paramref name="height"/>, and the same <see cref="IconEntry.BitDepth"/> as <paramref name="bitDepth"/>  was found;
            /// <c>false</c> if no such cursor entry was found in the list.</returns>
            public bool ContainsSimilar(short width, short height, BitDepth bitDepth)
            {
                return _entries.ContainsSimilar(width, height, bitDepth);
            }

            /// <summary>
            /// Gets the index of the specified cursor entry.
            /// </summary>
            /// <param name="item">The cursor entry to search for in the list.</param>
            /// <returns><c>true</c> if <paramref name="item"/> was found; <c>false</c> otherwise.</returns>
            public int IndexOf(CursorEntry item)
            {
                return _entries.IndexOf(item);
            }

            int IList.IndexOf(object item)
            {
                return IndexOf(item as CursorEntry);
            }

            /// <summary>
            /// Gets the index of an element similar to the specified cursor entry.
            /// </summary>
            /// <param name="item">The cursor entry to compare.</param>
            /// <returns>The index of a cursor entry with the same <see cref="IconEntry.Width"/>, <see cref="IconEntry.Height"/>, and <see cref="IconEntry.BitDepth"/>
            /// as <paramref name="item"/>, if found; otherwise, -1.</returns>
            public int IndexOfSimilar(CursorEntry item)
            {
                return _entries.IndexOfSimilar(item);
            }

            /// <summary>
            /// Gets the index of an element similar to the specified value.
            /// </summary>
            /// <param name="key">The entry key to compare.</param>
            /// <returns>The index of a cursor entry with the same <see cref="IconEntry.Width"/>, <see cref="IconEntry.Height"/>, and <see cref="IconEntry.BitDepth"/>
            /// as <paramref name="key"/>, if found; otherwise, -1.</returns>
            public int IndexOfSimilar(EntryKey key)
            {
                return _entries.IndexOfSimilar(key);
            }

            /// <summary>
            /// Gets the index of an element similar to the specified values.
            /// </summary>
            /// <param name="width">The width of the cursor entry to search for.</param>
            /// <param name="height">The height of the cursor entry to search for.</param>
            /// <param name="bitDepth">The bit depth of the cursor entry to search for.</param>
            /// <returns>The index of a cursor entry with the same <see cref="IconEntry.Width"/> as <paramref name="width"/>, the same <see cref="IconEntry.Height"/>
            /// as <paramref name="height"/>, and the same <see cref="IconEntry.BitDepth"/> as <paramref name="bitDepth"/>, if found; otherwise, -1.</returns>
            public int IndexOfSimilar(short width, short height, BitDepth bitDepth)
            {
                return _entries.IndexOfSimilar(width, height, bitDepth);
            }

            /// <summary>
            /// Returns an enumerator which iterates through the list.
            /// </summary>
            /// <returns>An enumerator which iterates through the list.</returns>
            public Enumerator GetEnumerator()
            {
                return new Enumerator(this);
            }

            IEnumerator<CursorEntry> IEnumerable<CursorEntry>.GetEnumerator()
            {
                return GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            /// <summary>
            /// Moves an element from one index to another.
            /// </summary>
            /// <param name="oldIndex">The index of the element to move.</param>
            /// <param name="newIndex">The destination index.</param>
            public void Move(int oldIndex, int newIndex)
            {
                _entries.Move(oldIndex, newIndex);
            }

            /// <summary>
            /// Searches for an element which matches the specified predicate, and returns the first matching cursor entry in the list.
            /// </summary>
            /// <param name="match">A predicate used to define the element to search for.</param>
            /// <returns>A cursor entry matching the specified predicate, or <c>null</c> if no such cursor entry was found.</returns>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="match"/> is <c>null</c>.
            /// </exception>
            public CursorEntry Find(Predicate<CursorEntry> match)
            {
                if (match == null) throw new ArgumentNullException("match");
                return (CursorEntry)_entries.Find(i => match((CursorEntry)i));
            }

            /// <summary>
            /// Searches for an element which matches the specified predicate, and returns the index of the first matching cursor entry in the list.
            /// </summary>
            /// <param name="match">A predicate used to define the element to search for.</param>
            /// <returns>The index of the cursor entry matching the specified predicate, or -1 if no such cursor entry was found.</returns>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="match"/> is <c>null</c>.
            /// </exception>
            public int FindIndex(Predicate<CursorEntry> match)
            {
                if (match == null) throw new ArgumentNullException("match");
                return _entries.FindIndex(i => match((CursorEntry)i));
            }

            /// <summary>
            /// Determines whether any element matching the specified predicate exists in the list.
            /// </summary>
            /// <param name="match">A predicate used to define the elements to search for.</param>
            /// <returns><c>true</c> if at least one element matches the specified predicate; <c>false</c> otherwise.</returns>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="match"/> is <c>null</c>.
            /// </exception>
            public bool Exists(Predicate<CursorEntry> match)
            {
                if (match == null) throw new ArgumentNullException("match");
                return _entries.Exists(i => match((CursorEntry)i));
            }

            /// <summary>
            /// Determines whether every element in the list matches the specified predicate.
            /// </summary>
            /// <param name="match">A predicate used to define the elements to search for.</param>
            /// <returns><c>true</c> if every element in the list matches the specified predicate; <c>false</c> otherwise.</returns>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="match"/> is <c>null</c>.
            /// </exception>
            public bool TrueForAll(Predicate<CursorEntry> match)
            {
                if (match == null) throw new ArgumentNullException("match");
                return _entries.TrueForAll(i => match((CursorEntry)i));
            }

            /// <summary>
            /// Returns a list containing all cursor entries which match the specified predicate.
            /// </summary>
            /// <param name="match">A predicate used to define the elements to search for.</param>
            /// <returns>A list containing all elements matching <paramref name="match"/>.</returns>
            public List<CursorEntry> FindAll(Predicate<CursorEntry> match)
            {
                if (match == null) throw new ArgumentNullException("match");
                return this.Where(i => match(i)).ToList();
            }

            /// <summary>
            /// Returns an array containing all elements in the current list.
            /// </summary>
            /// <returns>An array containing elements copied from the current list.</returns>
            public CursorEntry[] ToArray()
            {
                return this.ToArray<CursorEntry>();
            }

            bool ICollection<CursorEntry>.IsReadOnly
            {
                get { return false; }
            }

            bool IList.IsReadOnly
            {
                get { return false; }
            }

            bool IList.IsFixedSize
            {
                get { return false; }
            }

            bool ICollection.IsSynchronized
            {
                get { return false; }
            }

            object ICollection.SyncRoot
            {
                get { return ((ICollection)_entries).SyncRoot; }
            }

            /// <summary>
            /// An enumerator which iterates through the list.
            /// </summary>
            public struct Enumerator : IEnumerator<CursorEntry>
            {
                private IEnumerator<IconEntry> _enum;
                private CursorEntry _current;

                internal Enumerator(EntryList list)
                {
                    _enum = list._entries.GetEnumerator();
                    _current = null;
                }

                /// <summary>
                /// Disposes of the current instance.
                /// </summary>
                public void Dispose()
                {
                    if (_enum == null) return;
                    _enum.Dispose();
                    _current = null;
                }

                /// <summary>
                /// Gets the element at the current position in the enumerator.
                /// </summary>
                public CursorEntry Current
                {
                    get { return _current; }
                }

                object IEnumerator.Current
                {
                    get { return _current; }
                }

                /// <summary>
                /// Advances the enumerator to the next position in the list.
                /// </summary>
                /// <returns><c>true</c> if the enumerator successfully advanced; <c>false</c> if the enumerator has passed the end of the list.</returns>
                public bool MoveNext()
                {
                    if (_enum == null) return false;
                    if (_enum.MoveNext())
                    {
                        _current = (CursorEntry)_enum.Current;
                        return true;
                    }
                    Dispose();
                    return false;
                }

                void IEnumerator.Reset()
                {
                    _enum.Reset();
                }
            }

            private class DebugView
            {
                private EntryList _list;

                public DebugView(EntryList list)
                {
                    _list = list;
                }

                [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
                public IconEntry[] Items
                {
                    get { return _list.ToArray(); }
                }
            }
        }
    }
}
