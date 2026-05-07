using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AvaloniaApplication.Views;

public sealed partial class ConfirmationDialogView : Window
{
    public ConfirmationDialogView() => InitializeComponent();

    private void CancelButton_OnClick(object? sender, RoutedEventArgs args) => Close(false);

    private void ConfirmButton_OnClick(object? sender, RoutedEventArgs args) => Close(true);
}
