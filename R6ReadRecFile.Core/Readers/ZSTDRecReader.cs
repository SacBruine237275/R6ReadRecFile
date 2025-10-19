using R6ReadRecFile.Core.Models;
using R6ReadRecFile.Core.Utils;

namespace R6ReadRecFile.Core.Readers
{
    public class ZSTDRecReader
    {
        string filePath;
        public ZSTDRecReader(string filePath)
        {
            this.filePath = filePath;
        }

        public static List<PlayerInfo> ExtractPlayerInfo(byte[] fileData, string binaryOutputPath)
        {
            var result= new List<PlayerInfo>();
            var reader = new ZSTDHelper(fileData);
            var playerIndicador = new Byte[] { 0x22, 0x07, 0x94, 0x9B, 0xDC };
            var spawnIndicator = new Byte[] { 0xAF, 0x98, 0x99, 0xCA };
            while (reader.Seek(playerIndicador))
            {
                var player = new PlayerInfo();
                string username=reader.ReadString();
                player.Name = username;
                if (reader.Seek(spawnIndicator))
                {
                    string spawn=reader.ReadString();
                    player.Spawn = spawn;

                }
                result.Add(player);
            }
            string defenserSpawn = "";
            do
            {
                reader.Seek(spawnIndicator);
                defenserSpawn=reader.ReadString();
            } while (!defenserSpawn.Contains("<br/>"));
            result.Where(p => string.IsNullOrEmpty(p.Spawn)).ToList().ForEach(p => p.Spawn = defenserSpawn.Replace("<br/>", ", "));
            return result;
        }
    }
}