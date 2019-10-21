using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using WaslickiUbezpieczenia.Klasy;

namespace WaslickiUbezpieczenia.Widoki.Converters {
    class FormatowanieDaty : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null) return null;
            if (!(value is DateTime item)) return null;

            return item.ToString("yyyy-MM-dd");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null) return null;
            if (!(value is string item)) return null;

            return item.CanParse<DateTime>() ? (DateTime?)item.Parse<DateTime>() : null;
        }
    }
}