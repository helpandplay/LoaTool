using System.Collections.ObjectModel;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using LoaTool.Define.Interfaces;
using LoaTool.Define.Structs;
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
    [ObservableProperty]
    private SolidColorBrush _colorInfoForeground = ColorUtil.GetForegroundColor(Colors.White);

    private SolidColorBrush _color = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
    public SolidColorBrush Color
    {
        get => _color;
        set
        {
            System.Windows.Media.Color color = _color.Color;
            if(color.R != value.Color.R || color.G != value.Color.G || color.B != value.Color.B)
            {
                SetProperty(ref _color, value);
                ColorHex = ColorUtil.RGBToHex(color);
            }
        }
    }

    [ObservableProperty]
    public string _colorHex;

    private int _red;
    public int Red
    {
        get => _red;
        set
        {
            if(value !=  _red)
            {
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
                SetProperty(ref _blue, value);
            }
        }
    }

    [ObservableProperty]
    private ObservableCollection<SolidColorBrush> _colorHistories = new ObservableCollection<SolidColorBrush>();

    public ColorPickerViewModel()
    {
        this.colorExtractor = Ioc.Default.GetService<ColorExtractor>();
        ColorHex = ColorUtil.RGBToHex(Colors.Black);
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

            Color = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
            ColorInfoForeground = ColorUtil.GetForegroundColor(Colors.White);
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

    [RelayCommand]
    private void ChangeColorValue(RGBValueStruct rgbValueStruct)
    {
        switch(rgbValueStruct.Type)
        {
            case Define.Enums.RGB.Red:
                CaptureColor(ColorUtil.CreateColor(rgbValueStruct.Value, Green, Blue));
                break;
            case Define.Enums.RGB.Green:
                CaptureColor(ColorUtil.CreateColor(Red, rgbValueStruct.Value, Blue));
                break;
            case Define.Enums.RGB.Blue:
                CaptureColor(ColorUtil.CreateColor(Red, Green, rgbValueStruct.Value));
                break;
        }
    }

    private void CaptureColor(SolidColorBrush colorBrush)
    {
        if(!ColorUtil.Equals(Color, colorBrush))
        {
            Red = colorBrush.Color.R;
            Green = colorBrush.Color.G;
            Blue = colorBrush.Color.B;
            Color = colorBrush;
            NoteColor(colorBrush);
            ColorInfoForeground = ColorUtil.GetForegroundColor(colorBrush.Color);
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
