using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace WaslickiUbezpieczenia.Widoki.Converters {
    class KolorDni : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (!(value is double myval)) return Brushes.Black;

            if (myval <= 0)
                return Brushes.Red;
            else if (myval > 0 && myval <= 7)
                return Brushes.Orange;
            else
                return Brushes.ForestGreen;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
    }
}