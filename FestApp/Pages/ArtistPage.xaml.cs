using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;
using Microsoft.Phone.Tasks;
using System.Diagnostics;
using FestApp.Utils;

namespace FestApp
{
    public partial class ArtistPage : PhoneApplicationPage
    {
        private ArtistViewModel _viewModel;

        // Constructor
        public ArtistPage()
        {
            InitializeComponent();
            this.EnableTransitions();
        }

        public static Uri GetPageUri(string id)
        {
            return new Uri(string.Format("/Pages/ArtistPage.xaml?selectedItem={0}", Uri.EscapeDataString(id)), UriKind.Relative);
        }

        // When page is navigated to set data context to selected item in list
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            string selectedId = "";
            if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedId)) {
                using (Utils.LoadingIndicatorHelper.StartLoading("Refreshing data..."))
                {
                    await ArtistViewModel.LoadSingle(selectedId, viewModel =>
                    {
                        DataContext = _viewModel = viewModel;
                    });
                }
            }
        }

        private void YoutubeButtonTapped(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigateToPage(_viewModel.YoutubeUrl);
        }

        private void SpotifyButtonTapped(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigateToPage(_viewModel.SpotifyUrl);
        }

        private void FavoriteButtonTapped(object sender, GestureEventArgs e)
        {
            _viewModel.Favorited = !_viewModel.Favorited;
        }

        private void NavigateToPage(string url)
        {
            if (url == null)
            {
                Debug.WriteLine("No URL");
                return;
            }

            WebBrowserTask webTask = new WebBrowserTask();
            webTask.Uri = new Uri(url);
            webTask.Show();
        }
    }

    class DesignerArtist : ArtistViewModel
    {
        private static Models.Artist SampleArtist {
            get {
                List<Models.Artist> artists = DesignData.JsonLoader.Artists();

                if (!artists.Any())
                {
                    return null;
                }

                return artists[0];
            }
        }

        public DesignerArtist() : base(SampleArtist) { }
    }
}