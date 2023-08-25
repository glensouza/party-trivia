using Microsoft.AspNetCore.Components.WebView.Maui;
using PartyTriviaMAUI.Data;
using Syncfusion.Blazor;

namespace PartyTriviaMAUI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
#endif

        builder.Services.AddSyncfusionBlazor();

        builder.Services.AddSingleton<WeatherForecastService>();

        return builder.Build();
    }
}