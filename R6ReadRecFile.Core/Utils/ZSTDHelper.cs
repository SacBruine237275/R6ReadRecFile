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
        {
            _data = DecompressAllZstdBlocks(data);
        }

        // Décompresse tous les blocs Zstd dans les données
        private byte[] DecompressAllZstdBlocks(byte[] data)
        {
            var result = new List<byte>();
            int pos = 4; // Skip header
            while (pos < data.Length - 4)
            {
                if (data[pos] == ZstdMagic[0] &&
                    data[pos + 1] == ZstdMagic[1] &&
                    data[pos + 2] == ZstdMagic[2] &&
                    data[pos + 3] == ZstdMagic[3])
                {
                    try
                    {
                        int compressedSize = BitConverter.ToInt32(data, pos - 4);
                        var decompressor = new Decompressor();
                        var compressed = new ReadOnlySpan<byte>(data, pos, compressedSize);
                        var decompressed = decompressor.Unwrap(compressed);
                        result.AddRange(decompressed.ToArray());
                        pos += compressedSize;
                    }
                    catch
                    {
                        pos++;
                    }
                }
                else
                {
                    pos++;
                }
            }
            return result.ToArray();
        }

        // Cherche un motif et retourne sa position
        public bool Seek(byte[] pattern)
        {
            for (int i = _offset; i < _data.Length - pattern.Length; i++)
            {
                if (_data.Skip(i).Take(pattern.Length).SequenceEqual(pattern))
                {
                    _offset = i + pattern.Length;
                    return true;
                }
            }
            return false;
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
