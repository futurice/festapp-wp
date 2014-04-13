using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Collections.ObjectModel;
using FestApp.Utils;
using FestApp.ViewModels;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;

namespace FestApp.Pages
{
    public class DesignerArtistListVM : ArtistListViewModel
    {
        public DesignerArtistListVM()
        {
            IEnumerable<ArtistViewModel> artistVMs = DesignData.JsonLoader.Artists().Select<Models.Artist, ArtistViewModel>(
                artist => new ArtistViewModel(artist));

            PopulateArtistsFromList(artistVMs);
        }
    }

    public partial class ArtistListPage : PhoneApplicationPage
    {
        private ArtistListViewModel _viewModel;

        public ArtistListPage()
        {
            InitializeComponent();
            DataContext = _viewModel = new ArtistListViewModel();
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
            return new Uri(string.Format("/Pages/ArtistListPage.xaml"), UriKind.Relative);
        }

        private void ArtistSelected(ArtistViewModel artist)
        {
            NavigationService.Navigate(ArtistPage.GetPageUri(artist.Model.Id));
        }

        private void ListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LongListSelector listBox = (LongListSelector)sender;
            ArtistViewModel selectedArtist = (ArtistViewModel)listBox.SelectedItem;

            if (selectedArtist != null)
            {
                ArtistSelected(selectedArtist);
                listBox.SelectedItem = null;
            }
        }
    }
}