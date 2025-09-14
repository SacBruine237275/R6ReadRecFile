using R6ReadRecFile.Core.Interfaces;
using R6ReadRecFile.Core.Services;
using System.Diagnostics;

namespace R6ReadRecFile.CLI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0 || args.Contains("-help"))
            {
                ShowHelp();
                return;
            }
            bool recursive = args.Contains("-r");
            string? pathFile = args.FirstOrDefault(a => a != "-r");
            try
            {
                if (recursive) {
                    if (!Directory.Exists(pathFile)) {
                        throw new DirectoryNotFoundException($"Error: folder '{pathFile}' not found.");
                    }
                   string[] recFiles=Directory.GetFiles(pathFile);
                    foreach (string recFile in recFiles)
                        DisplayRecFile(recFile);
                }
                else
                {
                    DisplayRecFile(pathFile);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static void DisplayRecFile(string? pathFile)
        {
            IRecParser recParser = new RecParser();
            if (!File.Exists(pathFile))
            {
                throw new FileNotFoundException($"Error: file '{pathFile}' not found.");
            }
            using var file = new FileStream(pathFile, FileMode.Open);
            var rec = recParser.Parse(file);
            Console.WriteLine(rec.Metadata.ToString() + "\n");
            foreach (var player in rec.Players)
            {
                Console.WriteLine(player.ToString());
            }
            Console.WriteLine("\n");
        }
        private static void ShowHelp()
        {
            Console.WriteLine("Usage: R6ReadRecFile.CLI.exe \"<path_to_file.rec>\"");
            Console.WriteLine("Note: Make sure to put the file path in double quotes if it contains spaces.");
            Console.WriteLine("-r Recursive mode: provide a folder, all .rec files inside will be parsed.");
            Console.WriteLine("-help   Show this help message.");
        }
    }
}