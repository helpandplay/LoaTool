using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LoaTool.Define.Interfaces;

namespace LoaTool.ViewModel;
public partial class AuctionViewModel : DialogViewModelBase, IContext
{
    private string? _price;

    public string Price
    {
        get => _price;
        set
        {

            if (_price != value)
            {
                SetProperty(ref _price, value);
                AuctionPerFour = value;
                AuctionPerEight = value;
                AuctionPerSixteen = value;
            }
        }
    }

    [ObservableProperty]
    private string? _auctionPerFour;
    [ObservableProperty]
    private string? _auctionPerEight;
    [ObservableProperty]
    private string? _auctionPerSixteen;

    [RelayCommand]
    protected override void Execute(IDialog dialog)
    {
        if(_dialogService != null)
        {
            IDialog mainDialog = _dialogService.GetDialog<MainViewModel>();
            dialog.Positioning(mainDialog);
        }
    }
}
