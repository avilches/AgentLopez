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
└── src/
    ├── AgentLopez.Shared/          # Razor Class Library (shared UI)
    │   ├── Components/
    │   ├── Layouts/
    │   ├── Models/
    │   ├── Pages/
    │   └── Services/
    │
    ├── AgentLopez.Web/             # Blazor WebAssembly (port 5191)
    │
    ├── AgentLopez.Desktop/         # MAUI Blazor Hybrid (Windows/macOS)
    │
    └── AgentLopez.API/             # ASP.NET Core Minimal API (port 5076)
```

## Configuration

| Settings Class | Location | Used By |
|----------------|----------|---------|
| `ApiSettings` | `src/AgentLopez.API/ApiSettings.cs` | API backend |
| `ClientSettings` | `src/AgentLopez.Shared/Models/ClientSettings.cs` | Web/Desktop clients |

### Config Files
- API: `src/AgentLopez.API/appsettings.json`
- Web: `src/AgentLopez.Web/wwwroot/appsettings.json`
- Desktop: `src/AgentLopez.Desktop/wwwroot/appsettings.json`

### Environment Variables
- `WORKSPACE` - Path for storing data files (overrides `ApiSettings.Workspace`)
- `ASPNETCORE_URLS` - Override default ports

## Workspace Structure

Configured via `WORKSPACE` environment variable:
```
$WORKSPACE/
├── providers.json
├── agents/*.json
└── sessions/<session_id>/
```

See `src/AgentLopez.Shared/Models/Provider.cs` for provider schema.

## Running

```bash
# API (required first)
cd src/AgentLopez.API && dotnet run

# Web
cd src/AgentLopez.Web && dotnet watch run

# Desktop (macOS)
cd src/AgentLopez.Desktop && dotnet build -t:Run -f net9.0-maccatalyst
```

## API Endpoints

All require `X-API-Key` header. See `src/AgentLopez.API/Program.cs` for full list.

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET/POST/PUT/DELETE | /api/providers | Provider CRUD |
| POST | /api/chat | Chat (placeholder) |

## Development Guidelines

### Adding New Features
1. Models → `AgentLopez.Shared/Models/`
2. Service interface → `AgentLopez.Shared/Services/`
3. API implementation → `AgentLopez.API/Services/`
4. HTTP client → `AgentLopez.Shared/Services/`
5. Endpoints → `AgentLopez.API/Program.cs`
6. UI components → `AgentLopez.Shared/Components/`
7. Pages → `AgentLopez.Shared/Pages/`

### Code Standards
- All comments and user-facing text in English
- Use `spellcheck="false"` on input/textarea fields
- Follow MudBlazor component patterns
- No inline styles - use MudBlazor's built-in classes

## Skills

- `/add-config <VariableName> <api|client>` - Add new configuration variable
