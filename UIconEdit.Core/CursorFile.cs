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

        internal override bool IsValid(IconEntry entry)
        {
            return entry != null;
        }

        internal override ushort GetImgX(IconEntry entry)
        {
            return entry.HotspotX;
        }

        internal override ushort GetImgY(IconEntry entry)
        {
            return entry.HotspotY;
        }

        /// <summary>
        /// Returns a duplicate of the current instance.
        /// </summary>
        /// <returns>A duplicate of the current instance, with copies of every icon entry and clones of each
        /// entry's <see cref="IconEntry.BaseImage"/> in <see cref="IconFileBase.Entries"/>.</returns>
        public override IconFileBase Clone()
        {
            return CloneAsCursorFile();
        }
    }
}
