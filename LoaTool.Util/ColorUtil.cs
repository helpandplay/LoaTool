using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace LoaTool.Util;
public partial class ColorUtil
{
    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool GetCursorPos(out POINT lpPoint);

    public struct POINT
    {
        public int X;
        public int Y;
    }

    [LibraryImport("gdi32.dll", SetLastError = true, StringMarshalling = StringMarshalling.Utf16)]
    private static partial IntPtr CreateDC(string lpszDriver, string lpszDeviceName, string lpszOutput, int lpInitData);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
    private static extern bool DeleteDC([In] IntPtr hdc);

    [LibraryImport("gdi32.dll")]
    private static partial uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

    private static Color GetColorAt(int x, int y)
    {
        IntPtr hdc = CreateDC("Display", lpszDeviceName: null, lpszOutput: null, 0);
        uint pixel = GetPixel(hdc, x, y);
        DeleteDC(hdc);
        var color = Color.FromRgb(
            (byte)( pixel & 0x000000FF ),
            (byte)( ( pixel & 0x0000FF00 ) >> 8 ),
            (byte)( ( pixel & 0x00FF0000 ) >> 16 )
        );

        return color;
    }

    public static Color GetColor()
    {
        if(GetCursorPos(out POINT lpPoint))
        {
            return GetColorAt(lpPoint.X, lpPoint.Y);
        }

        throw new InvalidOperationException("Failed to get the cursor position.");
    }
}
