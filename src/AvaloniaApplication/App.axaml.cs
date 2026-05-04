using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaApplication.Views;

namespace AvaloniaApplication;

public sealed class App : Application
{
    public override void Initialize() => AvaloniaXamlLoader.Load(this);

    public override void OnFrameworkInitializationCompleted()
    {
        IClassicDesktopStyleApplicationLifetime lifetime = Program.GetService<IClassicDesktopStyleApplicationLifetime>();
        lifetime.MainWindow = Program.GetService<MainWindowView>();

        base.OnFrameworkInitializationCompleted();
    }
}
