using FestApp.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FestApp.ViewModels
{
    public delegate void ArtistSelectedListener(ArtistViewModel artist);

    public class ArtistListViewModel : ViewModelBase
    {
        public ArtistListViewModel() { }

        public async Task LoadData()
        {
            try
            {
                await API.Artists.UseCachedThenFreshData(result => PopulateArtistsFromList(result.Data));
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error loading artists, HANDLE! {0}", e);
            }
        }

        protected void PopulateArtistsFromList(List<Models.Artist> artists)
        {
            IEnumerable<ArtistViewModel> artistViewModels = artists.Select<Models.Artist, ArtistViewModel>(
                artist => new ArtistViewModel(artist));

            PopulateArtistsFromList(artistViewModels);
        }

        protected void PopulateArtistsFromList(IEnumerable<ArtistViewModel> artistViewModels)
        {
            List<AlphaKeyGroup<ArtistViewModel>> artistGroups = AlphaKeyGroup<ArtistViewModel>.CreateGroups(artistViewModels,
                System.Threading.Thread.CurrentThread.CurrentUICulture,
                artist => artist.Name, true);

            foreach (AlphaKeyGroup<ArtistViewModel> group in artistGroups)
            {
                int index = 0;

                foreach (ArtistViewModel artist in group)
                {
                    artist.ListIndex = index++;
                }
            }

            SetVMProperty(() => ArtistGroups, artistGroups);
        }

        public List<AlphaKeyGroup<ArtistViewModel>> ArtistGroups { get; protected set; }
    }
}
