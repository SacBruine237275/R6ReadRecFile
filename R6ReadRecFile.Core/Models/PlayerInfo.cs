namespace R6ReadRecFile.Core.Models
{
    public class PlayerInfo
    {
        public string Name { get; set; } = string.Empty;
        public string Operator { get; set; } = string.Empty;
        public Team Team {  get; set; }
       /* public int Kill {  get; set; }
        public int Death { get; set; }
        public int Assist { get; set; }*/

    }

    public enum Team
    {
        Enemy=0,
        Your=1,
        Unknow=2
    }
}
