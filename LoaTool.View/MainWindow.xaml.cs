﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LoaTool.Util;
using LoaTool.ViewModel;

namespace LoaTool.View;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = App.Current.Services.GetService(typeof(MainViewModel));
        Define.View.WindowLocation windowLocation = ViewUtil.GetWindowLocation(Width);

        Left = windowLocation.Left;
        Top = windowLocation.Top;
    }
}