using System.Text;
using ZstdSharp;

namespace R6ReadRecFile.Core.Utils
{
    public class ZSTDHelper
    {
        private byte[] _data;
        private int _offset;
        private static readonly byte[] ZstdMagic = { 0x28, 0xB5, 0x2F, 0xFD };

        public ZSTDHelper(byte[] data)
            : this(data, null)
        {
        }

        // onBlockDecompressed : appelé pour chaque bloc décompressé (index, contenu)
        protected ZSTDHelper(byte[] data, Action<int, ReadOnlySpan<byte>>? onBlockDecompressed)
        {
            _data = DecompressAllZstdBlocks(data, onBlockDecompressed);
        }

        // Décompresse tous les blocs Zstd dans les données
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

        // Cherche un motif et retourne sa position
        public bool Seek(byte[] pattern)
        {
            int index = _data.AsSpan(_offset).IndexOf(pattern);
            if (index < 0)
                return false;
            _offset += index + pattern.Length;
            return true;
        }

        // Lit une chaîne de caractères
        public string ReadString()
        {
            int size = ReadByte();
            var bytes = ReadBytes(size);
            return Encoding.UTF8.GetString(bytes);
        }

        // Lit un certain nombre d'octets
        public byte[] ReadBytes(int n)
        {
            var result = new byte[n];
            Array.Copy(_data, _offset, result, 0, n);
            _offset += n;
            return result;
        }

        // Lit un octet
        public byte ReadByte()
        {
            return _data[_offset++];
        }

        // Lit un uint64
        public ulong ReadUint64()
        {
            var bytes = ReadBytes(8);
            return BitConverter.ToUInt64(bytes, 0);
        }

        // Saute un certain nombre d'octets
        public void Skip(int n)
        {
            _offset += n;
        }
    }
}
