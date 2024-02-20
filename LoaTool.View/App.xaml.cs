using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.DependencyInjection;
using LoaTool.Define.Interfaces;
using LoaTool.Util;
using LoaTool.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace LoaTool.View;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public sealed partial class App : Application
{
    /// <summary>
    /// Gets the current <see cref="App"/> instance in use
    /// </summary>
    public new static App Current => (App)Application.Current;

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

        RegistServices();
        RegistDialog();
    }

    private void RegistServices()
    {
        var services = new ServiceCollection();

        services.AddSingleton<MainViewModel>();
        services.AddSingleton<ColorPickerViewModel>();
        services.AddSingleton<IDialogService, DialogService>();

        ServiceProvider serviceProvider = services.BuildServiceProvider();

        Ioc.Default.ConfigureServices(serviceProvider);
    }

    private void RegistDialog()
    {
        IDialogService? dialogService = Ioc.Default.GetService<IDialogService>();

        if(dialogService != null)
        {
            dialogService.Register<ColorPickerWindow>();
        }
    }
}

