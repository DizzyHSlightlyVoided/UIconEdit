using System;
using System.IO;

namespace UIconEdit
{
    internal class OffsetStream : Stream
    {
        private bool _leaveOpen;
        private Stream _stream;

        private int _offset, _length, _remainingLength;

        internal OffsetStream(Stream stream, int offset, int length, bool leaveOpen)
        {
            _stream = stream;
            _offset = offset;
            _leaveOpen = leaveOpen;
            try
            {
                _length = _remainingLength = (int)Math.Min(length, stream.Length - (_offset + stream.Position));
            }
            catch
            {
                _length = _remainingLength = length;
            }
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
            get { return _length; }
        }

        public override long Position
        {
            get
            {
                if (_stream == null) throw new ObjectDisposedException(null);
                return _stream.Position - _offset;
            }
            set { throw new NotSupportedException(); }
        }

        public override void Flush()
        {
        }

        public override int Read(byte[] array, int offset, int count)
        {
            if (_stream == null) throw new ObjectDisposedException(null);
            if (array == null) throw new ArgumentNullException("buffer");
            new ArraySegment<byte>(array, offset, count);

            int readBytes = _stream.Read(array, offset, Math.Min(count, _remainingLength));
            if (readBytes == 0)
                _remainingLength = 0;
            else
                _remainingLength -= readBytes;
            return readBytes;
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
            try
            {
                if (disposing && !_leaveOpen)
                    _stream.Dispose();
                _stream = null;
                _leaveOpen = true;
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        public void CopyTo(BinaryWriter writer, int bufferSize)
        {
            byte[] buffer = new byte[bufferSize];

            int read = Read(buffer, 0, bufferSize);

            while (read != 0)
            {
                writer.Write(buffer, 0, read);
                read = Read(buffer, 0, bufferSize);
            }
        }

        public void CopyTo(BinaryWriter writer)
        {
            CopyTo(writer, 8192);
        }
    }
}
