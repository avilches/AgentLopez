namespace AgentLopez.Shared.Models;

public enum ProviderType
{
    Anthropic,
    OpenAI,
    Gemini
}

public class Provider
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public ProviderType Type { get; set; }
    public string ApiKey { get; set; } = string.Empty;
    public List<string> CachedModels { get; set; } = new();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

public class ProvidersConfig
{
    public List<Provider> Providers { get; set; } = new();
}
