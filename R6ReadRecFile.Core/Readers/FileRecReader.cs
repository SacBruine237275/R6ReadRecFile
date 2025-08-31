using R6ReadRecFile.Core.Models;
using R6ReadRecFile.Core.Utils;
using System.IO;

namespace R6ReadRecFile.Core.Readers
{
    public class FileRecReader
    {

        private readonly BinaryReader _reader;
        
        public FileRecReader(Stream stream)
        {
            _reader = new BinaryReader(stream);
        }

        public List<PlayerInfo> ReadPlayers(List<string> extractedStrings)
        {
            List<PlayerInfo>players = new List<PlayerInfo>();
            for(int i = 0; i < extractedStrings.Count; i++)
            {
                if(extractedStrings[i] == "playerid")
                {
                    PlayerInfo player = new PlayerInfo();
                    player.Name = extractedStrings[i + 5]; //Shift by 5 to get the name
                    int teamValue = Int32.Parse(extractedStrings[i + 7]);//Shift by 5 to get team
                    player.Team = Enum.IsDefined(typeof(Team), teamValue) ? (Team)teamValue : Team.Unknow;
                    player.Operator = extractedStrings[i + 15]; //We shift by 15 to get the operator's name
                    players.Add(player);
                }
            }
            return players;
        }

        public IEnumerable<string> GetStringsFromFile()
        {
            _reader.BaseStream.Seek(0, SeekOrigin.Begin);

            using var ms = new MemoryStream();
            _reader.BaseStream.CopyTo(ms);
            byte[] data = ms.ToArray();

            return BinaryHelper.ExtractStrings(data);
        }
    }
}
