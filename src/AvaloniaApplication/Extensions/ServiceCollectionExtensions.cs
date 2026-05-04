using Avalonia.Controls;
using AvaloniaApplication.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace AvaloniaApplication.Extensions;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection self)
    {
        public IServiceCollection AddSingletonViewAndViewModel<TView, TViewModel>() where TView : Control where TViewModel : ViewModel => self.AddSingleton<TView>()
                                                                                                                                              .AddSingleton<TViewModel>();

        public IServiceCollection AddTransientViewAndViewModel<TView, TViewModel>(bool registerInViewLocator) where TView : Control where TViewModel : ViewModel
        {
            if (registerInViewLocator) ViewLocator.RegisterView<TView, TViewModel>();
            return self.AddTransient<TView>()
                       .AddTransient<TViewModel>();
        }
    }
}
