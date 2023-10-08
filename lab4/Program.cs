using System;
using System.IO;
using Classes;

namespace YourConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Usage: YourConsoleApp <command>");
                Console.WriteLine("Commands:");
                Console.WriteLine("  version - Display program version and author.");
                Console.WriteLine("  run - Execute a lab task.");
                Console.WriteLine("  set-path - Set the LAB_PATH environment variable.");
                args = Console.ReadLine().Split(' ');
                string command = args[0].ToLower();

                switch (command)
                {
                    case "version":
                        DisplayVersionInfo();
                        break;
                    case "run":
                        ExecuteLabTask(args);
                        break;
                    case "set-path":
                        SetLabPath(args);
                        break;
                    default:
                        Console.WriteLine($"Unknown command: {command}");
                        break;
                }
                Console.ReadKey();
                Console.Clear();
            }
        }

        static void DisplayVersionInfo()
        {
            Console.WriteLine("YourConsoleApp Version 1.0");
            Console.WriteLine("Author: IPZ-42 Gnatishin Yuri");
        }

        static void ExecuteLabTask(string[] args)
        {
            string subcommand = GetParameter(args, "subcommand");

            if (string.IsNullOrEmpty(subcommand))
            {
                Console.WriteLine("Please provide a subcommand for 'run' (lab1, lab2, or lab3).");
                return;
            }

            string inputFilePath = GetParameter(args, "-I");
            string outputFilePath = GetParameter(args, "-o");

            if (string.IsNullOrEmpty(inputFilePath))
            {
                Console.WriteLine("Please provide an input file using -I.");
                return;
            }

            if (!File.Exists(inputFilePath))
            {
                Console.WriteLine($"Input file '{inputFilePath}' not found.");
                return;
            }

            if (string.IsNullOrEmpty(outputFilePath))
            {
                Console.WriteLine("Please provide an output file using -o.");
                return;
            }

            try
            {
                switch (subcommand.ToLower())
                {
                    case "lab1":
                        lab1.Run(inputFilePath, outputFilePath);
                        break;
                    case "lab2":
                        lab2.Run(inputFilePath, outputFilePath);
                        break;
                    case "lab3":
                        lab3.Run(inputFilePath, outputFilePath);
                        break;
                    default:
                        Console.WriteLine($"Unknown subcommand: {subcommand}");
                        break;
                }

                Console.WriteLine($"Lab task '{subcommand}' completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        static void SetLabPath(string[] args)
        {
            string labPath = GetParameter(args, "-p") ?? GetParameter(args, "--path");

            if (string.IsNullOrEmpty(labPath))
            {
                Console.WriteLine("Please provide a valid path using -p or --path.");
            }
            else
            {
                Environment.SetEnvironmentVariable("LAB_PATH", labPath, EnvironmentVariableTarget.User);
                Console.WriteLine($"LAB_PATH set to: {labPath}");
            }
        }

        static string GetParameter(string[] args, string paramName)
        {
            for (int i = 1; i < args.Length - 1; i++)
            {
                if (args[i].Equals(paramName, StringComparison.OrdinalIgnoreCase))
                {
                    return args[i + 1];
                }
            }
            return null;
        }
    }
}