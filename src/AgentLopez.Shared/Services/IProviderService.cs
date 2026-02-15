using AgentLopez.Shared.Models;

namespace AgentLopez.Shared.Services;

public interface IProviderService
{
    Task<List<Provider>> GetAllAsync();
    Task<Provider?> GetByIdAsync(string id);
    Task<Provider> CreateAsync(Provider provider);
    Task<Provider> UpdateAsync(Provider provider);
    Task DeleteAsync(string id);
}
