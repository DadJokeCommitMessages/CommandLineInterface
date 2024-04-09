using System;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http;
using System.Text.Json.Serialization;

class ApiCalls
{
    private static readonly string baseUrl = "http://localhost:5282/api/";
    private static readonly ApiHelper apiHelper = new ApiHelper(baseUrl);

    public static async Task GetJoke(string type = "")
    {
        string endpoint = "joke?jokeType=" + type;
        var getResponse = await apiHelper.GetAsync(endpoint);

        if (getResponse.IsSuccessStatusCode)
        {
            var responseContent = await getResponse.Content.ReadAsStringAsync();

            var joke = JsonSerializer.Deserialize<JokeResponse>(responseContent);

            if (joke != null)
            {
                Console.WriteLine(joke.story);
            }
            else
            {
                Display.PrintErrorMessage("Oops! Unable to display the joke. The response might be unexpected.");
            }
        }
        else
        {
            Display.PrintErrorMessage($"Failed to retrieve joke. Status code: {getResponse.StatusCode}");
        }
    }




    public static async Task GetUserJokes(string arg)
    {
        string newBaseUrl = "http://localhost:5282/";
        ApiHelper newApiHelper = new ApiHelper(newBaseUrl);
        string endpoint = "jokes";

        try
        {
            HttpResponseMessage response = await newApiHelper.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                List<JokeResponse> jokes = JsonSerializer.Deserialize<List<JokeResponse>>(jsonResponse);

                int maxStoryWidth = jokes.Max(joke => joke.story.Length);

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

                foreach (JokeResponse joke in jokes)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{joke.jokeID,5}");

                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write($" | {joke.story.PadRight(maxStoryWidth)}");

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($" | {joke.jokeType,-8}");

                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine($"Failed to fetch jokes. Status code: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }
    public static async Task AddJoke(string type, string joke)
    {
        string endpoint = "joke";

        JokePostRequest jokeRequest = new JokePostRequest(joke, type);
        string jsonPayload = jokeRequest.ToJsonString();

        try
        {
            HttpResponseMessage response = await apiHelper.PostAsync(endpoint, jsonPayload);

            if (response.IsSuccessStatusCode)
            {
                Display.PrintSuccessMessage("\nJoke submitted successfully. Status: " + response.StatusCode);
            }
            else
            {
                Display.PrintErrorMessage("Error: " + response.ReasonPhrase);
            }
        }
        catch (Exception e)
        {
            Display.PrintErrorMessage("Exception: " + e.Message);
        }
    }

    public static async Task EditJoke(string arg)
    {
        throw new NotImplementedException();
    }

    public static async Task DeleteJoke(string arg)
    {
        throw new NotImplementedException();
    }
}

