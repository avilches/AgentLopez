using System.Net.Http.Json;
using AgentLopez.Shared.Models;

namespace AgentLopez.Shared.Services;

public class ProviderApiService : IProviderService
{
    private readonly HttpClient _httpClient;

    public ProviderApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Provider>> GetAllAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<List<Provider>>("api/providers");
        return response ?? new List<Provider>();
    }

    public async Task<Provider?> GetByIdAsync(string id)
    {
        return await _httpClient.GetFromJsonAsync<Provider>($"api/providers/{id}");
    }

    public async Task<Provider> CreateAsync(Provider provider)
    {
        var response = await _httpClient.PostAsJsonAsync("api/providers", provider);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Provider>() ?? provider;
    }

    public async Task<Provider> UpdateAsync(Provider provider)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/providers/{provider.Id}", provider);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Provider>() ?? provider;
    }

    public async Task DeleteAsync(string id)
    {
        var response = await _httpClient.DeleteAsync($"api/providers/{id}");
        response.EnsureSuccessStatusCode();
    }
}
