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
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#if DRAWING
namespace UIconDrawing
#else
namespace UIconEdit
#endif
{
    /// <summary>
    /// Represents a read-only sorted list of <see cref="IconEntry"/> objects.
    /// </summary>
    public class FixedIconEntryList : IEntryList, IList
#if IREADONLY
        , IReadOnlyList<IconEntry>
#endif
    {
        private IconEntry[] _items;
        private Dictionary<IconEntryKey, int> _indices;

        /// <summary>
        /// Creates a new list with the specified collection of icon entries.
        /// </summary>
        /// <param name="collection">The collection whose elements will be copied to the new instance.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="collection"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <para><paramref name="collection"/> is empty.</para>
        /// <para>-OR-</para>
        /// <para><paramref name="collection"/> contains one or more <c>null</c> elements.</para>
        /// <para>-OR-</para>
        /// <para><paramref name="collection"/> contains multiple elements with the same <see cref="IconEntry.Width"/>, <see cref="IconEntry.Height"/>,
        /// and <see cref="IconEntry.BitDepth"/>.</para>
        /// </exception>
        public FixedIconEntryList(IEnumerable<IconEntry> collection)
        {
            if (collection == null) throw new ArgumentNullException("collection");

            HashSet<IconEntryKey> keySet = new HashSet<IconEntryKey>();
            int curCount = 0;
            _items = new IconEntry[4];

            foreach (IconEntry entry in collection)
            {
                if (curCount == _items.Length)
                    Array.Resize(ref _items, _items.Length + 1);

                if (entry == null)
                    throw new ArgumentException("The specified collection contains a null entry.", "collection");
                var key = entry.EntryKey;
                if (keySet.Contains(key))
                    throw new ArgumentException("The specified collection contains multiple entries with the same Width, Height, and BitDepth.", "collection");
                keySet.Add(key);
                _items[curCount++] = entry;
            }

            if (curCount == 0)
                throw new ArgumentException("The specified collection is empty.", "collection");

            if (curCount < _items.Length)
                Array.Resize(ref _items, curCount);

            Array.Sort(_items);
            _indices = _items.Select((v, i) => new KeyValuePair<IconEntryKey, int>(v.EntryKey, i)).ToDictionary(i => i.Key, i => i.Value);
        }

        /// <summary>
        /// Gets the element at the specified index.
        /// </summary>
        /// <param name="index">The index of the element to get.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> is less than 0 or is greater than or equal to <see cref="Count"/>.
        /// </exception>
        public IconEntry this[int index]
        {
            get
            {
                if (index < 0 || index >= _items.Length)
                    throw new ArgumentOutOfRangeException("index");
                return _items[index];
            }
        }

        IconEntry IList<IconEntry>.this[int index]
        {
            get { return this[index]; }
            set { throw new NotSupportedException(); }
        }

        object IList.this[int index]
        {
            get { return this[index]; }
            set { throw new NotSupportedException(); }
        }

        /// <summary>
        /// Gets the number of elements contained in the list.
        /// </summary>
        public int Count { get { return _items.Length; } }

        /// <summary>
        /// Determines if the specified value exists in the list.
        /// </summary>
        /// <param name="item">The value to search for in the list.</param>
        /// <returns><c>true</c> if <paramref name="item"/> was found; <c>false</c> otherwise.</returns>
        public bool Contains(IconEntry item)
        {
            return IndexOf(item) >= 0;
        }

        /// <summary>
        /// Determines if an element similar to the specified icon entry exists in the list.
        /// </summary>
        /// <param name="item">The icon entry to compare.</param>
        /// <returns><c>true</c> if an icon entry with the same with the same <see cref="IconEntry.Width"/>, <see cref="IconEntry.Height"/>, and <see cref="IconEntry.BitDepth"/>
        /// as <paramref name="item"/> exists in the list; <c>false</c> otherwise.</returns>
        public bool ContainsSimilar(IconEntry item)
        {
            if (item == null) return false;
            return ContainsSimilar(item.EntryKey);
        }

        /// <summary>
        /// Determines if an element similar to the specified icon entry exists in the list.
        /// </summary>
        /// <param name="key">The icon entry to compare.</param>
        /// <returns><c>true</c> if an icon entry with the same with the same <see cref="IconEntry.Width"/>, <see cref="IconEntry.Height"/>, and <see cref="IconEntry.BitDepth"/>
        /// as <paramref name="key"/> exists in the list; <c>false</c> otherwise.</returns>
        public bool ContainsSimilar(IconEntryKey key)
        {
            return _indices.ContainsKey(key);
        }

        /// <summary>
        /// Determines if an element similar to the specified values exists in the list.
        /// </summary>
        /// <param name="width">The width of the icon entry to search for.</param>
        /// <param name="height">The height of the icon entry to search for.</param>
        /// <param name="bitDepth">The bit depth of the icon entry to search for.</param>
        /// <returns><c>true</c> if an icon entry with the same <see cref="IconEntry.Width"/> as <paramref name="width"/>, the same <see cref="IconEntry.Height"/>
        /// as <paramref name="height"/>, and the same <see cref="IconEntry.BitDepth"/> as <paramref name="bitDepth"/>  was found;
        /// <c>false</c> if no such icon entry was found in the list.</returns>
        public bool ContainsSimilar(int width, int height, IconBitDepth bitDepth)
        {
            return ContainsSimilar(new IconEntryKey(width, height, bitDepth));
        }

        /// <summary>
        /// Gets the index of the specified value.
        /// </summary>
        /// <param name="value">The value to search for in the list.</param>
        /// <returns>The index of <paramref name="value"/>, or -1 if <paramref name="value"/> was not found in the list.</returns>
        public int IndexOf(IconEntry value)
        {
            int dex;
            if (value == null || !_indices.TryGetValue(value.EntryKey, out dex) || value != _items[dex])
                return -1;
            return dex;
        }

        /// <summary>
        /// Gets the index of an element similar to the specified item.
        /// </summary>
        /// <param name="item">The icon entry to compare.</param>
        /// <returns>The index of an icon entry with the same <see cref="IconEntry.Width"/>, <see cref="IconEntry.Height"/>, and <see cref="IconEntry.BitDepth"/>
        /// as <paramref name="item"/>, if found; otherwise, -1.</returns>
        public int IndexOfSimilar(IconEntry item)
        {
            if (item == null) return -1;
            return IndexOfSimilar(item.EntryKey);
        }

        /// <summary>
        /// Gets the index of an element similar to the specified item.
        /// </summary>
        /// <param name="key">The icon entry to compare.</param>
        /// <returns>The index of an icon entry with the same <see cref="IconEntry.Width"/>, <see cref="IconEntry.Height"/>, and <see cref="IconEntry.BitDepth"/>
        /// as <paramref name="key"/>, if found; otherwise, -1.</returns>
        public int IndexOfSimilar(IconEntryKey key)
        {
            int dex;
            if (_indices.TryGetValue(key, out dex))
                return dex;

            return -1;
        }

        /// <summary>
        /// Gets the index of an element similar to the specified values.
        /// </summary>
        /// <param name="width">The width of the icon entry to search for.</param>
        /// <param name="height">The height of the icon entry to search for.</param>
        /// <param name="bitDepth">The bit depth of the icon entry to search for.</param>
        /// <returns>The index of an icon entry with the same <see cref="IconEntry.Width"/> as <paramref name="width"/>, the same <see cref="IconEntry.Height"/>
        /// as <paramref name="height"/>, and the same <see cref="IconEntry.BitDepth"/> as <paramref name="bitDepth"/>, if found; otherwise, -1.</returns>
        public int IndexOfSimilar(int width, int height, IconBitDepth bitDepth)
        {
            return IndexOfSimilar(new IconEntryKey(width, height, bitDepth));
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
        public void CopyTo(IconEntry[] array, int arrayIndex)
        {
            ((IList<IconEntry>)_items).CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Determines if the elements of the current instance are all similar to the specified other <see cref="FixedIconEntryList"/>.
        /// </summary>
        /// <param name="other">The other <see cref="FixedIconEntryList"/> to compare.</param>
        /// <returns><c>true</c> if the current instance contains the same number of elements with the same <see cref="IconEntry.Width"/>,
        /// <see cref="IconEntry.Height"/>, and <see cref="IconEntry.BitDepth"/> values as the specified other <see cref="FixedIconEntryList"/>;
        /// <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="other"/> is <c>null</c>.
        /// </exception>
        public bool SimilarListEquals(FixedIconEntryList other)
        {
            if (other == null) throw new ArgumentNullException("other");
            if (_items.Length != other._items.Length) return false;

            using (IEnumerator<KeyValuePair<IconEntryKey, int>> enum1 = _indices.GetEnumerator())
            using (IEnumerator<KeyValuePair<IconEntryKey, int>> enum2 = other._indices.GetEnumerator())
            {
                while (enum1.MoveNext() && enum2.MoveNext())
                    if (enum1.Current.Key != enum2.Current.Key) return false;
            }
            return true;
        }

        /// <summary>
        /// Determines if the current instance and the specified other <see cref="FixedIconEntryList"/> have values in common.
        /// </summary>
        /// <param name="other">The other <see cref="FixedIconEntryList"/> to compare.</param>
        /// <returns><c>true</c> if <paramref name="other"/> contains at least one <see cref="IconEntry"/> which exists in the current instance;
        /// <c>false</c> otherwise.</returns>
        public bool Overlaps(FixedIconEntryList other)
        {
            if (other == null) throw new ArgumentNullException("other");
            foreach (IconEntry entry in _items)
            {
                if (other.Contains(entry))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Returns an enumerator which iterates through the list.
        /// </summary>
        /// <returns>An enumerator which iterates through the list.</returns>
        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator<IconEntry> IEnumerable<IconEntry>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #region Not Supported
        bool IEntryList.RemoveSimilar(IconEntry item)
        {
            throw new NotSupportedException();
        }

        bool IEntryList.RemoveSimilar(IconEntryKey key)
        {
            throw new NotSupportedException();
        }

        bool IEntryList.RemoveSimilar(int width, int height, IconBitDepth bitDepth)
        {
            throw new NotSupportedException();
        }

        void ICollection<IconEntry>.Add(IconEntry item)
        {
            throw new NotSupportedException();
        }

        void ICollection<IconEntry>.Clear()
        {
            throw new NotSupportedException();
        }

        void IList<IconEntry>.Insert(int index, IconEntry item)
        {
            throw new NotSupportedException();
        }

        bool ICollection<IconEntry>.Remove(IconEntry item)
        {
            throw new NotSupportedException();
        }

        void IList<IconEntry>.RemoveAt(int index)
        {
            throw new NotSupportedException();
        }

        int IList.Add(object value)
        {
            throw new NotSupportedException();
        }

        void IList.Clear()
        {
            throw new NotSupportedException();
        }

        void IList.Insert(int index, object value)
        {
            throw new NotSupportedException();
        }

        void IList.Remove(object value)
        {
            throw new NotSupportedException();
        }

        void IList.RemoveAt(int index)
        {
            throw new NotSupportedException();
        }
        #endregion

        bool IList.Contains(object value)
        {
            return value is IconEntry && Contains((IconEntry)value);
        }

        int IList.IndexOf(object value)
        {
            if (value is IconEntry)
                return IndexOf((IconEntry)value);

            return -1;
        }

        void ICollection.CopyTo(Array array, int index)
        {
            _items.CopyTo(array, index);
        }

        bool ICollection<IconEntry>.IsReadOnly
        {
            get { return true; }
        }

        bool IList.IsReadOnly
        {
            get { return true; }
        }

        bool IList.IsFixedSize
        {
            get { return true; }
        }

        object ICollection.SyncRoot
        {
            get { return _items.SyncRoot; }
        }

        bool ICollection.IsSynchronized
        {
            get { return true; }
        }

        /// <summary>
        /// An enumerator which iterates through the list.
        /// </summary>
        public struct Enumerator : IEnumerator<IconEntry>
        {
            private IconEntry[] _items;
            private int _index;
            private IconEntry _current;

            internal Enumerator(FixedIconEntryList list)
            {
                _items = list._items;
                _index = -1;
                _current = null;
            }

            /// <summary>
            /// Gets the element at the current position in the enumerator.
            /// </summary>
            public IconEntry Current
            {
                get { return _current; }
            }

            object IEnumerator.Current
            {
                get { return _current; }
            }

            /// <summary>
            /// Disposes of the current instance.
            /// </summary>
            public void Dispose()
            {
                if (_items == null) return;
                _items = null;
                _index = -1;
                _current = null;
            }


            /// <summary>
            /// Advances the enumerator to the next position in the list.
            /// </summary>
            /// <returns><c>true</c> if the enumerator successfully advanced; <c>false</c> if the enumerator has passed the end of the list.</returns>
            public bool MoveNext()
            {
                if (_items == null) return false;
                if (++_index >= _items.Length)
                {
                    Dispose();
                    return false;
                }
                _current = _items[_index];
                return true;
            }

            void IEnumerator.Reset()
            {
                throw new InvalidOperationException();
            }
        }
    }
}
