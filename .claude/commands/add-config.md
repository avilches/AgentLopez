# Add Configuration Variable

Add a new configuration variable to the AgentLopez application.

## Arguments
- `$ARGUMENTS` - Variable name and target (e.g., "MaxRetries api" or "DebugMode client")

## Instructions

Parse `$ARGUMENTS` to extract:
1. **Variable name** (e.g., `MaxRetries`)
2. **Target**: `api` (ApiSettings) or `client` (ClientSettings)

### For API Settings (`api`):

1. Read `src/AgentLopez.API/ApiSettings.cs` and add the new property with a sensible default
2. Read `src/AgentLopez.API/appsettings.json` and add the new key
3. If environment variable override is needed, update `src/AgentLopez.API/Services/WorkspaceService.cs` or the relevant service

### For Client Settings (`client`):

1. Read `src/AgentLopez.Shared/Models/ClientSettings.cs` and add the new property with a sensible default
2. Read and update `src/AgentLopez.Web/wwwroot/appsettings.json`
3. Read and update `src/AgentLopez.Desktop/wwwroot/appsettings.json`

### After adding:

- Ask if the user wants to inject `IOptions<ApiSettings>` or `IOptions<ClientSettings>` in any specific service/component
- Remind to update CLAUDE.md Configuration Variables table if needed