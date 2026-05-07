using Avalonia.Controls;
using Avalonia.Controls.Templates;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaApplication;

public sealed class ViewLocator : IDataTemplate
{
    private static readonly Dictionary<Type, Type> _registeredViews = [];

    public static void RegisterView<TView, TViewModel>() where TView : Control where TViewModel : ObservableObject => _registeredViews.Add(typeof(TViewModel), typeof(TView));

    public Control Build(object? data)
    {
        const string UnknownViewModelName = "NullViewModel";

        Type? viewModelType = data?.GetType();
        if (viewModelType is null) return new TextBlock {Text = $"No view for {UnknownViewModelName}"};
        Type? viewType = _registeredViews.GetValueOrDefault(viewModelType);
        return viewType is not null ? Program.GetService<Control>(viewType) : new TextBlock {Text = $"No view for {viewModelType.FullName ?? UnknownViewModelName}"};
    }

    public bool Match(object? data) => data is ObservableObject;
}
