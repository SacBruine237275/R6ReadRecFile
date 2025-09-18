using R6ReadRecFile.Core.Interfaces;
using R6ReadRecFile.Core.Models;
using R6ReadRecFile.Core.Services;
using System.Text.Json;

namespace R6ReadRecFile.CLI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0 || args.Contains("-help") || args.Contains("-h"))
            {
                ShowHelp();
                return;
            }
            bool recursive = false;
            string? jsonFile = null;
            string? pathFile = null;
            bool silent = false;

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i].ToLower())
                {
                    case "-r":
                        recursive = true;
                        break;

                    case "-json":
                        if (i + 1 < args.Length && !args[i + 1].StartsWith("-")) //If the following argument exists and is not an option, it is the name of the JSON
                        {
                            jsonFile = args[i + 1];
                            i++;
                        }
                        break;
                    case "-silent":
                    case "-s":
                        silent = true;
                        break;

                    default:
                        pathFile = args[i];
                        break;
                }
            }

            if (args.Contains("-json"))
            {
                if (string.IsNullOrEmpty(jsonFile) || Directory.Exists(jsonFile))
                {
                    string folder = string.IsNullOrEmpty(jsonFile) ? Directory.GetCurrentDirectory() : jsonFile;
                    jsonFile = Path.Combine(folder, "output.json");
                }
            }
            try
            {
                List<RecFile> allRecFiles = new List<RecFile>();
                if (recursive)
                {
                    if (!Directory.Exists(pathFile))
                    {
                        throw new DirectoryNotFoundException($"Error: folder '{pathFile}' not found.");
                    }
                    string[] recFiles = Directory.GetFiles(pathFile);
                    foreach (string recFile in recFiles)
                    {
                        var rec = DisplayRecFile(recFile, jsonFile, silent);
                        allRecFiles.Add(rec);
                    }
                }
                else
                {
                    var rec = DisplayRecFile(pathFile, jsonFile, silent);
                    allRecFiles.Add(rec);
                }
                if (!string.IsNullOrEmpty(jsonFile))
                {
                    string jsonString;
                    jsonString = JsonSerializer.Serialize<List<RecFile>>(allRecFiles, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(jsonFile, jsonString);
                    if (!silent)
                        Console.WriteLine($"JSON output saved to: {jsonFile}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static RecFile DisplayRecFile(string? pathFile, string? jsonFile, bool silent)
        {
            IRecParser recParser = new RecParser();
            if (!File.Exists(pathFile))
            {
                throw new FileNotFoundException($"Error: file '{pathFile}' not found.");
            }
            using var file = new FileStream(pathFile, FileMode.Open);
            var rec = recParser.Parse(file);
            if (!silent)
            {
                Console.WriteLine(rec.Metadata.ToString() + "\n");
                foreach (var player in rec.Players)
                {
                    Console.WriteLine(player.ToString());
                }
                Console.WriteLine("\n");
            }
            return rec;

        }
        private static void ShowHelp()
        {
            Console.WriteLine("Usage:    R6ReadRecFile.CLI.exe \"<path_to_file.rec>\"");
            Console.WriteLine("Note:     Make sure to put the file path in double quotes if it contains spaces.\n");
            Console.WriteLine("-r        Recursive mode: provide a folder, all .rec files inside will be parsed.\n");
            Console.WriteLine("-json     Generate a JSON output. You can optionally specify the JSON file name.");
            Console.WriteLine("          If no JSON file name is provided, the output will be created in the execution folder.");
            Console.WriteLine("          If a name is specified that name will be used. Otherwise a default name will be assigned.\n");
            Console.WriteLine("-silent   Suppress all console output. Useful with -json to only write data to a file.");
            Console.WriteLine("          Short form: -s\n");
            Console.WriteLine("-help     Show this help message.");
            Console.WriteLine("          Short form: -h");
        }
    }
}