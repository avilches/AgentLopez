# AgentLopez - Multi-Platform AI Agent Application

A cross-platform .NET 9 solution for an AI Agent that connects to ChatGPT, Gemini, and Claude.

## Version Information

- .NET: 9.0
- MudBlazor: 8.15.0
- MAUI: net9.0 (Windows/macOS)

## Project Structure

```
AgentLopez/
├── AgentLopez.sln
├── CLAUDE.md
└── src/
    ├── AgentLopez.Shared/          # Razor Class Library (shared UI components)
    │   ├── Components/              # Reusable Blazor components
    │   │   ├── ProviderEditor.razor
    │   │   └── ProviderDetails.razor
    │   ├── Layouts/
    │   │   └── MainLayout.razor     # Main dashboard layout with sidebar
    │   ├── Models/
    │   │   ├── Provider.cs          # Provider model (Anthropic/OpenAI/Gemini)
    │   │   ├── Agent.cs             # Agent configuration model
    │   │   └── Session.cs           # Session and ChatMessage models
    │   ├── Pages/
    │   │   ├── HomePage.razor       # Landing page
    │   │   └── SettingsPage.razor   # Settings with provider management
    │   └── Services/
    │       ├── IProviderService.cs  # Provider service interface
    │       └── ProviderApiService.cs # HTTP client implementation
    │
    ├── AgentLopez.Web/             # Blazor WebAssembly (browser client)
    │   ├── App.razor
    │   ├── Program.cs               # DI configuration
    │   └── wwwroot/
    │       ├── index.html           # MudBlazor CSS/JS included
    │       └── appsettings.json     # API URL and key configuration
    │
    ├── AgentLopez.Desktop/         # MAUI Blazor Hybrid (Windows/macOS)
    │   ├── MauiProgram.cs           # DI configuration
    │   ├── Components/
    │   │   └── Routes.razor
    │   └── wwwroot/
    │       └── index.html
    │
    └── AgentLopez.API/             # ASP.NET Core Minimal API (backend)
        ├── Program.cs               # Endpoints and middleware
        ├── Services/
        │   ├── WorkspaceService.cs  # Filesystem access (WORKSPACE env var)
        │   └── ProviderFileService.cs # Provider CRUD operations
        └── appsettings.json         # API key configuration
```

## Workspace Configuration

The API uses a filesystem-based storage configured via the `WORKSPACE` environment variable:

```
$WORKSPACE/
├── providers.json           # API keys for all providers
├── agents/
│   ├── agent1.json
│   └── agent2.json
└── sessions/
    └── <session_id>/
        ├── config.json
        └── messages.jsonl
```

### providers.json Structure
```json
{
  "providers": [
    {
      "id": "uuid",
      "name": "My OpenAI",
      "type": "OpenAI",       // "Anthropic", "OpenAI", "Gemini"
      "apiKey": "sk-...",
      "cachedModels": ["gpt-4", "gpt-3.5-turbo"],
      "createdAt": "2024-01-01T00:00:00Z",
      "updatedAt": "2024-01-01T00:00:00Z"
    }
  ]
}
```

## Running the Application

### Prerequisites
1. Set the WORKSPACE environment variable:
   ```bash
   export WORKSPACE=/path/to/workspace
   ```

2. Start the API (required for both Web and Desktop):
   ```bash
   cd src/AgentLopez.API
   dotnet run
   ```
   API runs on http://localhost:5076 by default.

### Running Web Client
```bash
cd src/AgentLopez.Web
dotnet run
```

With hot reload:
```bash
dotnet watch run
```

### Debugging Web Client in Rider

The application runs on any modern browser (Safari, Chrome, Firefox, Edge). However, **debugging with breakpoints in Rider** requires a Chromium-based browser (Chrome, Edge) because Rider uses the Chrome DevTools Protocol.

To configure debugging in Rider:
1. Go to **Run → Edit Configurations**
2. Select the "http" profile for AgentLopez.Web
3. In the **Browser** field, select Chrome or Edge (not "Default")
4. If no Chromium browser appears, add it in **Settings → Tools → Web Browsers and Preview**:
   - macOS Chrome path: `/Applications/Google Chrome.app/Contents/MacOS/Google Chrome`

Note: `launchBrowser` is set to `false` in `launchSettings.json` to avoid the "Default system browser not supported" error. Open the browser manually at `http://localhost:5191`.

### Running Desktop (MAUI)
```bash
cd src/AgentLopez.Desktop
dotnet build -t:Run -f net9.0-maccatalyst
```
Note: Desktop requires MAUI workload installed (`dotnet workload install maui`).

## API Endpoints

All endpoints require `X-API-Key` header (default: `dev-api-key`).

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/providers | List all providers |
| GET | /api/providers/{id} | Get provider by ID |
| POST | /api/providers | Create new provider |
| PUT | /api/providers/{id} | Update provider |
| DELETE | /api/providers/{id} | Delete provider |
| POST | /api/chat | Chat endpoint (placeholder) |

## UI Guidelines

- Using MudBlazor for all UI components (Material Design)
- Dashboard layout with collapsible sidebar
- Dark/Light theme toggle in header
- Settings page with tree navigation for providers

## Development Notes

### Adding New Features
1. Add models to `AgentLopez.Shared/Models/`
2. Add service interface to `AgentLopez.Shared/Services/`
3. Add API implementation to `AgentLopez.API/Services/`
4. Add HTTP client implementation to `AgentLopez.Shared/Services/`
5. Add endpoints to `AgentLopez.API/Program.cs`
6. Create UI components in `AgentLopez.Shared/Components/`
7. Create pages in `AgentLopez.Shared/Pages/`

### Code Standards
- All comments in English
- All user-facing text in English
- Use `spellcheck="false"` on input/textarea fields
- Follow MudBlazor component patterns
- No inline styles - use MudBlazor's built-in classes

### Future Implementation
- [ ] Agent CRUD operations
- [ ] Session management
- [ ] Chat interface with streaming
- [ ] Model selection per provider
- [ ] System prompt editor
