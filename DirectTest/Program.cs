using System;
using System.IO;

namespace DirectoryNavigator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Start navigation from the root directory of the system
            string currentDirectory = @"C:\";

            while (true)
            {
                Console.WriteLine($"Current Directory: {currentDirectory}");

                // List directories and files in the current directory
                string[] directories = Directory.GetDirectories(currentDirectory);
                string[] files = Directory.GetFiles(currentDirectory);

                Console.WriteLine("\nDirectories:");
                foreach (string directory in directories)
                {
                    Console.WriteLine(Path.GetFileName(directory));
                }

                Console.WriteLine("\nFiles:");
                foreach (string file in files)
                {
                    Console.WriteLine(Path.GetFileName(file));
                }

                Console.Write("\nEnter the name of the directory you want to navigate to (or '..' to go up): ");

                string partialInput = "";
                while (true)
                {
                    // Check if a key is available
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo key = Console.ReadKey(intercept: true);
                        if (key.Key == ConsoleKey.Tab)
                        {
                            // Autocomplete directory name
                            string[] matches = GetAutocompleteMatches(partialInput, directories);
                            if (matches.Length == 1)
                            {
                                Console.Write(matches[0].Substring(partialInput.Length));
                                partialInput = matches[0];
                            }
                            else if (matches.Length > 1)
                            {
                                Console.WriteLine();
                                foreach (string match in matches)
                                {
                                    Console.WriteLine(match);
                                }
                                Console.Write("\nPress Tab again to cycle through matches: ");
                            }
                        }
                        else if (key.Key == ConsoleKey.Enter)
                        {
                            Console.WriteLine();
                            break;
                        }
                        else
                        {
                            Console.Write(key.KeyChar);
                            partialInput += key.KeyChar;
                        }
                    }
                }

                if (partialInput == "..")
                {
                    // Move up one level
                    currentDirectory = Path.GetDirectoryName(currentDirectory);
                }
                else if (Directory.Exists(Path.Combine(currentDirectory, partialInput)))
                {
                    currentDirectory = Path.Combine(currentDirectory, partialInput);
                }
                else
                {
                    Console.WriteLine("Directory not found!");
                }
            }
        }

        static string[] GetAutocompleteMatches(string partialInput, string[] options)
        {
            partialInput = partialInput.ToLower();
            return Array.FindAll(options, option => option.ToLower().StartsWith(partialInput));
        }
    }
}
