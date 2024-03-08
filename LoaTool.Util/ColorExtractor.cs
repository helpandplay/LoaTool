using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Gma.System.MouseKeyHook;
using LoaTool.Define.Colors;

namespace LoaTool.Util;
public class ColorExtractor
{
    private IKeyboardMouseEvents _mouseHook;

    private Action<System.Drawing.Color>? CaptureColor { get; set; }
    private Action? FinishColor { get; set; }
    private int mouseTracker;
    private readonly int CAPTURE_INTERNAL = 3;

    public void Activate(Action<System.Drawing.Color> capturer, Action finisher)
    {
        this.CaptureColor = capturer;
        this.FinishColor = finisher;

        this._mouseHook = Hook.GlobalEvents();

        Mouse.SetCursor(Cursors.Cross);
        _mouseHook.MouseMove += MouseMoveHooker;
        _mouseHook.MouseDown += MouseDownHooker;
    }

    private void MouseDownHooker(object? sender, System.Windows.Forms.MouseEventArgs e)
    {
        if(CaptureColor != null)
        {
            System.Drawing.Color color = GetPixelColor(e);
            CaptureColor(color);
        }
        
        if(FinishColor != null)
        {
            FinishColor();
        }
    }

    private void MouseMoveHooker(object? sender, System.Windows.Forms.MouseEventArgs e)
    {
        
        if(CanCapture() && CaptureColor != null)
        {
            System.Drawing.Color color = GetPixelColor(e);
            CaptureColor(color);
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
    }

    private System.Drawing.Color GetPixelColor(System.Windows.Forms.MouseEventArgs e)
    {
        return new Pixel(e.X, e.Y).Get();
    }
}
