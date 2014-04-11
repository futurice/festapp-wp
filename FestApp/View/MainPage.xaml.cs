using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using Microsoft.Phone.Shell;
using FestApp.ViewModel;

namespace FestApp.View
{
    public partial class MainPage
    {
        public MainPage() : base(0.8)
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var viewModelLocator = Application.Current.Resources["Locator"] as ViewModelLocator;
            if (viewModelLocator != null) {
                var mainVM = viewModelLocator.Main;
                mainVM.SelectedArticle = null;
                mainVM.SelectedArtist = null;
            }
        }

        private void ApplicationBarMenuItem_OnClick(object sender, EventArgs e)
        {
            var viewModelLocator = Application.Current.Resources["Locator"] as ViewModelLocator;
            if (viewModelLocator == null) {
                return;
            }
            viewModelLocator.Main.NavigateAbout();
        }


    }
}