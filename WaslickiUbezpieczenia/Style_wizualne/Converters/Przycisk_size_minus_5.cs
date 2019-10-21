using System;
using System.Globalization;
using System.Windows.Data;

namespace WaslickiUbezpieczenia.Style_wizualne.Converters {
    class Przycisk_size_minus_5 : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (!(value is double val)) return value;
            return (val - 5);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (!(value is double val)) return value;
            return (val + 5);
        }
    }
}