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
using System.IO;
using System.Linq;
#if DRAWING
using System.Drawing;

namespace UIconDrawing
#else
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace UIconEdit
#endif
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
        }

#if DRAWING
        /// <summary>
        /// Loads an icon from the specified handle.
        /// </summary>
        /// <param name="handle">A Windows handle to an icon.</param>
        /// <returns>A loaded <see cref="IconFile"/>.</returns>
        /// <remarks>
        /// When using this method, you must dispose of the original icon by using the <c>DestroyIcon</c> method in the Win32 API
        /// to ensure that the resources are released.
        /// </remarks>
        public static IconFile FromHandle(IntPtr handle)
        {
            using (Icon icon = Icon.FromHandle(handle))
            using (MemoryStream ms = new MemoryStream())
            {
                icon.Save(ms);
                ms.Seek(0, SeekOrigin.Begin);
                return Load(ms);
            }
        }

        /// <summary>
        /// Creates a new instance using the specified <see cref="Icon"/>.
        /// </summary>
        /// <param name="icon">An <see cref="Icon"/> to decode.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="icon"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="IconLoadException">
        /// <paramref name="icon"/> contains invalid values.
        /// </exception>
        public IconFile(Icon icon)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                icon.Save(ms);
                ms.Seek(0, SeekOrigin.Begin);

                using (IconFile copy = Load(ms))
                    Entries.AddBulk(copy.Entries.Select(i => i.Clone()));
            }
        }
#else
        private IEnumerable<IconEntry> _genEntries(IEnumerable<BitmapFrame> frames)
        {
            foreach (BitmapFrame frame in frames)
            {
                IconEntry entry = new IconEntry((BitmapSource)frame.GetCurrentValueAsFrozen(), null,
                    IconEntry.GetBitDepth(frame.Thumbnail.Format.BitsPerPixel), false);

                entry.IsPng = entry.IsPngByDefault;

                yield return entry;
            }
        }

        /// <summary>
        /// Creates a new instance using elements copied from the specified <see cref="IconBitmapDecoder"/>.
        /// </summary>
        /// <param name="decoder">An <see cref="IconBitmapDecoder"/> containing decoded icon images.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="decoder"/> is <c>null</c>.
        /// </exception>
        public IconFile(IconBitmapDecoder decoder)
        {
            if (decoder == null)
                throw new ArgumentNullException("decoder");

            Entries.AddBulk(_genEntries(decoder.Frames).OrderBy(i => i, new IconEntryComparer()));
        }
#endif
        /// <summary>
        /// Loads a <see cref="IconFile"/> from the specified stream.
        /// </summary>
        /// <param name="input">A stream containing an icon file.</param>
        /// <returns>A <see cref="IconFile"/> loaded from <paramref name="input"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="input"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="input"/> is closed or does not support reading.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// <paramref name="input"/> is closed.
        /// </exception>
        /// <exception cref="FileFormatException">
        /// An error occurred when processing the icon file's format.
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurred.
        /// </exception>
        public static new IconFile Load(Stream input)
        {
            return (IconFile)Load(input, IconTypeCode.Icon, null);
        }

        /// <summary>
        /// Loads a <see cref="IconFile"/> from the specified stream.
        /// </summary>
        /// <param name="input">A stream containing an icon file.</param>
        /// <param name="handler">A delegate used to process <see cref="IconLoadException"/>s thrown when processing individual icon entries,
        /// or <c>null</c> to throw an exception in those cases.</param>
        /// <returns>A <see cref="IconFile"/> loaded from <paramref name="input"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="input"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="input"/> is closed or does not support reading.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// <paramref name="input"/> is closed.
        /// </exception>
        /// <exception cref="FileFormatException">
        /// An error occurred when processing the icon file's format.
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurred.
        /// </exception>
        public static new IconFile Load(Stream input, IconLoadExceptionHandler handler)
        {
            return (IconFile)Load(input, IconTypeCode.Icon, handler);
        }

        /// <summary>
        /// Loads an <see cref="IconFileBase"/> implementation from the specified path.
        /// </summary>
        /// <param name="path">The path to a icon file.</param>
        /// <param name="handler">A delegate used to process <see cref="IconLoadException"/>s thrown when processing individual icon entries,
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
        /// <exception cref="FileFormatException">
        /// An error occurred when processing the icon file's format.
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurred.
        /// </exception>
        public static new IconFile Load(string path, IconLoadExceptionHandler handler)
        {
            using (FileStream fs = File.OpenRead(path))
                return (IconFile)Load(fs, IconTypeCode.Icon, handler);
        }

        /// <summary>
        /// Loads an <see cref="IconFileBase"/> implementation from the specified path.
        /// </summary>
        /// <param name="path">The path to a icon file.</param>
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
        /// <exception cref="FileFormatException">
        /// An error occurred when processing the icon file's format.
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurred.
        /// </exception>
        public static new IconFile Load(string path)
        {
            return Load(path, null);
        }

        /// <summary>
        /// Gets the 16-bit type code for the current instance.
        /// </summary>
        public override IconTypeCode ID
        {
            get { return IconTypeCode.Icon; }
        }

        /// <summary>
        /// Returns a duplicate of the current instance.
        /// </summary>
        /// <returns>A duplicate of the current instance, with copies of every icon entry and clones of each
        /// entry's <see cref="IconEntry.BaseImage"/> in <see cref="IconFileBase.Entries"/>.</returns>
        public override IconFileBase Clone()
        {
            return CloneAsIconFile();
        }
    }
}
