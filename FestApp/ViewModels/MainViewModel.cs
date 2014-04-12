using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using FestApp.Models;
using System.Net;
using System.Threading.Tasks;


namespace FestApp
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.Items = new ObservableCollection<ArtistViewModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ArtistViewModel> Items { get; private set; }

        private string _sampleProperty = "Sample Runtime Property Value";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty) {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public async void LoadData()
        {
            List<Artist> artists;

            try
            {
                Debug.WriteLine("From cache");
                artists = await new DataLoader().Load<List<Artist>>("artists", LoadSource.CACHE);
                await CreateViewModels(artists);
            } catch (Exception) {
                Debug.WriteLine("Warning: loading from cache caused an error");
            }
            
            try {
                Debug.WriteLine("From network");
                artists = await new DataLoader().Load<List<Artist>>("artists", LoadSource.NETWORK);
                await CreateViewModels(artists);
                this.IsDataLoaded = true;
            }
            catch (Exception)
            {
                Debug.WriteLine("Could not load data"); // TODO

            }
        }

        private async Task CreateViewModels(List<Artist> artists)
        {
            this.Items.Clear();

            foreach (Artist artist in artists)
            {
                BitmapImage photo = null;

                if (!string.IsNullOrWhiteSpace(artist.Picture))
                {
                    try
                    {
                        photo = await new DataLoader().LoadImage(artist.Picture); // TODO wait for images later
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Warning: Could not load image: " + artist.Picture);
                    }
                }

                this.Items.Add(new ArtistViewModel()
                {
                    Name = artist.Name,
                    Description = artist.Content,
                    Photo = photo
                });
            }
        }

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