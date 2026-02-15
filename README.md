# AgentLopez

A cross-platform AI Agent application built with .NET 9 that connects to ChatGPT, Gemini, and Claude.

## Features

- **Multi-Provider Support**: Connect to OpenAI, Anthropic (Claude), and Google (Gemini) APIs
- **Cross-Platform**: Runs as a web app (Blazor WebAssembly) or desktop app (MAUI)
- **Provider Management**: Add, edit, and delete API providers with cached model lists
- **Modern UI**: Material Design with MudBlazor, dark/light theme support

## Architecture

```
┌─────────────────┐     ┌─────────────────┐
│   Web Client    │     │ Desktop Client  │
│ (Blazor WASM)   │     │    (MAUI)       │
└────────┬────────┘     └────────┬────────┘
         │                       │
         └───────────┬───────────┘
                     │ HTTP/REST
                     ▼
         ┌───────────────────────┐
         │      API Backend      │
         │   (ASP.NET Core)      │
         └───────────┬───────────┘
                     │
                     ▼
         ┌───────────────────────┐
         │   File System         │
         │   (WORKSPACE)         │
         └───────────────────────┘
```

## Quick Start

### Prerequisites

- .NET 9 SDK
- For Desktop: `dotnet workload install maui`

### 1. Start the API

```bash
# Set workspace directory
export WORKSPACE=/path/to/your/workspace

# Start API on default port (5076)
cd src/AgentLopez.API
dotnet run
```

### 2. Start the Web Client

```bash
# Start Web on default port (5191)
cd src/AgentLopez.Web
dotnet run
```

Open http://localhost:5191 in your browser.

## Custom Ports

Override default ports using environment variables or CLI arguments:

### Using Environment Variables

```bash
# Terminal 1: API on port 8080
export WORKSPACE=/path/to/workspace
export ASPNETCORE_URLS=http://localhost:8080
cd src/AgentLopez.API
dotnet run

# Terminal 2: Web on port 3000
export ASPNETCORE_URLS=http://localhost:3000
cd src/AgentLopez.Web
dotnet run
```

### Using CLI Arguments

```bash
# API on port 8080
cd src/AgentLopez.API
dotnet run --urls http://localhost:8080

# Web on port 3000
cd src/AgentLopez.Web
dotnet run --urls http://localhost:3000
```

### Connecting to a Different API Host

If the API runs on a different host/port, update the Web client configuration:

**Edit `src/AgentLopez.Web/wwwroot/appsettings.json`:**
```json
{
  "ApiBaseUrl": "http://your-api-host:8080",
  "ApiKey": "your-api-key"
}
```

## Configuration

### API Configuration

| Variable | Default | Environment Variable | Description |
|----------|---------|---------------------|-------------|
| Port | 5076 | `ASPNETCORE_URLS` | API server port |
| Workspace | - | `WORKSPACE` | Data storage path (required) |
| ApiKey | `dev-api-key` | `ApiKey` | Authentication key |

### Web Configuration

| Variable | Default | Config File | Description |
|----------|---------|-------------|-------------|
| Port | 5191 | `ASPNETCORE_URLS` env | Web server port |
| ApiBaseUrl | `http://localhost:5076` | `wwwroot/appsettings.json` | Backend API URL |
| ApiKey | `dev-api-key` | `wwwroot/appsettings.json` | API authentication key |

## Project Structure

| Project | Description |
|---------|-------------|
| `AgentLopez.API` | ASP.NET Core Minimal API backend |
| `AgentLopez.Web` | Blazor WebAssembly client |
| `AgentLopez.Desktop` | MAUI Blazor Hybrid client |
| `AgentLopez.Shared` | Shared components, models, and services |

## Development

```bash
# Hot reload for Web development
cd src/AgentLopez.Web
dotnet watch run

# Build Desktop (macOS)
cd src/AgentLopez.Desktop
dotnet build -t:Run -f net9.0-maccatalyst
```

## API Endpoints

All endpoints require `X-API-Key` header.

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/providers` | List all providers |
| GET | `/api/providers/{id}` | Get provider by ID |
| POST | `/api/providers` | Create provider |
| PUT | `/api/providers/{id}` | Update provider |
| DELETE | `/api/providers/{id}` | Delete provider |

## License

MIT
