namespace AgentLopez.API;

/// <summary>
/// API settings for the backend service.
/// Configured via appsettings.json or environment variables.
/// </summary>
public class ApiSettings
{
    /// <summary>
    /// API Key for authenticating incoming requests.
    /// </summary>
    public string ApiKey { get; set; } = "dev-api-key";

    /// <summary>
    /// Workspace path for storing data (providers, agents, sessions).
    /// Can be overridden with environment variable: WORKSPACE
    /// </summary>
    public string Workspace { get; set; } = "";
}
