using ZstdSharp;

namespace R6ReadRecFile.Core.Utils
{

    /// <summary>
    /// Helper used for development purposes only.
    /// Simplifies the creation of separate binary files 
    /// to allow easier analysis of decompressed data.
    /// </summary>
    public class ZSTDHelperForTest : ZSTDHelper
    {

        private byte[] _data;
        private int _offset;
        private static readonly byte[] ZstdMagic = { 0x28, 0xB5, 0x2F, 0xFD };

        public ZSTDHelperForTest(byte[] data, string outputDirectory)
            : base(data)
        {
            DecompressAllZstdBlocks(data, outputDirectory);
        }


        private static void DecompressAllZstdBlocks(byte[] data, string outputDirectory)
        {
            int pos = 4; // Skip header
            int blockCount = 0;
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
                        string blockFileName = Path.Combine(outputDirectory, $"block_{blockCount}.bin");
                        File.WriteAllBytes(blockFileName, decompressed.ToArray());
                        blockCount++;
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
        }

    }
}
