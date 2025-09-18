using R6ReadRecFile.Core.Models;
using R6ReadRecFile.Core.Services;

namespace R6ReadRecFile.Core.Tests.Services
{
    public class RecParserTest
    {

        [Fact]
        public void Parse_ShouldReturnPlayers_WhenFileIsValid()
        {
            List<PlayerInfo> expected = new List<PlayerInfo> {
                new PlayerInfo{Name="Name1",Operator="LESION",Team="ENEMY TEAM"},
                new PlayerInfo{Name="Name2",Operator="HIBANA",Team="YOUR TEAM"}
            };
            var tempFile = Path.GetTempFileName();


            var fakeContent = System.Text.Encoding.ASCII.GetBytes(
             "teamname0\0ENEMY TEAM\0" +
             "playerid\0id\0" +
             "profileid\0id\0" +
             "playername\0Name1\0" +
             "team\00\0" +
             "other\0ignored\0" +
             "other\0ignored\0" +
             "other\0ignored\0" +
             "operator\0LESION\0" +

             "teamname1\0YOUR TEAM\0" +
             "playerid\0id\0" +
             "profileid\0id\0" +
             "playername\0Name2\0" +
             "team\01\0" +
             "other\0ignored\0" +
             "other\0ignored\0" +
             "other\0ignored\0" +
             "operator\0HIBANA\0"
            );
            File.WriteAllBytes(tempFile, fakeContent);

            var parser = new RecParser();

            using var file = new FileStream(tempFile, FileMode.Open);
            var actual = parser.Parse(file);

            Assert.NotNull(actual);
            Assert.NotNull(actual.Players);
            Assert.Equal(expected.Count, actual.Players.Count);
            Assert.All(expected, expectedItem => Assert.Contains(actual.Players, actualItem => actualItem.Name == expectedItem.Name));
            Assert.All(expected, expectedItem => Assert.Contains(actual.Players, actualItem => actualItem.Operator == expectedItem.Operator));
            Assert.All(expected, expectedItem => Assert.Contains(actual.Players, actualItem => actualItem.Team == expectedItem.Team));

            file.Close();
            File.Delete(tempFile);
        }
    }
}
