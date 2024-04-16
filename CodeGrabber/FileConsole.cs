using System.Threading.Channels;

namespace MyConsoleApp;

public class FileConsole
{
    private string currentDirectory = @"C:\";
    public FileConsole()
    {
       DisplayCommands();
       while (true)
       {
           RunCommand();
       }
    }

    private void DisplayCommands()
    {
        System.Console.WriteLine("COMMANDS: \n" +
                                 "ls - list all in current directory \n" +
                                 "lf - List files in current directory \n" +
                                 "ld - List all folders in current directory\n" +
                                 "cd - Change your directory \n" +
                                 ".. - To go up a directory \n" +
                                 "fd - Open a list of your favorite directories \n" +
                                 "*  - Add a directory to your favorites \n" +
                                 "\n" +
                                 "Press ENTER to exit command menu: ");
    }

    private string[] Cleanse(string[] array)
    {
        foreach (var VARIABLE in array)
        {
            VARIABLE.Replace(currentDirectory, "");
        }

        return array;
    }
    private void DisplayFound(string command)
    {
        System.Console.Clear();
        string[] directories = Directory.GetDirectories(currentDirectory);
        string[] files = Directory.GetFiles(currentDirectory);
        switch (command)
        {
            case "ls":
                System.Console.Clear();
                foreach (var VARIABLE in directories)
                {
                    System.Console.WriteLine($"[FOLDER] {Path.GetFileName(VARIABLE)}");
                }

                foreach (var VARIABLE in files)
                {
                    System.Console.WriteLine($"[FILE] {Path.GetFileName(VARIABLE)}");
                }
                break;
            case "lf":
                System.Console.Clear();
                System.Console.WriteLine("-----FILES-----");
                foreach (var File in files)
                {
                    System.Console.WriteLine(Path.GetFileName(File));
                }
                break;
            case "ld":
                System.Console.Clear();
                System.Console.WriteLine("----FOLDERS----");

                foreach (var VARIABLE in directories)
                {
                    System.Console.WriteLine(Path.GetFileName(VARIABLE));
                }
                break;
        }
    }
    private void RunCommand()
    {
        string[] foldersRaw = Directory.GetDirectories(currentDirectory);
        string[] folders = new string[foldersRaw.Length];
        for (int i = 0; i < foldersRaw.Length; i++)
        {
            folders[i] = Path.GetFileName(foldersRaw[i]).ToLower();
            
        }

        System.Console.WriteLine($"Current Directory {currentDirectory}, press 'H' for the command list" +
                                 $"\nEnter a Command : ");
        string command = System.Console.ReadLine().ToLower();
        string[] commandList = command.Split(' '); 
        System.Console.Clear();
            if (commandList[0] == "ls" || commandList[0] == "lf" || commandList[0] == "ld")
        {
            DisplayFound(command);
        }
            
        if (commandList[0] == "cd")
        {
            
            if (folders.Contains(commandList[1]))
            {
                currentDirectory = Path.Combine(currentDirectory, commandList[1]);
            }
        }

        if (commandList[0] == "h")
        {
            DisplayCommands();
        }

        if (commandList[0] == "..")
        {
            List<string> directorySections = currentDirectory.Split('/').ToList();
            {
                directorySections.RemoveAt(directorySections.Count - 1);
                currentDirectory = string.Join(',', directorySections);
            }
        }
    }
   
}