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
    }
}
