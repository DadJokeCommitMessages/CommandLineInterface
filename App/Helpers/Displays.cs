class Display
{
        public static void ShowTitle()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("______                           _             _          _   _____                           _ _    ___  ___              ");
        Console.WriteLine("| ___ \\                         | |           | |        | | /  __ \\                         (_) |   |  \\/  |              ");
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine("| |_/ / ___  ___ _ __   ___  ___| |_ __ _  ___| | ___  __| | | /  \\/ ___  _ __ ___  _ __ ___  _| |_  | .  . |___  __ _ ___ ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("| ___ \\/ _ \\/ __| '_ \\ / _ \\/ __| __/ _` |/ __| |/ _ \\/ _` | | |    / _ \\| '_ ` _ \\| '_ ` _ \\| | __| | |\\/| / __|/ _` / __|");
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine("| |_/ /  __/\\__ \\ |_) |  __/ (__| || (_| | (__| |  __/ (_| | | \\__/\\ (_) | | | | | | | | | | | | |_  | |  | \\__ \\ (_| \\__ \\");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("\\____/ \\___||___/ .__/ \\___|\\___|\\__\\__,_|\\___|_|\\___|\\__,_|  \\____/\\___/|_| |_| |_|_| |_| |_|_|\\__| \\_|  |_/___/\\__,_|___/");
        Console.WriteLine("                | |                                                                                               __/ |    ");
        Console.WriteLine("                |_|                                                                                              |___/     ");
        Console.ResetColor();
        Console.WriteLine("\n\n");
    }

public static void ShowJokeTypes(string prompt)
{
    Console.WriteLine(prompt + ":\n");

    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.WriteLine("1. Random Joke");
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("2. Fix Joke");
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine("3. Feature Joke");

    Console.ForegroundColor = ConsoleColor.White;

    Console.Write("\nEnter your choice (1-3): ");
}


}