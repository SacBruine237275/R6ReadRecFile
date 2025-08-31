namespace R6ReadRecFile.Core.Models
{
    public class GameMetada
    {
        public string Version { get; set; } = string.Empty;
        public TimeSpan Duration { get;set; }
        public string Map { get; set; } = string.Empty;
        public string Mode {  get; set; } = string.Empty;
    }
}
