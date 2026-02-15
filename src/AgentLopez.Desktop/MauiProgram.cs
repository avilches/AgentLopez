using Microsoft.Extensions.Logging;
using MudBlazor.Services;
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

        builder.Services.AddMauiBlazorWebView();

        // Configure API base URL
        var apiBaseUrl = "https://localhost:5001";
        var apiKey = "dev-api-key";

        builder.Services.AddHttpClient<IProviderService, ProviderApiService>(client =>
        {
            client.BaseAddress = new Uri(apiBaseUrl);
            client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
        });

        builder.Services.AddMudServices();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
