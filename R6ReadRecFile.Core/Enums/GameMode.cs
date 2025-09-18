using System.Text.Json;
using System.Text.Json.Serialization;

namespace R6ReadRecFile.Core.Enums
{
    public enum GameMode
    {
        QuickMatch=1,
        Ranked=2,
        CustomeLocal=3,
        CustomOnline=4,
        Standard=8
        //Add Dual Front
    }

    public static class GameModeExtensions
    {
        public static string GetDisplayName(this GameMode gameMode)
        {
            return gameMode.ToString();
        }
    }

    public class GameModeConverter : JsonConverter<GameMode>
    {
        public override GameMode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, GameMode value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.GetDisplayName());
        }
    }
}
