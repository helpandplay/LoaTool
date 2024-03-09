using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Gma.System.MouseKeyHook;
using LoaTool.Define.Colors;

using MediaColor = System.Windows.Media.Color;
using SolidColorBrush = System.Windows.Media.SolidColorBrush;
using DrawingColor = System.Drawing.Color;

namespace LoaTool.Util;
public class ColorExtractor
{
    private IKeyboardMouseEvents _mouseHook;

    private Action<SolidColorBrush>? CaptureColor { get; set; }
    private Action? FinishColor { get; set; }
    private int mouseTracker;
    private readonly int CAPTURE_INTERNAL = 3;

    public void Activate(Action<SolidColorBrush> capturer, Action finisher)
    {
        this.CaptureColor = capturer;
        this.FinishColor = finisher;

        this._mouseHook = Hook.GlobalEvents();

        Mouse.OverrideCursor = Cursors.Cross;
        _mouseHook.MouseMove += MouseMoveHooker;
        _mouseHook.MouseDown += MouseDownHooker;
    }

    private void MouseDownHooker(object? sender, System.Windows.Forms.MouseEventArgs e)
    {
        if(CaptureColor != null)
        {
            System.Diagnostics.Trace.WriteLine("Mouse Down: Cature Color");
            SolidColorBrush colorBrush = GetPixelColorBrush(e);
            CaptureColor(colorBrush);
        }
        
        if(FinishColor != null)
        {
            System.Diagnostics.Trace.WriteLine("Mouse Down: Finish Color");
            Mouse.OverrideCursor = Cursors.Arrow;
            FinishColor();
            DeActivate();
        }
    }

    private void MouseMoveHooker(object? sender, System.Windows.Forms.MouseEventArgs e)
    {
        
        if(CanCapture() && CaptureColor != null)
        {
            System.Diagnostics.Trace.WriteLine("Mouse Move: Cature Color");
            SolidColorBrush colorBrush = GetPixelColorBrush(e);
            CaptureColor(colorBrush);
        }
    }

    private bool CanCapture()
    {
        mouseTracker++;

        if(mouseTracker > CAPTURE_INTERNAL)
        {
            mouseTracker = default(int);
            return true;
        }
        return false;
    }

    public void DeActivate()
    {
        _mouseHook.MouseMove -= MouseMoveHooker;
        _mouseHook.MouseDown -= MouseDownHooker;

        CaptureColor = null;
        FinishColor = null;

        _mouseHook.Dispose();
        System.Diagnostics.Trace.WriteLine("Color Extractor: DeActivate");
    }

    private DrawingColor GetPixelColor(System.Windows.Forms.MouseEventArgs e)
    {
        return new Pixel(e.X, e.Y).Get();
    }

    private SolidColorBrush GetPixelColorBrush(System.Windows.Forms.MouseEventArgs e)
    {
        DrawingColor color = GetPixelColor(e);
        return new SolidColorBrush(MediaColor.FromArgb(color.A, color.R, color.G, color.B));
    }
}
