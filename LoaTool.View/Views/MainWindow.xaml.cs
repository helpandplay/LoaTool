using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using LoaTool.Define.Interfaces;
using LoaTool.Util;
using LoaTool.ViewModel;

namespace LoaTool.View;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, IDialog
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = Ioc.Default.GetService<MainViewModel>();
    }

    public void Positioning(IDialog? parent = null)
    {
        Define.View.WindowLocation windowLocation = ViewUtil.GetWindowLocation(Width);

        Left = windowLocation.Left;
        Top = windowLocation.Top;
    }
}