using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaApplication.ViewModels;

public abstract partial class DialogViewModel : ViewModel
{
    [ObservableProperty] public partial string Title { get; set; } = string.Empty;
    [ObservableProperty] public partial string Text { get; set; } = string.Empty;
    [ObservableProperty] public partial TextAlignment TextAlignment { get; set; }
    [ObservableProperty] public partial f64 MinWidth { get; set; } = 200.0D;
    [ObservableProperty] public partial f64 MinHeight { get; set; } = 100.0D;
}
