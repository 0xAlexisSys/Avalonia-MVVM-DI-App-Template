using Avalonia.Controls;
using AvaloniaApplication.ViewModels;

namespace AvaloniaApplication.Views;

public sealed partial class MainWindowView : Window
{
    public MainWindowView() => InitializeComponent();

    public MainWindowView(MainWindowViewModel viewModel) : this() => DataContext = viewModel;
}
