using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using AgentLopez.Shared.Models;
using AgentLopez.Shared.Services;

namespace AgentLopez.Desktop;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        // Load configuration from embedded appsettings.json
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream("AgentLopez.Desktop.wwwroot.appsettings.json");
        if (stream != null)
        {
            var config = new ConfigurationBuilder()
                .AddJsonStream(stream)
                .Build();
            builder.Configuration.AddConfiguration(config);
        }

        // Register ClientSettings from configuration
        builder.Services.Configure<ClientSettings>(builder.Configuration);

        builder.Services.AddMauiBlazorWebView();

        // Read settings for HttpClient configuration
        var settings = new ClientSettings();
        builder.Configuration.Bind(settings);

        builder.Services.AddHttpClient<IProviderService, ProviderApiService>(client =>
        {
            client.BaseAddress = new Uri(settings.ApiBaseUrl);
            client.DefaultRequestHeaders.Add("X-API-Key", settings.ApiKey);
        });

        builder.Services.AddMudServices();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
