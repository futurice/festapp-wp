using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Linq;

namespace FestApp
{
    public delegate void ArtistViewModelLoaded(ArtistViewModel viewModel);

    public class ArtistViewModel : INotifyPropertyChanged
    {
        private bool _favorited;

        public bool Favorited
        {
            get
            {
                return _favorited;
            }
            set
            {
                if (value != _favorited)
                {
                    _favorited = value;
                    NotifyPropertyChanged("Favorited");
                    NotifyPropertyChanged("FavoritedImage");
                }
            }
        }

        public BitmapImage FavoritedImage
        {
            get
            {
                if (_favorited)
                {
                    return new BitmapImage(new Uri("/Images/Icons/band_page_star_icon_active.png", UriKind.Relative));
                }
                else
                {
                    return new BitmapImage(new Uri("/Images/Icons/band_page_star_icon.png", UriKind.Relative));
                }
            }
        }

        public Models.Artist Model { get; private set; }

        public ArtistViewModel() { }

        public ArtistViewModel(Models.Artist artist)
        {
            Model = artist;
            Name = artist.Name;
            Description = artist.Content;
            PhotoUrl = artist.Picture;
            SpotifyUrl = artist.Spotify;
            YoutubeUrl = artist.Youtube;
        }

        public static async Task LoadSingle(string id, ArtistViewModelLoaded listener)
        {
            try
            {
                await API.Artists.UseCachedThenFreshData(result =>
                {
                    foreach (Models.Artist artist in result.Data)
                    {
                        if (artist.Id == id)
                        {
                            listener(new ArtistViewModel(artist));
                        }
                    }
                });
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error loading artists, HANDLE! {0}", e);
            }
        }

        private string _name;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != _name) {
                    _name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        private string _description;

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (value != _description) {
                    _description = value;
                    NotifyPropertyChanged("Description");
                }
            }
        }

        private string _photoUrl;

        public string PhotoUrl
        {
            get
            {
                return _photoUrl;
            }
            set
            {
                if (value != _photoUrl)
                {
                    _photoUrl = value;
                    NotifyPropertyChanged("PhotoUrl");
                }
            }
        }

        private string _youtubeUrl = null;

        public string YoutubeUrl
        {
            get
            {
                return _youtubeUrl;
            }
            set
            {
                if (value != _youtubeUrl)
                {
                    _youtubeUrl = value;
                    NotifyPropertyChanged("YoutubeUrl");
                }
            }
        }

        private string _spotifyUrl = null;

        public string SpotifyUrl
        {
            get
            {
                return _spotifyUrl;
            }
            set
            {
                if (value != _spotifyUrl)
                {
                    _spotifyUrl = value;
                    NotifyPropertyChanged("SpotifyUrl");
                }
            }
        }

        public int ListIndex { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler) {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}