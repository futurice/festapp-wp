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
using FestApp.ViewModels;

namespace FestApp.Pages
{
    public partial class News : PhoneApplicationPage
    {
        private NewsViewModel _viewModel;

        public News()
        {
            InitializeComponent();
            this.EnableTransitions();
            DataContext = _viewModel = new NewsViewModel();
            Loaded += PageLoaded;
        }

        // Load data for the ViewModel Items
        private async void PageLoaded(object sender, RoutedEventArgs e)
        {
            using (Utils.LoadingIndicatorHelper.StartLoading("Refreshing data..."))
            {
                await _viewModel.LoadData();
            }
        }

        public static Uri GetPageUri()
        {
            return new Uri("/Pages/News.xaml", UriKind.Relative);
        }

        private void NewsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            NewsViewModel.NewsItem selectedItem = (NewsViewModel.NewsItem)listBox.SelectedItem;

            if (selectedItem != null)
            {
                NavigationService.Navigate(NewsItem.GetPageUri(selectedItem.Model.Id));
                selectedItem = null;
            }
        }
    }
    
    public class DesignerNews : NewsViewModel
    {
        public DesignerNews()
        {
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
}