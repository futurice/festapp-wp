using FestApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestApp.ViewModels
{
    public class ArtistListViewModel
    {
        public ArtistListViewModel() { }

        public void GetData(IEnumerable<ArtistViewModel> artists)
        {
            ArtistGroups = AlphaKeyGroup<ArtistViewModel>.CreateGroups(artists,
                System.Threading.Thread.CurrentThread.CurrentUICulture,
                artist => artist.Name, true);

            foreach (AlphaKeyGroup<ArtistViewModel> group in ArtistGroups)
            {
                int index = 0;

                foreach (ArtistViewModel artist in group)
                {
                    artist.ListIndex = index++;
                }
            }
        }

        public List<AlphaKeyGroup<ArtistViewModel>> ArtistGroups { get; protected set; }
    }
}
