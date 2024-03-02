using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using LoaTool.Define.Interfaces;

namespace LoaTool.ViewModel;
public partial class ColorPickerViewModel : DialogViewModelBase, IContext
{
    [ObservableProperty]
    private bool _pinned;

    [RelayCommand]
    private void Exit()
    {
        if(_dialogService != null && !Pinned)
        {
            _dialogService.Close(this);
        } 
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
    private void Pinning()
    {
        Pinned = !Pinned;
    }
}
