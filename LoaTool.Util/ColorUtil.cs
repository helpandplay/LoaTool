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
    public static bool Equals(SolidColorBrush color1, SolidColorBrush color2)
    {
        return Equals(color1.Color, color2.Color);
    }

    public static bool Equals(Color color1, Color color2)
    {
        return color1.R == color2.R && color1.G == color2.G && color1.B == color2.B;
    }
}
