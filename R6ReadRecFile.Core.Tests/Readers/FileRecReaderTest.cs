using R6ReadRecFile.Core.Enums;
using R6ReadRecFile.Core.Models;
using R6ReadRecFile.Core.Readers;

namespace R6ReadRecFile.Core.Tests.Readers
{
    public class FileRecReaderTest
    {
        FileRecReader reader;
        public FileRecReaderTest()
        {
            reader = new FileRecReader(new MemoryStream());
        }

        [Fact]
        public void ReadPlayers_ReturnsExpectedPlayers()
        {
            List<PlayerInfo> expected = new List<PlayerInfo>
            {
               new PlayerInfo{Name="Name1",Operator="LESION",Team="ENEMY TEAM"},
               new PlayerInfo{Name="Name2",Operator="HIBANA",Team="YOUR TEAM"}
            };
            List<string> content = new List<string>{
                "teamname0","ENEMY TEAM",
                "playerid", "id",
                "profileid", "id",
                "playername", "Name1",
                 "team", "0",
                "other", "ignored",
                "other", "ignored",
                "other", "ignored",
                "operator", "LESION",

                "teamname1","YOUR TEAM",
                "playerid", "id",
                "profileid", "id",
                "playername", "Name2",
                "team", "1",
                "other", "ignored",
                "other", "ignored", 
                "other", "ignored",
                "operator", "HIBANA"
            };
            var actual = reader.ReadPlayers(content);
            Assert.Equal(expected.Count, actual.Count);
            Assert.All(expected, expectedItem => Assert.Contains(actual, actualItem => actualItem.Name == expectedItem.Name));
            Assert.All(expected, expectedItem => Assert.Contains(actual, actualItem => actualItem.Operator == expectedItem.Operator));
            Assert.All(expected, expectedItem => Assert.Contains(actual, actualItem => actualItem.Team == expectedItem.Team));
        }

        [Fact]
        public void ReadPlayers_ReturnsNoPlayers()
        {
            List<PlayerInfo> expected = new List<PlayerInfo>
            {
            };
            List<string> content = new List<string>{
                "playerimg",
                "1",
                "playerimg",
                "img"
            }
            ;
            var actual = reader.ReadPlayers(content);
            Assert.Equal(expected.Count, actual.Count);
        }

        [Fact]
        public void GetStringsFromFile_ShouldExtractStrings_WithMinLengthDefault()
        {
            byte[] fakeData = new byte[]
            {
            (byte)'H', (byte)'i', 0,
            (byte)'H', (byte)'e', (byte)'l', (byte)'l', (byte)'o', 0,
            (byte)'A', 0,
            (byte)'W', (byte)'o', (byte)'r', (byte)'l', (byte)'d', 0
            };

            using var ms = new MemoryStream(fakeData);
            var reader = new FileRecReader(ms);
            var result = reader.GetStringsFromFile();

            Assert.Contains("Hello", result);
            Assert.Contains("World", result);
            Assert.Contains("Hi", result);
            Assert.Contains("A", result);
        }

        [Fact]
        public void ReadGameMetadata_ShouldParseAllFieldsCorrectly()
        {
            string versionExpected = "1.2.3";
            string dateExpected = "2025-09-11-21-15-25";

            var fakeData = new List<string>
            {
                "version", "1.2.3", "ignore1", "ignore2", "DateTime", "2025-09-11-21-15-25",
                "GameModeId", "1",
                "MapId", "413845419788"
            };

            
            GameMetadata gameMetadata = reader.ReadGameMetadata(fakeData);

            Assert.Equal(versionExpected, gameMetadata.Version);
            Assert.Equal(dateExpected, gameMetadata.DateTime);
            Assert.Equal(GameMode.QuickMatch, gameMetadata.Mode);
            Assert.Equal(Map.KafeDostoyevsky, gameMetadata.Map);
        }

        [Fact]
        public void ReadGameMetadata_ShouldReturnDefault_WhenVersionNotFound()
        {
            var fakeData = new List<string> { "random", "data" };

            GameMetadata gameMetadata = reader.ReadGameMetadata(fakeData);

            Assert.Equal(string.Empty, gameMetadata.Version);
            Assert.Equal(string.Empty, gameMetadata.DateTime);
            Assert.Equal(default(GameMode), gameMetadata.Mode);
            Assert.Equal(default(Map), gameMetadata.Map);
        }
    }
}
