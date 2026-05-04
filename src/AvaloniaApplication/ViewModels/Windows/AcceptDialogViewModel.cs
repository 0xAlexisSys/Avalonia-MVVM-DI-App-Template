using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaApplication.ViewModels;

public sealed partial class AcceptDialogViewModel : DialogViewModel
{
    [ObservableProperty] public partial string AcceptButtonText { get; set; } = "OK";

    public AcceptDialogViewModel() => Console.WriteLine("Ready");

    [RelayCommand]
    private static void Accept(Window window) => window.Close();
}
