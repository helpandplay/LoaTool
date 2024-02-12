using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoaTool.Define.View;
public class WindowLocation(
    double top,
    double left
        )
{
    public double Top { get; } = top;
    public double Left { get; } = left;
}
