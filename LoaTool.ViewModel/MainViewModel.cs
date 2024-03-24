
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
    private readonly IDictionary<Type, IContext> _viewModels = new Dictionary<Type, IContext>();

    public MainViewModel(ColorPickerViewModel colorPickerViewModel, AuctionViewModel auctionViewModel)
    {
        _viewModels.Add(typeof(ColorPickerViewModel), colorPickerViewModel);
        _viewModels.Add(typeof(AuctionViewModel), auctionViewModel);
    }

    [RelayCommand]
    protected override void Execute(IDialog dialog)
    {
        dialog?.Positioning();
    }

    [RelayCommand]
    private void RunColorPicker()
    {
        if(_viewModels.ContainsKey(typeof(ColorPickerViewModel)))
        {
            IContext colorPickerViewModel = _viewModels[typeof(ColorPickerViewModel)];
            if(colorPickerViewModel != null)
            {
                _dialogService.Show(colorPickerViewModel);
            }
        }
    }

    [RelayCommand]
    private void RunCalculate()
    {
        if(_viewModels.ContainsKey(typeof(ColorPickerViewModel)))
        {
            IContext auctionViewModel = _viewModels[typeof(AuctionViewModel)];
            if(auctionViewModel != null)
            {
                _dialogService.Show(auctionViewModel);
            }
        }
    }
}

