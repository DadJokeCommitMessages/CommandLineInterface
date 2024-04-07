using System;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http;

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
        int userID = User.GetUserID();
        throw new NotImplementedException();
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

