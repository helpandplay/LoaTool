using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CommunityToolkit.Mvvm.DependencyInjection;
using LoaTool.Define.Interfaces;
using LoaTool.Util;
using LoaTool.ViewModel;
using Microsoft.Extensions.DependencyInjection;

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