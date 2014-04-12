using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace FestApp.Utils
{
    public class ListItemAlternateBrushConverter : IValueConverter
    {
        public Brush OddBrush { get; set; }
        public Brush EvenBrush { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int index = (int)value;

            if (index % 2 == 0)
            {
                return EvenBrush;
            }

            return OddBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
