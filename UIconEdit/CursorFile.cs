using System;
using System.Collections;
using System.Collections.Generic;
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
            _frames = new CursorFrameList(this);
        }

        /// <summary>
        /// Loads a <see cref="CursorFile"/> from the specified stream.
        /// </summary>
        /// <param name="input">A stream containing an icon or cursor file.</param>
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
        /// <exception cref="InvalidDataException">
        /// <paramref name="input"/> does not contain a valid icon or cursor file.
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurred.
        /// </exception>
        public static new CursorFile Load(Stream input)
        {
            return (CursorFile)Load(input, IconTypeCode.Cursor);
        }

        /// <summary>
        /// Gets the 16-bit type code for the current instance.
        /// </summary>
        public override IconTypeCode ID
        {
            get { return IconTypeCode.Cursor; }
        }

        private CursorFrameList _frames;
        /// <summary>
        /// Gets a collection containing all frames in the cursor file.
        /// </summary>
        protected override IFrameCollection FrameCollection
        {
            get { return _frames; }
        }
        /// <summary>
        /// Gets a collection containing all frames in the cursor file.
        /// </summary>
        public new CursorFrameList Frames { get { return _frames; } }

        /// <summary>
        /// Gets the horizontal offset of the hotspot in the specified frame from the left of the specified cursor frame.
        /// </summary>
        /// <param name="frame">The frame from which to get the horizontal offset.</param>
        /// <returns>The horizontal offset of the hotspot from the left in pixels.</returns>
        protected override ushort GetImgX(IconFrame frame)
        {
            return ((CursorFrame)frame).HotspotX;
        }

        /// <summary>
        /// Gets the vertical offset of the hotspot in the specified frame from the top of the specified cursor frame.
        /// </summary>
        /// <param name="frame">The frame from which to get the horizontal offset.</param>
        /// <returns>The vertical offset of the hotspot from the top in pixels.</returns>
        protected override ushort GetImgY(IconFrame frame)
        {
            return ((CursorFrame)frame).HotspotY;
        }

        /// <summary>
        /// Represents a list of cursor frames.
        /// </summary>
        public class CursorFrameList : IList<CursorFrame>, IFrameCollection, ICollection
#if IREADONLY
            , IReadOnlyList<CursorFrame>, IReadOnlyList<IconFrame>
#endif
        {
            internal CursorFrameList(CursorFile file)
            {
                _file = file;
                _items = new List<CursorFrame>();
                _set = new HashSet<CursorFrame>(new IconFrameComparer());
            }

            private CursorFile _file;
            private List<CursorFrame> _items;
            private HashSet<CursorFrame> _set;

            /// <summary>
            /// Gets the number of frames contained in the list.
            /// </summary>
            public int Count { get { return _items.Count; } }

            /// <summary>
            /// Gets and sets the cursor frame at the specified index.
            /// </summary>
            /// <param name="index">The cursor frame at the specified index.</param>
            /// <exception cref="ArgumentOutOfRangeException">
            /// <para><paramref name="index"/> is less than 0 or is greater than or equal to <see cref="Count"/>.</para>
            /// <para>-OR-</para>
            /// <para>In a set operation, the specified value is <c>null</c>.</para>
            /// </exception>
            /// <exception cref="NotSupportedException">
            /// In a set operation, the specified value is <c>null</c> or is already associated with a different cursor file.
            /// </exception>
            public CursorFrame this[int index]
            {
                get { return _items[index]; }
                set
                {
                    if (value == null) throw new ArgumentOutOfRangeException(null, new ArgumentNullException().Message);
                    if (!_setValue(index, value, true))
                        throw new NotSupportedException();
                }
            }

            IconFrame IList<IconFrame>.this[int index]
            {
                get { return _items[index]; }
                set { this[index] = value as CursorFrame; }
            }
#if IREADONLY
            IconFrame IReadOnlyList<IconFrame>.this[int index]
            {
                get { return _items[index]; }
            }
#endif
            private bool _setValue(int index, CursorFrame value, bool setter)
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
            /// <param name="item">The value to set.</param>
            /// <returns><c>true</c> if <paramref name="item"/> was successfully set; <c>false</c> if <paramref name="item"/> is <c>null</c>,
            /// is already associated with a different icon file, or if an element with the same <see cref="IconFrame.Width"/>, <see cref="IconFrame.Height"/>,
            /// and <see cref="IconFrame.BitDepth"/> already exists in the list at a different index.</returns>
            /// <exception cref="ArgumentOutOfRangeException">
            /// <paramref name="index"/> is less than 0 or is greater than <see cref="Count"/>.
            /// </exception>
            public bool SetValue(int index, CursorFrame item)
            {
                return _setValue(index, item, false);
            }

            bool IFrameCollection.SetValue(int index, IconFrame item)
            {
                return SetValue(index, item as CursorFrame);
            }

            /// <summary>
            /// Adds the specified cursor frame to the list.
            /// </summary>
            /// <param name="item">The cursor frame to add to the list.</param>
            /// <returns><c>true</c> if <paramref name="item"/> was successfully added; <c>false</c> if <paramref name="item"/> is <c>null</c>,
            /// is already associated with a different icon file, or if an element with the same <see cref="IconFrame.Width"/>, <see cref="IconFrame.Height"/>,
            /// and <see cref="IconFrame.BitDepth"/> already exists in the list.</returns>
            public bool Add(CursorFrame item)
            {
                return Insert(_items.Count, item);
            }

            bool IFrameCollection.Add(IconFrame item)
            {
                return Add(item as CursorFrame);
            }

            void ICollection<IconFrame>.Add(IconFrame item)
            {
                Add(item as CursorFrame);
            }

            void ICollection<CursorFrame>.Add(CursorFrame item)
            {
                Add(item);
            }

            /// <summary>
            /// Inserts the specified cursor frame into the list at the specified index.
            /// </summary>
            /// <param name="index">The index at which the cursor frame will be inserted.</param>
            /// <param name="item">The cursor frame to add to the list.</param>
            /// <returns><c>true</c> if <paramref name="item"/> was successfully added; <c>false</c> if <paramref name="item"/> is <c>null</c>,
            /// is already associated with a different icon file, or if an element with the same <see cref="IconFrame.Width"/>, <see cref="IconFrame.Height"/>,
            /// and <see cref="IconFrame.BitDepth"/> already exists in the list.</returns>
            public bool Insert(int index, CursorFrame item)
            {
                if (index < 0 || index > _items.Count) throw new ArgumentOutOfRangeException("index");
                if (item == null || item.File != null || !_set.Add(item)) return false;
                _items.Insert(index, item);
                item.File = _file;
                return true;
            }

            void IList<CursorFrame>.Insert(int index, CursorFrame item)
            {
                Insert(index, item);
            }

            bool IFrameCollection.Insert(int index, IconFrame item)
            {
                return Insert(index, item as CursorFrame);
            }

            void IList<IconFrame>.Insert(int index, IconFrame item)
            {
                Insert(index, item as CursorFrame);
            }

            private void _removeAt(int index, bool disposing)
            {
                CursorFrame item = _items[index];
                _items.RemoveAt(index);
                _set.Remove(item);
                item.File = null;
                if (disposing)
                    item.Dispose();
            }

            /// <summary>
            /// Removes the element at the specified index.
            /// </summary>
            /// <param name="index">The index of the icon frame to remove.</param>
            /// <exception cref="ArgumentOutOfRangeException">
            /// <paramref name="index"/> is less than 0 or is greater than or equal to <see cref="Count"/>.
            /// </exception>
            public void RemoveAt(int index)
            {
                _removeAt(index, false);
            }

            /// <summary>
            /// Removes the element at the specified index and, if it does not exist elsewhere in the file, immediately calls <see cref="IconFrame.Dispose()"/>.
            /// </summary>
            /// <param name="index">The index of the icon frame to remove.</param>
            public void RemoveAndDisposeAt(int index)
            {
                _removeAt(index, true);
            }

            private bool _remove(CursorFrame item, bool disposing)
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
            /// Removes the specified cursor frame from the list.
            /// </summary>
            /// <param name="item">The cursor frame to remove from the list.</param>
            /// <returns><c>true</c> if an icon frame with the same <see cref="IconFrame.Width"/>, <see cref="IconFrame.Height"/>, and <see cref="IconFrame.BitDepth"/>
            /// as <paramref name="item"/> was successfully found and removed; <c>false</c> if no such icon frame was found in the list.</returns>
            public bool Remove(CursorFrame item)
            {
                return _remove(item, false);
            }

            bool ICollection<IconFrame>.Remove(IconFrame item)
            {
                return _remove(item as CursorFrame, false);
            }

            /// <summary>
            /// Removes the specified cursor frame from the list, immediately calls <see cref="IconFrame.Dispose()"/>.
            /// </summary>
            /// <param name="item">The cursor frame to remove from the list.</param>
            /// <returns><c>true</c> if an icon frame with the same <see cref="IconFrame.Width"/>, <see cref="IconFrame.Height"/>, and <see cref="IconFrame.BitDepth"/>
            /// as <paramref name="item"/> was successfully found and removed; <c>false</c> if no such icon frame was found in the list.</returns>
            public bool RemoveAndDispose(CursorFrame item)
            {
                return _remove(item, true);
            }

            bool IFrameCollection.RemoveAndDispose(IconFrame item)
            {
                return _remove(item as CursorFrame, true);
            }

            /// <summary>
            /// Copies all elements in the list to the specified array.
            /// </summary>
            /// <param name="array">The array to which all elements in the list will be copied.</param>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="array"/> is <c>null</c>.
            /// </exception>
            /// <exception cref="ArgumentException">
            /// The length of <paramref name="array"/> is less than <see cref="Count"/>.
            /// </exception>
            public void CopyTo(CursorFrame[] array)
            {
                _items.CopyTo(array);
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
            public void CopyTo(CursorFrame[] array, int arrayIndex)
            {
                _items.CopyTo(array, arrayIndex);
            }

            /// <summary>
            /// Copies a range of elements in the list to the specified array.
            /// </summary>
            /// <param name="index">The index of the first item to copy.</param>
            /// <param name="array">The array to which all elements in the list will be copied.</param>
            /// <param name="arrayIndex">The index in <paramref name="array"/> at which copying begins.</param>
            /// <param name="count">The number of elements to copy.</param>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="array"/> is <c>null</c>.
            /// </exception>
            /// <exception cref="ArgumentOutOfRangeException">
            /// <paramref name="index"/>, <paramref name="arrayIndex"/>, or <paramref name="count"/> is less than 0.
            /// </exception>
            /// <exception cref="ArgumentException">
            /// <para><paramref name="index"/> and <paramref name="count"/> do not indicate a valid range of elements in the current instance.</para>
            /// <para>-OR-</para>
            /// <para><paramref name="arrayIndex"/> and <paramref name="count"/> do not indicate a valid range of elements in <paramref name="array"/>.</para>
            /// </exception>
            public void CopyTo(int index, CursorFrame[] array, int arrayIndex, int count)
            {
                _items.CopyTo(index, array, arrayIndex, count);
            }

            void ICollection<IconFrame>.CopyTo(IconFrame[] array, int arrayIndex)
            {
                ((IList)_items).CopyTo(array, arrayIndex);
            }

            void ICollection.CopyTo(Array array, int index)
            {
                ((IList)_items).CopyTo(array, index);
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
            /// Removes all elements from the collection.
            /// </summary>
            public void Clear()
            {
                _clear(false);
            }

            /// <summary>
            /// Removes all elements from the collection and immediately calls <see cref="IconFrame.Dispose()"/> on each one.
            /// </summary>
            public void ClearAndDispose()
            {
                _clear(true);
            }

            /// <summary>
            /// Determines if an element similar to the specified icon frame exists in the list.
            /// </summary>
            /// <param name="item">The cursor frame to search for in the list.</param>
            /// <returns><c>true</c> if a cursor frame with the same with the same <see cref="IconFrame.Width"/>, <see cref="IconFrame.Height"/>, and <see cref="IconFrame.BitDepth"/>
            /// as <paramref name="item"/> exists in the list; <c>false</c> otherwise.</returns>
            public bool Contains(CursorFrame item)
            {
                return item != null && _set.Contains(item);
            }

            bool ICollection<IconFrame>.Contains(IconFrame item)
            {
                return Contains(item as CursorFrame);
            }

            /// <summary>
            /// Gets the index of an element similar to the specified item.
            /// </summary>
            /// <param name="item">The cursor frame to search for in the list.</param>
            /// <returns>The index of a cursor frame with the same <see cref="IconFrame.Width"/>, <see cref="IconFrame.Height"/>, and <see cref="IconFrame.BitDepth"/>
            /// as <paramref name="item"/>, if found; otherwise, -1.</returns>
            public int IndexOf(CursorFrame item)
            {
                if (item == null) return -1;
                for (int i = 0; i < _items.Count; i++)
                    if (_set.Comparer.Equals(item, _items[i])) return i;
                return -1;
            }

            int IList<IconFrame>.IndexOf(IconFrame item)
            {
                return IndexOf(item as CursorFrame);
            }

            /// <summary>
            /// Returns an enumerator which iterates through the list.
            /// </summary>
            /// <returns>An enumerator which iterates through the list.</returns>
            public Enumerator GetEnumerator()
            {
                return new Enumerator(this);
            }

            IEnumerator<CursorFrame> IEnumerable<CursorFrame>.GetEnumerator()
            {
                return GetEnumerator();
            }

            IEnumerator<IconFrame> IEnumerable<IconFrame>.GetEnumerator()
            {
                return GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            /// <summary>
            /// Returns an array containing all elements in the current list.
            /// </summary>
            /// <returns>An array containing elements copied from the current list.</returns>
            public CursorFrame[] ToArray()
            {
                return _items.ToArray();
            }

            IconFrame[] IFrameCollection.ToArray()
            {
                return this.ToArray<IconFrame>();
            }

            bool ICollection<CursorFrame>.IsReadOnly
            {
                get { return false; }
            }

            bool ICollection<IconFrame>.IsReadOnly
            {
                get { return false; }
            }

            bool ICollection.IsSynchronized
            {
                get { return false; }
            }

            object ICollection.SyncRoot
            {
                get { return ((ICollection)_items).SyncRoot; }
            }

            /// <summary>
            /// An enumerator which iterates through the list.
            /// </summary>
            public struct Enumerator : IEnumerator<CursorFrame>, IEnumerator<IconFrame>
            {
                private IEnumerator<CursorFrame> _enum;
                private CursorFrame _current;

                internal Enumerator(CursorFrameList list)
                {
                    _enum = list._items.GetEnumerator();
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
                public CursorFrame Current
                {
                    get { return _current; }
                }

                IconFrame IEnumerator<IconFrame>.Current
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
                        _current = _enum.Current;
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
        }
    }
}
