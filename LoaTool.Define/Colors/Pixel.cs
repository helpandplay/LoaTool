using System.Drawing;

namespace LoaTool.Define.Colors;
public class Pixel
{
    private readonly Bitmap _pixel;

    public Pixel(int x, int y)
    {
        this._pixel = new Bitmap(1, 1);
        var graphics = Graphics.FromImage(this._pixel);

        graphics.CopyFromScreen(x, y, 0, 0, new Size(1, 1));
    }

    public Color Get()
    {
        return _pixel.GetPixel(0, 0);
    }
}
