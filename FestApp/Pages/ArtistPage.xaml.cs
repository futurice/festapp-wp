using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;
using Microsoft.Phone.Tasks;
using System.Diagnostics;

namespace FestApp
{
    public partial class ArtistPage : PhoneApplicationPage
    {
        private ArtistViewModel _viewModel;

        // Constructor
        public ArtistPage()
        {
            InitializeComponent();
        }

        public static void Open()
        {

        }

        // When page is navigated to set data context to selected item in list
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string selectedIndex = "";
            if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex)) {
                int index = int.Parse(selectedIndex);
                DataContext = _viewModel = App.ViewModel.Items[index];
            }
        }

        private void YoutubeButtonTapped(object sender, GestureEventArgs e)
        {
            NavigateToPage(_viewModel.YoutubeUrl);
        }

        private void SpotifyButtonTapped(object sender, GestureEventArgs e)
        {
            NavigateToPage(_viewModel.SpotifyUrl);
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
        public DesignerArtist()
        {
            Name = "Test artist";
            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras et purus vel diam malesuada blandit eget sit amet lorem. Integer nisi sem, pulvinar id mollis sit amet, ultrices in ligula. Sed adipiscing, lectus vitae ultricies vehicula, eros nunc condimentum ligula, sit amet fermentum lectus massa ullamcorper lorem.";
            Photo = new BitmapImage(new Uri("/DesignData/BadFinance.jpg", UriKind.Relative));
            YoutubeUrl = "https://www.youtube.com/watch?v=xRKzk0tKchE";
            //SpotifyUrl = "Foo";
        }
    }
}