using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using FestApp.ViewModels;

namespace FestApp.Pages
{
    /*
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
    }*/

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

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var vm = new InfoViewModel();
            DataContext = vm;

            try
            {
                using (Utils.LoadingIndicatorHelper.StartLoading("Loading info..."))
                {
                    await vm.LoadData();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load latest info!");
            }

        }
    }
}