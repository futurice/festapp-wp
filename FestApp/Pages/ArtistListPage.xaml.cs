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

namespace FestApp.Pages
{
    public class DummyArtistListVM
    {
        public ObservableCollection<ArtistGroupViewModel> ArtistGroups { get; set; }
    }

    public partial class ArtistListPage : PhoneApplicationPage
    {
        public ArtistListPage()
        {
            InitializeComponent();

            var dummyVM = new DummyArtistListVM()
            {
                ArtistGroups = new ObservableCollection<ArtistGroupViewModel>()
            };

            dummyVM.ArtistGroups.Add(new ArtistGroupViewModel(new ArtistViewModel[] {
                new ArtistViewModel() { Name = "Test Artist" }, new ArtistViewModel() { Name = "Second Artist" },
                new ArtistViewModel() { Name = "Test Artist 2" }})
            {
                Key = "T"
            });

            dummyVM.ArtistGroups.Add(new ArtistGroupViewModel(new ArtistViewModel[] {
                new ArtistViewModel() { Name = "Test Artist" }, new ArtistViewModel() { Name = "Second Artist" },
                new ArtistViewModel() { Name = "Test Artist 2" }})
            {
                Key = "U"
            });

            DataContext = dummyVM;
        }
    }
}