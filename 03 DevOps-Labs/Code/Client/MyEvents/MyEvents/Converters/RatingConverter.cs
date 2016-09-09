using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyEvents
{
    class RatingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var rating = (int)value;
            if (rating == 1)
                return "Disappointed. \ud83d\ude1e";
            if (rating == 2)
                return "Not a fan. \ud83d\ude0f";
            if (rating == 3)
                return "Just fine. \ud83d\ude10";
            if (rating == 4)
                return "Like it. \ud83d\ude0d";
            if (rating == 5)
                return "Love it. \ud83d\ude0d \ud83d\udc96 ";

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
