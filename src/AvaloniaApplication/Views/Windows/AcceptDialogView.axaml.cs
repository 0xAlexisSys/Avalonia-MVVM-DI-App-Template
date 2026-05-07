using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AvaloniaApplication.Views;

public sealed partial class AcceptDialogView : Window
{
    public AcceptDialogView() => InitializeComponent();

    private void AcceptButton_OnClick(object? sender, RoutedEventArgs args) => Close();
}
