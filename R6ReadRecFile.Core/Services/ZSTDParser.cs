using R6ReadRecFile.Core.Interfaces;
using R6ReadRecFile.Core.Models;
using R6ReadRecFile.Core.Readers;

namespace R6ReadRecFile.Core.Services
{
    public class ZSTDParser : IRecParser
    {
        public RecFile Parse(string pathFile)
        {
            ZSTDRecReader zSTDParser = new ZSTDRecReader(pathFile);
            byte[] fileData = File.ReadAllBytes(pathFile);
            var rec = new RecFile();
            rec.Players=ZSTDRecReader.ExtractPlayerInfo(fileData, "output.bin");
            return rec;
        }

        public RecFile Parse(FileStream file)
        {
            throw new NotImplementedException();
        }
    }
}
