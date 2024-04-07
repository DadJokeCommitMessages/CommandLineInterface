using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

class ApiCalls
{
    //This is just a test call to some api (will be deleted)
    public static async Task GetRandomJoke()
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://official-joke-api.appspot.com/random_joke");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                var jokeData = JsonSerializer.Deserialize<Joke>(responseBody);
                string setup = jokeData?.setup ?? "No setup found";
                string punchline = jokeData?.punchline ?? "No punchline found";

                Console.WriteLine("Here's a joke for you:");
                Console.WriteLine($"Setup: {setup}");
                Console.WriteLine($"Punchline: {punchline}");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"An error occurred while fetching a joke: {e.Message}");
            }
        }
    }

    public class Joke
    {
        public string? setup { get; set; }
        public string? punchline { get; set; }
    }

    public static async Task GetJoke(string type = "")
    {

    }

    public static async Task GetUserJokes(string type)
    {

    }

    public static async Task AddJoke(string type)
    {

    }

    public static async Task EditJoke(string type)
    {

    }

    public static async Task DeleteJoke(string type)
    {

    }

    public static Dictionary<string, Func<string, Task>> JokeMethods = new Dictionary<string, Func<string, Task>>
    {
        { "get-joke", GetJoke },
        { "add-joke", AddJoke }
    };

    public static Dictionary<string, Func<string, Task>> UserJokeMethods = new Dictionary<string, Func<string, Task>>
    {
        { "get-user-jokes", GetUserJokes },
        { "edit-joke", EditJoke },
        { "delete-joke", DeleteJoke }
    };

}