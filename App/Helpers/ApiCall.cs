using System.Text.Json;
using System.Collections.Specialized;
using Microsoft.AspNetCore.Authentication.BearerToken;
using System.Net.Http.Json;


class ApiCalls
{
    private static readonly string baseUrl = "http://ec2-3-250-229-22.eu-west-1.compute.amazonaws.com:5282/api/";
    private static readonly ApiHelper apiHelper = new ApiHelper(baseUrl);

    private static OrderedDictionary jokeMap = new OrderedDictionary();

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
                Console.WriteLine("git commit -m '"+joke.story+"'");
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




    public static async Task GetUserJokes()
    {
        string newBaseUrl = "http://ec2-3-250-229-22.eu-west-1.compute.amazonaws.com:5282/api/";
        ApiHelper newApiHelper = new ApiHelper(newBaseUrl);
        string endpoint = "jokes";
        jokeMap = new OrderedDictionary();

        try
        {
            HttpResponseMessage response = await newApiHelper.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                List<JokeResponse> jokes = JsonSerializer.Deserialize<List<JokeResponse>>(jsonResponse);

                string counter = "1";
                foreach (JokeResponse joke in jokes)
                {
                    jokeMap.Add(counter, joke);
                    counter = (int.Parse(counter) + 1).ToString();
                }


                Display.DisplayTable(jokeMap);
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
        catch (Exception ex)
        {
            Display.PrintErrorMessage($"An unexpected error occurred: {ex.Message}");
        }
    }

    public static async Task EditJoke()
    {
        await GetUserJokes();

        Display.SetConsoleColor("DarkGreen");
        Console.WriteLine("\nPlease indicate the ID of the joke you want to edit");
        Display.SetConsoleColor("White");

        Console.Write($"\nEnter your choice (1-{jokeMap.Count}): ");
        string jokeNumberInput = Console.ReadLine();

        if (!int.TryParse(jokeNumberInput, out int jokeNumber) || jokeNumber < 1 || jokeNumber > jokeMap.Count)
        {
            Display.PrintErrorMessage("Invalid Choice");
            return;
        }

        JokeResponse? selectedJoke = jokeMap[jokeNumberInput] as JokeResponse;


        Console.WriteLine($"\nEditing joke #{jokeNumber}: ");

        Display.SetConsoleColor("Blue");
        Console.Write($"\"{selectedJoke.story}\"");
        Display.SetConsoleColor("white");


        Console.Write("\n\nEnter the updated joke : ");
        string updatedJoke = Console.ReadLine();

        selectedJoke.story = updatedJoke;

        var jsonPayload = JsonSerializer.Serialize(selectedJoke);

        var endpoint = $"joke/{selectedJoke.jokeID}";
        try
        {
            var response = await apiHelper.PutAsync(endpoint, jsonPayload);

            if (response.IsSuccessStatusCode)
            {
                Display.PrintSuccessMessage("Joke updated successfully!");
            }
            else
            {
                Display.PrintErrorMessage($"Failed to update joke. Status code: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Display.PrintErrorMessage($"An unexpected error occurred: {ex.Message}");
        }
    }

    public static async Task DeleteJoke()
    {
        await GetUserJokes();

        Console.WriteLine("\nPlease indicate the ID of the joke you want to delete");
        Console.Write($"\nEnter your choice (1-{jokeMap.Count}): ");
        string jokeNumberInput = Console.ReadLine();

        if (!int.TryParse(jokeNumberInput, out int jokeNumber) || jokeNumber < 1 || jokeNumber > jokeMap.Count)
        {
            Display.PrintErrorMessage("Invalid Choice");
            return;
        }

        JokeResponse? selectedJoke = jokeMap[jokeNumberInput] as JokeResponse;

        Console.WriteLine($"\nDeleting joke #{jokeNumber}: \"{selectedJoke.story}\"");

        bool confirmation = Display.GetUserConfirmation("\nWould you like to confirm deletion?");

        if (!confirmation) return;

        var endpoint = $"joke/{selectedJoke.jokeID}";
        try
        {
            var response = await apiHelper.DeleteAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {

                Display.PrintSuccessMessage($"\nJoke with ID {selectedJoke.jokeID} deleted successfully!");
            }
            else
            {
                Display.PrintErrorMessage($"Failed to delete joke with ID {selectedJoke.jokeID}. Status code: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Display.PrintErrorMessage($"An unexpected error occurred: {ex.Message}");
        }
    }

    public static async Task<string?> Authenticate(string newBaseUrl)
    {
        ApiHelper newApiHelper = new ApiHelper(newBaseUrl);
        var endpoint = $"";
        try
        {
            var response = await newApiHelper.PostAsync(endpoint, "");

            if (response.IsSuccessStatusCode)
            {
                var dataObjects = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
                
                if (dataObjects !=null && dataObjects["access_token"] != null)
                {
                    Display.PrintSuccessMessage($"Successfully signed in!");
                    return dataObjects["access_token"].ToString();
                    
                }
                else
                {
                    Display.PrintSuccessMessage($"Oauth failed - the token got recieved from Google but the server failed to save it");
                    return null;
                }
                    
            }
            else
            {
                Display.PrintErrorMessage($"Failed to authenticate. Status code: {response.StatusCode}");
                return null;
            }
        }
        catch (Exception ex)
        {
            Display.PrintErrorMessage($"An unexpected error occurred: {ex.Message}");
            return null;
        }
    }
}

