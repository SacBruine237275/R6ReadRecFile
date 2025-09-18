using R6ReadRecFile.Core.Enums;
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
            string teamName0 = extractedStrings[extractedStrings.IndexOf("teamname0") + 1]; //The team numbers may vary from one file to another, so we retrieve the name of team number 0
            string teamName1 = extractedStrings[extractedStrings.IndexOf("teamname1") + 1]; //The team numbers may vary from one file to another, so we retrieve the name of team number 1
            for (int i = 0; i < extractedStrings.Count; i++)
            {
                if(extractedStrings[i] == "playerid")
                {
                    PlayerInfo player = new PlayerInfo();
                    player.Name = extractedStrings[i + 5]; //Shift by 5 to get the name
                    int teamValue = Int32.Parse(extractedStrings[i + 7]);//Shift by 5 to get team
                    string playerTeamName = teamValue == 0 ? teamName0 : teamName1;
                    player.Team = playerTeamName;
                    player.Operator = extractedStrings[i + 15]; //We shift by 15 to get the operator's name
                    players.Add(player);
                }
            }
            return players;
        }


        public GameMetadata ReadGameMetadata(List<string> extractedStrings)
        {
            var gameMetadata= new GameMetadata();
            for(int i = 0; i < extractedStrings.Count; i++)
            {
                if(extractedStrings[i] == "version")
                {
                    gameMetadata.Version = extractedStrings[i + 1];
                    gameMetadata.DateTime = extractedStrings[i + 5];
                    gameMetadata.Mode =(GameMode) int.Parse(extractedStrings[i + 7]);
                    gameMetadata.Map =(Map) long.Parse(extractedStrings[i + 9]);
                }
            }
            return gameMetadata;
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
