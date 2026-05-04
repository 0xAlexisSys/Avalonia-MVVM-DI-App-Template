using Avalonia.Media;

namespace AvaloniaApplication.Models;

public abstract record class DialogOptions(string? Text = null, string? Title = null, TextAlignment TextAlignment = TextAlignment.Center, f64 MinWidth = 0.0D, f64 MinHeight = 0.0D);
public sealed record class AcceptDialogOptions(string? AcceptButtonText = null) : DialogOptions;
public sealed record class ConfirmationDialogOptions(string? CancelButtonText = null, string? ConfirmButtonText = null) : DialogOptions;
