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
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;

namespace UIconEdit
{
    /// <summary>
    /// Represents an icon file.
    /// </summary>
    public class IconFile : IconFileBase
    {
        /// <summary>
        /// Creates a new <see cref="IconFile"/> instance.
        /// </summary>
        public IconFile()
        {
            _set = new FrameList(this);
        }

        /// <summary>
        /// Loads an <see cref="IconFile"/> from the specified stream.
        /// </summary>
        /// <param name="input">A stream containing an icon or cursor file.</param>
        /// <returns>An <see cref="IconFile"/> loaded from <paramref name="input"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="input"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="input"/> is closed or does not support reading.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// <paramref name="input"/> is closed.
        /// </exception>
        /// <exception cref="InvalidDataException">
        /// <paramref name="input"/> does not contain a valid icon or cursor file.
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurred.
        /// </exception>
        public static new IconFile Load(Stream input)
        {
            return (IconFile)Load(input, IconTypeCode.Icon);
        }

        /// <summary>
        /// Gets the 16-bit type code for the current instance.
        /// </summary>
        public override IconTypeCode ID
        {
            get { return IconTypeCode.Icon; }
        }

        private FrameList _set;
        /// <summary>
        /// Gets a collection containing all frames in the icon file.
        /// </summary>
        protected override IFrameCollection FrameCollection
        {
            get { return _set; }
        }

        /// <summary>
        /// Gets a collection containing all frames in the icon file.
        /// </summary>
        public FrameList Frames { get { return _set; } }

        /// <summary>
        /// Gets an <see cref="Icon"/> from a single frame.
        /// </summary>
        /// <param name="frame">The icon frame from which to get an <see cref="Icon"/>.</param>
        /// <returns>An <see cref="Icon"/> created from <paramref name="frame"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="frame"/> is <c>null</c>.
        /// </exception>
        public static Icon GetIcon(IconFrame frame)
        {
            if (frame == null) throw new ArgumentNullException("frame");

            IconFile file = new IconFile();
            file.Frames.Add(frame);
            using (MemoryStream ms = new MemoryStream())
            {
                file.Save(ms);
                ms.Seek(0, SeekOrigin.Begin);
                return new Icon(ms);
            }
        }

        /// <summary>
        /// Returns the color panes.
        /// </summary>
        /// <param name="frame">This parameter is ignored.</param>
        protected override ushort GetImgX(IconFrame frame)
        {
            return 1;
        }

        /// <summary>
        /// Returns the number of bits per pixel in the specified frame.
        /// </summary>
        /// <param name="frame">The frame for which to get the bits-per-pixel.</param>
        /// <returns>The number of bits per pixel in <paramref name="frame"/>.</returns>
        protected override ushort GetImgY(IconFrame frame)
        {
            switch (frame.BitDepth)
            {
                case BitDepth.Color2:
                    return 1;
                case BitDepth.Color16:
                    return 4;
                case BitDepth.Color256:
                    return 8;
                case BitDepth.Bit24:
                    return 24;
                default:
                    return 32;
            }
        }

        /// <summary>
        /// Represents a hash list of frames. This collection treats <see cref="IconFrame"/> objects with the same
        /// <see cref="IconFrame.Width"/>, <see cref="IconFrame.Height"/>, and <see cref="IconFrame.BitDepth"/> as though they were equal.
        /// </summary>
        [DebuggerDisplay("Count = {Count}")]
        [DebuggerTypeProxy(typeof(DebugView))]
        public class FrameList : IFrameCollection, ICollection
#if IREADONLY
            , IReadOnlyList<IconFrame>
#endif
        {
            private HashSet<IconFrame> _set;
            private List<IconFrame> _items;
            private IconFile _file;

            internal FrameList(IconFile file)
            {
                _file = file;
                _set = new HashSet<IconFrame>(new IconFrameComparer());
                _items = new List<IconFrame>();
            }

            /// <summary>
            /// Gets and sets the element at the specified index.
            /// </summary>
            /// <param name="index">The index of the element to get or set.</param>
            /// <exception cref="ArgumentOutOfRangeException">
            /// <para><paramref name="index"/> is less than 0 or is greater than or equal to <see cref="Count"/>.</para>
            /// <para>-OR-</para>
            /// <para>In a set operation, the specified value is <c>null</c>.</para>
            /// </exception>
            /// <exception cref="NotSupportedException">
            /// In a set operation, an element with the same <see cref="IconFrame.Width"/>, <see cref="IconFrame.Height"/>, and <see cref="IconFrame.BitDepth"/>
            /// already exists in the list at a different index, or the specified value is already associated with a different icon file.
            /// </exception>
            public IconFrame this[int index]
            {
                get { return _items[index]; }
                set
                {
                    if (value == null) throw new ArgumentOutOfRangeException(null, new ArgumentNullException(null).Message);
                    if (!_setValue(index, value, true))
                        throw new NotSupportedException();
                }
            }

            /// <summary>
            /// Gets the number of elements in the list.
            /// </summary>
            public int Count { get { return _items.Count; } }

            /// <summary>
            /// Adds the specified icon frame to the list.
            /// </summary>
            /// <param name="item">The icon frame to add to the list.</param>
            /// <returns><c>true</c> if <paramref name="item"/> was successfully added; <c>false</c> if <paramref name="item"/> is <c>null</c>,
            /// is already associated with a different icon file, or if an element with the same <see cref="IconFrame.Width"/>, <see cref="IconFrame.Height"/>,
            /// and <see cref="IconFrame.BitDepth"/> already exists in the list.</returns>
            public bool Add(IconFrame item)
            {
                return Insert(_items.Count, item);
            }

            void ICollection<IconFrame>.Add(IconFrame item)
            {
                Add(item);
            }

            /// <summary>
            /// Adds the specified icon frame to the list at the specified index.
            /// </summary>
            /// <param name="index">The index at which to insert the icon frame.</param>
            /// <param name="item">The icon frame to add to the list.</param>
            /// <returns><c>true</c> if <paramref name="item"/> was successfully added; <c>false</c> if <paramref name="item"/> is <c>null</c>,
            /// is already associated with a different icon file, or if an element with the same <see cref="IconFrame.Width"/>, <see cref="IconFrame.Height"/>,
            /// and <see cref="IconFrame.BitDepth"/> already exists in the list.</returns>
            /// <exception cref="ArgumentOutOfRangeException">
            /// <paramref name="index"/> is less than 0 or is greater than <see cref="Count"/>.
            /// </exception>
            public bool Insert(int index, IconFrame item)
            {
                if (index < 0 || index > _items.Count) throw new ArgumentOutOfRangeException("index");
                if (item == null || item.File != null || !_set.Add(item)) return false;
                _items.Insert(index, item);
                item.File = _file;
                return true;
            }

            void IList<IconFrame>.Insert(int index, IconFrame item)
            {
                Insert(index, item);
            }

            private bool _setValue(int index, IconFrame value, bool setter)
            {
                if (setter && index == _items.Count)
                    return Add(value);
                var oldItem = _items[index];
                if (value == null || value.File != null || (_set.Contains(value) && !_set.Comparer.Equals(oldItem, value)))
                    return false;
                _set.Remove(oldItem);
                _set.Add(value);
                _items[index] = value;
                oldItem.File = null;
                value.File = _file;
                return true;
            }

            /// <summary>
            /// Sets the value at the specified index.
            /// </summary>
            /// <param name="index">The index of the value to set.</param>
            /// <param name="item">The item to set at the specified index.</param>
            /// <returns><c>true</c> if <paramref name="item"/> was successfully set; <c>false</c> if <paramref name="item"/> is <c>null</c>,
            /// is already associated with a different icon file, or if an element with the same <see cref="IconFrame.Width"/>, <see cref="IconFrame.Height"/>,
            /// and <see cref="IconFrame.BitDepth"/> already exists at a different index.</returns>
            public bool SetValue(int index, IconFrame item)
            {
                return _setValue(index, item, false);
            }

            private void _removeAt(int index, bool disposing)
            {
                IconFrame item = _items[index];
                _items.RemoveAt(index);
                _set.Remove(item);
                item.File = null;
                if (disposing)
                    item.Dispose();
            }

            /// <summary>
            /// Removes the element at the specified index.
            /// </summary>
            /// <param name="index">The element at the specified index.</param>
            /// <exception cref="ArgumentOutOfRangeException">
            /// <paramref name="index"/> is less than 0 or is greater than or equal to <see cref="Count"/>.
            /// </exception>
            public void RemoveAt(int index)
            {
                _removeAt(index, false);
            }


            /// <summary>
            /// Removes the element at the specified index and immediately calls <see cref="IconFrame.Dispose()"/>.
            /// </summary>
            /// <param name="index">The element at the specified index.</param>
            /// <exception cref="ArgumentOutOfRangeException">
            /// <paramref name="index"/> is less than 0 or is greater than or equal to <see cref="Count"/>.
            /// </exception>
            public void RemoveAndDisposeAt(int index)
            {
                _removeAt(index, true);
            }

            private bool _remove(IconFrame item, bool disposing)
            {
                for (int i = 0; i < _items.Count; i++)
                {
                    if (_set.Comparer.Equals(item, _items[i]))
                    {
                        _removeAt(i, disposing);
                        return true;
                    }
                }
                return false;
            }

            /// <summary>
            /// Removes an icon frame similar to the specified value from the list.
            /// </summary>
            /// <param name="item">The icon frame to search for.</param>
            /// <returns><c>true</c> if an icon frame with the same <see cref="IconFrame.Width"/>, <see cref="IconFrame.Height"/>, and <see cref="IconFrame.BitDepth"/>
            /// as <paramref name="item"/> was successfully found and removed; <c>false</c> if no such icon frame was found in the list.</returns>
            public bool Remove(IconFrame item)
            {
                return _remove(item, false);
            }

            /// <summary>
            /// Removes an icon frame similar to the specified value from the list
            /// and immediately calls <see cref="IconFrame.Dispose()"/>.
            /// </summary>
            /// <param name="item">The icon frame to search for.</param>
            /// <returns><c>true</c> if an icon frame with the same <see cref="IconFrame.Width"/>, <see cref="IconFrame.Height"/>, and <see cref="IconFrame.BitDepth"/>
            /// as <paramref name="item"/> was successfully found and removed; <c>false</c> if no such icon frame was found in the list.</returns>
            public bool RemoveAndDispose(IconFrame item)
            {
                return _remove(item, true);
            }

            private void _clear(bool disposing)
            {
                foreach (IconFrame item in _items)
                {
                    item.File = null;
                    if (disposing)
                        item.Dispose();
                }
                _set.Clear();
                _items.Clear();
            }

            /// <summary>
            /// Removes all elements from the list.
            /// </summary>
            public void Clear()
            {
                _clear(false);
            }

            /// <summary>
            /// Removes all elements from the list and immediately calls <see cref="IconFrame.Dispose()"/> on each one.
            /// </summary>
            public void ClearAndDispose()
            {
                _clear(true);
            }

            /// <summary>
            /// Determines if an element similar to the specified icon frame exists in the list.
            /// </summary>
            /// <param name="item">The icon frame to search for in the list.</param>
            /// <returns><c>true</c> if an icon frame with the same with the same <see cref="IconFrame.Width"/>, <see cref="IconFrame.Height"/>, and <see cref="IconFrame.BitDepth"/>
            /// as <paramref name="item"/> exists in the list; <c>false</c> otherwise.</returns>
            public bool Contains(IconFrame item)
            {
                return _set.Contains(item);
            }

            /// <summary>
            /// Gets the index of an element similar to the specified item.
            /// </summary>
            /// <param name="item">The icon frame to search for in the list.</param>
            /// <returns>The index of an icon frame with the same <see cref="IconFrame.Width"/>, <see cref="IconFrame.Height"/>, and <see cref="IconFrame.BitDepth"/>
            /// as <paramref name="item"/>, if found; otherwise, -1.</returns>
            public int IndexOf(IconFrame item)
            {
                if (item == null) return -1;
                for (int i = 0; i < _items.Count; i++)
                    if (_set.Comparer.Equals(item, _items[i])) return i;
                return -1;
            }

            /// <summary>
            /// Copies all elements in the list to the specified array.
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
            public void CopyTo(IconFrame[] array, int arrayIndex)
            {
                _items.CopyTo(array, arrayIndex);
            }

            /// <summary>
            /// Returns an enumerator which iterates through the list.
            /// </summary>
            /// <returns>An enumerator which iterates through the list.</returns>
            public Enumerator GetEnumerator()
            {
                return new Enumerator(this);
            }

            /// <summary>
            /// Returns an array containing all elements in the current list.
            /// </summary>
            /// <returns>An array containing elements copied from the current list.</returns>
            public IconFrame[] ToArray()
            {
                return _items.ToArray();
            }

            /// <summary>
            /// Removes all elements matching the specified predicate.
            /// </summary>
            /// <param name="match">A predicate used to define the elements to remove.</param>
            /// <returns>The number of elements which were removed.</returns>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="match"/> is <c>null</c>.
            /// </exception>
            public int RemoveWhere(Predicate<IconFrame> match)
            {
                if (match == null) throw new ArgumentNullException("match");
                int removed = 0;
                for (int i = 0; i < _items.Count; i++)
                {
                    while (i < _items.Count && match(_items[i]))
                    {
                        removed++;
                        _set.Remove(_items[i]);
                        _items.RemoveAt(i);
                    }
                }
                return removed;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            IEnumerator<IconFrame> IEnumerable<IconFrame>.GetEnumerator()
            {
                return GetEnumerator();
            }

            bool ICollection<IconFrame>.IsReadOnly
            {
                get { return true; }
            }

            bool ICollection.IsSynchronized
            {
                get { return false; }
            }

            object ICollection.SyncRoot
            {
                get { return ((ICollection)_items).SyncRoot; }
            }

            void ICollection.CopyTo(Array array, int index)
            {
                ((ICollection)_items).CopyTo(array, index);
            }


            /// <summary>
            /// An enumerator which iterates through the list.
            /// </summary>
            public struct Enumerator : IEnumerator<IconFrame>
            {
                private IconFrame _current;
                private IEnumerator<IconFrame> _enum;

                internal Enumerator(FrameList set)
                {
                    _current = null;
                    _enum = set._items.GetEnumerator();
                }

                /// <summary>
                /// Gets the element at the current position in the enumerator.
                /// </summary>
                public IconFrame Current
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
                    if (_enum == null) return;
                    _enum.Dispose();
                    _enum = null;
                    _current = null;
                }

                /// <summary>
                /// Advances the enumerator to the next position in the list.
                /// </summary>
                /// <returns><c>true</c> if the enumerator successfully advanced; <c>false</c> if the enumerator has passed the end of the list.</returns>
                public bool MoveNext()
                {
                    if (_enum == null) return false;
                    if (!_enum.MoveNext())
                    {
                        Dispose();
                        return false;
                    }
                    _current = _enum.Current;
                    return true;
                }

                void IEnumerator.Reset()
                {
                    _enum.Reset();
                }
            }

            private class DebugView
            {
                private FrameList _list;

                public DebugView(FrameList list)
                {
                    _list = list;
                }

                [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
                public IconFrame[] Items
                {
                    get { return _list._items.ToArray(); }
                }
            }
        }
    }
}
