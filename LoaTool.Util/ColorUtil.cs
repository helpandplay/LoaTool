﻿using System;
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

    public static SolidColorBrush GetForegroundColor(Color backgroundColor)
    {
        // RGB 값을 가중치를 적용하여 밝기 계산
        double brightness = ( backgroundColor.R * 0.299 + backgroundColor.G * 0.587 + backgroundColor.B * 0.114 ) / 255;

        // 밝기에 따라 전경색 결정
        if(brightness < 0.5)
        {
            // 어두운 배경에는 흰색 글자
            return new SolidColorBrush(Colors.White);
        }
        else
        {
            // 밝은 배경에는 검정색 글자
            return new SolidColorBrush(Colors.Black);
        }
    }

    public static SolidColorBrush CreateColor(byte r, byte g, byte b)
    {
        return new SolidColorBrush(Color.FromRgb(r, g, b));
    }

    public static SolidColorBrush CreateColor(int r, int g, int b)
    {
        return CreateColor(Convert.ToByte(r), Convert.ToByte(g), Convert.ToByte(b));
    }
}
