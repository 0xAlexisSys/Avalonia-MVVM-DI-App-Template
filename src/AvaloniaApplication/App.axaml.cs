using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaApplication.Attributes;
using AvaloniaApplication.Views;

namespace AvaloniaApplication;

public sealed partial class App : Application
{
    [Service] private static partial IClassicDesktopStyleApplicationLifetime Lifetime { get; }
    [Service] private static partial MainWindowView MainWindowView { get; }

    public override void Initialize() => AvaloniaXamlLoader.Load(this);

    public override void OnFrameworkInitializationCompleted()
    {
        Lifetime.MainWindow = MainWindowView;
        base.OnFrameworkInitializationCompleted();
    }
}
