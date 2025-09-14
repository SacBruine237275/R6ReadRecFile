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
}
