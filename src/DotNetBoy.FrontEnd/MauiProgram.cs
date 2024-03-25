using System.Reflection;
using DotNetBoy.Emulator.Extensions;
using DotNetBoy.FrontEnd.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DotNetBoy.FrontEnd;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        var assembly = Assembly.GetExecutingAssembly();
        using var mainAppSettingsJsonStream = assembly.GetManifestResourceStream("DotNetBoy.FrontEnd.appsettings.json");
        using var appSettingsDevelopmentJsonStream = assembly.GetManifestResourceStream("DotNetBoy.FrontEnd.appsettings.development.json");

        var config = new ConfigurationBuilder()
            .AddJsonStream(mainAppSettingsJsonStream);

        if (appSettingsDevelopmentJsonStream != null)
        {
            config.AddJsonStream(appSettingsDevelopmentJsonStream);
        }

        var configurationRoot = config.Build();
        

        builder.Configuration.AddConfiguration(configurationRoot);
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        builder.Services.AddEmulator();
        builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddSingleton<MainPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}