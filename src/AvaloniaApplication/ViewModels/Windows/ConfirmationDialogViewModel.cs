using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaApplication.ViewModels;

public sealed partial class ConfirmationDialogViewModel : DialogViewModel
{
    [ObservableProperty] public partial string CancelButtonText { get; set; } = "Cancel";
    [ObservableProperty] public partial string ConfirmButtonText { get; set; } = "OK";

    [RelayCommand]
    private static void Cancel(Window window) => window.Close(false);

    [RelayCommand]
    private static void Confirm(Window window) => window.Close(true);
}
