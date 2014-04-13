using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FestApp
{
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

        private BitmapImage _photo;

        public BitmapImage Photo
        {
            get
            {
                return _photo;
            }
            set
            {
                if (value != _photo) {
                    _photo = value;
                    NotifyPropertyChanged("Photo");
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