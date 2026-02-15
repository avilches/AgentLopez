using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using AgentLopez.Web;
using AgentLopez.Shared.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure API base URL (can be overridden in appsettings.json)
var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "https://localhost:5001";
var apiKey = builder.Configuration["ApiKey"] ?? "dev-api-key";

builder.Services.AddHttpClient<IProviderService, ProviderApiService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
    client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
});

builder.Services.AddMudServices();

await builder.Build().RunAsync();
