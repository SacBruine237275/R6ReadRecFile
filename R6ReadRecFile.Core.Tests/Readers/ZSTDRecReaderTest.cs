using R6ReadRecFile.Core.Readers;
using R6ReadRecFile.Core.Tests.Helpers;
using static R6ReadRecFile.Core.Tests.Helpers.FakeZstdRecBuilder;

namespace R6ReadRecFile.Core.Tests.Readers
{
    public class ZSTDRecReaderTest
    {
        private const uint Player1Id = 0xF00CE594;
        private const uint Player2Id = 0xF00CE585;

        [Fact]
        public void ExtractPlayerInfo_ShouldReadNamesIdsAndSpawns()
        {
            var fileData = new FakeZstdRecBuilder()
                .AddPlayer(Player1Id, "Name1", "Ruins")
                .AddPlayer(Player2Id, "Name2", "Bar")
                .Build();

            var actual = ZSTDRecReader.ExtractPlayerInfo(fileData);

            Assert.Equal(2, actual.Count);
            Assert.Contains(actual, p => p.Name == "Name1" && p.Id == unchecked((int)Player1Id) && p.Spawn == "Ruins");
            Assert.Contains(actual, p => p.Name == "Name2" && p.Id == unchecked((int)Player2Id) && p.Spawn == "Bar");
        }

        [Fact]
        public void ExtractPlayerInfo_ShouldReadStatsFromInitBlock()
        {
            var fileData = new FakeZstdRecBuilder()
                .AddPlayer(Player1Id, "Name1", "Ruins", kills: 10, deaths: 1, assists: 2, score: 1983)
                .Build();

            var actual = ZSTDRecReader.ExtractPlayerInfo(fileData).Single();

            Assert.Equal(10, actual.Stats.Kill);
            Assert.Equal(1, actual.Stats.Death);
            Assert.Equal(2, actual.Stats.Assist);
            Assert.Equal(1983, actual.Stats.Score);
        }

        [Fact]
        public void ExtractPlayerInfo_ShouldKeepLatestStatUpdates()
        {
            var fileData = new FakeZstdRecBuilder()
                .AddPlayer(Player1Id, "Name1", "Ruins")
                .AddPlayer(Player2Id, "Name2", "Bar")
                .AddStatUpdate(Player1Id, (KillIndicator, 1), (ScoreIndicator, 120))
                .AddStatUpdate(Player2Id, (DeathIndicator, 1))
                .AddStatUpdate(Player1Id, (AssistIndicator, 1))
                .AddStatUpdate(Player1Id, (KillIndicator, 2), (ScoreIndicator, 290))
                .Build();

            var actual = ZSTDRecReader.ExtractPlayerInfo(fileData);

            var player1 = actual.Single(p => p.Name == "Name1");
            Assert.Equal(2, player1.Stats.Kill);
            Assert.Equal(0, player1.Stats.Death);
            Assert.Equal(1, player1.Stats.Assist);
            Assert.Equal(290, player1.Stats.Score);
            var player2 = actual.Single(p => p.Name == "Name2");
            Assert.Equal(0, player2.Stats.Kill);
            Assert.Equal(1, player2.Stats.Death);
            Assert.Equal(0, player2.Stats.Assist);
            Assert.Equal(0, player2.Stats.Score);
        }

        [Fact]
        public void ExtractPlayerInfo_ShouldIgnoreStatWithoutPlayerHeader()
        {
            var fileData = new FakeZstdRecBuilder()
                .AddPlayer(Player1Id, "Name1", "Ruins", kills: 3)
                .AddPadding(200) // No "23 <id> 00000000" header within reach
                .AddBytes(KillIndicator).AddBytes(0x04, 99, 0, 0, 0)
                .Build();

            var actual = ZSTDRecReader.ExtractPlayerInfo(fileData).Single();

            Assert.Equal(3, actual.Stats.Kill);
        }

        [Fact]
        public void ExtractPlayerInfo_ShouldIgnoreStatOfUnknownObject()
        {
            var fileData = new FakeZstdRecBuilder()
                .AddPlayer(Player1Id, "Name1", "Ruins", score: 50)
                .AddStatUpdate(0xF00CD354, (ScoreIndicator, 1))
                .Build();

            var actual = ZSTDRecReader.ExtractPlayerInfo(fileData).Single();

            Assert.Equal(50, actual.Stats.Score);
        }

        [Fact]
        public void ExtractPlayerInfo_ShouldReturnNoPlayers_WhenNoPlayerBlock()
        {
            var fileData = new FakeZstdRecBuilder()
                .AddStatUpdate(Player1Id, (KillIndicator, 1))
                .Build();

            var actual = ZSTDRecReader.ExtractPlayerInfo(fileData);

            Assert.Empty(actual);
        }
    }
}
