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
public partial class ColorPickerViewModel : ObservableObject, IContext
{
    private readonly IDialogService? _dialogService;

    public ColorPickerViewModel()
    {
        _dialogService = Ioc.Default.GetService<IDialogService>();
    }

    [RelayCommand]
    private void Exit()
    {
        if(_dialogService != null)
        {
            _dialogService.Close(this);
        } 
    }

    [RelayCommand]
    private void Execute(IDialog dialog)
    {
        if(_dialogService != null)
        {
            IDialog mainDialog = _dialogService.GetDialog<MainViewModel>();
            dialog.Positioning(mainDialog);
        }
    }
}
