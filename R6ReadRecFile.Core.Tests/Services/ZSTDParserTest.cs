using R6ReadRecFile.Core.Services;
using R6ReadRecFile.Core.Tests.Helpers;
using static R6ReadRecFile.Core.Tests.Helpers.FakeZstdRecBuilder;

namespace R6ReadRecFile.Core.Tests.Services
{
    public class ZSTDParserTest
    {
        [Fact]
        public void Parse_ShouldReturnPlayersWithStats_WhenFileIsValid()
        {
            var tempFile = Path.GetTempFileName();
            var fileData = new FakeZstdRecBuilder()
                .AddPlayer(0xF00CE594, "Name1", "Ruins", kills: 1, score: 100)
                .AddPlayer(0xF00CE585, "Name2", "Bar")
                .AddStatUpdate(0xF00CE585, (DeathIndicator, 1))
                .Build();
            File.WriteAllBytes(tempFile, fileData);

            var parser = new ZSTDParser();
            var actual = parser.Parse(tempFile);

            Assert.NotNull(actual);
            Assert.NotNull(actual.Players);
            Assert.Equal(2, actual.Players.Count);
            Assert.Contains(actual.Players, p => p.Name == "Name1" && p.Spawn == "Ruins" && p.Stats.Kill == 1 && p.Stats.Score == 100);
            Assert.Contains(actual.Players, p => p.Name == "Name2" && p.Spawn == "Bar" && p.Stats.Death == 1);

            File.Delete(tempFile);
        }
    }
}
