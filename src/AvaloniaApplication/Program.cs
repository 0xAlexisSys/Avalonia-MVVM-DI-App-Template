using System.Diagnostics.CodeAnalysis;
using Avalonia;
using AvaloniaApplication.Services;
using AvaloniaApplication.ViewModels;
using AvaloniaApplication.Views;
using AvaloniaApplication.Extensions;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace AvaloniaApplication;

[SuppressMessage("Style", "IDE0053")]
public static class Program
{
    private static ServiceProvider? _services;

    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp().StartWithClassicDesktopLifetime(args, static lifetime =>
    {
        _services = new ServiceCollection().AddSingleton(lifetime)
                                           .AddSingleton(WeakReferenceMessenger.Default)
                                           .AddSingleton(StrongReferenceMessenger.Default)
                                           .AddSingleton<DialogService>()
                                           .AddSingletonViewAndViewModel<MainWindowView, MainWindowViewModel>(false)
                                           .AddTransient<AcceptDialogView>()
                                           .AddTransient<ConfirmationDialogView>()
                                           .BuildServiceProvider();
    });

    public static T GetService<T>() where T : class => _services!.GetRequiredService<T>();

    public static T GetService<T>(Type type) where T : class => (T)_services!.GetRequiredService(type);

    private static AppBuilder BuildAvaloniaApp() => AppBuilder.Configure<App>()
                                                              .UsePlatformDetect()
                                                              #if DEBUG
                                                              .WithDeveloperTools()
                                                              #endif
                                                              .WithInterFont()
                                                              .LogToTrace();
}
