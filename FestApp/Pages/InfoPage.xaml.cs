using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace FestApp.Pages
{
    public class InfoViewModel
    {
        public class InfoItem
        {
            public string Title { get; set; }
            public int ListIndex { get; set; }
        }

        public List<InfoItem> Info { get; set; }
    }

    public class DesignerInfo : InfoViewModel
    {
        public DesignerInfo()
        {
            Info = new List<InfoItem>();
            Info.Add(new InfoItem()
            {
                Title = "General info",
                ListIndex = 0
            });
            Info.Add(new InfoItem()
            {
                Title = "Food & Drinks",
                ListIndex = 1
            });
            Info.Add(new InfoItem()
            {
                Title = "Services",
                ListIndex = 2
            });
            Info.Add(new InfoItem()
            {
                Title = "Transport",
                ListIndex = 3
            });
        }
    }

    public partial class InfoPage : PhoneApplicationPage
    {
        public InfoPage()
        {
            InitializeComponent();
        }

        public static Uri GetPageUri()
        {
            return new Uri("/Pages/InfoPage.xaml", UriKind.Relative);
        }
    }
}