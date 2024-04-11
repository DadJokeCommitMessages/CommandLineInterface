using System.Collections;
using System.Collections.Specialized;

class Display
{
    private static Dictionary<string, ConsoleColor> colours = new Dictionary<string, ConsoleColor>
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
    WriteLineColoured("______                           _             _          _   _____                           _ _    ___  ___              ", "Blue");
    WriteLineColoured("| ___ \\                         | |           | |        | | /  __ \\                         (_) |   |  \\/  |              ", "DarkMagenta");
    WriteLineColoured("| |_/ / ___  ___ _ __   ___  ___| |_ __ _  ___| | ___  __| | | /  \\/ ___  _ __ ___  _ __ ___  _| |_  | .  . |___  __ _ ___ ", "Green");
    WriteLineColoured("| ___ \\/ _ \\/ __| '_ \\ / _ \\/ __| __/ _` |/ __| |/ _ \\/ _` | | |    / _ \\| '_ ` _ \\| '_ ` _ \\| | __| | |\\/| / __|/ _` / __|", "Green");
    WriteLineColoured("| |_/ /  __/\\__ \\ |_) |  __/ (__| || (_| | (__| |  __/ (_| | | \\__/\\ (_) | | | | | | | | | | | |_  | |  | \\__ \\ (_| \\__ \\", "DarkMagenta");
    WriteLineColoured("\\____/ \\___||___/ .__/ \\___|\\___|\\__\\__,_|\\___|_|\\___|\\__,_|  \\____/\\___/|_| |_| |_|_| |_| |_|_|\\__| \\_|  |_/___/\\__,_|___/", "Blue");
    WriteLineColoured("                | |                                                                                               __/ |    ", "Blue");
    WriteLineColoured("                |_|                                                                                              |___/     ", "Blue");
    WriteLineColoured("\n\n", "White");
}
    public static void DisplayPrompt()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("\nBCM > ");
        Console.ResetColor();
    }

    public static void ShowJokeTypes(string prompt, string task)
{
    WriteLineColoured(prompt + ":\n", "White");

    string start = (task == "get") ? "0" : "1";
    if (task == "get")
    {
        WriteLineColoured("0. Random Joke", "DarkYellow");
    }
    WriteLineColoured("1. Fix Joke", "Cyan");
    WriteLineColoured("2. Feature Joke", "Magenta");
    WriteLineColoured("3. Refactor Joke", "Green");
    WriteLineColoured("4. Style Joke", "Blue");
    WriteLineColoured("5. Documentation Joke", "DarkCyan");
    WriteLineColoured("6. Test Joke", "DarkCyan");
    WriteLineColoured("7. Chore Joke", "DarkMagenta");

    WriteLineColoured($"\nEnter your choice ({start}-7): ", "White");
}

   public static void PrintErrorMessage(string message)
{
    WriteLineColoured(message, "DarkRed");
}

public static void PrintSuccessMessage(string message)
{
    WriteLineColoured(message, "Green");
}

    public static void SetConsoleColour(string colorName)
    {
        if (colours.ContainsKey(colorName))
            Console.ForegroundColor = colours[colorName];
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
            SetConsoleColour("DarkYellow");
            Console.Write($"{message} (yes/no): ");
            Console.WriteLine();
            SetConsoleColour("White");
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

        Console.Write("\nEnter your choice (1-2): ");

        return Console.ReadLine();
    }

    public static void WriteLineColoured(string line, string colour)
    {

        Display.SetConsoleColour(colour);
        Console.WriteLine(line);
        Display.SetConsoleColour("White");

    }
    
    public static void DisplayHelp()
    {
        WriteLineColoured("Welcome to Bespectacled Commit Message.", "DarkYellow");
        Console.WriteLine("");
        
        WriteLineColoured("Tired of writing helpful commit messages?\nWell, you've come to the right place. Get your dad joke commit messages today.", "Blue");
        Console.WriteLine("");
        
        WriteLineColoured("Available commands:\n", "DarkMagenta");
        WriteLineColoured("- get-joke: Retrieve a joke.", "Cyan");
        WriteLineColoured("- add-joke: Add a new joke.", "Magenta");
        WriteLineColoured("- get-my-jokes: View all jokes added by you.", "Green");
        WriteLineColoured("- edit-joke: Edit a joke.", "Blue");
        WriteLineColoured("- delete-joke: Delete a joke.", "DarkCyan");
        
        Console.WriteLine("");
        WriteLineColoured("Enjoy joking around! :) \n\n", "DarkMagenta");
        
    }

}