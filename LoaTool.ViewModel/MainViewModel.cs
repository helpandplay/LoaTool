using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using LoaTool.Define.Interfaces;
using LoaTool.Service;

namespace LoaTool.ViewModel;

public partial class MainViewModel : DialogViewModelBase, IContext
{
    private readonly IDictionary<Type, IContext> _viewModels = new Dictionary<Type, IContext>();
    private readonly LostarkService lostark;
    public MainViewModel(
        ColorPickerViewModel colorPickerViewModel,
        AuctionViewModel auctionViewModel)
    {
        _viewModels.Add(typeof(ColorPickerViewModel), colorPickerViewModel);
        _viewModels.Add(typeof(AuctionViewModel), auctionViewModel);

        this.lostark = Ioc.Default.GetService<LostarkService>();
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

    [RelayCommand]
    private void ApiTest()
    {
        lostark.getData();
    }
}

