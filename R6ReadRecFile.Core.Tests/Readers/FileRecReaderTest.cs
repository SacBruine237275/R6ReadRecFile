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
               new PlayerInfo{Name="Name1"},
               new PlayerInfo{Name="Name2"}
            };
            List<string> content = new List<string>{
                "playername",
                "Name1",
                "playername",
                "Name2"
            }
            ;
            var actual = reader.ReadPlayers(content);
            Assert.Equal(expected.Count, actual.Count);
            Assert.All(expected, expectedItem => Assert.Contains(actual, actualItem => actualItem.Name == expectedItem.Name));
        }

        [Fact]
        public void ReadPlayers_ReturnsNoPlayers()
        {
            List<PlayerInfo> expected = new List<PlayerInfo>
            {
            };
            List<string> content = new List<string>{
                "playerid",
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
            Assert.DoesNotContain("Hi", result);
            Assert.DoesNotContain("A", result);
        }

        [Fact]
        public void GetStringsFromFile_ShouldExtractStrings_WithMinLengthOne()
        {
            // Arrange
            byte[] fakeData = new byte[]
            {
            (byte)'H', (byte)'i', 0,
            (byte)'A', 0
            };

            using var ms = new MemoryStream(fakeData);
            var reader = new FileRecReader(ms);
            var result = reader.GetStringsFromFile(minLength: 1);

            // Assert
            Assert.Contains("Hi", result);
            Assert.Contains("A", result);
        }
    }
}
