using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZstdSharp;

namespace R6ReadRecFile.Core.Utils
{
    public class ZSTDHelper
    {
        private static readonly byte[] ZstdMagic = { 0x28, 0xB5, 0x2F, 0xFD };
        public static List<string> DecompressFile(byte[] data)
        {
            List<string> result = new List<string>();
            int pos = 4; //Position 4 because with compression, the first 4 bytes are for the header
            while (pos < data.Length - 4)
            {
                if (data[pos] == ZstdMagic[0] &&
                    data[pos + 1] == ZstdMagic[1] &&
                    data[pos + 2] == ZstdMagic[2] &&
                    data[pos + 3] == ZstdMagic[3])
                {
                    try
                    {
                        int compressedSize=BitConverter.ToInt32(data, pos-4);
                        var decompressor = new Decompressor();
                        var compressed=new ReadOnlySpan<byte>(data,pos, compressedSize);
                        var decompressed= decompressor.Unwrap(compressed);
                        foreach (byte b in decompressed.ToArray())
                        {
                            result.Add($"0x{b:X2}");
                        }
                        pos += compressedSize;
                        continue;
                    }
                    catch
                    {
                        //We do nothing about exceptions
                    }
                }
                pos++;
            }
            return result;
        }
    }
}
