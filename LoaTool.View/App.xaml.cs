using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Input;
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

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        var cursorGrabStream = GetResourceStream(new Uri("pack://application:,,,/LoaTool.Define;component/Views/Resources/Cursors/grab.cur"));
        var cursorGrabbingStream = GetResourceStream(new Uri("pack://application:,,,/LoaTool.Define;component/Views/Resources/Cursors/grabbing.cur"));

        if(cursorGrabStream == null || cursorGrabbingStream == null)
        {
            throw new ResourceReferenceKeyNotFoundException();
        }

        var cursorGrab = new Cursor(cursorGrabStream.Stream);
        var cursorGrabbing = new Cursor(cursorGrabbingStream.Stream);
        Resources.Add("CursorGrab", cursorGrab);
        Resources.Add("CursorGrabbing", cursorGrabbing);
    }
}

