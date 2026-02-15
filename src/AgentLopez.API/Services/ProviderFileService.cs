using AgentLopez.Shared.Models;
using AgentLopez.Shared.Services;

namespace AgentLopez.API.Services;

public class ProviderFileService : IProviderService
{
    private readonly WorkspaceService _workspaceService;

    public ProviderFileService(WorkspaceService workspaceService)
    {
        _workspaceService = workspaceService;
    }

    public async Task<List<Provider>> GetAllAsync()
    {
        var config = await _workspaceService.LoadProvidersAsync();
        return config.Providers;
    }

    public async Task<Provider?> GetByIdAsync(string id)
    {
        var config = await _workspaceService.LoadProvidersAsync();
        return config.Providers.FirstOrDefault(p => p.Id == id);
    }

    public async Task<Provider> CreateAsync(Provider provider)
    {
        var config = await _workspaceService.LoadProvidersAsync();
        provider.Id = Guid.NewGuid().ToString();
        provider.CreatedAt = DateTime.UtcNow;
        provider.UpdatedAt = DateTime.UtcNow;
        config.Providers.Add(provider);
        await _workspaceService.SaveProvidersAsync(config);
        return provider;
    }

    public async Task<Provider> UpdateAsync(Provider provider)
    {
        var config = await _workspaceService.LoadProvidersAsync();
        var index = config.Providers.FindIndex(p => p.Id == provider.Id);
        if (index == -1)
        {
            throw new KeyNotFoundException($"Provider with id {provider.Id} not found");
        }

        provider.UpdatedAt = DateTime.UtcNow;
        config.Providers[index] = provider;
        await _workspaceService.SaveProvidersAsync(config);
        return provider;
    }

    public async Task DeleteAsync(string id)
    {
        var config = await _workspaceService.LoadProvidersAsync();
        var provider = config.Providers.FirstOrDefault(p => p.Id == id);
        if (provider == null)
        {
            throw new KeyNotFoundException($"Provider with id {id} not found");
        }

        config.Providers.Remove(provider);
        await _workspaceService.SaveProvidersAsync(config);
    }
}
