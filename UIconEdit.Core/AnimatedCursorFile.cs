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
using System.Collections.ObjectModel;
using System.IO;

#if DRAWING
namespace UIconDrawing
#else
using System.Windows.Threading;

namespace UIconEdit
#endif
{
    /// <summary>
    /// Represents an animated cursor file.
    /// </summary>
    public class AnimatedCursorFile :
#if DRAWING
        IDisposable
#else
        DispatcherObject
#endif
    {
        private ObservableCollection<IconFileBase> _entries = new ObservableCollection<IconFileBase>();
        /// <summary>
        /// Gets a list of <see cref="IconFileBase"/> objects containing all entries in the animated cursor file.
        /// </summary>
        public ObservableCollection<IconFileBase> Entries { get { return _entries; } }

        private ObservableCollection<AnimatedCursorFrame> _frames = new ObservableCollection<AnimatedCursorFrame>();
        /// <summary>
        /// Gets a list containing all frame information in the current instance.
        /// </summary>
        public ObservableCollection<AnimatedCursorFrame> Frames { get { return _frames; } }

#if DRAWING
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
                foreach (CursorFile curFile in _entries)
                    curFile.Dispose();
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

        /// <summary>
        /// Raised when the current instance is disposed.
        /// </summary>
        public event EventHandler Disposed;
#endif
    }

    /// <summary>
    /// Represents a single frame of an animated cursor.
    /// </summary>
    public struct AnimatedCursorFrame : IEquatable<AnimatedCursorFrame>
    {
        /// <summary>
        /// Creates a new instance with the specified values.
        /// </summary>
        /// <param name="index">The index of the frame in <see cref="AnimatedCursorFile.Entries"/>.</param>
        /// <param name="jiffies">The delay before displaying the next frame in the animated cursor, in "jiffies" (1/60 of a second).</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> or <paramref name="jiffies"/> are less than 0.
        /// </exception>
        public AnimatedCursorFrame(int index, int jiffies)
        {
            if (index < 0) throw new ArgumentOutOfRangeException("index");
            if (jiffies < 0) throw new ArgumentOutOfRangeException("jiffies");
            _index = index;
            _jiffies = jiffies;
        }

        /// <summary>
        /// Creates a new instance with the specified values.
        /// </summary>
        /// <param name="index">The index of the frame in <see cref="AnimatedCursorFile.Entries"/>.</param>
        /// <param name="length">The delay before displaying the next frame in the animated cursor.
        /// Fitted to the nearest "jiffy" (1/60 of a second).</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <para><paramref name="index"/> is less than 0.</para>
        /// <para>-OR-</para>
        /// <para><paramref name="length"/> is less than <see cref="TimeSpan.Zero"/>.</para>
        /// </exception>
        public AnimatedCursorFrame(int index, TimeSpan length)
        {
            if (index < 0) throw new ArgumentOutOfRangeException("index");
            if (length < TimeSpan.Zero) throw new ArgumentOutOfRangeException("length");
            _index = index;
            _jiffies = (int)(length.TotalSeconds * 60);
        }

        private int _index;
        /// <summary>
        /// Gets and sets the index of the frame in <see cref="AnimatedCursorFile.Entries"/>.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// In a set operation, the specified value is less than 0.
        /// </exception>
        public int Index
        {
            get { return _index; }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException();
                _index = value;
            }
        }

        private int _jiffies;
        /// <summary>
        /// Gets and sets the delay before displaying the next frame in the animated cursor, in "jiffies" (1/60 of a second).
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// In a set operation, the specified value is less than 0.
        /// </exception>
        public int Jiffies
        {
            get { return _jiffies; }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException();
                _jiffies = value;
            }
        }

        /// <summary>
        /// Gets and sets the delay before displaying the next frame in the animated cursor.
        /// Fitted to the nearest "jiffy" (1/60 of a second).
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// In a set operation, the specified value is less than <see cref="TimeSpan.Zero"/>.
        /// </exception>
        public TimeSpan Length
        {
            get { return TimeSpan.FromSeconds(_jiffies / 60.0); }
            set
            {
                if (value < TimeSpan.Zero) throw new ArgumentOutOfRangeException();
                _jiffies = (int)(value.TotalSeconds * 60);
            }
        }

        /// <summary>
        /// Returns a string representation of the current instance.
        /// </summary>
        /// <returns>A string representation of the current instance.</returns>
        public override string ToString()
        {
            return string.Format("Index: {0}, Jiffies: {1} ({2})", _index, _jiffies, Length);
        }


        /// <summary>
        /// Determines if the current instance is equal to the specified other <see cref="AnimatedCursorFrame"/> value.
        /// </summary>
        /// <param name="other">The other <see cref="AnimatedCursorFrame"/> to compare.</param>
        /// <returns><c>true</c> if the current instance is equal to <paramref name="other"/>; <c>false</c> otherwise.</returns>
        public bool Equals(AnimatedCursorFrame other)
        {
            return _index == other._index && _jiffies == other._jiffies;
        }

        /// <summary>
        /// Determines if the current instance is equal to the specified other object.
        /// </summary>
        /// <param name="obj">The other objecto compare.</param>
        /// <returns><c>true</c> if the current instance is equal to <paramref name="obj"/>; <c>false</c> otherwise.</returns>
        public override bool Equals(object obj)
        {
            return obj is AnimatedCursorFrame && Equals((AnimatedCursorFrame)obj);
        }

        /// <summary>
        /// Returns the hash code for the current value.
        /// </summary>
        /// <returns>The hash code for the current value.</returns>
        public override int GetHashCode()
        {
            return _index ^ _jiffies;
        }

        /// <summary>
        /// Determines equality of two <see cref="AnimatedCursorFrame"/> objects.
        /// </summary>
        /// <param name="a1">An <see cref="AnimatedCursorFrame"/> to compare.</param>
        /// <param name="a2">An <see cref="AnimatedCursorFrame"/> to compare.</param>
        /// <returns><c>true</c> if <paramref name="a1"/> is equal to <paramref name="a2"/>; <c>false</c> otherwise.</returns>
        public static bool operator ==(AnimatedCursorFrame a1, AnimatedCursorFrame a2)
        {
            return a1.Equals(a2);
        }

        /// <summary>
        /// Determines inequality of two <see cref="AnimatedCursorFrame"/> objects.
        /// </summary>
        /// <param name="a1">An <see cref="AnimatedCursorFrame"/> to compare.</param>
        /// <param name="a2">An <see cref="AnimatedCursorFrame"/> to compare.</param>
        /// <returns><c>true</c> if <paramref name="a1"/> is not equal to <paramref name="a2"/>; <c>false</c> otherwise.</returns>
        public static bool operator !=(AnimatedCursorFrame a1, AnimatedCursorFrame a2)
        {
            return a1.Equals(a2);
        }
    }
}