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
            DataContext = _viewModel;
        }
    }
}