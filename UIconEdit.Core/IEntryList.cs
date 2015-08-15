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
using System.Collections.Generic;

#if DRAWING
namespace UIconDrawing
#else
namespace UIconEdit
#endif
{
    /// <summary>
    /// A base class for <see cref="IconEntry"/> collections.
    /// </summary>
    public interface IEntryList : IList<IconEntry>
    {
        /// <summary>
        /// Determines if an element similar to the specified icon entry exists in the list.
        /// </summary>
        /// <param name="item">The icon entry to compare.</param>
        /// <returns><c>true</c> if an icon entry with the same with the same <see cref="IconEntry.Width"/>, <see cref="IconEntry.Height"/>, and <see cref="IconEntry.BitDepth"/>
        /// as <paramref name="item"/> exists in the list; <c>false</c> otherwise.</returns>
        bool ContainsSimilar(IconEntry item);

        /// <summary>
        /// Determines if an element similar to the specified icon entry exists in the list.
        /// </summary>
        /// <param name="key">The icon entry to compare.</param>
        /// <returns><c>true</c> if an icon entry with the same with the same <see cref="IconEntry.Width"/>, <see cref="IconEntry.Height"/>, and <see cref="IconEntry.BitDepth"/>
        /// as <paramref name="key"/> exists in the list; <c>false</c> otherwise.</returns>
        bool ContainsSimilar(IconEntryKey key);

        /// <summary>
        /// Determines if an element similar to the specified values exists in the list.
        /// </summary>
        /// <param name="width">The width of the icon entry to search for.</param>
        /// <param name="height">The height of the icon entry to search for.</param>
        /// <param name="bitDepth">The bit depth of the icon entry to search for.</param>
        /// <returns><c>true</c> if an icon entry with the same <see cref="IconEntry.Width"/> as <paramref name="width"/>, the same <see cref="IconEntry.Height"/>
        /// as <paramref name="height"/>, and the same <see cref="IconEntry.BitDepth"/> as <paramref name="bitDepth"/>  was found;
        /// <c>false</c> if no such icon entry was found in the list.</returns>
        bool ContainsSimilar(int width, int height, IconBitDepth bitDepth);


        /// <summary>
        /// Gets the index of an element similar to the specified item.
        /// </summary>
        /// <param name="item">The icon entry to compare.</param>
        /// <returns>The index of an icon entry with the same <see cref="IconEntry.Width"/>, <see cref="IconEntry.Height"/>, and <see cref="IconEntry.BitDepth"/>
        /// as <paramref name="item"/>, if found; otherwise, -1.</returns>
        int IndexOfSimilar(IconEntry item);

        /// <summary>
        /// Gets the index of an element similar to the specified item.
        /// </summary>
        /// <param name="key">The icon entry to compare.</param>
        /// <returns>The index of an icon entry with the same <see cref="IconEntry.Width"/>, <see cref="IconEntry.Height"/>, and <see cref="IconEntry.BitDepth"/>
        /// as <paramref name="key"/>, if found; otherwise, -1.</returns>
        int IndexOfSimilar(IconEntryKey key);

        /// <summary>
        /// Gets the index of an element similar to the specified values.
        /// </summary>
        /// <param name="width">The width of the icon entry to search for.</param>
        /// <param name="height">The height of the icon entry to search for.</param>
        /// <param name="bitDepth">The bit depth of the icon entry to search for.</param>
        /// <returns>The index of an icon entry with the same <see cref="IconEntry.Width"/> as <paramref name="width"/>, the same <see cref="IconEntry.Height"/>
        /// as <paramref name="height"/>, and the same <see cref="IconEntry.BitDepth"/> as <paramref name="bitDepth"/>, if found; otherwise, -1.</returns>
        int IndexOfSimilar(int width, int height, IconBitDepth bitDepth);

        /// <summary>
        /// Removes an icon entry similar to the specified value from the list.
        /// </summary>
        /// <param name="item">The icon entry to compare.</param>
        /// <returns><c>true</c> if an icon entry with the same <see cref="IconEntry.Width"/>, <see cref="IconEntry.Height"/>, and <see cref="IconEntry.BitDepth"/>
        /// as <paramref name="item"/> was successfully found and removed; <c>false</c> if no such icon entry was found in the list.</returns>
        /// <exception cref="NotSupportedException">
        /// The collection is read-only.
        /// </exception>
        bool RemoveSimilar(IconEntry item);

        /// <summary>
        /// Removes an icon entry similar to the specified value from the list.
        /// </summary>
        /// <param name="key">The icon entry to compare.</param>
        /// <returns><c>true</c> if an icon entry with the same <see cref="IconEntry.Width"/>, <see cref="IconEntry.Height"/>, and <see cref="IconEntry.BitDepth"/>
        /// as <paramref name="key"/> was successfully found and removed; <c>false</c> if no such icon entry was found in the list.</returns>
        /// <exception cref="NotSupportedException">
        /// The collection is read-only.
        /// </exception>
        bool RemoveSimilar(IconEntryKey key);

        /// <summary>
        /// Removes an icon entry similar to the specified values from the list.
        /// </summary>
        /// <param name="width">The width of the icon entry to search for.</param>
        /// <param name="height">The height of the icon entry to search for.</param>
        /// <param name="bitDepth">The bit depth of the icon entry to search for.</param>
        /// <returns><c>true</c> if an icon entry with the same <see cref="IconEntry.Width"/> as <paramref name="width"/>, the same <see cref="IconEntry.Height"/>
        /// as <paramref name="height"/>, and the same <see cref="IconEntry.BitDepth"/> as <paramref name="bitDepth"/>  was successfully found and removed;
        /// <c>false</c> if no such icon entry was found in the list.</returns>
        /// <exception cref="NotSupportedException">
        /// The collection is read-only.
        /// </exception>
        bool RemoveSimilar(int width, int height, IconBitDepth bitDepth);
    }
}
