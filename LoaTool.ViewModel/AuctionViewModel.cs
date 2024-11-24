using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using LoaTool.Define.Enums;
using LoaTool.Define.Interfaces;
using LoaTool.Service;
using LoaTool.Util;

namespace LoaTool.ViewModel;
public partial class AuctionViewModel : DialogViewModelBase, IContext
{
    private readonly AuctionService _auctionService;

    private string? _price;

    public string Price
    {
        get => _price;
        set
        {

            if(_price != value)
            {
                SetProperty(ref _price, value);
                if(!string.IsNullOrEmpty(value) &&
                    TextValidationUtil.IsOnlyNumber(value) &&
                    int.TryParse(value, out int price))
                {
                    System.Diagnostics.Trace.WriteLine("Price: " + price);
                    AuctionPerFour = GenerateCalculateResultString(_auctionService.Calculate(price, 4));
                    AuctionPerEight = GenerateCalculateResultString(_auctionService.Calculate(price, 8));
                    AuctionPerSixteen = GenerateCalculateResultString(_auctionService.Calculate(price, 16));
                }
            }
        }
    }

    [ObservableProperty]
    private string? _auctionPerFour;
    [ObservableProperty]
    private string? _auctionPerEight;
    [ObservableProperty]
    private string? _auctionPerSixteen;

    public AuctionViewModel()
    {
        this._auctionService = Ioc.Default.GetService<AuctionService>();
    }

    [RelayCommand]
    protected override void Execute(IDialog dialog)
    {
        if(_dialogService != null)
        {
            IDialog mainDialog = _dialogService.GetDialog<MainViewModel>();
            dialog.Positioning(mainDialog);
        }
    }

    [RelayCommand]
    private void Exit()
    {
        if(_dialogService != null)
        {
            _dialogService.Close(this);
        }
    }

    private static string GenerateCalculateResultString(int[] results)
    {
        //0: result
        //1: recommand
        return "손익분기점 - " + results[0] + ", 경매추천가 - " + results[1];
    }
}
