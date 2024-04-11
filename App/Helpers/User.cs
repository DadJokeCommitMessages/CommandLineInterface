using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using System.Net;
using System.Net.Http.Headers;
class User
{
    public static int userID;
    static string? clientSecret = Environment.GetEnvironmentVariable("CLIENT_SECRET");
    static string? clientID = Environment.GetEnvironmentVariable("CLIENT_ID");
    static string? redirectURL = "http://localhost:8080";
    public static string? accessToken { get; set; }

    static string? firstUri = $"https://accounts.google.com/o/oauth2/auth?response_type=code&client_id={clientID}&scope=openid%20profile%20email&redirect_uri={redirectURL}";
    public static async void  SignIn()
    {
        // User authentication 
        userID = 1; // Example ID
        Display.DisplayPrompt();
        Console.WriteLine($"Click on the link to sign in: \n{firstUri} ");

        HttpListenerRequest authRequest = Server.StartServer([]);
        string authCode;
        if (authRequest.QueryString.HasKeys() && authRequest.QueryString["code"]!=null)
        {
            authCode = authRequest.QueryString["code"];
        }
        else
        {
            Display.PrintErrorMessage("Authentication failed. Please restart the app to try again");
            return;
        }

        string SecondUri = $"https://oauth2.googleapis.com/token?client_id={clientID}&client_secret={clientSecret}&redirect_uri={redirectURL}&grant_type=authorization_code&code={authCode}";
        accessToken = await ApiCalls.Authenticate(SecondUri);
        if (accessToken == null)
        {
            Display.PrintErrorMessage($"Oauth failed - the token got recieved from Google but the server failed to save it");
        }
    }

    public static int GetUserID()
    {
        return userID;
    }

    public static Boolean IsAccessTokenValid()
    {
        if (accessToken == null)
        {
            return false;
        }
        else
        {
            var client = new HttpClient();
            string uri = $"https://www.googleapis.com/oauth2/v1/tokeninfo?access_token={accessToken}";

            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.PostAsync(client.BaseAddress, null).Result;

            if (response.IsSuccessStatusCode)
            {
                return true;

            }
            return false;
        }

    }
}