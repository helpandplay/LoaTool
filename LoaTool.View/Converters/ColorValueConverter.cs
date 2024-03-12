using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using LoaTool.Define.Enums;
using LoaTool.Define.Structs;

namespace LoaTool.View.Converters
{
    public class ColorValueConverter : IMultiValueConverter
    {
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if(value is RGBValueStruct rgbValueStruct)
            {
                return [rgbValueStruct.Type, rgbValueStruct.Value];
            }
            throw new ArgumentException("Cannot convert back object: " + value);
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if(values.Length != 2)
            {
                throw new ArgumentException("Cannot convert RGBValueStruct. [" + values.ToString() + "]");
            }

            if(values[1] == DependencyProperty.UnsetValue)
            {
                values[1] = default(int);
            }

            if(values[1] is double doubleValue)
            {
                values[1] = System.Convert.ToInt32(doubleValue);
            }

            if(values[0] is RGB rgb &&
               (values[1] is int value))
            {
                return new RGBValueStruct(rgb, value);
            }

            throw new InvalidOperationException("Cannot parse RGBValueStruct. [" + values[0] + " " + values[1] + "]");
        }
    }
}
