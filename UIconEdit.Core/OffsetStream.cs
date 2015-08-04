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

#if DRAWING
namespace UIconDrawing
#else
namespace UIconEdit
#endif
{
    internal class OffsetStream : Stream
    {
        private bool _leaveOpen;
        private Stream _stream;

        private long _length, _remainingLength;
        private MemoryStream _ms;

        internal OffsetStream(Stream stream, long length, bool leaveOpen)
        {
            _stream = stream;
            _leaveOpen = leaveOpen;
            _length = _remainingLength = length;
        }

        internal OffsetStream(Stream stream, byte[] buffer, long length, bool leaveOpen)
            : this(stream, length, leaveOpen)
        {
            _ms = new MemoryStream(buffer);
        }

        public override bool CanRead
        {
            get { return _stream != null; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override long Length
        {
            get { throw new NotSupportedException(); }
        }

        public override long Position
        {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }

        public override void Flush()
        {
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (_stream == null) throw new ObjectDisposedException(null);
            if (buffer == null) throw new ArgumentNullException("buffer");

            new ArraySegment<byte>(buffer, offset, count);

            int read = 0;

            if (_ms != null)
            {
                read = _ms.Read(buffer, offset, count);
                count -= read;
                offset += read;
                if (count == 0) return read;
                if (_ms.Position >= _ms.Length || read == 0)
                {
                    _ms.Dispose();
                    _ms = null;
                }
            }
            if (count == 0 || _remainingLength <= 0) return 0;

            read += _stream.Read(buffer, offset, (int)Math.Min(count, _remainingLength));
            if (read == 0) _remainingLength = 0;
            else _remainingLength -= read;
            return read;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        protected override void Dispose(bool disposing)
        {
            if (_ms != null)
                _ms.Dispose();
            try
            {
                if (_stream != null)
                {
                    int read = 0;
                    const int bufferCount = 8192;
                    byte[] endBuffer = new byte[bufferCount];
                    while (_remainingLength != 0 && read != 0)
                    {
                        read = _stream.Read(endBuffer, 0, (int)Math.Min(_remainingLength, bufferCount));
                        if (read == 0) _remainingLength = 0;
                        else _remainingLength -= read;
                    }

                    if (disposing && !_leaveOpen)
                        _stream.Dispose();
                }
            }
            finally
            {
                _stream = null;
                _leaveOpen = true;
                _ms = null;
                base.Dispose(disposing);
            }
        }
    }
}
