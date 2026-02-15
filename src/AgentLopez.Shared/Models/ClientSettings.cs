namespace AgentLopez.Shared.Models;

/// <summary>
/// Client settings for Web and Desktop applications.
/// Configured via appsettings.json.
/// </summary>
public class ClientSettings
{
    /// <summary>
    /// Base URL for the API backend.
    /// </summary>
    public string ApiBaseUrl { get; set; } = "http://localhost:5076";

    /// <summary>
    /// API Key for authenticating requests to the backend.
    /// </summary>
    public string ApiKey { get; set; } = "dev-api-key";
}
