using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using LoaTool.Define.Interfaces;
using LoaTool.Define.Interfaces.Enums;
using LoaTool.Define.Resources;
using LoaTool.Define.Views.Enums;
using LoaTool.Util;

namespace LoaTool.ViewModel;
public partial class ColorPickerViewModel : DialogViewModelBase, IContext
{
    [ObservableProperty]
    private bool _pinned;
    [ObservableProperty]
    private bool _picking;

    private readonly ColorExtractor colorExtractor;

    public ColorPickerViewModel(ColorExtractor colorExtractor)
    {
        this.colorExtractor = colorExtractor;
    }

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

    [RelayCommand]
    private void PickColor()
    {
        Picking = true;
        colorExtractor.Activate(CaptureColor, FinishColor);
    }

    private void CaptureColor(System.Drawing.Color color)
    {

    }

    private void FinishColor()
    {
        Picking = false;
    }
}
