using System.Text.Json;
using System.Text.Json.Serialization;

namespace R6ReadRecFile.Core.Enums
{
    public enum Map : long
    {
        Kanal = 1460220617,
        Yacht = 1767965020,
        PresidentialPlane = 2609218856,
        Coastline = 42090092951,
        Tower = 53627213396,
        Villa = 88107330328,
        Fortress = 126196841359,
        HerefordBase = 127951053400,
        ThemePark = 199824623654,
        Oregon = 231702797556,
        House = 237873412352,
        Skyscraper = 276279025182,
        Favela = 329867321446,
        Outback = 362605108559,
        EmeraldPlains = 365284490964,
        NighthavenLabs = 378595635123,
        Consulate = 379218689149,
        Lair = 388073319671,
        Stadium = 405306299908,
        Bank = 413779563590,
        Border = 407987100456,
        Chalet = 407558616688,
        ClubHouse = 407193663917,
        KafeDostoyevsky = 413845419788
    }

    public static class MapExtensions
    {
        public static string GetDisplayName(this Map map)
        {
            return map switch
            {
                Map.ClubHouse => "Club House",
                Map.KafeDostoyevsky => "Kafe Dostoyevsky",
                Map.PresidentialPlane => "Presidential Plane",
                Map.HerefordBase => "Hereford Base",
                Map.ThemePark => "Theme Park",
                Map.EmeraldPlains => "Emerald Plains",
                Map.NighthavenLabs => "Nighthaven Labs",
                _ => map.ToString()
            };
        }
    }

    public class MapConverter : JsonConverter<Map>
    {
        public override Map Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, Map value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.GetDisplayName());
        }
    }
}
