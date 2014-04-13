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
using FestApp.Utils;

namespace FestApp.Pages
{
    public class DesignerNews
    {
        public class NewsItem
        {
            public string Title { get; set; }
            public string Time { get; set; }
            public int ListIndex { get; set; }
        }

        public List<NewsItem> News { get; set; }

        public DesignerNews()
        {
            News = new List<NewsItem>();
            News.Add(new NewsItem()
            {
                Title = "New First",
                Time = "Fri 10.10.2010",
                ListIndex = 0
            });
            News.Add(new NewsItem()
            {
                Title = "New First",
                Time = "Fri 10.10.2010",
                ListIndex = 1
            });
            News.Add(new NewsItem()
            {
                Title = "New First",
                Time = "Fri 10.10.2010",
                ListIndex = 2
            });
            News.Add(new NewsItem()
            {
                Title = "New First",
                Time = "Fri 10.10.2010",
                ListIndex = 3
            });
        }
    }

    public partial class News : PhoneApplicationPage
    {
        public News()
        {
            InitializeComponent();
            this.EnableTransitions();
        }

        public static Uri GetPageUri()
        {
            return new Uri("/Pages/News.xaml", UriKind.Relative);
        }
    }
}