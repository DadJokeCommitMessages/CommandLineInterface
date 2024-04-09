using System;
using System.Collections.Generic;

class Display
{
    private static Dictionary<string, ConsoleColor> colors = new Dictionary<string, ConsoleColor>
    {
        { "DarkYellow", ConsoleColor.DarkYellow },
        { "Cyan", ConsoleColor.Cyan },
        { "Magenta", ConsoleColor.Magenta },
        { "Green", ConsoleColor.Green },
        { "Blue", ConsoleColor.Blue },
        { "DarkCyan", ConsoleColor.DarkCyan },
        { "DarkMagenta", ConsoleColor.DarkMagenta },
        { "DarkGreen", ConsoleColor.DarkGreen }
    };

    public static void ShowTitle()
    {
        SetConsoleColor("Blue");
        Console.WriteLine("______                           _             _          _   _____                           _ _    ___  ___              ");
        SetConsoleColor("DarkMagenta");
        Console.WriteLine("| ___ \\                         | |           | |        | | /  __ \\                         (_) |   |  \\/  |              ");
        SetConsoleColor("Green");
        Console.WriteLine("| |_/ / ___  ___ _ __   ___  ___| |_ __ _  ___| | ___  __| | | /  \\/ ___  _ __ ___  _ __ ___  _| |_  | .  . |___  __ _ ___ ");
        SetConsoleColor("Green");
        Console.WriteLine("| ___ \\/ _ \\/ __| '_ \\ / _ \\/ __| __/ _` |/ __| |/ _ \\/ _` | | |    / _ \\| '_ ` _ \\| '_ ` _ \\| | __| | |\\/| / __|/ _` / __|");
        SetConsoleColor("DarkMagenta");
        Console.WriteLine("| |_/ /  __/\\__ \\ |_) |  __/ (__| || (_| | (__| |  __/ (_| | | \\__/\\ (_) | | | | | | | | | | | |_  | |  | \\__ \\ (_| \\__ \\");
        SetConsoleColor("Blue");
        Console.WriteLine("\\____/ \\___||___/ .__/ \\___|\\___|\\__\\__,_|\\___|_|\\___|\\__,_|  \\____/\\___/|_| |_| |_|_| |_| |_|_|\\__| \\_|  |_/___/\\__,_|___/");
        Console.WriteLine("                | |                                                                                               __/ |    ");
        Console.WriteLine("                |_|                                                                                              |___/     ");
        Console.ResetColor();
        Console.WriteLine("\n\n");
    }

    public static void ShowJokeTypes(string prompt, string task)
    {
        Console.WriteLine(prompt + ":\n");

        string start = (task == "get") ? "0" : "1";
        if (task == "get")
        {
            SetConsoleColor("DarkYellow");
            Console.WriteLine("0. Random Joke");
        }
        SetConsoleColor("Cyan");
        Console.WriteLine("1. Fix Joke");
        SetConsoleColor("Magenta");
        Console.WriteLine("2. Feature Joke");
        SetConsoleColor("Green");
        Console.WriteLine("3. Refactor Joke");
        SetConsoleColor("Blue");
        Console.WriteLine("4. Style Joke");
        SetConsoleColor("DarkCyan");
        Console.WriteLine("5. Documentation Joke");
        SetConsoleColor("DarkCyan");
        Console.WriteLine("6. Test Joke");
        SetConsoleColor("DarkMagenta");
        Console.WriteLine("7. Chore Joke");

        SetConsoleColor("White");

        Console.Write($"\nEnter your choice ({start}-7): ");
    }

    public static void PrintErrorMessage(string message)
    {
        SetConsoleColor("Red");
        Console.WriteLine(message);
        Console.ResetColor();
    }
    public static void PrintSuccessMessage(string message)
    {
        SetConsoleColor("Green");
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void SetConsoleColor(string colorName)
    {
        if (colors.ContainsKey(colorName))
            Console.ForegroundColor = colors[colorName];
        else
            Console.ForegroundColor = ConsoleColor.White;
    }
}