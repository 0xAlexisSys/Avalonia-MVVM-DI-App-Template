using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using AvaloniaApplication.Models;
using AvaloniaApplication.Views;

namespace AvaloniaApplication.Services;

public sealed class DialogService(IClassicDesktopStyleApplicationLifetime lifetime)
{
    private readonly Lazy<IStorageProvider> _storageProvider = new(() => TopLevel.GetTopLevel(lifetime.MainWindow)!.StorageProvider);

    private static T CreateDialog<T>(DialogOptions options) where T : Window
    {
        T dialog = Program.GetService<T>();
        if (options.Title is not null) dialog.Title = options.Title;
        if (options.MinWidth > 0.0D) dialog.MinWidth = options.MinWidth;
        if (options.MinHeight > 0.0D) dialog.MinHeight = options.MinHeight;
        TextBlock content = dialog.GetControl<TextBlock>("TextBlock");
        if (options.Text is not null) content.Text = options.Text;
        content.TextAlignment = options.TextAlignment;
        return dialog;
    }

    public async Task ShowAcceptDialogAsync(AcceptDialogOptions options)
    {
        AcceptDialogView dialog = CreateDialog<AcceptDialogView>(options);
        if (options.AcceptButtonText is not null) dialog.AcceptButton.Content = options.AcceptButtonText;

        await dialog.ShowDialog(lifetime.MainWindow!);
    }

    public async Task<bool> ShowConfirmationDialogAsync(ConfirmationDialogOptions options)
    {
        ConfirmationDialogView dialog = CreateDialog<ConfirmationDialogView>(options);
        if (options.CancelButtonText is not null) dialog.CancelButton.Content = options.CancelButtonText;
        if (options.ConfirmButtonText is not null) dialog.ConfirmButton.Content = options.ConfirmButtonText;

        return await dialog.ShowDialog<bool>(lifetime.MainWindow!);
    }

    public async Task<string[]> ShowOpenFileDialogAsync(FilePickerOpenOptions options) => [..(await _storageProvider.Value.OpenFilePickerAsync(options)).Select(static file => file.Path.AbsolutePath)];

    public async Task<string?> ShowSaveFileDialogAsync(FilePickerSaveOptions options) => (await _storageProvider.Value.SaveFilePickerAsync(options))?.Path.AbsolutePath;

    public async Task<string[]> ShowOpenDirectoryDialogAsync(FolderPickerOpenOptions options) => [..(await _storageProvider.Value.OpenFolderPickerAsync(options)).Select(static folder => folder.Path.AbsolutePath)];
}
