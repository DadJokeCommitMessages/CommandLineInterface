using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class ApiHelper
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public ApiHelper(string baseUrl)
    {
        _httpClient = new HttpClient();
        _baseUrl = baseUrl;
        _httpClient.BaseAddress = new Uri(baseUrl);
    }

    public async Task<HttpResponseMessage> PostAsync(string endpoint, string jsonPayload)
    {
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        return await _httpClient.PostAsync(endpoint, content);
    }

    public async Task<HttpResponseMessage> GetAsync(string endpoint)
    {
        return await _httpClient.GetAsync(endpoint);
    }

    public async Task<HttpResponseMessage> PutAsync(string endpoint, string jsonPayload)
    {
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        return await _httpClient.PutAsync(endpoint, content);
    }

    public async Task<HttpResponseMessage> DeleteAsync(string endpoint)
    {
        return await _httpClient.DeleteAsync(endpoint);
    }
}