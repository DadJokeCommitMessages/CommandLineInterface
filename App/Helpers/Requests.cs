using System.Text;

public class ApiHelper
{
    private readonly HttpClient httpClient;
    private readonly string baseUrl;

    public ApiHelper(string baseUrl)
    {
        this.httpClient = new HttpClient();
        this.baseUrl = baseUrl;
        this.httpClient.BaseAddress = new Uri(baseUrl);
    }

    public async Task<HttpResponseMessage> PostAsync(string endpoint, string jsonPayload)
    {
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        return await this.httpClient.PostAsync(endpoint, content);
    }

    public async Task<HttpResponseMessage> GetAsync(string endpoint)
    {
        return await this.httpClient.GetAsync(endpoint);
    }

    public async Task<HttpResponseMessage> PutAsync(string endpoint, string jsonPayload)
    {
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        return await this.httpClient.PutAsync(endpoint, content);
    }

    public async Task<HttpResponseMessage> DeleteAsync(string endpoint)
    {
        return await this.httpClient.DeleteAsync(endpoint);
    }
}