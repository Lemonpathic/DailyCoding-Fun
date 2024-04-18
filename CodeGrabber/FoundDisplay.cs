namespace CodeGrabber;

public class FoundDisplay
{
    public void Display(string command, string directory)
    {
        Console.Clear();
        string[] directories = Directory.GetDirectories(directory);
        string[] files = Directory.GetFiles(directory);
        switch (command)
        {
            case "ls":
                Console.Clear();
                foreach (var folder in directories)
                {
                    Console.WriteLine($"[FOLDER] {Path.GetFileName(folder)}");
                }

                foreach (var file in files)
                {
                    Console.WriteLine($"[FILE] {Path.GetFileName(file)}");
                }

                break;
            case "lf":
                Console.Clear();
                Console.WriteLine("-----FILES-----");
                foreach (var File in files)
                {
                    Console.WriteLine(Path.GetFileName(File));
                }

                break;
            case "ld":
                Console.Clear();
                Console.WriteLine("----FOLDERS----");

                foreach (var VARIABLE in directories)
                {
                    Console.WriteLine(Path.GetFileName(VARIABLE));
                }

                break;
        }
    }
}