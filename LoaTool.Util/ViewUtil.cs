
using System.Windows;
using LoaTool.Define.View;

namespace LoaTool.Util;

public class ViewUtil
{
    public static WindowLocation GetWindowLocation(double applicationWidth, double applicationTop = 0.0)
    {
        double windowWidth = SystemParameters.WorkArea.Width;

        double applicationLeft = (windowWidth / 2) - (applicationWidth / 2);

        return new WindowLocation(applicationTop, applicationLeft);
    }
}

