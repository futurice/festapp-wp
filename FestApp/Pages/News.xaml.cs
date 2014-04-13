using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using System.Windows.Data;

namespace FestApp.Pages
{
    public class EvenToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Color color;

            if ((bool)value)
            {
                color = Color.FromArgb(1, 1, 0, 0);
            }
            else
            {
                color = Color.FromArgb(1, 1, 0, 1);
            }

            return new SolidColorBrush(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DesignerNews
    {
        public class NewsItem
        {
            public string Title { get; set; }
            public string Time { get; set; }
            public bool IsEven { get; set; }
        }

        public List<NewsItem> News { get; set; }

        public DesignerNews()
        {
            News = new List<NewsItem>();
            News.Add(new NewsItem()
            {
                Title = "New First",
                Time = "10.10.2010",
                IsEven = false
            });
            News.Add(new NewsItem()
            {
                Title = "New First",
                Time = "10.10.2010",
                IsEven = true
            });
            News.Add(new NewsItem()
            {
                Title = "New First",
                Time = "10.10.2010",
                IsEven = false
            });
            News.Add(new NewsItem()
            {
                Title = "New First",
                Time = "10.10.2010",
                IsEven = true
            });
        }
    }

    public partial class News : PhoneApplicationPage
    {
        public News()
        {
            InitializeComponent();
        }

        public static Uri GetPageUri()
        {
            return new Uri("/Pages/News.xaml", UriKind.Relative);
        }
    }
}