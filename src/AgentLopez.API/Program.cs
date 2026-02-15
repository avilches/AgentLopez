using Microsoft.Extensions.Options;
using AgentLopez.API;
using AgentLopez.API.Services;
using AgentLopez.Shared.Models;
using AgentLopez.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// Register ApiSettings from configuration (appsettings.json + environment variables)
builder.Services.Configure<ApiSettings>(builder.Configuration);

builder.Services.AddOpenApi();
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("*");
    });
});

builder.Services.AddSingleton<WorkspaceService>();
builder.Services.AddSingleton<IProviderService, ProviderFileService>();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
}

app.UseCors("AllowAll");

// API Key middleware - reads from ApiSettings
var settings = app.Services.GetRequiredService<IOptions<ApiSettings>>().Value;
app.Use(async (context, next) => {
    // Skip API key check for CORS preflight requests
    if (context.Request.Method == "OPTIONS") {
        await next();
        return;
    }

    if (context.Request.Path.StartsWithSegments("/api")) {
        if (!context.Request.Headers.TryGetValue("X-API-Key", out var extractedApiKey) ||
            extractedApiKey != settings.ApiKey) {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid API Key");
            return;
        }
    }
    await next();
});

// Provider endpoints
var providerGroup = app.MapGroup("/api/providers");

providerGroup.MapGet("/", async (IProviderService service) => {
    var providers = await service.GetAllAsync();
    return Results.Ok(providers);
});

providerGroup.MapGet("/{id}", async (string id, IProviderService service) => {
    var provider = await service.GetByIdAsync(id);
    return provider is not null ? Results.Ok(provider) : Results.NotFound();
});

providerGroup.MapPost("/", async (Provider provider, IProviderService service) => {
    var created = await service.CreateAsync(provider);
    return Results.Created($"/api/providers/{created.Id}", created);
});

providerGroup.MapPut("/{id}", async (string id, Provider provider, IProviderService service) => {
    provider.Id = id;
    var updated = await service.UpdateAsync(provider);
    return Results.Ok(updated);
});

providerGroup.MapDelete("/{id}", async (string id, IProviderService service) => {
    await service.DeleteAsync(id);
    return Results.NoContent();
});

// Chat endpoint (placeholder for future implementation)
app.MapPost("/api/chat", () => { return Results.Ok(new { message = "Chat endpoint - not yet implemented" }); });

app.Run();
