class Command
{

    public static Dictionary<string, Func<Task>> JokeMethods = new Dictionary<string, Func<Task>>()
    {
        { "get-joke", GetJoke },
        { "add-joke", AddJoke }
    };

    public static Dictionary<string, Func<Task>> UserJokeMethods = new Dictionary<string, Func<Task>>()
    {
        { "get-my-jokes", ViewUserJokes },
        { "edit-joke", EditJoke },
        { "delete-joke", DeleteJoke }
    };

    private static readonly Dictionary<string, Func<Task>> GetJokeActions = new Dictionary<string, Func<Task>>
    {
        ["0"] = async () => await ApiCalls.GetJoke(),
        ["1"] = async () => await ApiCalls.GetJoke("fix"),
        ["2"] = async () => await ApiCalls.GetJoke("feature"),
        ["3"] = async () => await ApiCalls.GetJoke("refactor"),
        ["4"] = async () => await ApiCalls.GetJoke("style"),
        ["5"] = async () => await ApiCalls.GetJoke("docs"),
        ["6"] = async () => await ApiCalls.GetJoke("test"),
        ["7"] = async () => await ApiCalls.GetJoke("chore")
    };

    private static readonly Dictionary<string, Func<Task>> AddJokeActions = new Dictionary<string, Func<Task>>
    {

        ["1"] = async () =>
        {
            Console.WriteLine("Please enter the fix joke:");
            string joke = Console.ReadLine();
            await ApiCalls.AddJoke("fix", joke);
        },
        ["2"] = async () =>
        {
            Console.WriteLine("Please enter the feature joke:");
            string joke = Console.ReadLine();
            await ApiCalls.AddJoke("feature", joke);
        },
        ["3"] = async () =>
        {
            Console.WriteLine("Please enter the refactor joke:");
            string joke = Console.ReadLine();
            await ApiCalls.AddJoke("refactor", joke);
        },
        ["4"] = async () =>
        {
            Console.WriteLine("Please enter the style joke:");
            string joke = Console.ReadLine();
            await ApiCalls.AddJoke("style", joke);
        },

        ["5"] = async () =>
        {
            Console.WriteLine("Please enter the documentation joke:");
            string joke = Console.ReadLine();
            await ApiCalls.AddJoke("docs", joke);
        },


        ["6"] = async () =>
        {
            Console.WriteLine("Please enter the test joke:");
            string joke = Console.ReadLine();
            await ApiCalls.AddJoke("test", joke);
        },
        ["7"] = async () =>
        {
            Console.WriteLine("Please enter the chore joke:");
            string joke = Console.ReadLine();
            await ApiCalls.AddJoke("chore", joke);
        }
    };


    public static async Task GetJoke()
    {
        string choice = Console.ReadLine();
        Console.WriteLine();
        if (GetJokeActions.TryGetValue(choice, out var action))
        {
            await action();
        }
        else
        {
            Display.PrintErrorMessage("Invalid choice.");
        }
    }

    public static async Task AddJoke()
    {
        string submission = Console.ReadLine();
        if (AddJokeActions.TryGetValue(submission, out var action))
        {
            await action();
        }
        else
        {
            Display.PrintErrorMessage("Invalid choice");
        }
    }

    public static async Task ViewUserJokes()
    {
        try
        {
            await ApiCalls.GetUserJokes();
        }
        catch (Exception ex)
        {
            Display.PrintErrorMessage("An error occurred while trying to get your jokes: " + ex.Message);
        }
    }

    public static async Task EditJoke()
    {
        try
        {
            await ApiCalls.EditJoke();
        }
        catch (Exception ex)
        {
            Display.PrintErrorMessage("An error occurred while trying to edit your joke: " + ex.Message);
        }
    }

    public static async Task DeleteJoke()
    { 
        try
        {
            await ApiCalls.DeleteJoke();
        }
        catch (Exception ex)
        {
            Display.PrintErrorMessage("An error occurred while trying to delete your joke: " + ex.Message);
        }
    }
}