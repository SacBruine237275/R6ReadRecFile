using R6ReadRecFile.Core.Interfaces;
using R6ReadRecFile.Core.Models;
using R6ReadRecFile.Core.Readers;

namespace R6ReadRecFile.Core.Services
{
    public class RecParser : IRecParser
    {
        public RecFile Parse(FileStream file)
        {
            var reader = new FileRecReader(file);

            var recFile = new RecFile();
            var extractedStrings = reader.GetStringsFromFile().ToList();
            recFile.Players = reader.ReadPlayers(extractedStrings);
            recFile.Metadata=reader.ReadGameMetadata(extractedStrings);
            return recFile;
        }
    }
}
