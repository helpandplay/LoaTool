using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using LoaTool.Define.Interfaces;

namespace LoaTool.ViewModel;
public abstract partial class DialogViewModelBase : ObservableObject
{
    protected readonly IDialogService _dialogService;

    [ObservableProperty]
    private bool _isMouseEnter = false;

    [ObservableProperty]
    private bool _isClickLeftMouseButton = false;

    public DialogViewModelBase()
    {
        _dialogService = Ioc.Default.GetService<IDialogService>();
    }

    [RelayCommand]
    private void ReadyDrag()
    {
        if(IsMouseEnter) return;
        IsMouseEnter = true;
        System.Diagnostics.Debug.WriteLine($"isMouseEnter: {IsMouseEnter}");
    }
    [RelayCommand]
    private void NoReadyDrag()
    {
        if(!IsMouseEnter) return;
        IsMouseEnter = false;
        System.Diagnostics.Debug.WriteLine($"isMouseEnter: {IsMouseEnter}");
    }

    [RelayCommand]
    private void ActiveDrag(Window window)
    {
        if(!IsMouseEnter) return;
        IsClickLeftMouseButton = true;
        System.Diagnostics.Debug.WriteLine($"Mouse left button down: {IsMouseEnter}");
        window.DragMove();
        IsClickLeftMouseButton = false;
    }

    protected abstract void Execute(IDialog dialog);
}
