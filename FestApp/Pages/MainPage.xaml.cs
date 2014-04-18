﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using FestApp.Utils;

namespace FestApp.Pages
{
    public partial class MainPage : PhoneApplicationPage
    {
        private ViewModels.MainPage _viewModel;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            this.EnableTransitions();

            DataContext = _viewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        // Load data for the ViewModel Items
        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null) { return; }

            _viewModel = new ViewModels.MainPage();
            DataContext = _viewModel;

            try
            {
                using (Utils.LoadingIndicatorHelper.StartLoading("Refreshing data..."))
                {
                    await _viewModel.LoadData();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load latest data!");
            }
        }

        private void Info_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var pageUri = InfoPage.GetPageUri();
            NavigationService.Navigate(pageUri);
        }

        private void Map_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var pageUri = MapPage.GetPageUri();
            NavigationService.Navigate(pageUri);
        }

        private void Instagram_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }

        private void Bands_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var pageUri = ArtistListPage.GetPageUri();
            NavigationService.Navigate(pageUri);
        }

        private void News_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var pageUri = News.GetPageUri();
            NavigationService.Navigate(pageUri);
        }

        private void Gig_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var pageUri = SchedulePage.GetPageUri();
            NavigationService.Navigate(pageUri);
        }
    }
}