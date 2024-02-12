using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using LoaTool.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace LoaTool.View;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public sealed partial class App : Application
{
    public App()
    {
        Services = IocService;
    }

    /// <summary>
    /// Gets the current <see cref="App"/> instance in use
    /// </summary>
    public new static App Current => (App)Application.Current;

    /// <summary>
    /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
    /// </summary>
    public IServiceProvider Services { get; }

    private static IServiceProvider IocService
    {
        get
        {
            var services = new ServiceCollection();

            services.AddSingleton<MainViewModel>();

            return services.BuildServiceProvider();
        }
    }
}

