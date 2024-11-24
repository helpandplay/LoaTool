using LoaTool.Define.Enums;

namespace LoaTool.Define.Structs;
public class RGBValueStruct
{
    public RGBValueStruct(RGB rgb, int value)
    {
        Type = rgb;
        Value = value;
    }

    public RGB Type { get; set; }
    public int Value { get; set; }
}
