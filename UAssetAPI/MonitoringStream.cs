using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace UAssetAPI
{
#if DEBUG || DEBUGVERBOSE || DEBUGTRACING
    /// <summary>
    /// Pass-through stream for debugging.
    /// </summary>
    public class MonitoringStream : Stream
    {
        public Stream InnerStream;
        public UAsset Asset;

        /// <summary>
        /// Whether or not to enable monitoring.
        /// </summary>
        public static bool Enabled = false;
        /// <summary>
        /// Offset of a byte to place a breakpoint at for debugging purposes. Set to -1 to disable.
        /// </summary>
        public static long StopOffset = -1;
        /// <summary>
        /// If true, StopOffset is interpreted as an offset relative to the start of the .uexp file.
        /// </summary>
        public static bool IsUexpOffset = true;

        public MonitoringStream(Stream innerStream, UAsset asset)
        {
            InnerStream = innerStream;
            Asset = asset;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (Asset != null && Asset.IsParsingToPullSchemas) return InnerStream.Read(buffer, offset, count);

            long ourStopOffset = StopOffset;
            if (StopOffset >= 0 && IsUexpOffset)
            {
                if (Asset == null || Asset.Exports == null || Asset.Exports.Count == 0)
                {
                    ourStopOffset = -1;
                }
                else
                {
                    ourStopOffset += Asset.Exports[0].SerialOffset;
                }
            }
            if (ourStopOffset >= 0 && InnerStream.Position <= ourStopOffset && (InnerStream.Position + count) > ourStopOffset)
            {
                long correctedPosition = InnerStream.Position;
                if (IsUexpOffset) correctedPosition -= Asset.Exports[0].SerialOffset;
                Debug.WriteLine($"Stop byte reached at read time: reading {count} bytes starting at {correctedPosition}");
                Debugger.Break();
            }
            return InnerStream.Read(buffer, offset, count);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            long ourStopOffset = StopOffset;
            if (StopOffset >= 0 && IsUexpOffset)
            {
                if (Asset == null || Asset.Exports == null || Asset.Exports.Count == 0)
                {
                    ourStopOffset = -1;
                }
                else
                {
                    ourStopOffset += Asset.Exports[0].SerialOffset;
                }
            }
            if (ourStopOffset >= 0 && InnerStream.Position <= ourStopOffset && (InnerStream.Position + count) > ourStopOffset)
            {
                long correctedPosition = InnerStream.Position;
                if (IsUexpOffset) correctedPosition -= Asset.Exports[0].SerialOffset;
                Debug.WriteLine($"Stop byte reached at write time: writing {count} bytes starting at {correctedPosition}");
                Debugger.Break();
            }

            InnerStream.Write(buffer, offset, count);
        }

        public override long Position
        {
            get => InnerStream.Position;
            set => InnerStream.Position = value;
        }
        public override long Length => InnerStream.Length;
        public override bool CanRead => InnerStream.CanRead;
        public override bool CanSeek => InnerStream.CanSeek;
        public override bool CanWrite => InnerStream.CanWrite;
        public override void Flush() => InnerStream.Flush();
        public override long Seek(long offset, SeekOrigin origin) => InnerStream.Seek(offset, origin);
        public override void SetLength(long value) => InnerStream.SetLength(value);
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                InnerStream.Dispose();
            }
            base.Dispose(disposing);
        }

        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) => InnerStream.ReadAsync(buffer, offset, count, cancellationToken);

        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) => InnerStream.WriteAsync(buffer, offset, count, cancellationToken);

        public override Task FlushAsync(CancellationToken cancellationToken) => InnerStream.FlushAsync(cancellationToken);
    }
#endif
}
