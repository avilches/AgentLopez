using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using AgentLopez.Web;
using AgentLopez.Shared.Models;
using AgentLopez.Shared.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Register ClientSettings from configuration (wwwroot/appsettings.json)
builder.Services.Configure<ClientSettings>(builder.Configuration);

// Read settings for HttpClient configuration
var settings = new ClientSettings();
builder.Configuration.Bind(settings);

builder.Services.AddHttpClient<IProviderService, ProviderApiService>(client =>
{
    client.BaseAddress = new Uri(settings.ApiBaseUrl);
    client.DefaultRequestHeaders.Add("X-API-Key", settings.ApiKey);
});

builder.Services.AddMudServices();

await builder.Build().RunAsync();
