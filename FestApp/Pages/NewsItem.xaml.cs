using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using FestApp.Utils;
using FestApp.ViewModels;

namespace FestApp.Pages
{
    public partial class NewsItem : PhoneApplicationPage
    {
        private NewsViewModel.NewsItem _viewModel;

        public NewsItem()
        {
            InitializeComponent();
            this.EnableTransitions();
        }

        public static Uri GetPageUri(string id)
        {
            return new Uri(string.Format("/Pages/NewsItem.xaml?selectedItem={0}", Uri.EscapeDataString(id)), UriKind.Relative);
        }

        // When page is navigated to set data context to selected item in list
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            string selectedId = "";
            if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedId))
            {
                using (Utils.LoadingIndicatorHelper.StartLoading("Refreshing data..."))
                {
                    await NewsViewModel.NewsItem.LoadSingle(selectedId, viewModel =>
                    {
                        DataContext = _viewModel = viewModel;
                    });
                }
            }
        }
    }
}