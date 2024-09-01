using MauiApp1.Scripts.Connection;
using MauiApp1.Scripts.Creation;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;
using System.Reflection;
using System.Text.Json;
using CommunityToolkit.Maui;
using MauiApp1.Model;
using MauiApp1.ViewModel.Forms;
using MauiApp1.View.FormPages;
using MauiApp1.View;
using MauiApp1.Services;
using MauiApp1.View.StatusPages;
using MauiApp1.ViewModel.Status;

namespace MauiApp1;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        var assembly = Assembly.GetExecutingAssembly();
        Console.WriteLine($"{assembly.GetName().Name}.appsettings.json");
        using var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.appsettings.json");

        var apiSettings = JsonSerializer.Deserialize<SettingsRoot>(stream);

        builder
            .UseMauiApp(serviceProvider => new App(
                new ApiConnection(apiSettings.ApiSettings),
                new AlertDataSender(apiSettings.ApiSettings),
                new PagePooler(),
                new AlertDataToSend()
            ))
            .UseMauiCommunityToolkit()
            .UseSkiaSharp(true)
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton<IDialogService, DialogService>();
        builder.Services.AddSingleton<ILoadingPopupService, LoadingPopupService>();
        builder.Services.AddSingleton<IFormBackgroundTaskObserver, FormBackgroundTaskObserver>();
        builder.Services.AddSingleton<IAlertDataSender>(new AlertDataSender(apiSettings.ApiSettings));

        builder.Services.AddSingleton<AlertDataToSend>();

        builder.Services.AddSingleton<FormPhotoViewModel>();
        builder.Services.AddSingleton<PhotoPage>();

        builder.Services.AddSingleton<FormCategoryViewModel>();
        builder.Services.AddSingleton<CategoryPage>();

        builder.Services.AddSingleton<FormLocalizationViewModel>();
        builder.Services.AddSingleton<LocalizationPage>();

        builder.Services.AddSingleton<FormDescriptionViewModel>();
        builder.Services.AddSingleton<DescriptionPage>();

        builder.Services.AddTransient<ReportSendLoadingViewModel>();
        builder.Services.AddTransient<ReportSendLoadingPage>();

        builder.Services.AddTransient<SendingResultViewModel>();
        builder.Services.AddTransient<SendingResultPage>();

        builder.Services.AddSingleton<MainPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
