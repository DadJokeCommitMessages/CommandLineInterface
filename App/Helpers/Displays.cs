using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

using System.Linq;


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
        { "DarkGreen", ConsoleColor.DarkGreen },
        {"DarkRed", ConsoleColor.DarkRed },
        {"White", ConsoleColor.White },
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

    public static void DisplayPrompt()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("\nBCM > ");
        Console.ResetColor();
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
        SetConsoleColor("DarkRed");
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

    public static void DisplayTable(OrderedDictionary jokeMap)
    {
        int maxStoryWidth = jokeMap.Values.Cast<JokeResponse>().Max(j => j.story.Length);

        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write($"  {"ID".PadRight(5)}  ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write($" {"Joke".PadRight(maxStoryWidth)} ");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($" {"Type".PadRight(8)}");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine($"{new string('-', 8)}{new string('-', maxStoryWidth)}{new string('-', 11)}");
        Console.ResetColor();

        foreach (DictionaryEntry entry in jokeMap)
        {
            JokeResponse joke = (JokeResponse)entry.Value;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{entry.Key,5}");

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write($" | {joke.story.PadRight(maxStoryWidth)}");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($" | {joke.jokeType,-8}");

            Console.ResetColor();
        }
        Console.WriteLine();
    }

    public static bool GetUserConfirmation(string message)
    {
        bool confirmed;
        do
        {
            SetConsoleColor("DarkYellow");
            Console.Write($"{message} (yes/no): ");
            Console.WriteLine();
            SetConsoleColor("White");
            string userInput = Console.ReadLine().Trim().ToLower();

            switch (userInput)
            {
                case "yes":
                    confirmed = true;
                    return confirmed;
                case "no":
                    confirmed = false;
                    return confirmed;
                default:
                    PrintErrorMessage("\nInvalid input. Please enter 'yes' or 'no'.");
                    break;
            }
        } while (true);
    }

    public static string GetType(string[] jokeTypes)
    {
        int typeIndex;
        do
        {
            Console.Write($"\nEnter your choice ({1}-{jokeTypes.Length}): ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out typeIndex))
            {
                return jokeTypes[typeIndex - 1];
            }
            else
            {
                PrintErrorMessage("Invalid input. Please enter a valid integer.");
                return "";
            }
        }
        while (typeIndex > 0 && typeIndex < jokeTypes.Length);
    }

    public static string GetEditOptions()
    {
        Console.WriteLine("\nWhat would you like to edit? \n");
        Console.WriteLine("1. Edit Joke");
        Console.WriteLine("2. Edit Joke Type");

        return Console.ReadLine();
    }

    public static void WriteLineColoured(string line, string colour)
    {

        Display.SetConsoleColor(colour);
        Console.WriteLine(line);
        Display.SetConsoleColor("White");

    }
}