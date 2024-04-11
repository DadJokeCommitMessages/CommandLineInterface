class Application
{
    static void Main()
    {
        Display.ShowTitle();
        Display.DisplayHelp();

        User.SignIn();
        while (true)
        {
            Display.DisplayPrompt();
            string? input = Console.ReadLine() ?? "".ToLower();
            Console.WriteLine();

            if (input == "exit")  break;

            if (input == "help")
            {
                Display.DisplayHelp();
                continue;
            }

            if (Command.JokeMethods.TryGetValue(input, out Func<Task>? action))
            {
                string task = input == "get-joke" ? "get" : "submit";

                Display.ShowJokeTypes($"Please choose the type of joke you would like to " + task + "?", task);
                action().Wait();
            }
            else if (Command.UserJokeMethods.TryGetValue(input, out Func<Task>? userAction))
            {
                userAction().Wait();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Invalid command.");
                Console.ResetColor();
            }
        }
    }
}