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

namespace FestApp.Pages
{
    public class DesignerArtistListVM : ArtistListViewModel
    {
        public DesignerArtistListVM()
        {
            GetData(new ArtistViewModel[] {
                new ArtistViewModel() { Name = "Matti Meikäläinen" }, new ArtistViewModel() { Name = "Kalle Ankka" },
                new ArtistViewModel() { Name = "Matti Suomalainen" }, new ArtistViewModel() { Name = "Matti MuuMies" }
            });
        }
    }

    public partial class ArtistListPage : PhoneApplicationPage
    {
        private ArtistListViewModel _viewModel;

        public ArtistListPage()
        {
            InitializeComponent();
            _viewModel = new ArtistListViewModel();
        }

        private async Task TestLoad()
        {
            await App.ViewModel.LoadData();
            _viewModel.GetData(App.ViewModel.Items);
            DataContext = _viewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            TestLoad();
        }

        private void ArtistSelected(ArtistViewModel artist)
        {
            int index = App.ViewModel.Items.IndexOf(artist); // What if artist updates?

            if (index < 0)
            {
                Debug.WriteLine("Artist not found anymore");
                //return;
                index = 1; // TODO remove
            }

            NavigationService.Navigate(new Uri(string.Format("/Pages/ArtistPage.xaml?selectedItem={0}", index), UriKind.Relative));
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