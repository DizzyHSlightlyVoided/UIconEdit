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

#if DRAWING
using FileFormatException = System.IO.IOException;

namespace UIconDrawing
#else
namespace UIconEdit
#endif
{
    /// <summary>
    /// The exception that is thrown when an icon file contains invalid data.
    /// </summary>
    public class IconLoadException : FileFormatException
    {
        internal static string DefaultMessage { get { return new InvalidDataException().Message; } }

        private const int DefaultIndex = -1;

        /// <summary>
        /// Creates a new instance using a message which describes the error and the specified error code.
        /// </summary>
        /// <param name="message">A message describing the error.</param>
        /// <param name="code">The error code used to identify the cause of the error.</param>
        /// <param name="typeCode">The type code of the file which caused the error.</param>
        /// <param name="value">The value which caused the error.</param>
        /// <param name="entryIndex">The index in the icon file of the entry in the icon directory which caused the exception,
        /// or a value less than 0 if the error was not caused by an icon entry.</param>
        public IconLoadException(string message, IconErrorCode code, IconTypeCode typeCode, object value, int entryIndex)
            : base(message)
        {
            _code = code;
            _value = value;
            _typeCode = typeCode;
            _index = entryIndex;
        }

        /// <summary>
        /// Creates a new instance using a message which describes the error and the specified error code.
        /// </summary>
        /// <param name="message">A message describing the error.</param>
        /// <param name="code">The error code used to identify the cause of the error.</param>
        /// <param name="typeCode">The type code of the file which caused the error.</param>
        /// <param name="value">The value which caused the error.</param>
        public IconLoadException(string message, IconErrorCode code, IconTypeCode typeCode, object value)
            : this(message, code, typeCode, value, DefaultIndex)
        {
        }

        /// <summary>
        /// Creates a new instance using a message which describes the error and the specified error code.
        /// </summary>
        /// <param name="message">A message describing the error.</param>
        /// <param name="code">The error code used to identify the cause of the error.</param>
        /// <param name="typeCode">The type code of the file which caused the error.</param>
        /// <param name="entryIndex">The index in the icon file of the entry in the icon directory which caused the exception,
        /// or a value less than 0 if the error was not caused by an icon entry.</param>
        public IconLoadException(string message, IconErrorCode code, IconTypeCode typeCode, int entryIndex)
            : this(message, code, typeCode, null, entryIndex)
        {
        }

        /// <summary>
        /// Creates a new instance using a message which describes the error and the specified error code.
        /// </summary>
        /// <param name="message">A message describing the error.</param>
        /// <param name="code">The error code used to identify the cause of the error.</param>
        /// <param name="typeCode">The type code of the file which caused the error.</param>
        public IconLoadException(string message, IconErrorCode code, IconTypeCode typeCode)
            : this(message, code, typeCode, null, DefaultIndex)
        {
        }

        /// <summary>
        /// Creates a new instance with the default message and the specified error code.
        /// </summary>
        /// <param name="code">The error code used to identify the cause of the error.</param>
        /// <param name="typeCode">The type code of the file which caused the error.</param>
        /// <param name="entryIndex">The index in the icon file of the entry in the icon directory which caused the exception,
        /// or a value less than 0 if the error was not caused by an icon entry.</param>
        public IconLoadException(IconErrorCode code, IconTypeCode typeCode, int entryIndex)
            : this(DefaultMessage, code, typeCode, null, entryIndex)
        {
        }

        /// <summary>
        /// Creates a new instance with the default message and the specified error code.
        /// </summary>
        /// <param name="code">The error code used to identify the cause of the error.</param>
        /// <param name="typeCode">The type code of the file which caused the error.</param>
        public IconLoadException(IconErrorCode code, IconTypeCode typeCode)
            : this(DefaultMessage, code, typeCode, null, DefaultIndex)
        {
        }

        /// <summary>
        /// Creates a new instance with the default message and the specified error code.
        /// </summary>
        /// <param name="code">The error code used to identify the cause of the error.</param>
        /// <param name="typeCode">The type code of the file which caused the error.</param>
        /// <param name="value">The value which caused the error.</param>
        /// <param name="entryIndex">The index in the icon file of the entry in the icon directory which caused the exception,
        /// or a value less than 0 if the error was not caused by an icon entry.</param>
        public IconLoadException(IconErrorCode code, IconTypeCode typeCode, object value, int entryIndex)
            : this(DefaultMessage, code, typeCode, value, entryIndex)
        {
        }

        /// <summary>
        /// Creates a new instance with the default message and the specified error code.
        /// </summary>
        /// <param name="code">The error code used to identify the cause of the error.</param>
        /// <param name="typeCode">The type code of the file which caused the error.</param>
        /// <param name="value">The value which caused the error.</param>
        public IconLoadException(IconErrorCode code, IconTypeCode typeCode, object value)
            : this(DefaultMessage, code, typeCode, value, DefaultIndex)
        {
        }

        /// <summary>
        /// Creates a new instance with the specified message and error code and a reference to the exception which caused this error.
        /// </summary>
        /// <param name="message">A message describing the error.</param>
        /// <param name="code">The error code used to identify the cause of the error.</param>
        /// <param name="typeCode">The type code of the file which caused the error.</param>
        /// <param name="entryIndex">The index in the icon file of the entry in the icon directory which caused the exception,
        /// or a value less than 0 if the error was not caused by an icon entry.</param>
        /// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException"/> parameter
        /// is not <c>null</c>, the current exception should be raised in a <c>catch</c> block which handles the inner exception.</param>
        public IconLoadException(string message, IconErrorCode code, IconTypeCode typeCode, int entryIndex, Exception innerException)
            : base(message, innerException)
        {
            _code = code;
            _typeCode = typeCode;
            _index = entryIndex;
        }

        /// <summary>
        /// Creates a new instance with the specified message and error code and a reference to the exception which caused this error.
        /// </summary>
        /// <param name="message">A message describing the error.</param>
        /// <param name="code">The error code used to identify the cause of the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException"/> parameter
        /// is not <c>null</c>, the current exception should be raised in a <c>catch</c> block which handles the inner exception.</param>
        /// <param name="typeCode">The type code of the file which caused the error.</param>
        public IconLoadException(string message, IconErrorCode code, IconTypeCode typeCode, Exception innerException)
            : this(message, code, typeCode, DefaultIndex, innerException)
        {
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
            IconLoadException iloe = innerException as IconLoadException;
            if (iloe != null)
            {
                _code = iloe._code;
                _typeCode = iloe._typeCode;
                _index = iloe._index;
                _value = iloe._value;
            }
        }

        internal IconLoadException(IconLoadException e)
#if DEBUG
            : base(e.BaseMessage, e)
#else
            : base(e.BaseMessage)
#endif
        {
            _code = e._code;
            _value = e._value;
            _typeCode = e._typeCode;
            _index = e._index;
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
                if (_value != null)
                    messages.Add("Value: " + _value);
                if (_index >= 0)
                    messages.Add("Index: " + _index);
                switch (messages.Count)
                {
                    case 0: return base.Message;
                    case 1: return messages[0];
                    default: return string.Join(Environment.NewLine, messages);
                }
            }
        }

        private int _index;
        /// <summary>
        /// Gets the index in the icon file of the entry in the icon directory which caused the exception,
        /// or a value less than 0 if the error was not caused by an icon entry.
        /// </summary>
        /// <remarks>
        /// This refers to the index of the entry within the list of icon directory entries; it may not refer to the position of the image
        /// within the rest of the icon file.
        /// </remarks>
        public int EntryIndex { get { return _index; } }

        private IconErrorCode _code;
        /// <summary>
        /// Gets an error code describing the icon error.
        /// </summary>
        public IconErrorCode Code { get { return _code; } }

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
            _index = index;
        }

        internal IconExtractException(Exception e, IconTypeCode code, int index)
            : base(e.Message, IconErrorCode.Unknown, code, e)
        {
            _index = index;
        }

        /// <summary>
        /// Gets a message describing the error.
        /// </summary>
        public override string Message
        {
            get { return string.Concat(base.Message, Environment.NewLine, "Extracted index: ", _index); }
        }

        private int _index;
        /// <summary>
        /// Gets the index in the DLL or EXE file of the icon or cursor which caused the error.
        /// </summary>
        public int ExtractIndex { get { return _index; } }
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
        /// Code 0x3: An icon was expected but a cursor was loaded, or vice versa.
        /// <see cref="IconLoadException.TypeCode"/> contains the expected value, and <see cref="IconLoadException.Value"/> contains the actual value.
        /// This is a fatal error, and the icon file cannot continue processing when it occurs.
        /// </summary>
        WrongType = 3,
        /// <summary>
        /// Code 0x1000: an error occurred when attempting to parse an icon frame.
        /// </summary>
        EntryParseError = 0x1000,
        /// <summary>
        /// Code 0x1001: The bit depth of an individual entry is not 1, 4, 8, 24, or 32.
        /// <see cref="IconLoadException.Value"/> contains the bit depth.
        /// </summary>
        InvalidBitDepth = 0x1001,
        /// <summary>
        /// Code 0x1002: There are no remaining valid entries after processing.
        /// This is a fatal error, and the icon file cannot continue processing when it occurs.
        /// </summary>
        ZeroValidEntries = 0x1002,
    }

    /// <summary>
    /// A delegate function for handling <see cref="IconLoadException"/> errors.
    /// </summary>
    /// <param name="e">An <see cref="IconLoadException"/> containing information about the error.</param>
    public delegate void IconLoadExceptionHandler(IconLoadException e);

    /// <summary>
    /// A delegate function for handling <see cref="IconExtractException"/> errors.
    /// </summary>
    /// <param name="e">An <see cref="IconExtractException"/> containing information about the error.</param>
    public delegate void IconExtractExceptionHandler(IconExtractException e);
}
