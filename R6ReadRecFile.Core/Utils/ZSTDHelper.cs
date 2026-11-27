using System.Text;
using ZstdSharp;

namespace R6ReadRecFile.Core.Utils
{
    public class ZSTDHelper
    {
        private byte[] _data;
        private int _offset;
        private static readonly byte[] ZstdMagic = { 0x28, 0xB5, 0x2F, 0xFD };

        // Read position in the decompressed data
        public int Position
        {
            get => _offset;
            set => _offset = value;
        }

        public ZSTDHelper(byte[] data)
            : this(data, null)
        {
        }

        // onBlockDecompressed: called for each decompressed block (index, content)
        protected ZSTDHelper(byte[] data, Action<int, ReadOnlySpan<byte>>? onBlockDecompressed)
        {
            _data = DecompressAllZstdBlocks(data, onBlockDecompressed);
        }

        // Decompresses all Zstd blocks in the data
        private static byte[] DecompressAllZstdBlocks(byte[] data, Action<int, ReadOnlySpan<byte>>? onBlockDecompressed)
        {
            using var result = new MemoryStream();
            using var decompressor = new Decompressor();
            int pos = 4; // Skip header
            int blockCount = 0;
            while (pos < data.Length - 4)
            {
                int index = data.AsSpan(pos).IndexOf(ZstdMagic);
                if (index < 0)
                    break;
                pos += index;
                try
                {
                    int compressedSize = BitConverter.ToInt32(data, pos - 4);
                    var compressed = new ReadOnlySpan<byte>(data, pos, compressedSize);
                    var decompressed = decompressor.Unwrap(compressed);
                    result.Write(decompressed);
                    onBlockDecompressed?.Invoke(blockCount++, decompressed);
                    pos += compressedSize;
                }
                catch (Exception ex) when (ex is ZstdException or ArgumentException)
                {
                    pos++; // False positive: magic bytes inside data, or invalid size
                }
            }
            return result.ToArray();
        }

        // Searches for a pattern and moves the position right after it
        public bool Seek(byte[] pattern)
        {
            int index = _data.AsSpan(_offset).IndexOf(pattern);
            if (index < 0)
                return false;
            _offset += index + pattern.Length;
            return true;
        }

        // Reads a length-prefixed string
        public string ReadString()
        {
            int size = ReadByte();
            var bytes = ReadBytes(size);
            return Encoding.UTF8.GetString(bytes);
        }

        // Reads a given number of bytes
        public byte[] ReadBytes(int n)
        {
            var result = new byte[n];
            Array.Copy(_data, _offset, result, 0, n);
            _offset += n;
            return result;
        }

        // Reads a byte
        public byte ReadByte()
        {
            return _data[_offset++];
        }

        // Reads a uint32
        public uint ReadUInt32()
        {
            var bytes = ReadBytes(4);
            return BitConverter.ToUInt32(bytes, 0);
        }

        // Reads a uint64
        public ulong ReadUint64()
        {
            var bytes = ReadBytes(8);
            return BitConverter.ToUInt64(bytes, 0);
        }

        // Skips a given number of bytes (negative to go back)
        public void Skip(int n)
        {
            _offset += n;
        }
    }
}
