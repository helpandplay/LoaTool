using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using LoaTool.Define.Enums;
using LoaTool.Define.Interfaces;
using LoaTool.Service;
using LoaTool.Util;
using LoaTool.View.Views;
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
        RegistServices();
        RegistResources();
        RegistDialog();

        IDialogService? dialogService = Ioc.Default.GetService<IDialogService>();
        MainViewModel? mainViewModel = Ioc.Default.GetService<MainViewModel>();
        if(dialogService != null &&
            mainViewModel != null)
        {
            dialogService.Show(mainViewModel);
        }
    }

    private void RegistResources()
    {
        IResourceService? resourceService = Ioc.Default.GetService<IResourceService>();

        string cursorGrabResourcePath = "pack://application:,,,/LoaTool.Define;component/Views/Resources/Cursors/grab.cur";
        string cursorGrabbingResourcePath = "pack://application:,,,/LoaTool.Define;component/Views/Resources/Cursors/grabbing.cur";
        var cursorGrabStream = GetResourceStream(new Uri(cursorGrabResourcePath));
        var cursorGrabbingStream = GetResourceStream(new Uri(cursorGrabbingResourcePath));

        if(cursorGrabStream == null || cursorGrabbingStream == null)
        {
            throw new ResourceReferenceKeyNotFoundException();
        }

        var cursorGrab = new System.Windows.Input.Cursor(cursorGrabStream.Stream);
        var cursorGrabbing = new System.Windows.Input.Cursor(cursorGrabbingStream.Stream);
        Resources.Add("CursorGrab", cursorGrab);
        Resources.Add("CursorGrabbing", cursorGrabbing);

        //NOTE: enum은 Enum을 상속하지 않음. 따라서 타입을 인터페이스로 통일하고 클래스로 타입구별해야함. IResource -> Image, Cursor, Audio ...
        var cursorGrabResource = new Define.Resources.Cursor(cursorGrabResourcePath, CustomCursors.Grab);
        var cursorGrabbingResource = new Define.Resources.Cursor(cursorGrabbingResourcePath, CustomCursors.Grabbing);

        if(resourceService != null)
        {
            resourceService.AddResource(cursorGrabResource);
            resourceService.AddResource(cursorGrabbingResource);
        }
    }

    private static void RegistServices()
    {
        var services = new ServiceCollection();

        //ViewModel
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<ColorPickerViewModel>();
        services.AddSingleton<AuctionViewModel>();

        //Util
        services.AddSingleton<ColorExtractor>();

        //Service
        services.AddSingleton<IDialogService, DialogService>();
        services.AddSingleton<IResourceService, ResourceService>();
        services.AddSingleton<AuctionService>();
        services.AddSingleton<LostarkService>();

        //Build
        ServiceProvider serviceProvider = services.BuildServiceProvider();

        Ioc.Default.ConfigureServices(serviceProvider);
    }

    private static void RegistDialog()
    {
        IDialogService? dialogService = Ioc.Default.GetService<IDialogService>();

        if(dialogService != null)
        {
            dialogService.Register<MainWindow>();
            dialogService.Register<ColorPickerWindow>();
            dialogService.Register<AuctionWindow>();
        }
    }
}

