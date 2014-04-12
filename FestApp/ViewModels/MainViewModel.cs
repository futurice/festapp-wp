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
using System.Linq;


namespace FestApp
{
    public class ArtistGroupViewModel : ObservableCollection<ArtistViewModel>
    {
        public ArtistGroupViewModel(IEnumerable<ArtistViewModel> artistVMs) : base(artistVMs) { }

        public string Key { get; set; }
    }

    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.Items = new ObservableCollection<ArtistViewModel>();
            this.ArtistGroups = new ObservableCollection<ArtistGroupViewModel>();
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

        public bool IsDataLoading
        {
            get;
            private set;
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        public ObservableCollection<ArtistGroupViewModel> ArtistGroups { get; private set; }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public async Task LoadData()
        {
            IsDataLoading = true;

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

            ILookup<string, ArtistViewModel> artistGroupQuery = this.Items.ToLookup(artist => {
                return artist.Name.Length > 0 ? artist.Name.Substring(1) : "";
            });

            ArtistGroups.Clear();

            foreach (var artistGroupMapping in artistGroupQuery)
            {
                string groupName = artistGroupMapping.Key;
                ArtistGroupViewModel groupVM = new ArtistGroupViewModel(artistGroupMapping);
                groupVM.Key = groupName;
                ArtistGroups.Add(groupVM);
            }
        }

        //public ObservableCollection<ArtistGroup>

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