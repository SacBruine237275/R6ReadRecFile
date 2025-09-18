namespace R6ReadRecFile.Core.Models
{
    public class RecFile
    {
        public string FileName {  get; set; }=string.Empty;
        public long FileSize {  get; set; }

        public GameMetadata Metadata { get; set; }
        public List<PlayerInfo> Players { get; set; }
    }
}
