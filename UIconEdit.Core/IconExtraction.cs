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
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace UIconEdit
{
    /// <summary>
    /// Provides methods for extracting icons from EXE and DLL files.
    /// </summary>
    public static class IconExtraction
    {
        private static int _extractCount(string path, int lpszType)
        {
            if (path == null) throw new ArgumentNullException("path");

            IntPtr hModule = IntPtr.Zero;
            int iconCount = 0;
            try
            {
                hModule = Win32Funcs.LoadLibraryEx(path, IntPtr.Zero, Win32Funcs.LOAD_LIBRARY_AS_DATAFILE);

                if (hModule == IntPtr.Zero)
                    throw new Win32Exception();

                ENUMRESNAMEPROC lpEnumFunc = delegate (IntPtr h, int t, IntPtr name, IntPtr l)
                {
                    iconCount++;
                    return true;
                };

                if (!Win32Funcs.EnumResourceNames(hModule, lpszType, lpEnumFunc, 0))
                {
                    var xc = new Win32Exception();
                    if (xc.NativeErrorCode == Win32Funcs.ERROR_RESOURCE_TYPE_NOT_FOUND)
                        return 0;
                    throw xc;
                }
                return iconCount;
            }
            finally
            {
                if (hModule != IntPtr.Zero)
                    Win32Funcs.FreeLibrary(hModule);
            }
        }

        /// <summary>
        /// Determines the number of icons in the specified EXE or DLL file.
        /// </summary>
        /// <param name="path">The path to the file to load.</param>
        /// <returns>The number of icons in the specified file.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="Win32Exception">
        /// An error occurred when attempting to load resources from <paramref name="path"/>.
        /// </exception>
        public static int ExtractIconCount(string path)
        {
            return _extractCount(path, Win32Funcs.RT_GROUP_ICON);
        }

        /// <summary>
        /// Determines the number of cursors in the specified EXE or DLL file.
        /// </summary>
        /// <param name="path">The path to the file to load.</param>
        /// <returns>The number of cursors in the specified file.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="Win32Exception">
        /// An error occurred when attempting to load resources from <paramref name="path"/>.
        /// </exception>
        public static int ExtractCursorCount(string path)
        {
            return _extractCount(path, Win32Funcs.RT_GROUP_CURSOR);
        }

        private static IconFileBase _extractSingle(IntPtr hModule, int lpszType, IntPtr name, IconTypeCode typeCode, IconLoadExceptionHandler handler)
        {
            using (MemoryStream dirStream = Win32Funcs.ExtractData(hModule, lpszType, name))
            using (BinaryReader dirReader = new BinaryReader(dirStream))
            {
                uint head = dirReader.ReadUInt32();
                ushort entryCount = dirReader.ReadUInt16();
                int tSingle = lpszType - 11;

                int iconLength = 6 + (entryCount * 16), picOffset = iconLength;
                for (int i = 0; i < entryCount; i++)
                {
                    int iOff = (14 * i) + 6 + 8;

                    dirStream.Seek(iOff, SeekOrigin.Begin);

                    iconLength += dirReader.ReadInt32();
                }

                using (MemoryStream iconStream = new MemoryStream(iconLength))
                using (BinaryWriter iconWriter = new BinaryWriter(iconStream))
                {
                    iconStream.SetLength(picOffset);

                    iconWriter.Write(head);
                    iconWriter.Write(entryCount);

                    for (int i = 0; i < entryCount; i++)
                    {
                        int dOff = (14 * i) + 6;
                        int iOff = (16 * i) + 6;

                        dirStream.Seek(dOff + 12, SeekOrigin.Begin);
                        ushort id = dirReader.ReadUInt16();

                        using (MemoryStream picStream = Win32Funcs.ExtractData(hModule, tSingle, (IntPtr)id))
                        {
                            iconStream.Seek(iOff, SeekOrigin.Begin);
                            iconStream.Write(dirStream.GetBuffer(), dOff, 8); //First 8 bytes are the same.
                            iconWriter.Write((int)picStream.Length);
                            iconWriter.Write(picOffset);

                            iconStream.Seek(picOffset, SeekOrigin.Begin);
                            picStream.CopyTo(iconStream);

                            picOffset += (int)picStream.Length;
                        }
                    }

                    iconStream.Seek(0, SeekOrigin.Begin);
                    return IconFileBase.Load(iconStream, typeCode, handler);
                }
            }
        }

        private static IconFileBase _extractSingle(string path, int index, int lpszType, IconTypeCode typeCode, IconLoadExceptionHandler handler)
        {
            if (path == null) throw new ArgumentNullException("path");
            if (index < 0) throw new ArgumentOutOfRangeException("index");
            int iconCount = 0;
            IntPtr hModule = IntPtr.Zero;
            try
            {
                hModule = Win32Funcs.LoadLibraryEx(path, IntPtr.Zero, Win32Funcs.LOAD_LIBRARY_AS_DATAFILE);
                if (hModule == IntPtr.Zero)
                    throw new Win32Exception();

                IconFileBase returner = null;
                Exception x = null;
                ENUMRESNAMEPROC lpEnumFunc = delegate (IntPtr h, int t, IntPtr name, IntPtr l)
                {
                    try
                    {
                        if (iconCount == index)
                        {
                            returner = _extractSingle(h, t, name, typeCode, handler);
                            return false;
                        }
                    }
                    catch (Exception e)
                    {
                        x = e;
                        return false;
                    }
                    finally
                    {
                        iconCount++;
                    }
                    return true;
                };

                if (Win32Funcs.EnumResourceNames(hModule, lpszType, lpEnumFunc, 0))
                    throw new ArgumentOutOfRangeException("index");

                if (returner != null) return returner;
                if (x == null) throw new Win32Exception();
                throw x;
            }
            finally
            {
                if (hModule != IntPtr.Zero)
                    Win32Funcs.FreeLibrary(hModule);
            }
        }

        /// <summary>
        /// Extracts a single icon from the specified EXE or DLL file.
        /// </summary>
        /// <param name="path">The path to the file to load.</param>
        /// <param name="index">The zero-based index of the icon in <paramref name="path"/>.</param>
        /// <param name="handler">A delegate used to handle non-fatal <see cref="IconLoadException"/> errors, 
        /// or <c>null</c> to always throw an exception.</param>
        /// <returns>The icon with the specified key in <paramref name="path"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> is less than 0 or is greater than the number of icons in <paramref name="path"/>.
        /// </exception>
        /// <exception cref="Win32Exception">
        /// An error occurred when attempting to load resources from <paramref name="path"/>.
        /// </exception>
        /// <exception cref="FileFormatException">
        /// An error occurred when loading the icon.
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurred.
        /// </exception>
        public static IconFile ExtractIconSingle(string path, int index, IconLoadExceptionHandler handler)
        {
            return (IconFile)_extractSingle(path, index, Win32Funcs.RT_GROUP_ICON, IconTypeCode.Icon, handler);
        }

        /// <summary>
        /// Extracts a single icon from the specified EXE or DLL file.
        /// </summary>
        /// <param name="path">The path to the file to load.</param>
        /// <param name="index">The index of the icon in <paramref name="path"/>.</param>
        /// <returns>The icon with the specified key in <paramref name="path"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> is less than 0 or is greater than the number of icons in <paramref name="path"/>.
        /// </exception>
        /// <exception cref="Win32Exception">
        /// An error occurred when attempting to load resources from <paramref name="path"/>.
        /// </exception>
        /// <exception cref="FileFormatException">
        /// An error occurred when loading the icon.
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurred.
        /// </exception>
        public static IconFile ExtractIconSingle(string path, int index)
        {
            return ExtractIconSingle(path, index, null);
        }

        /// <summary>
        /// Extracts a single cursor from the specified EXE or DLL file.
        /// </summary>
        /// <param name="path">The path to the file to load.</param>
        /// <param name="index">The index of the cursor in <paramref name="path"/>.</param>
        /// <param name="handler">A delegate used to handle non-fatal <see cref="IconLoadException"/> errors, 
        /// or <c>null</c> to always throw an exception.</param>
        /// <returns>The cursor with the specified key in <paramref name="path"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> is less than 0 or is greater than the number of cursors in <paramref name="path"/>.
        /// </exception>
        /// <exception cref="Win32Exception">
        /// An error occurred when attempting to load resources from <paramref name="path"/>.
        /// </exception>
        /// <exception cref="FileFormatException">
        /// An error occurred when loading the icon.
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurred.
        /// </exception>
        public static CursorFile ExtractCursorSingle(string path, int index, IconLoadExceptionHandler handler)
        {
            return (CursorFile)_extractSingle(path, index, Win32Funcs.RT_GROUP_CURSOR, IconTypeCode.Cursor, handler);
        }

        /// <summary>
        /// Extracts a single cursor from the specified EXE or DLL file.
        /// </summary>
        /// <param name="path">The path to the file to load.</param>
        /// <param name="index">The <see cref="IntPtr"/> key of the cursor in <paramref name="path"/>.</param>
        /// <returns>The cursor with the specified key in <paramref name="path"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> is less than 0 or is greater than the number of cursors in <paramref name="path"/>.
        /// </exception>
        /// <exception cref="Win32Exception">
        /// An error occurred when attempting to load resources from <paramref name="path"/>.
        /// </exception>
        /// <exception cref="FileFormatException">
        /// An error occurred when loading the icon.
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurred.
        /// </exception>
        public static CursorFile ExtractCursorSingle(string path, int index)
        {
            return ExtractCursorSingle(path, index, null);
        }

        private static void _forEachIcon<TIconFile>(string path, int lpszType, IconTypeCode typeCode, IconExtractCallback<TIconFile> callback,
            IconExtractExceptionHandler singleHandler, IconExtractExceptionHandler allHandler)
            where TIconFile : IconFileBase
        {
            IconLoadExceptionHandler sHandler = null;

            IntPtr hModule = IntPtr.Zero;

            int curIndex = 0;
            try
            {
                hModule = Win32Funcs.LoadLibraryEx(path, IntPtr.Zero, Win32Funcs.LOAD_LIBRARY_AS_DATAFILE);

                IconExtractException x = null;
                ENUMRESNAMEPROC lpEnumFunc = delegate (IntPtr h, int t, IntPtr name, IntPtr l)
                {
                    try
                    {
                        if (singleHandler != null)
                            sHandler = e => singleHandler(new IconExtractException(e, curIndex));
                        callback(curIndex, (TIconFile)_extractSingle(h, t, name, typeCode, sHandler));
                        return true;
                    }
                    catch (Exception e)
                    {
                        if (e is IconLoadException)
                            x = new IconExtractException((IconLoadException)e, curIndex);
                        else
                            x = new IconExtractException(e, typeCode, curIndex);
                        if (allHandler != null)
                        {
                            allHandler(x);
                            x = null;
                            return true;
                        }
                        return false;
                    }
                    finally
                    {
                        curIndex++;
                    }
                };

                if (!Win32Funcs.EnumResourceNames(hModule, lpszType, lpEnumFunc, 0))
                {
                    if (x == null) throw new Win32Exception();
                    throw x;
                }
            }
            finally
            {
                if (hModule != null)
                    Win32Funcs.FreeLibrary(hModule);
            }
        }

        private static TIconFile[] _extractAll<TIconFile>(string path, int lpszType, IconTypeCode typeCode,
            IconExtractExceptionHandler singleHandler, IconExtractExceptionHandler allHandler)
            where TIconFile : IconFileBase
        {
            LinkedList<TIconFile> allItems = new LinkedList<TIconFile>();

            _forEachIcon<TIconFile>(path, lpszType, typeCode, (curDex, icon) => allItems.AddLast(icon), singleHandler, allHandler);

            return allItems.ToArray();
        }

        /// <summary>
        /// Extracts all icons from the specified EXE or DLL file.
        /// </summary>
        /// <param name="path">The path to the file from which to load all icons.</param>
        /// <param name="singleHandler">A delegate used to handle <see cref="IconLoadException"/>s thrown by a single icon entry in a single icon file,
        /// or <c>null</c> to always throw an exception regardless.</param>
        /// <param name="allHandler">A delegate used to handle all other excpetions thrown by a single icon entry in an icon file,
        /// or <c>null</c> to always throw an exception regardless.</param>
        /// <returns>An array containing all icon files that could be loaded from <paramref name="path"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="Win32Exception">
        /// An error occurred when attempting to load resources from <paramref name="path"/>.
        /// </exception>
        /// <exception cref="IconExtractException">
        /// An error occurred when loading an icon.
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurred.
        /// </exception>
        public static IconFile[] ExtractAllIcons(string path, IconExtractExceptionHandler singleHandler, IconExtractExceptionHandler allHandler)
        {
            return _extractAll<IconFile>(path, Win32Funcs.RT_GROUP_ICON, IconTypeCode.Icon, singleHandler, allHandler);
        }

        /// <summary>
        /// Extracts all icons from the specified EXE or DLL file.
        /// </summary>
        /// <param name="path">The path to the file from which to load all icons.</param>
        /// <returns>An array containing all icon files that could be loaded from <paramref name="path"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="Win32Exception">
        /// An error occurred when attempting to load resources from <paramref name="path"/>.
        /// </exception>
        /// <exception cref="IconExtractException">
        /// An error occurred when loading an icon.
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurred.
        /// </exception>
        public static IconFile[] ExtractAllIcons(string path)
        {
            return ExtractAllIcons(path, null, null);
        }

        /// <summary>
        /// Extracts all cursors from the specified EXE or DLL file.
        /// </summary>
        /// <param name="path">The path to the file from which to load all cursors.</param>
        /// <param name="singleHandler">A delegate used to handle <see cref="IconLoadException"/>s thrown by a single cursor entry in a single cursor file,
        /// or <c>null</c> to always throw an exception regardless.</param>
        /// <param name="allHandler">A delegate used to handle all other excpetions thrown by a single cursor entry in a cursor file,
        /// or <c>null</c> to always throw an exception regardless.</param>
        /// <returns>An array containing all cursor files that could be loaded from <paramref name="path"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="Win32Exception">
        /// An error occurred when attempting to load resources from <paramref name="path"/>.
        /// </exception>
        /// <exception cref="IconExtractException">
        /// An error occurred when loading a cursor.
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurred.
        /// </exception>
        public static CursorFile[] ExtractAllCursors(string path, IconExtractExceptionHandler singleHandler, IconExtractExceptionHandler allHandler)
        {
            return _extractAll<CursorFile>(path, Win32Funcs.RT_GROUP_CURSOR, IconTypeCode.Cursor, singleHandler, allHandler);
        }

        /// <summary>
        /// Extracts all cursors from the specified EXE or DLL file.
        /// </summary>
        /// <param name="path">The path to the file from which to load all cursors.</param>
        /// <returns>An array containing all cursor files that could be loaded from <paramref name="path"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="Win32Exception">
        /// An error occurred when attempting to load resources from <paramref name="path"/>.
        /// </exception>
        /// <exception cref="IconExtractException">
        /// An error occurred when loading a cursor.
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurred.
        /// </exception>
        public static CursorFile[] ExtractAllCursors(string path)
        {
            return ExtractAllCursors(path, null, null);
        }

        /// <summary>
        /// Iterates through each icon in the specified EXE or DLL file, and performs the specified action on each one.
        /// </summary>
        /// <param name="path">The path to the file from which to load all icons.</param>
        /// <param name="callback">An action to perform on each icon.</param>
        /// <param name="singleHandler">A delegate used to handle <see cref="IconLoadException"/>s thrown by a single icon entry in a single cursor file,
        /// or <c>null</c> to always throw an exception regardless.</param>
        /// <param name="allHandler">A delegate used to handle all other excpetions thrown by a single icon entry in an icon file,
        /// or <c>null</c> to always throw an exception regardless.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> or <paramref name="callback"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="Win32Exception">
        /// An error occurred when attempting to load resources from <paramref name="path"/>.
        /// </exception>
        /// <exception cref="IconExtractException">
        /// An error occurred when loading an icon.
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurred.
        /// </exception>
        public static void ExtractIconsForEach(string path, IconExtractCallback<IconFile> callback,
            IconExtractExceptionHandler singleHandler, IconExtractExceptionHandler allHandler)
        {
            if (path == null) throw new ArgumentNullException("path");
            if (callback == null) throw new ArgumentNullException("callback");

            _forEachIcon(path, Win32Funcs.RT_GROUP_ICON, IconTypeCode.Icon, callback, singleHandler, allHandler);
        }

        /// <summary>
        /// Iterates through each icon in the specified EXE or DLL file, and performs the specified action on each one.
        /// </summary>
        /// <param name="path">The path to the file from which to load all icons.</param>
        /// <param name="callback">An action to perform on each icon.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> or <paramref name="callback"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="Win32Exception">
        /// An error occurred when attempting to load resources from <paramref name="path"/>.
        /// </exception>
        /// <exception cref="IconExtractException">
        /// An error occurred when loading an icon.
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurred.
        /// </exception>
        public static void ExtractIconsForEach(string path, IconExtractCallback<IconFile> callback)
        {
            ExtractIconsForEach(path, callback, null, null);
        }

        /// <summary>
        /// Iterates through each cursor in the specified EXE or DLL file, and performs the specified action on each one.
        /// </summary>
        /// <param name="path">The path to the file from which to load all cursors.</param>
        /// <param name="callback">An action to perform on each cursor.</param>
        /// <param name="singleHandler">A delegate used to handle <see cref="IconLoadException"/>s thrown by a single icon entry in a single cursor file,
        /// or <c>null</c> to always throw an exception regardless.</param>
        /// <param name="allHandler">A delegate used to handle all other excpetions thrown by a single icon entry in a cursor file,
        /// or <c>null</c> to always throw an exception regardless.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> or <paramref name="callback"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="Win32Exception">
        /// An error occurred when attempting to load resources from <paramref name="path"/>.
        /// </exception>
        /// <exception cref="IconExtractException">
        /// An error occurred when loading a cursor.
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurred.
        /// </exception>
        public static void ExtractCursorsForEach(string path, IconExtractCallback<CursorFile> callback,
            IconExtractExceptionHandler singleHandler, IconExtractExceptionHandler allHandler)
        {
            if (path == null) throw new ArgumentNullException("path");
            if (callback == null) throw new ArgumentNullException("callback");

            _forEachIcon(path, Win32Funcs.RT_GROUP_CURSOR, IconTypeCode.Cursor, callback, singleHandler, allHandler);
        }

        /// <summary>
        /// Iterates through each cursor in the specified EXE or DLL file, and performs the specified action on each one.
        /// </summary>
        /// <param name="path">The path to the file from which to load all cursors.</param>
        /// <param name="callback">An action to perform on each cursor.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> or <paramref name="callback"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="Win32Exception">
        /// An error occurred when attempting to load resources from <paramref name="path"/>.
        /// </exception>
        /// <exception cref="IconExtractException">
        /// An error occurred when loading a cursor.
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurred.
        /// </exception>
        public static void ExtractCursorsForEach(string path, IconExtractCallback<CursorFile> callback)
        {
            ExtractCursorsForEach(path, callback, null, null);
        }
    }

    /// <summary>
    /// A delegate function to perform on each cursor or icon extracted from a DLL or EXE file.
    /// </summary>
    /// <typeparam name="TIconFile">The type of the <see cref="IconFileBase"/> implementation.</typeparam>
    /// <param name="index">The index of the current cursor or icon to process.</param>
    /// <param name="iconFile">The cursor or icon which was extracted.</param>
    public delegate void IconExtractCallback<TIconFile>(int index, TIconFile iconFile)
        where TIconFile : IconFileBase;
}
