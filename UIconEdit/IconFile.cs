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

namespace UIconEdit
{
    /// <summary>
    /// Represents an icon file.
    /// </summary>
    public class IconFile : IconFileBase
    {
        /// <summary>
        /// Gets the 16-bit type code for the current instance.
        /// </summary>
        public override IconTypeCode ID
        {
            get { return IconTypeCode.Icon; }
        }

        private FrameSet _set = new FrameSet();
        /// <summary>
        /// Gets a collection containing all frames in the icon file.
        /// </summary>
        protected override ICollection<IconFrame> FrameCollection
        {
            get { return _set; }
        }

        /// <summary>
        /// Returns the color panes.
        /// </summary>
        /// <param name="frame">This parameter is ignored.</param>
        protected override short GetImgX(IconFrame frame)
        {
            return 1;
        }

        /// <summary>
        /// Returns the number of bits per pixel in the specified frame.
        /// </summary>
        /// <param name="frame">The frame for which to get the bits-per-pixel.</param>
        /// <returns>The number of bits per pixel in <paramref name="frame"/>.</returns>
        protected override short GetImgY(IconFrame frame)
        {
            return frame.BitsPerPixel;
        }

        /// <summary>
        /// Represents a set of frames.
        /// </summary>
        public class FrameSet : IReadOnlyCollection<IconFrame>, ISet<IconFrame>, ICollection
        {
            private HashSet<IconFrame> _set;

            /// <summary>
            /// Creates a new empty set.
            /// </summary>
            public FrameSet()
            {
                _set = new HashSet<IconFrame>(new IconFrameComparer());
            }

            /// <summary>
            /// Creates a new set containing elements copied from the specified collection.
            /// </summary>
            /// <param name="collection">A collection containing elements to add.</param>
            public FrameSet(IEnumerable<IconFrame> collection)
            {
                _set = new HashSet<IconFrame>(collection, new IconFrameComparer());
                _set.Remove(null);
            }

            /// <summary>
            /// Gets the number of elements in the set.
            /// </summary>
            public int Count { get { return _set.Count; } }

            /// <summary>
            /// Adds the specified icon frame to the set.
            /// </summary>
            /// <param name="item">The icon frame to add to the set.</param>
            /// <returns><c>true</c> if <paramref name="item"/> was successfully added; <c>false</c> if <paramref name="item"/> is <c>null</c>
            /// or if an element with the same <see cref="IconFrame.Width"/>, <see cref="IconFrame.Height"/>, and <see cref="IconFrame.BitDepth"/>
            /// already exists in the set.</returns>
            public bool Add(IconFrame item)
            {
                if (item == null) return false;
                return _set.Add(item);
            }

            void ICollection<IconFrame>.Add(IconFrame item)
            {
                Add(item);
            }

            /// <summary>
            /// Removes the specified icon frame from the set.
            /// </summary>
            /// <param name="item">The icon frame to remove.</param>
            /// <returns><c>true</c> if an icon frame with the specified <paramref name="item"/> was successfully found and removed; <c>false</c> if <paramref name="item"/>
            /// was not found in the set.</returns>
            public bool Remove(IconFrame item)
            {
                return _set.Remove(item);
            }

            /// <summary>
            /// Removes all elements from the set.
            /// </summary>
            public void Clear()
            {
                _set.Clear();
            }

            /// <summary>
            /// Determines if an element similar to the specified icon frame exists in the set.
            /// </summary>
            /// <param name="item">The icon frame to search for in the set.</param>
            /// <returns><c>true</c> if <paramref name="item"/> exists in the set; <c>false</c> otherwise.</returns>
            public bool Contains(IconFrame item)
            {
                return item != null && _set.Contains(item);
            }

            /// <summary>
            /// Copies all elements in the set to the specified array.
            /// </summary>
            /// <param name="array">The array to which all elements in the set will be copied.</param>
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
                _set.CopyTo(array, arrayIndex);
            }

            /// <summary>
            /// Adds all elements in the specified collection to the current instance.
            /// </summary>
            /// <param name="other">The other collection to compare.</param>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="other"/> is <c>null</c>.
            /// </exception>
            public void UnionWith(IEnumerable<IconFrame> other)
            {
                if (other is FrameSet) other = ((FrameSet)other)._set;
                _set.UnionWith(other);
            }

            /// <summary>
            /// Removes all elements in the current set which do not exist in the specified collection.
            /// </summary>
            /// <param name="other">The other collection to compare.</param>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="other"/> is <c>null</c>.
            /// </exception>
            public void IntersectWith(IEnumerable<IconFrame> other)
            {
                if (other is FrameSet) other = ((FrameSet)other)._set;
                _set.IntersectWith(other);
            }

            /// <summary>
            /// Removes all elements in the specified collection from the current set.
            /// </summary>
            /// <param name="other">The other collection to compare.</param>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="other"/> is <c>null</c>.
            /// </exception>
            public void ExceptWith(IEnumerable<IconFrame> other)
            {
                if (other is FrameSet) other = ((FrameSet)other)._set;
                _set.ExceptWith(other);
            }

            /// <summary>
            /// Modifies the current set so that it contains elements which are either in the original set or the specified collection, but not both.
            /// </summary>
            /// <param name="other">The other collection to compare.</param>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="other"/> is <c>null</c>.
            /// </exception>
            public void SymmetricExceptWith(IEnumerable<IconFrame> other)
            {
                if (other is FrameSet) other = ((FrameSet)other)._set;
                _set.SymmetricExceptWith(other);
            }

            /// <summary>
            /// Determines if the current set is a subset of the specified collection.
            /// </summary>
            /// <param name="other">The other collection to compare.</param>
            /// <returns><c>true</c> if all elements in the current set are contained in <paramref name="other"/>; <c>false</c> otherwise.</returns>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="other"/> is <c>null</c>.
            /// </exception>
            public bool IsSubsetOf(IEnumerable<IconFrame> other)
            {
                if (other is FrameSet) other = ((FrameSet)other)._set;
                return _set.IsSubsetOf(other);
            }

            /// <summary>
            /// Determines if the current set is a superset of the specified collection.
            /// </summary>
            /// <param name="other">The other collection to compare.</param>
            /// <returns><c>true</c> if all elements in <paramref name="other"/> are contained in the current set; <c>false</c> otherwise.</returns>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="other"/> is <c>null</c>.
            /// </exception>
            public bool IsSupersetOf(IEnumerable<IconFrame> other)
            {
                if (other is FrameSet) other = ((FrameSet)other)._set;
                return _set.IsSupersetOf(other);
            }

            /// <summary>
            /// Determines if the current set is a proper subset of the specified collection.
            /// </summary>
            /// <param name="other">The other collection to compare.</param>
            /// <returns><c>true</c> if all elements in the current set are contained in <paramref name="other"/>, and
            /// <paramref name="other"/> contains at least one element which the current set does not have; <c>false</c> otherwise.</returns>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="other"/> is <c>null</c>.
            /// </exception>
            public bool IsProperSubsetOf(IEnumerable<IconFrame> other)
            {
                if (other is FrameSet) other = ((FrameSet)other)._set;
                return _set.IsProperSubsetOf(other);
            }

            /// <summary>
            /// Determines if the current set is a proper superset of the specified collection.
            /// </summary>
            /// <param name="other">The other collection to compare.</param>
            /// <returns><c>true</c> if all elements in <paramref name="other"/> are contained in the current set, and
            /// the current set contains at least one element which <paramref name="other"/> does not have; <c>false</c> otherwise.</returns>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="other"/> is <c>null</c>.
            /// </exception>
            public bool IsProperSupersetOf(IEnumerable<IconFrame> other)
            {
                if (other is FrameSet) other = ((FrameSet)other)._set;
                return _set.IsProperSupersetOf(other);
            }

            /// <summary>
            /// Determines if the specified collection overlaps with the current set.
            /// </summary>
            /// <param name="other">The other collection to compare.</param>
            /// <returns><c>true</c> if <paramref name="other"/> contains at least one element in the current set; <c>false</c> otherwise.</returns>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="other"/> is <c>null</c>.
            /// </exception>
            public bool Overlaps(IEnumerable<IconFrame> other)
            {
                if (other is FrameSet) other = ((FrameSet)other)._set;
                return _set.Overlaps(other);
            }

            /// <summary>
            /// Determines if the current set and the specified collection are equivalent.
            /// </summary>
            /// <param name="other">The other collection to compare.</param>
            /// <returns><c>true</c> if every element in the current set exists in <paramref name="other"/> and vice versa; <c>false</c> otherwise.</returns>
            public bool SetEquals(IEnumerable<IconFrame> other)
            {
                if (other is FrameSet) other = ((FrameSet)other)._set;
                return _set.SetEquals(other);
            }

            /// <summary>
            /// Returns an enumerator which iterates through the set.
            /// </summary>
            /// <returns>An enumerator which iterates through the set.</returns>
            public Enumerator GetEnumerator()
            {
                return new Enumerator(this);
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
                get { return ((ICollection)_set).IsSynchronized; }
            }

            object ICollection.SyncRoot
            {
                get { return ((ICollection)_set).SyncRoot; }
            }

            void ICollection.CopyTo(Array array, int index)
            {
                ((ICollection)_set).CopyTo(array, index);
            }

            /// <summary>
            /// An enumerator which iterates through the set.
            /// </summary>
            public struct Enumerator : IEnumerator<IconFrame>
            {
                private IconFrame _current;
                private IEnumerator<IconFrame> _enum;

                internal Enumerator(FrameSet set)
                {
                    _current = null;
                    _enum = set._set.GetEnumerator();
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
                /// Advances the enumerator to the next position in the set.
                /// </summary>
                /// <returns><c>true</c> if the enumerator successfully advanced; <c>false</c> if the enumerator has passed the end of the set.</returns>
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
        }
    }
}
