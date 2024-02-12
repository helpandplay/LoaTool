
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LoaTool.Define.View;
using LoaTool.Util;

namespace LoaTool.ViewModel;

public partial class MainViewModel : ObservableObject
{
    private bool _isMouseEnter = false;

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

