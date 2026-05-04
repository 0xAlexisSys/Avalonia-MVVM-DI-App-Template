using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using AvaloniaApplication.Models;
using AvaloniaApplication.ViewModels;
using AvaloniaApplication.Views;

namespace AvaloniaApplication.Services;

public sealed class DialogService(IClassicDesktopStyleApplicationLifetime lifetime)
{
    private IStorageProvider StorageProvider
    {
        get
        {
            field ??= TopLevel.GetTopLevel(lifetime.MainWindow)!.StorageProvider;
            return field;
        }
    }

    private static T CreateDialogView<T>(DialogViewModel viewModel) where T : Window
    {
        T view = Program.GetService<T>();
        view.DataContext = viewModel;
        return view;
    }

    private static T CreateDialogViewModel<T>(DialogOptions options, string defaultTitle) where T : DialogViewModel, new()
    {
        T viewModel = Program.GetService<T>();
        viewModel.Title = options.Title ?? defaultTitle;
        if (options.Text is not null) viewModel.Text = options.Text;
        viewModel.TextAlignment = options.TextAlignment;
        if (options.MinWidth > 0.0D) viewModel.MinWidth = options.MinWidth;
        if (options.MinHeight > 0.0D) viewModel.MinHeight = options.MinHeight;
        return viewModel;
    }

    public async Task ShowAcceptDialogAsync(AcceptDialogOptions options)
    {
        AcceptDialogViewModel viewModel = CreateDialogViewModel<AcceptDialogViewModel>(options, "Alert!");
        if (options.AcceptButtonText is not null) viewModel.AcceptButtonText = options.AcceptButtonText;

        await CreateDialogView<AcceptDialogView>(viewModel).ShowDialog(lifetime.MainWindow!);
    }

    public async Task<bool> ShowConfirmationDialogAsync(ConfirmationDialogOptions options)
    {
        ConfirmationDialogViewModel viewModel = CreateDialogViewModel<ConfirmationDialogViewModel>(options, "Please Confirm...");
        if (options.CancelButtonText is not null) viewModel.CancelButtonText = options.CancelButtonText;
        if (options.ConfirmButtonText is not null) viewModel.ConfirmButtonText = options.ConfirmButtonText;

        return await CreateDialogView<ConfirmationDialogView>(viewModel).ShowDialog<bool>(lifetime.MainWindow!);
    }

    public async Task<string[]> ShowOpenFileDialogAsync(FilePickerOpenOptions options) => [..(await StorageProvider.OpenFilePickerAsync(options)).Select(static file => file.Path.AbsolutePath)];

    public async Task<string?> ShowSaveFileDialogAsync(FilePickerSaveOptions options) => (await StorageProvider.SaveFilePickerAsync(options))?.Path.AbsolutePath;

    public async Task<string[]> ShowOpenDirectoryDialogAsync(FolderPickerOpenOptions options) => [..(await StorageProvider.OpenFolderPickerAsync(options)).Select(static folder => folder.Path.AbsolutePath)];
}
