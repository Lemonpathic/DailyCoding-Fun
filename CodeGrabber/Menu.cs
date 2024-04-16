using System;
namespace MyConsoleApp;

public class Menu
{
    public Menu()
    {
        System.Console.WriteLine("Welcome to Code Grabber\n" +
                          "Lets Navigate to your directory\n");
        System.Console.ReadLine();
        new FileConsole();
    }
}