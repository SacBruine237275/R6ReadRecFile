using R6ReadRecFile.Core.Interfaces;
using R6ReadRecFile.Core.Models;
using R6ReadRecFile.Core.Readers;

namespace R6ReadRecFile.Core.Services
{
    public class RecParser : IRecParser
    {
        public RecFile Parse(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Error: file '{filePath}' not found.");
            }
            using var file = new FileStream(filePath, FileMode.Open);
            var reader = new FileRecReader(file);

            var recFile = new RecFile();
            var extractedStrings = reader.GetStringsFromFile().ToList();
            recFile.Players = reader.ReadPlayers(extractedStrings);
            return recFile;
        }
    }
}
