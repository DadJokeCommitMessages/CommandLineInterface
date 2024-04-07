
class Command
{


    public static async Task getJoke()
    {

        string? choice = Console.ReadLine();
        Console.WriteLine("\n");

        switch (choice)
        {
            case "1":
                //await ApiCalls.GetRandomJoke();
                //Make API get Call  await ApiCalls.GetJoke();
                break;
            case "2":
                Console.WriteLine("Fix Joke selected.");
                //Make API get Call  await ApiCalls.GetJoke(type);
                break;
            case "3":
                Console.WriteLine("Feature Joke selected.");
                //Make API post Call await ApiCalls.AddJoke(type);

                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }

        Console.WriteLine("\n\n");
    }


    public static async Task SubmitJoke()
    {
        string? submission = Console.ReadLine();
        string? joke;

        switch (submission)
        {
            case "1":
                Console.WriteLine("Please enter the random joke:");
                joke = Console.ReadLine();
                Console.WriteLine("Submitting random joke...");
                //await ApiCalls.SubmitJoke(type,joke);
                break;

            case "2":
                Console.WriteLine("Please enter the fix joke:");
                joke = Console.ReadLine();
                Console.WriteLine("Submitting fix joke...");
                //await ApiCalls.SubmitJoke(type,joke);
                break;

            case "3":
                Console.WriteLine("Please enter the feature joke:");
                joke = Console.ReadLine();
                Console.WriteLine("Submitting feature joke...");
                //await ApiCalls.SubmitJoke(type,joke);
                break;

            default:
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Invalid choice");
                Console.ResetColor();
                break;
        }

        Console.WriteLine("\n\n");
    }

    public static async Task ViewUserJokes()
    {

    }


    public static async Task EditJoke()
    {

    }

    public static async Task DeleteJoke()
    {

    }

}




