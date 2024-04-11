using System.Text.Json;
using System.Collections.Specialized;


class ApiCalls
{
    private static readonly string baseUrl = "http://localhost:5282/api/";
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
                Console.WriteLine("git commit -m '" + joke.story + "'");
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
        string newBaseUrl = "http://localhost:5282/";
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

        Display.WriteLineColoured("\nPlease indicate the ID of the joke you want to edit","DarkGreen");

        Console.Write($"\nEnter your choice (1-{jokeMap.Count}): ");
        string jokeNumberInput = Console.ReadLine();

        JokeResponse? selectedJoke = jokeMap[jokeNumberInput] as JokeResponse;

        string editChoice = Display.GetEditOptions();

        if (editChoice == "2")
        {
            string answer = await askType();
            selectedJoke.jokeType = answer;
            await makePutRequest(selectedJoke);
            return;
        }
        else if (editChoice != "1")
        {
            Display.PrintErrorMessage("Invalid Choice.");
            return;
        }

        if (!int.TryParse(jokeNumberInput, out int jokeNumber) || jokeNumber < 1 || jokeNumber > jokeMap.Count)
        {
            Display.PrintErrorMessage("Invalid Choice");
            return;
        }

        Console.WriteLine($"\nEditing joke #{jokeNumber}: ");

        Display.WriteLineColoured($"\"{selectedJoke.story}\"","Blue");

        Console.Write("\n\nEnter the updated joke : ");
        string? updatedJoke = Console.ReadLine();

        selectedJoke.story = updatedJoke;
        await makePutRequest(selectedJoke);
    }



    public static async Task makePutRequest( JokeResponse joke){

        var jsonPayload = JsonSerializer.Serialize(joke);
        var endpointPutJoke = $"joke/{joke.jokeID}";

        try
        {
            var response = await apiHelper.PutAsync(endpointPutJoke, jsonPayload);

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

                Display.PrintSuccessMessage($"\nJoke with ID {jokeNumberInput} deleted successfully!");
            }
            else
            {
                Display.PrintErrorMessage($"Failed to delete joke with ID {jokeNumberInput}. Status code: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Display.PrintErrorMessage($"An unexpected error occurred: {ex.Message}");
        }
    }


    public static async Task<string> askType()
    {
        Console.WriteLine("What type are you interested in changing the current one to?\n");
        var endpointGetType = $"jokeType/all ";

        string[] colorNames = { "Cyan", "Magenta", "Green", "Blue", "DarkCyan", "DarkMagenta" };

        try
        {
            HttpResponseMessage response = await apiHelper.GetAsync(endpointGetType);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                string[] jokeTypes = JsonSerializer.Deserialize<string[]>(jsonResponse);
                int counter = 0;

                foreach (string type in jokeTypes)
                {
                    string colorName = colorNames[counter % colorNames.Length];
                    Display.SetConsoleColour(colorName);
                    Console.WriteLine((counter + 1) + ". " + type);
                    counter++;
                }

                Display.SetConsoleColour("White");

                string typeChosen = Display.GetType(jokeTypes);
                return typeChosen;
            }
            else
            {
                Display.PrintErrorMessage($"Failed to fetch jokes. Status code: {response.StatusCode}");
                bool confirmation = Display.GetUserConfirmation("\nWould you still like to submit your joke without changing the type?");
                if (confirmation) return "yes";
                else return "no";
            }
        }
        catch (Exception ex)
        {
            Display.PrintErrorMessage("An error occurred: " + ex.Message);
            bool confirmation = Display.GetUserConfirmation("\nWould you still like to submit your joke without changing the type?");
            if (confirmation) return "yes";
            else return "no";
        }
    }
}

