using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace AvaloniaApplication.Extensions;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection self)
    {
        public IServiceCollection AddSingletonViewAndViewModel<TView, TViewModel>(bool registerInViewLocator) where TView : Control where TViewModel : ObservableObject
        {
            if (registerInViewLocator) ViewLocator.RegisterView<TView, TViewModel>();
            return self.AddSingleton<TView>()
                       .AddSingleton<TViewModel>();
        }

        public IServiceCollection AddTransientViewAndViewModel<TView, TViewModel>(bool registerInViewLocator) where TView : Control where TViewModel : ObservableObject
        {
            if (registerInViewLocator) ViewLocator.RegisterView<TView, TViewModel>();
            return self.AddTransient<TView>()
                       .AddTransient<TViewModel>();
        }
    }
}
