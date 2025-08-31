using R6ReadRecFile.Core.Interfaces;
using R6ReadRecFile.Core.Services;
using System.Diagnostics;

namespace R6ReadRecFile.CLI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Error: no file provided.");
                Console.WriteLine("Usage: R6ReadRecFile.CLI.exe \"<path_to_file.rec>\"");
                Console.WriteLine("Note: Make sure to put the file path in double quotes if it contains spaces.");
                return;
            }
            string pathFile = args[0];
            try
            {
                IRecParser recParser = new RecParser();
                var rec = recParser.Parse(pathFile);
                foreach (var player in rec.Players)
                {
                    Console.WriteLine($"Pseudo: {player.Name} | Operator: {player.Operator} | Team: {player.Team} Team");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}