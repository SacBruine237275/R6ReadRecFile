using R6ReadRecFile.Core.Enums;
using System.Text.Json.Serialization;

namespace R6ReadRecFile.Core.Models
{
    public class GameMetadata
    {
        public string Version { get; set; } = string.Empty;
        public string DateTime { get;set; } =string.Empty;
        [JsonConverter(typeof(MapConverter))]
        public Map Map { get; set; }
        [JsonConverter(typeof(GameModeConverter))]
        public GameMode Mode {  get; set; }

        public override string ToString()
        {
            return $"Game version: {Version}, Date: {DateTime}, Map: {Map.GetDisplayName()}, Gamemode: {Mode.GetDisplayName()}";
        }
    }
}
