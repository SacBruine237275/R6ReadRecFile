using R6ReadRecFile.Core.Utils;
using System.Reflection.PortableExecutable;
using System.Text;
using ZstdSharp;

namespace R6ReadRecFile.Core.Readers
{
    public class ZSTDRecReader
    {
        string filePath;
        public ZSTDRecReader(string filePath)
        {
            this.filePath = filePath;
        }
        public List<string> GetData()
        {
            byte[] data = File.ReadAllBytes(filePath);
            var decompressData = ZSTDHelper.DecompressFile(data);
            return decompressData;

        }
    }
}