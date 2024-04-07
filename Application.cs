using System;
using System.Collections.Generic;
using System.Threading.Tasks;

class Application
{
    static void Main()
    {
        Display.ShowTitle();

        Dictionary<string, Func<Task>> commandActions = new Dictionary<string, Func<Task>>()
        {
            { "get-joke", Command.getJoke },
            { "submit-joke", Command.SubmitJoke }
        };

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\nBCM > ");
            Console.ResetColor();
            string? input = Console.ReadLine()  ?? "".ToLower();
            Console.WriteLine();

            if (input == "exit")
                break;

            if (commandActions.TryGetValue(input, out Func<Task>? action))
            {
                string task = "";
                if(input=="get-joke") task = "get";
                else task= "submit";
                Display.ShowJokeTypes($"Please choose the type of joke you would like to " + task + "?");
                action().Wait();
            }
            else if(false){


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