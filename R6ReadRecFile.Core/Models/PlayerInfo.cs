using System.Numerics;

namespace R6ReadRecFile.Core.Models
{
    public class PlayerInfo
    {
        public string Name { get; set; } = string.Empty;
        public string Operator { get; set; } = string.Empty;
        public string Team {  get; set; }

        public string Spawn { get; set; }
       /* public int Kill {  get; set; }
        public int Death { get; set; }
        public int Assist { get; set; }*/

        public override string ToString()
        {
            return $"Pseudo: {Name} | Operator: {Operator} | Team: {Team} | Spawn: {Spawn}";
        }
    }
}
