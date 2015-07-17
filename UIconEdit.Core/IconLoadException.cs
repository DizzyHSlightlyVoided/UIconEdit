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
using System.IO;
using System.Windows.Media.Imaging;

namespace UIconEdit
{

    /// <summary>
    /// The exception that is thrown when an icon file contains invalid data.
    /// </summary>
    public class IconLoadException : FileFormatException
    {
        private static string DefaultMessage { get { return new InvalidDataException().Message; } }

        internal const int BeforeEntries = -1;

        /// <summary>
        /// Creates a new instance using a message which describes the error and the specified error code.
        /// </summary>
        /// <param name="message">A message describing the error.</param>
        /// <param name="code">The error code used to identify the cause of the error.</param>
        /// <param name="typeCode">The type code of the file which caused the error.</param>
        /// <param name="value">The value which caused the error.</param>
        /// <param name="index">The index of the entry in the icon file which caused this error, or -1 if it occurred before processing the icon entries.</param>
        public IconLoadException(string message, IconErrorCode code, IconTypeCode typeCode, object value, int index)
            : base(message)
        {
            _code = code;
            _index = index;
            _value = value;
            _typeCode = typeCode;
        }

        /// <summary>
        /// Creates a new instance using a message which describes the error and the specified error code.
        /// </summary>
        /// <param name="message">A message describing the error.</param>
        /// <param name="code">The error code used to identify the cause of the error.</param>
        /// <param name="typeCode">The type code of the file which caused the error.</param>
        /// <param name="value">The value which caused the error.</param>
        public IconLoadException(string message, IconErrorCode code, IconTypeCode typeCode, object value)
            : this(message, code, typeCode, value, BeforeEntries)
        {
        }

        /// <summary>
        /// Creates a new instance using a message which describes the error and the specified error code.
        /// </summary>
        /// <param name="message">A message describing the error.</param>
        /// <param name="code">The error code used to identify the cause of the error.</param>
        /// <param name="typeCode">The type code of the file which caused the error.</param>
        /// <param name="index">The index of the entry in the icon file which caused this error, or -1 if it occurred before processing the icon entries.</param>
        public IconLoadException(string message, IconErrorCode code, IconTypeCode typeCode, int index)
            : this(message, code, typeCode, null, index)
        {
        }

        /// <summary>
        /// Creates a new instance with the default message and the specified error code.
        /// </summary>
        /// <param name="code">The error code used to identify the cause of the error.</param>
        /// <param name="typeCode">The type code of the file which caused the error.</param>
        /// <param name="value">The value which caused the error.</param>
        /// <param name="index">The index of the entry in the icon file which caused this error, or -1 if it occurred before processing the icon entries.</param>
        public IconLoadException(IconErrorCode code, IconTypeCode typeCode, object value, int index)
            : this(DefaultMessage, code, typeCode, value, index)
        {
        }

        /// <summary>
        /// Creates a new instance with the default message and the specified error code.
        /// </summary>
        /// <param name="code">The error code used to identify the cause of the error.</param>
        /// <param name="typeCode">The type code of the file which caused the error.</param>
        /// <param name="index">The index of the entry in the icon file which caused this error, or -1 if it occurred before processing the icon entries.</param>
        public IconLoadException(IconErrorCode code, IconTypeCode typeCode, int index)
            : this(DefaultMessage, code, typeCode, null, index)
        {
        }

        /// <summary>
        /// Creates a new instance with the default message and the specified error code.
        /// </summary>
        /// <param name="code">The error code used to identify the cause of the error.</param>
        /// <param name="typeCode">The type code of the file which caused the error.</param>
        /// <param name="value">The value which caused the error.</param>
        public IconLoadException(IconErrorCode code, IconTypeCode typeCode, object value)
            : this(DefaultMessage, code, typeCode, value, BeforeEntries)
        {
        }

        /// <summary>
        /// Creates a new instance with the default message and the specified error code.
        /// </summary>
        /// <param name="code">The error code used to identify the cause of the error.</param>
        /// <param name="typeCode">The type code of the file which caused the error.</param>
        public IconLoadException(IconErrorCode code, IconTypeCode typeCode)
            : this(DefaultMessage, code, typeCode, null, BeforeEntries)
        {
        }

        /// <summary>
        /// Creates a new instance with the specified message and error code and a reference to the exception which caused this error.
        /// </summary>
        /// <param name="message">A message describing the error.</param>
        /// <param name="code">The error code used to identify the cause of the error.</param>
        /// <param name="index">The index of the entry in the icon file which caused this error, or -1 if it occurred before processing the icon entries.</param>
        /// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException"/> parameter
        /// is not <c>null</c>, the current exception should be raised in a <c>catch</c> block which handles the inner exception.</param>
        /// <param name="typeCode">The type code of the file which caused the error.</param>
        public IconLoadException(string message, IconErrorCode code, IconTypeCode typeCode, int index, Exception innerException)
            : base(message, innerException)
        {
            _code = code;
            _index = index;
            _typeCode = typeCode;
        }

        /// <summary>
        /// Creates a new instance with the specified message and a reference to the exception which caused this error.
        /// </summary>
        /// <param name="message">A message describing the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException"/> parameter
        /// is not <c>null</c>, the current exception should be raised in a <c>catch</c> block which handles the inner exception.</param>
        public IconLoadException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        internal IconLoadException(IconLoadException e)
#if DEBUG
            : base(e.BaseMessage, e)
#else
            : base(e.BaseMessage)
#endif
        {
            _code = e._code;
            _index = e._index;
            _value = e._value;
            _typeCode = e._typeCode;
        }

        internal string BaseMessage { get { return base.Message; } }

        /// <summary>
        /// Gets a message describing the error.
        /// </summary>
        public override string Message
        {
            get
            {
                List<string> messages = new List<string>();

                if (!string.IsNullOrWhiteSpace(base.Message)) messages.Add(base.Message);
                if (_code != IconErrorCode.Unknown)
                    messages.Add(string.Format("Code: 0x{0:x}, {1}", (int)_code, _code));
                if (_index >= 0)
                    messages.Add("Entry index: " + _index);
                if (_value != null)
                    messages.Add("Value: " + _value);
                switch (messages.Count)
                {
                    case 0: return base.Message;
                    case 1: return messages[0];
                    default: return string.Join(Environment.NewLine, messages);
                }
            }
        }

        private IconErrorCode _code;
        /// <summary>
        /// Gets an error code describing the icon error.
        /// </summary>
        public IconErrorCode Code { get { return _code; } }

        private int _index;
        /// <summary>
        /// Gets the index in the icon file of the icon entry which caused this exception,
        /// or -1 if it occurred before the icon entries were processed.
        /// </summary>
        public int Index { get { return _index; } }

        private object _value;
        /// <summary>
        /// Gets an object whose value caused the error, or <c>null</c> if there was no such value.
        /// </summary>
        public object Value { get { return _value; } }

        private IconTypeCode _typeCode;
        /// <summary>
        /// Gets a value indicating the type of the icon file.
        /// </summary>
        public IconTypeCode TypeCode { get { return _typeCode; } }
    }

    /// <summary>
    /// The exception that is thrown when an icon file extracted from an EXE or DLL file contains invalid data.
    /// </summary>
    public class IconExtractException : IconLoadException
    {
        internal IconExtractException(IconLoadException e, int index)
            : base(e)
        {
            _extractIndex = index;
        }

        internal IconExtractException(Exception e, int index)
            : base(e.Message, e)
        {
            _extractIndex = index;
        }

        private int _extractIndex;
        /// <summary>
        /// The index in the DLL or EXE file of the loaded icon or cursor.
        /// </summary>
        public int ExtractIndex { get { return _extractIndex; } }
    }

    /// <summary>
    /// Indicates the cause of an <see cref="IconLoadException"/>.
    /// </summary>
    public enum IconErrorCode : ushort
    {
        /// <summary>
        /// Code 0: the cause of the error is unknown.
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Code 0x1: The file is not a valid cursor or icon format.
        /// This is a fatal error, and the icon file cannot continue processing when it occurs.
        /// </summary>
        InvalidFormat = 1,
        /// <summary>
        /// Code 0x2: The icon contains zero entries.
        /// This is a fatal error, and the icon file cannot continue processing when it occurs.
        /// </summary>
        ZeroEntries = 2,
        /// <summary>
        /// Code 0x3: One of the icon directory entries has a length less than or equal to 40 bytes, which is logically too small for either a BMP or a PNG file.
        /// <see cref="IconLoadException.Value"/> contains the length.
        /// This is a fatal error, and the icon file cannot continue processing when it occurs.
        /// </summary>
        ResourceTooSmall = 3,
        /// <summary>
        /// Code 0x4: One of the icon directory entries has a starting offset which would overlap with the list of entries.
        /// <see cref="IconLoadException.Value"/> contains the offset.
        /// This is a fatal error, and the icon file cannot continue processing when it occurs.
        /// </summary>
        ResourceTooEarly = 4,
        /// <summary>
        /// Code 0x5: One or more of the icon directory entries have overlapping offset/length combinations.
        /// This is a fatal error, and the icon file cannot continue processing when it occurs.
        /// </summary>
        ResourceOverlap = 5,
        /// <summary>
        /// Code 0x6: An icon was expected but a cursor was loaded, or vice versa.
        /// <see cref="IconLoadException.Value"/> contains the expected value.
        /// This is a fatal error, and the icon file cannot continue processing when it occurs.
        /// </summary>
        WrongType = 6,
        /// <summary>
        /// Code 0x1000: the file type of an entry is invalid.
        /// </summary>
        InvalidEntryType = 0x1000,
        /// <summary>
        /// Code 0x1001: the file is an icon, and an icon directory entry has a bit depth with any value other than 0, 1, 4, 8, 24, or 32.
        /// <see cref="IconLoadException.Value"/> contains the bit depth.
        /// </summary>
        InvalidBitDepth = 0x1001,
        /// <summary>
        /// There are no remaining valid entries after processing.
        /// This is a fatal error, and the icon file cannot continue processing when it occurs.
        /// </summary>
        ZeroValidEntries = 0x1002,
        /// <summary>
        /// Code 0x1100: an error occurred when attempting to load a PNG entry. The inner exception may contain more information.
        /// </summary>
        InvalidPngFile = 0x1100,
        /// <summary>
        /// Code 0x1102: the width or height of a PNG entry is less than <see cref="IconEntry.MinDimension"/> or greater than <see cref="IconEntry.MaxDimension"/>.
        /// <see cref="IconLoadException.Value"/> contains the size of the image.
        /// </summary>
        InvalidPngSize = 0x1102,
        /// <summary>
        /// Code 0x1105: the width or height of a PNG entry does not match the width or height listed in the icon directory entry.
        /// </summary>
        PngSizeMismatch = 0x1103,
        /// <summary>
        /// Code 0x1205: there is a mismatch between the bit depth of a PNG entry and the expected bit depth of the file.
        /// <see cref="IconLoadException.Value"/> contains a <see cref="Tuple{T1, T2}"/> in which the <see cref="Tuple{T1, T2}.Item1"/> is the bit depth
        /// listed in the icon directory entry, and <see cref="Tuple{T1, T2}.Item2"/> is the bit depth listed in the PNG entry.
        /// </summary>
        PngBitDepthMismatch = 0x1105,
        /// <summary>
        /// Code 0x1200: an error occurred when attempting to process a BMP entry. The inner exception may contain more information.
        /// </summary>
        InvalidBmpFile = 0x1200,
        /// <summary>
        /// Code 0x1201: the bit depth of a BMP entry is not supported.
        /// <see cref="IconLoadException.Value"/> contains the bit depth.
        /// </summary>
        InvalidBmpBitDepth = 0x1201,
        /// <summary>
        /// Code 0x1202: the width or height of a BMP entry is less than <see cref="IconEntry.MinDimension"/> or greater than <see cref="IconEntry.MaxDimension"/>.
        /// The maximum height is doubled in images with a bit depth less than 32.
        /// <see cref="IconLoadException.Value"/> contains the size of the image.
        /// </summary>
        InvalidBmpSize = 0x1202,
        /// <summary>
        /// Code 0x1203: the width or height of a BMP entry does not match the width or height listed in the icon directory entry.
        /// <see cref="IconLoadException.Value"/> contains a <see cref="Tuple{T1, T2}"/> in which the <see cref="Tuple{T1, T2}.Item1"/> is the 
        /// size listed in the icon directory entry, and <see cref="Tuple{T1, T2}.Item2"/> is the actual size.
        /// </summary>
        BmpSizeMismatch = 0x1203,
        /// <summary>
        /// Code 0x1204: the height of a BMP entry is an odd number, indicating that there is no AND (transparency) mask.
        /// <see cref="IconLoadException.Value"/> contains the <see cref="BitmapSource.PixelHeight"/> of the image.
        /// </summary>
        InvalidBmpHeightOdd = 0x1204,
        /// <summary>
        /// Code 0x1205: there is a mismatch between the bit depth of a BMP entry and the expected bit depth of the file.
        /// <see cref="IconLoadException.Value"/> contains a <see cref="Tuple{T1, T2}"/> in which the <see cref="Tuple{T1, T2}.Item1"/> is the bit depth
        /// listed in the icon directory entry, and <see cref="Tuple{T1, T2}.Item2"/> is the bit depth listed in the BMP entry.
        /// </summary>
        BmpBitDepthMismatch = 0x1205,
    }
}
