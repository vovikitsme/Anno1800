﻿using Anno1800.Database;
using Anno1800.Services;
using Anno1800.Views;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace Anno1800;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
        .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        IServiceCollection services = builder.Services;

        services.AddSingleton<SqliteConnectionFactory>();

        services.AddSingleton<DataService>();

        services.AddSingleton<MainView>();
        services.AddSingleton<MainViewModel>();

        services.AddSingleton<FarmersView>();
        services.AddSingleton<FarmersViewModel>();

        return builder.Build();
    }
}