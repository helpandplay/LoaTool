
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using LoaTool.Define.Interfaces;
using LoaTool.Define.View;
using LoaTool.Util;

namespace LoaTool.ViewModel;

public partial class MainViewModel(IDialogService dialogService,
    ColorPickerViewModel colorPickerViewModel) : ObservableObject
{
    [ObservableProperty]
    private bool _isMouseEnter = false;

    [ObservableProperty]
    private bool _isClickLeftMouseButton = false;

    private readonly IDialogService _dialogService = dialogService;
    private readonly ColorPickerViewModel colorPickerViewModel = colorPickerViewModel;

    /// <summary>
    /// MainWindow.xaml / OnMouseEnter
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void OnMouseEnter(object sender, MouseEventArgs e)
    {
        if(IsMouseEnter) return;
        IsMouseEnter = true;
        System.Diagnostics.Debug.WriteLine($"isMouseEnter: {IsMouseEnter}");
    }

    /// <summary>
    /// MainWindow.xaml / OnMouseLeave
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void OnMouseLeave(object sender, MouseEventArgs e)
    {
        if(!IsMouseEnter) return;
        IsMouseEnter = false;
        System.Diagnostics.Debug.WriteLine($"isMouseEnter: {IsMouseEnter}");
    }

    [RelayCommand]
    private void ClickDrag(Window window)
    {
        if(!IsMouseEnter) return;
        IsClickLeftMouseButton = true;
        System.Diagnostics.Debug.WriteLine($"Mouse left button down: {IsMouseEnter}");
        window.DragMove();
        IsClickLeftMouseButton = false;
    }

    [RelayCommand]
    private void ClickColorPicker()
    {
        _dialogService.Show(colorPickerViewModel);
    }
}

