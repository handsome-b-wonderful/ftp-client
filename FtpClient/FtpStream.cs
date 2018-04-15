using System.IO;

namespace WebBore.FtpClient
{
    internal delegate void FTPStreamCallback();

    public class FTPStream : Stream
    {
        public enum EAllowedOperation { Read = 1, Write = 2 }

        Stream innerStream;
        FTPStreamCallback streamClosedCallback;
        EAllowedOperation allowedOp;

        internal FTPStream(Stream innerStream, EAllowedOperation allowedOp, FTPStreamCallback streamClosedCallback)
        {
            this.innerStream = innerStream;
            this.streamClosedCallback = streamClosedCallback;
            this.allowedOp = allowedOp;
        }

        public override bool CanRead
        {
            get { return innerStream.CanRead && (allowedOp & EAllowedOperation.Read) == EAllowedOperation.Read; }
        }

        public override bool CanSeek
        {
            get { return innerStream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return innerStream.CanWrite && (allowedOp & EAllowedOperation.Write) == EAllowedOperation.Write; }
        }

        public override void Flush()
        {
            innerStream.Flush();
        }

        public override long Length
        {
            get { return innerStream.Length; }
        }

        public override long Position
        {
            get
            {
                return innerStream.Position;
            }
            set
            {
                innerStream.Position = value;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (!CanRead)
                throw new FTPException("Operation not allowed");

            return innerStream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return innerStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            innerStream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (!CanWrite)
                throw new FTPException("Operation not allowed");

            innerStream.Write(buffer, offset, count);
        }

        public override void Close()
        {
            base.Close();
            streamClosedCallback();
        }
    }
}
