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

        public static List<PlayerInfo> ExtractPlayerInfo(byte[] fileData)
        {
            var result = new List<PlayerInfo>();
            var reader = new ZSTDHelper(fileData);
            var playerIndicador = new Byte[] { 0x22, 0x07, 0x94, 0x9B, 0xDC };
            var spawnIndicator = new Byte[] { 0xAF, 0x98, 0x99, 0xCA };
            while (reader.Seek(playerIndicador))
            {
                var player = new PlayerInfo();
                int namePosition = reader.Position;
                reader.Position = namePosition - 153; // The stats object id is 148 bytes before the name indicator
                uint id = reader.ReadUInt32();
                player.Id = (int)id;
                reader.Position = namePosition;
                string username = reader.ReadString();
                player.Name = username;
                if (reader.Seek(spawnIndicator))
                {
                    string spawn = reader.ReadString();
                    player.Spawn = spawn;

                }
                if (!result.Any(p => p.Name == player.Name)) // Player names reappear later in the stream (kill feed...)
                    result.Add(player);
            }
            string? defenserSpawn = null;
            while (reader.Seek(spawnIndicator))
            {
                string spawn = reader.ReadString();
                if (spawn.Contains("<br/>"))
                {
                    defenserSpawn = spawn;
                    break;
                }
            }
            if (defenserSpawn != null)
                result.Where(p => string.IsNullOrEmpty(p.Spawn)).ToList().ForEach(p => p.Spawn = defenserSpawn.Replace("<br/>", ", "));
            ExtractPlayerScoreMetadata(reader, result);
            return result;
        }

        private static void ExtractPlayerScoreMetadata(ZSTDHelper reader, List<PlayerInfo> players)
        {
            var killIndicator = new Byte[] { 0x1C, 0xD2, 0xB1, 0x9D };
            var deathIndicator = new Byte[] { 0xCD, 0x9C, 0x5D, 0x72 };
            var assistIndicator = new Byte[] { 0x4D, 0x73, 0x7F, 0x9E };
            var scoreIndicator = new Byte[] { 0xEC, 0xDA, 0x4F, 0x80 };
            ExtractPlayerStat(reader, players, killIndicator, (stats, value) => stats.Kill = value);
            ExtractPlayerStat(reader, players, deathIndicator, (stats, value) => stats.Death = value);
            ExtractPlayerStat(reader, players, assistIndicator, (stats, value) => stats.Assist = value);
            ExtractPlayerStat(reader, players, scoreIndicator, (stats, value) => stats.Score = value);
        }

        // A stat is "<indicator> 04 <uint32>" inside an object update starting with "23 <playerId> 00 00 00 00".
        // Values are cumulative, so the last one found is the right one.
        private static void ExtractPlayerStat(ZSTDHelper reader, List<PlayerInfo> players, byte[] indicator, Action<Stats, int> setStat)
        {
            reader.Position = 0;
            while (reader.Seek(indicator))
            {
                var oldReaderPosition = reader.Position;
                uint? playerId = FindPlayerId(reader, oldReaderPosition - indicator.Length);
                reader.Position = oldReaderPosition;
                if (playerId == null)
                    continue;
                reader.Skip(1); // Size byte (04)
                var value = reader.ReadUInt32();
                foreach (var player in players.Where(p => p.Id == (int)playerId))
                {
                    player.Stats ??= new Stats();
                    setStat(player.Stats, (int)value);
                }
            }
        }

        // The stat is not always the first property of the update (e.g. "... 1C D2 B1 9D 04 <kills> 22 CD 9C 5D 72 04 <deaths>"),
        // so we go back to the closest "23 <playerId> 00 00 00 00" header
        private static uint? FindPlayerId(ZSTDHelper reader, int indicatorPosition)
        {
            const int maxDistance = 128;
            for (int headerPosition = indicatorPosition - 9; headerPosition >= Math.Max(0, indicatorPosition - maxDistance); headerPosition--)
            {
                reader.Position = headerPosition;
                if (reader.ReadByte() != 0x23)
                    continue;
                uint playerId = reader.ReadUInt32();
                if (reader.ReadUInt32() == 0)
                    return playerId;
            }
            return null;
        }
    }
}