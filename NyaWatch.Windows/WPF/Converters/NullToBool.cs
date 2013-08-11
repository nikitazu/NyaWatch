using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace NyaWatch.Windows.WPF.Converters
{
    public class NullToBool : ConverterBase<NullToBool>
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value != null;
        }
    }
}
