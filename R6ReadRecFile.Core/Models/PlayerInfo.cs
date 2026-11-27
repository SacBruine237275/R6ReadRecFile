using System.Numerics;

namespace R6ReadRecFile.Core.Models
{
    public class PlayerInfo
    {
        public string Name { get; set; } = string.Empty;
        public string Operator { get; set; } = string.Empty;
        public string Team {  get; set; }

        public string Spawn { get; set; }
        public Stats Stats { get; set; } = new Stats();

        public int Id;

        public override string ToString()
        {
            return $"Pseudo: {Name} | Operator: {Operator} | Team: {Team} | Spawn: {Spawn} | Stats: {Stats.Kill}/{Stats.Death}/{Stats.Assist} | Score: {Stats.Score}";
        }
    }
}
