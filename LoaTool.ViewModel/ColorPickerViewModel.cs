﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
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
    private readonly int MAX_COLOR_HISTORIES = 18;
    private readonly ColorExtractor? colorExtractor;

    [ObservableProperty]
    private bool _pinned;
    [ObservableProperty]
    private bool _picking;

    private SolidColorBrush _color = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
    public SolidColorBrush Color
    {
        get => _color;
        set
        {
            System.Windows.Media.Color color = _color.Color;
            if(color.R != value.Color.R || color.G != value.Color.G || color.B != value.Color.B)
            {
                System.Diagnostics.Trace.WriteLine("Color Change");
                SetProperty(ref _color, value);
            }
        }
    }

    private int _red;
    public int Red
    {
        get => _red;
        set
        {
            if(value !=  _red)
            {
                System.Diagnostics.Trace.WriteLine("Red Change");
                SetProperty(ref _red, value);
            }
        }
    }

    private int _green;
    public int Green
    {
        get => _green;
        set
        {
            if(value != _green)
            {
                System.Diagnostics.Trace.WriteLine("Green Change");
                SetProperty(ref _green, value);
            }
        }
    }

    private int _blue;
    public int Blue
    {
        get => _blue;
        set
        {
            if(value != _blue)
            {
                System.Diagnostics.Trace.WriteLine("Blue Change");
                SetProperty(ref _blue, value);
            }
        }
    }

    [ObservableProperty]
    private ObservableCollection<SolidColorBrush> _colorHistories = new ObservableCollection<SolidColorBrush>();

    public ColorPickerViewModel()
    {
        this.colorExtractor = Ioc.Default.GetService<ColorExtractor>();
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
        System.Diagnostics.Trace.WriteLine("PickColor");
        Picking = true;
        Pinned = true;
        colorExtractor?.Activate(CaptureColor, FinishColor);
    }

    private void CaptureColor(SolidColorBrush colorBrush)
    {
        if(!ColorUtil.Equals(Color, colorBrush))
        {
            System.Diagnostics.Trace.WriteLine("ColorPickerViewModel: CaptureColor");
            Red = colorBrush.Color.R;
            Green = colorBrush.Color.G;
            Blue = colorBrush.Color.B;
            Color = colorBrush;
            NoteColor(colorBrush);
        }
    }

    private void FinishColor()
    {
        Picking = false;
        System.Diagnostics.Trace.WriteLine("FinishColor");
    }

    private void NoteColor(SolidColorBrush newColor)
    {
        if(ColorHistories.Count() > MAX_COLOR_HISTORIES)
        {
            ColorHistories.RemoveAt(0);
        }
        ColorHistories.Add(newColor);
    }
}
