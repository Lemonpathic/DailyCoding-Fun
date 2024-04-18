using System.Text.Json;

namespace CodeGrabber;

public class FileConsole
{
    private string currentDirectory = @"C:\";
    private DisplayCommands displayCommands = new DisplayCommands();
    public FileConsole()
    {
       displayCommands.Display();
       while (true)
       {
           RunCommand();
       }
    }

    

    private void CreateFavorite()
    {
        string filePath = "favorites.Json";
        Console.WriteLine(File.Exists("favorites.Json"));
        Console.WriteLine("What is the shortcut name for this directory?");
        var nickName = Console.ReadLine();
        if (nickName is null)
        {
            var uuid = Guid.NewGuid().ToString();
            nickName = uuid;
        }
        var favorite = new jsonElementFavorites();

        favorite.Nickname = nickName;
        favorite.Directory = currentDirectory;
            

        if (File.Exists("favorites.json"))
        {
            string? existingJson = File.ReadAllText(filePath);

            var data = JsonSerializer.Deserialize<List<jsonElementFavorites>>(existingJson);
            data?.Add(favorite);
            var jsonStringList = JsonSerializer.Serialize(data, new JsonSerializerOptions {WriteIndented = true});
            File.WriteAllText(filePath,jsonStringList);
            

        }
        else
        {
            var favList = new List<jsonElementFavorites> { favorite };
            var jsonString = JsonSerializer.Serialize(favList, new JsonSerializerOptions { WriteIndented = true });
            
            File.WriteAllText(filePath, jsonString);
        }  
    }
    private void NavigateFavorite(int navigationNumber)
    {
        
    }
private void RunCommand()
{
    string[] foldersRaw = Directory.GetDirectories(currentDirectory);
    string[] folders = new string[foldersRaw.Length];
    for (int i = 0; i < foldersRaw.Length; i++)
    {
        folders[i] = Path.GetFileName(foldersRaw[i]).ToLower();
    }

    Console.WriteLine($"Current Directory {currentDirectory}, press 'H' for the command list" +
                             $"\nEnter a Command : ");
    string? command = Console.ReadLine().ToLower();
    string[] commandList = command.Split(' '); 
    Console.Clear();

    switch (commandList[0])
    {
        case "ls":
        case "lf":
        case "ld":
            var filesDisplay = new FoundDisplay();
            filesDisplay.Display(command, currentDirectory);
            break;

        case "cd":
            if (commandList.Length > 1 && folders.Contains(commandList[1]))
            {
                currentDirectory = Path.Combine(currentDirectory, commandList[1]);
            }
            else if (commandList.Length > 1 && commandList[1] == "..")
            {
                List<string> directorySections = currentDirectory.Split('\\').ToList();
                if (directorySections.Count > 1)
                {
                    directorySections.RemoveAt(directorySections.Count - 1);
                    currentDirectory = string.Join('\\', directorySections);
                    if (currentDirectory.ToLower() == "c:")
                    {
                        currentDirectory = "C:\\";
                    }
                }
            }
            break;

        case "h":
            displayCommands.Display();
            break;

        case "*":
        case "fav":
            CreateFavorite();
            break;

        case "fd":
            string filePath = "favorites.json";
            if (File.Exists(filePath))
            {
                var jsonLines = File.ReadAllText(filePath);
                var jsonData = JsonSerializer.Deserialize<List<jsonElementFavorites>>(jsonLines);

                Console.WriteLine("Directory Shortcuts: ");
                int index = 0;
                if (jsonData is not null)
                {
                    foreach (var jsonObj in jsonData)
                    {
                        string? nickname = jsonObj.Nickname;
                        string? directory = jsonObj.Directory;

                        Console.WriteLine($"-----------------TYPE '{index}' TO NAVIGATE----------------");
                        Console.WriteLine($"[NAME]: {nickname}\n" +
                                          $"[DIRECTORY]: {directory}");
                        Console.WriteLine("----------------------------------------------------");

                        index += 1;
                    }
                }
            }
            break;

        default:
            Console.WriteLine("Invalid command. Press 'H' for help.");
            break;
    }
}
    }
   
