namespace CodeGrabber;


public class DisplayCommands
{
    public void Display()
    {
        Console.WriteLine("COMMANDS: \n" +
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
}
