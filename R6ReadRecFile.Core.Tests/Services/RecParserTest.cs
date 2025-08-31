using R6ReadRecFile.Core.Models;
using R6ReadRecFile.Core.Services;

namespace R6ReadRecFile.Core.Tests.Services
{
    public class RecParserTest
    {

        [Fact]
        public void Parse_ShouldThrow_WhenFileDoesNotExist()
        {
            var parser = new RecParser();

            var ex = Assert.Throws<FileNotFoundException>(() => parser.Parse("not_existing.rec"));
            Assert.Contains("not found", ex.Message);
        }

        [Fact]
        public void Parse_ShouldReturnPlayers_WhenFileIsValid()
        {
            List<PlayerInfo> expected = new List<PlayerInfo> {
                new PlayerInfo{Name="Name1"},
                new PlayerInfo{Name="Name2"}
            };
            var tempFile = Path.GetTempFileName();
            

            var fakeContent = System.Text.Encoding.ASCII.GetBytes("playername\0Name1\0playername\0Name2\0");
            File.WriteAllBytes(tempFile, fakeContent);

            var parser = new RecParser();

            var actual = parser.Parse(tempFile);

            Assert.NotNull(actual);
            Assert.NotNull(actual.Players);
            Assert.Equal(expected.Count, actual.Players.Count);
            Assert.All(expected, expectedItem => Assert.Contains(actual.Players, actualItem => actualItem.Name == expectedItem.Name));

            File.Delete(tempFile);
        }
    }
}
