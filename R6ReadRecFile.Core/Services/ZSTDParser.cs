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
            zSTDParser.GetData();
            return null;
        }

        public RecFile Parse(FileStream file)
        {
            throw new NotImplementedException();
        }
    }
}
