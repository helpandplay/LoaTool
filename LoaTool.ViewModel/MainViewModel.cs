
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using LoaTool.Define.Interfaces;
using LoaTool.Define.View;
using LoaTool.Util;

namespace LoaTool.ViewModel;

public partial class MainViewModel : DialogViewModelBase, IContext
{
    private readonly ColorPickerViewModel colorPickerViewModel;

    public MainViewModel(ColorPickerViewModel colorPickerViewModel)
    {
        this.colorPickerViewModel = colorPickerViewModel;
    }

    [RelayCommand]
    protected override void Execute(IDialog dialog)
    {
        dialog?.Positioning();
    }

    [RelayCommand]
    private void RunColorPicker()
    {
        _dialogService.Show(colorPickerViewModel);
    }
}

