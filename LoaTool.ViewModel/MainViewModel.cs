
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LoaTool.Define.View;
using LoaTool.Util;

namespace LoaTool.ViewModel;

public partial class MainViewModel : ObservableObject
{
    private static readonly WindowLocation _WINDOW_LOCATION = ViewUtil.GetWindowLocation(ViewDefines.Main.APP_WIDTH);
    private bool _isMouseEnter = false;

    [ObservableProperty]
    private bool _isAlwaysOnTop = true;

    [ObservableProperty]
    private int _width = ViewDefines.Main.APP_WIDTH;

    [ObservableProperty]
    private int _height = ViewDefines.Main.APP_HEIGHT;

    [ObservableProperty]
    private ResizeMode _resizeMode = ViewDefines.Main.RESIZE_MODE;

    [ObservableProperty]
    private double _top = _WINDOW_LOCATION.Top;

    [ObservableProperty]
    private double _left = _WINDOW_LOCATION.Left;

    [ObservableProperty]
    private double _windowOpacity = ViewDefines.Main.OPACITY;
    /// <summary>
    /// MainWindow.xaml / OnMouseEnter
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void OnMouseEnter(object sender, MouseEventArgs e)
    {
        if(_isMouseEnter) return;
        _isMouseEnter = true;
        System.Diagnostics.Debug.WriteLine($"isMouseEnter: {_isMouseEnter}");
    }
    /// <summary>
    /// MainWindow.xaml / OnMouseLeave
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void OnMouseLeave(object sender, MouseEventArgs e)
    {
        if(!_isMouseEnter) return;
        _isMouseEnter = false;
        System.Diagnostics.Debug.WriteLine($"isMouseEnter: {_isMouseEnter}");
    }
}

