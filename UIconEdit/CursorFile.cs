#region
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
        /// Loads an <see cref="IconFileBase"/> implementation from the specified path.
        /// </summary>
        /// <param name="path">The path to a cursor file.</param>
        /// <returns>An <see cref="IconFileBase"/> implementation loaded from <paramref name="path"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="path"/> is empty, contains only whitespace, or contains one or more invalid path characters as defined in <see cref="Path.GetInvalidPathChars()"/>.
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
        /// <exception cref="InvalidDataException">
        /// <paramref name="path"/> does not contain a valid cursor file.
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurred.
        /// </exception>
        public static new CursorFile Load(string path)
        {
            using (FileStream fs = File.OpenRead(path))
                return Load(fs);
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
        public new CursorFrameList Frames { get { return _frames; } }

        /// <summary>
        /// Gets a valid indicating whether the specified instance is a valid <see cref="CursorFrame"/> object.
        /// </summary>
        /// <param name="frame">The icon frame to test.</param>
        /// <returns><c>true</c> if <paramref name="frame"/> is a <see cref="CursorFrame"/> instance; <c>false</c> otherwise.</returns>
        protected override bool IsValid(IconFrame frame)
        {
            return frame is CursorFrame;
        }

        /// <summary>
        /// Returns a duplicate of the current instance.
        /// </summary>
        /// <returns>A duplicate of the current instance, with copies of every icon frame and clones of each
        /// frame's <see cref="IconFrame.BaseImage"/> in <see cref="Frames"/>.</returns>
        public override IconFileBase Clone()
        {
            CursorFile copy = (CursorFile)base.Clone();
            copy._frames = new CursorFrameList(copy);
            return copy;
        }

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
        [DebuggerDisplay("Count = {Count}")]
        [DebuggerTypeProxy(typeof(DebugView))]
        public class CursorFrameList : IList<CursorFrame>, IList
#if IREADONLY
            , IReadOnlyList<CursorFrame>
#endif
        {
            internal CursorFrameList(CursorFile file)
            {
                _file = file;
            }

            private IconFileBase _file;

            /// <summary>
            /// Gets the number of frames contained in the list.
            /// </summary>
            public int Count { get { return _file.Frames.Count; } }

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
                get { return (CursorFrame)_file.Frames[index]; }
                set { _file.Frames[index] = value; }
            }

            object IList.this[int index]
            {
                get { return _file.Frames[index]; }
                set { ((IList)_file.Frames)[index] = value; }
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
                return _file.Frames.SetValue(index, item);
            }

            /// <summary>
            /// Adds the specified cursor frame to the list.
            /// </summary>
            /// <param name="item">The cursor frame to add to the list.</param>
            /// <returns><c>true</c> if <paramref name="item"/> was successfully added; <c>false</c> if <paramref name="item"/> is <c>null</c>,
            /// is already associated with a different icon file, <see cref="Count"/> is equal to <see cref="ushort.MaxValue"/>, or if an element with the same
            /// <see cref="IconFrame.Width"/>, <see cref="IconFrame.Height"/>, and <see cref="IconFrame.BitDepth"/> already exists in the list.</returns>
            public bool Add(CursorFrame item)
            {
                return _file.Frames.Add(item);
            }

            void ICollection<CursorFrame>.Add(CursorFrame item)
            {
                Add(item);
            }

            int IList.Add(object value)
            {
                return ((IList)_file.Frames).Add(value);
            }

            /// <summary>
            /// Inserts the specified cursor frame into the list at the specified index.
            /// </summary>
            /// <param name="index">The index at which the cursor frame will be inserted.</param>
            /// <param name="item">The cursor frame to add to the list.</param>
            /// <returns><c>true</c> if <paramref name="item"/> was successfully added; <c>false</c> if <paramref name="item"/> is <c>null</c>,
            /// is already associated with a different icon file, <see cref="Count"/> is equal to <see cref="ushort.MaxValue"/>, or if an element with the same
            /// <see cref="IconFrame.Width"/>, <see cref="IconFrame.Height"/>, and <see cref="IconFrame.BitDepth"/> already exists in the list.</returns>
            public bool Insert(int index, CursorFrame item)
            {
                return _file.Frames.Insert(index, item);
            }

            void IList<CursorFrame>.Insert(int index, CursorFrame item)
            {
                Insert(index, item);
            }

            void IList.Insert(int index, object value)
            {
                ((IList)_file.Frames).Insert(index, value);
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
                _file.Frames.RemoveAt(index);
            }

            /// <summary>
            /// Removes the element at the specified index and, if it does not exist elsewhere in the file, immediately calls <see cref="IconFrame.Dispose()"/>.
            /// </summary>
            /// <param name="index">The index of the icon frame to remove.</param>
            public void RemoveAndDisposeAt(int index)
            {
                _file.Frames.RemoveAndDisposeAt(index);
            }

            /// <summary>
            /// Removes the specified cursor frame from the list.
            /// </summary>
            /// <param name="item">The cursor frame to to remove from the list.</param>
            /// <returns><c>true</c> if <paramref name="item"/> was found and successfully removed; <c>false</c> otherwise.</returns>
            public bool Remove(CursorFrame item)
            {
                return _file.Frames.Remove(item);
            }

            void IList.Remove(object value)
            {
                ((IList)_file.Frames).Remove(value);
            }

            /// <summary>
            /// Removes the specified cursor frame from the list and immediately calls <see cref="IconFrame.Dispose()"/>.
            /// </summary>
            /// <param name="item">The cursor frame to to remove from the list.</param>
            /// <returns><c>true</c> if <paramref name="item"/> was found and successfully removed; <c>false</c> otherwise.</returns>
            public bool RemoveAndDispose(CursorFrame item)
            {
                return _file.Frames.RemoveAndDispose(item);
            }

            /// <summary>
            /// Removes an element similar to the specified cursor frame from the list.
            /// </summary>
            /// <param name="item">The cursor frame to to compare.</param>
            /// <returns><c>true</c> if an icon frame with the same <see cref="IconFrame.Width"/>, <see cref="IconFrame.Height"/>, and <see cref="IconFrame.BitDepth"/>
            /// as <paramref name="item"/> was successfully found and removed; <c>false</c> if no such icon frame was found in the list.</returns>
            public bool RemoveSimilar(CursorFrame item)
            {
                return _file.Frames.RemoveSimilar(item);
            }

            /// <summary>
            /// Removes the specified cursor frame from the list, immediately calls <see cref="IconFrame.Dispose()"/>.
            /// </summary>
            /// <param name="item">The cursor frame to compare.</param>
            /// <returns><c>true</c> if an icon frame with the same <see cref="IconFrame.Width"/>, <see cref="IconFrame.Height"/>, and <see cref="IconFrame.BitDepth"/>
            /// as <paramref name="item"/> was successfully found and removed; <c>false</c> if no such icon frame was found in the list.</returns>
            public bool RemoveAndDisposeSimilar(CursorFrame item)
            {
                return _file.Frames.RemoveAndDisposeSimilar(item);
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
                _file.Frames.CopyTo(array);
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
                _file.Frames.CopyTo(array, arrayIndex);
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
                _file.Frames.CopyTo(index, array, arrayIndex, count);
            }

            void ICollection.CopyTo(Array array, int index)
            {
                ((IList)_file.Frames).CopyTo(array, index);
            }

            /// <summary>
            /// Removes all elements from the collection.
            /// </summary>
            public void Clear()
            {
                _file.Frames.Clear();
            }

            /// <summary>
            /// Removes all elements from the collection and immediately calls <see cref="IconFrame.Dispose()"/> on each one.
            /// </summary>
            public void ClearAndDispose()
            {
                _file.Frames.Clear();
            }

            /// <summary>
            /// Determines if the specified cursor frame exists in the list.
            /// </summary>
            /// <param name="item">The cursor frame to search for in the list.</param>
            /// <returns><c>true</c> if <paramref name="item"/> was found; <c>false</c> otherwise.</returns>
            public bool Contains(CursorFrame item)
            {
                return _file.Frames.Contains(item);
            }

            bool IList.Contains(object item)
            {
                return Contains(item as CursorFrame);
            }

            /// <summary>
            /// Determines if an element similar to the specified cursor frame exists in the list.
            /// </summary>
            /// <param name="item">The cursor frame to compare.</param>
            /// <returns><c>true</c> if a cursor frame with the same with the same <see cref="IconFrame.Width"/>, <see cref="IconFrame.Height"/>, and <see cref="IconFrame.BitDepth"/>
            /// as <paramref name="item"/> exists in the list; <c>false</c> otherwise.</returns>
            public bool ContainsSimilar(CursorFrame item)
            {
                return _file.Frames.ContainsSimilar(item);
            }

            /// <summary>
            /// Gets the index of the specified cursor frame.
            /// </summary>
            /// <param name="item">The cursor frame to search for in the list.</param>
            /// <returns><c>true</c> if <paramref name="item"/> was found; <c>false</c> otherwise.</returns>
            public int IndexOf(CursorFrame item)
            {
                return _file.Frames.IndexOf(item);
            }

            int IList.IndexOf(object item)
            {
                return IndexOf(item as CursorFrame);
            }

            /// <summary>
            /// Gets the index of an element similar to the specified cursor frame.
            /// </summary>
            /// <param name="item">The cursor frame to compare.</param>
            /// <returns>The index of a cursor frame with the same <see cref="IconFrame.Width"/>, <see cref="IconFrame.Height"/>, and <see cref="IconFrame.BitDepth"/>
            /// as <paramref name="item"/>, if found; otherwise, -1.</returns>
            public int IndexOfSimilar(CursorFrame item)
            {
                return _file.Frames.IndexOfSimilar(item);
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

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            /// <summary>
            /// Removes all elements matching the specified predicate.
            /// </summary>
            /// <param name="match">A predicate used to define the elements to remove.</param>
            /// <returns>The number of elements which were removed.</returns>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="match"/> is <c>null</c>.
            /// </exception>
            public int RemoveWhere(Predicate<CursorFrame> match)
            {
                if (match == null) throw new ArgumentNullException("match");
                return _file.Frames.RemoveWhere(i => match((CursorFrame)i));
            }

            /// <summary>
            /// Removes all elements matching the specified predicate and immediately calls <see cref="IconFrame.Dispose()"/>.
            /// </summary>
            /// <param name="match">A predicate used to define the elements to remove.</param>
            /// <returns>The number of elements which were removed.</returns>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="match"/> is <c>null</c>.
            /// </exception>
            public int RemoveAndDisposeWhere(Predicate<CursorFrame> match)
            {
                if (match == null) throw new ArgumentNullException("match");
                return _file.Frames.RemoveAndDisposeWhere(i => match((CursorFrame)i));
            }

            /// <summary>
            /// Returns an array containing all elements in the current list.
            /// </summary>
            /// <returns>An array containing elements copied from the current list.</returns>
            public CursorFrame[] ToArray()
            {
                return this.ToArray<CursorFrame>();
            }

            bool ICollection<CursorFrame>.IsReadOnly
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
                get { return ((ICollection)_file.Frames).SyncRoot; }
            }

            /// <summary>
            /// An enumerator which iterates through the list.
            /// </summary>
            public struct Enumerator : IEnumerator<CursorFrame>
            {
                private IEnumerator<IconFrame> _enum;
                private CursorFrame _current;

                internal Enumerator(CursorFrameList list)
                {
                    _enum = list._file.Frames.GetEnumerator();
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
                        _current = (CursorFrame)_enum.Current;
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
                private CursorFrameList _list;

                public DebugView(CursorFrameList list)
                {
                    _list = list;
                }

                [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
                public IconFrame[] Items
                {
                    get { return _list.ToArray(); }
                }
            }
        }
    }
}
