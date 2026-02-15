using System.Text.Json;
using AgentLopez.Shared.Models;

namespace AgentLopez.API.Services;

public class WorkspaceService
{
    private readonly string _workspacePath;
    private readonly JsonSerializerOptions _jsonOptions;

    public WorkspaceService()
    {
        _workspacePath = Environment.GetEnvironmentVariable("WORKSPACE")
            ?? throw new InvalidOperationException("WORKSPACE environment variable is not set");

        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        EnsureDirectoryStructure();
    }

    private void EnsureDirectoryStructure()
    {
        Directory.CreateDirectory(_workspacePath);
        Directory.CreateDirectory(Path.Combine(_workspacePath, "agents"));
        Directory.CreateDirectory(Path.Combine(_workspacePath, "sessions"));

        var providersPath = GetProvidersPath();
        if (!File.Exists(providersPath))
        {
            var emptyConfig = new ProvidersConfig();
            File.WriteAllText(providersPath, JsonSerializer.Serialize(emptyConfig, _jsonOptions));
        }
    }

    public string GetProvidersPath() => Path.Combine(_workspacePath, "providers.json");
    public string GetAgentsPath() => Path.Combine(_workspacePath, "agents");
    public string GetSessionsPath() => Path.Combine(_workspacePath, "sessions");

    public async Task<ProvidersConfig> LoadProvidersAsync()
    {
        var path = GetProvidersPath();
        if (!File.Exists(path))
        {
            return new ProvidersConfig();
        }

        var json = await File.ReadAllTextAsync(path);
        return JsonSerializer.Deserialize<ProvidersConfig>(json, _jsonOptions) ?? new ProvidersConfig();
    }

    public async Task SaveProvidersAsync(ProvidersConfig config)
    {
        var path = GetProvidersPath();
        var json = JsonSerializer.Serialize(config, _jsonOptions);
        await File.WriteAllTextAsync(path, json);
    }
}
