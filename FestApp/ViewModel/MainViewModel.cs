using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using Newtonsoft.Json;
using FestApp.Model;
using FestApp.Model.Json;
using FestApp.Service;
using System.Linq;
using System.Windows.Threading;

namespace FestApp.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly DataService _dataService;
        public readonly NavigationService _navigationService;

        private bool _isLoadingNews;

        private bool _settingUpArtists;

        private bool _settingUpNews;

        public bool IsLoadingNews {
            get {
                return _isLoadingNews;
            }
            set {
                _isLoadingNews = value;
                
                if (value) {
                    IsLoading = true;                 
                }
                else {
                    if (!IsLoadingArtists) {
                        IsLoading = false;
                    }
                }
            }
        }

        private bool _isLoadingArtists;

        public bool IsLoadingArtists
        {
            get { return _isLoadingArtists; }
            set {
                _isLoadingArtists = value;

                if (value) {
                    IsLoading = true;                 
                }
                else {
                    if (!IsLoadingNews) {
                        IsLoading = false;
                    }
                }
            }
        }


        private string _loadingErrorMessage;

        public string LoadingErrorMessage
        {
            get { return _loadingErrorMessage; }
            set {
                _loadingErrorMessage = value;
                RaisePropertyChanged(LoadingMessagePropertyName);
            }
        }


        public MainViewModel()
        {
        }
        private const string LOADING_ERROR_MESSAGE = "Uusimpia tietoja ei voitu ladata";

        [PreferredConstructor]
        public MainViewModel(DataService dataService)
        {
            ShowAllArtistsActionCommand =
                new RelayCommand<RoutedEventArgs>(e => _navigationService.NavigateTo(ViewModelLocator.ArtistsPageUri));
            ShowScheduleActionCommand =
                new RelayCommand<RoutedEventArgs>(e => _navigationService.NavigateTo(ViewModelLocator.SchedulePageUri));
            
            _dataService = dataService;            
            _navigationService = new NavigationService();

            LoadData();
        }
  
        private void LoadData()
        {
            IsLoadingArtists = true;
            SetupArtistsAndScheduleFrom(GetFromCache<ObservableCollection<Artist>>("Artist.json"));
            _dataService.GetArtists(
                (item, error) => {
                    IsLoadingArtists = false;
                    if (error != null) {
                        LoadingErrorMessage = LOADING_ERROR_MESSAGE;
                        return;
                    }

                    SetupArtistsAndScheduleFrom(item);
                });

            IsLoadingNews = true;
            SetupNewsFrom(GetFromCache<ObservableCollection<News>>("News.json"));
            _dataService.GetNews(
                (item, error) => {
                    IsLoadingNews = false;
                    if (error != null) {
                        LoadingErrorMessage = LOADING_ERROR_MESSAGE;
                        return;
                    }

                    SetupNewsFrom(item);
                });
        }

        private void SetupNewsFrom(ObservableCollection<News> news)
        {
            _settingUpNews = true;
            NewsCollection = news;
            _settingUpNews = false;
        }

        private void SetupArtistsAndScheduleFrom(ObservableCollection<Artist> artists)
        {
            _settingUpArtists = true;
            
            Artists = new ObservableCollection<Artist>(artists.OrderBy((a) => a.Name));
            
            ChooseRandomArtists(artists);
            GetStages(artists);
            
            _settingUpArtists = false;
        }

        private T GetFromCache<T>(string filename)
        {
            var bandCache = Application.GetResourceStream(new Uri(string.Format("Cache/{0}", filename), UriKind.Relative));
            var sr = new StreamReader(bandCache.Stream);
            string content = sr.ReadToEnd();
            sr.Dispose();

            return JsonConvert.DeserializeObject<T>(content);
        }

        public void NavigateAbout()
        {
            _navigationService.NavigateTo(ViewModelLocator.AboutPageUri);
        }

        private void ChooseRandomArtists(ObservableCollection<Artist> item)
        {
            var toTake = Math.Min(item.Count, 8);
            var random = new Random();
            var randomNumber = random.Next(0, item.Count - (toTake - 1));
            RandomArtists = new ObservableCollection<Artist>(item.Skip(randomNumber).Take(toTake));
        }

        private void GetStages(IEnumerable<Artist> item)
        {
            var queryList = item.ToLookup(x => x.Stage);
            var stages = new ObservableCollection<Stage>();
            var nextPerformances = new ObservableCollection<Artist>();
            Func<Artist, object> orderByFunc = artist => artist.Start;
            var t = (DateTime.UtcNow - new DateTime(1970, 1, 1, 3, 0, 0));
            foreach (var stage in queryList)
            {
                var performances =
                    new ObservableCollection<Artist>(
                        stage.OrderBy(orderByFunc).Where(x => x.TimeStop.CompareTo(t.TotalSeconds) > 0));
                var performanceDays = new ObservableCollection<PerformanceDay>();
                
                foreach (var performanceDay in performances.ToLookup(x => x.Start.Day))
                {
                    performanceDays.Add(new PerformanceDay{ Name = performanceDay.First().Start.Day.ToString(CultureInfo.InvariantCulture), Performances = new ObservableCollection<Artist>(performanceDay)});
                }

                var newStage = new Stage
                                   {
                                       Name = stage.First().Stage,
                                       PerformanceDays = performanceDays
                                   };
                if (performances.Count > 0)
                {
                    nextPerformances.Add(performances[0]);
                }
                stages.Add(newStage);
            }
            Stages = stages;
            NextPerformances = nextPerformances;
        }


        #region Bindable variables

        public const string NewsCollectionPropertyName = "NewsCollection";
        private ObservableCollection<News> _newsCollection; 
        public ObservableCollection<News> NewsCollection
        {
            get { return _newsCollection; }
            set
            {
                if (_newsCollection == value)
                {
                    return;
                }
                _newsCollection = value;
                RaisePropertyChanged(NewsCollectionPropertyName);
            }
        }

        public const string StagesPropertyName = "Stages";
        private ObservableCollection<Stage> _stages;
        public ObservableCollection<Stage> Stages
        {
            get { return _stages; }
            set
            {
                if (_stages == value)
                {
                    return;
                }
                _stages = value;
                RaisePropertyChanged(StagesPropertyName);
            }
        }

        public const string LoadingMessagePropertyName = "LoadingMessage";
        public string LoadingMessage
        {
            get {
                return !String.IsNullOrWhiteSpace(LoadingErrorMessage) ? LoadingErrorMessage
                        : IsLoading ? "P‰ivitet‰‰n tietoja.."
                        : "";
            }
        }

        public const string IsLoadingPropertyName = "IsLoading";
        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                if (_isLoading == value)
                {
                    return;
                }
                _isLoading = value;
                RaisePropertyChanged(IsLoadingPropertyName);
                RaisePropertyChanged(LoadingMessagePropertyName);
            }
        }


        public const string NextPerformancesPropertyName = "NextPerformances";
        private ObservableCollection<Artist> _nextPerformances;
        public ObservableCollection<Artist> NextPerformances
        {
            get { return _nextPerformances; }
            set
            {
                if (_nextPerformances == value)
                {
                    return;
                }
                _nextPerformances = value;
                RaisePropertyChanged(NextPerformancesPropertyName);
            }
        } 

        public const string ArtistsPropertyName = "Artists";
        private ObservableCollection<Artist> _artists;
        public ObservableCollection<Artist> Artists
        {
            get { return _artists; }
            set
            {
                if (_artists == value)
                {
                    return;
                }
                _artists = value;
                RaisePropertyChanged(ArtistsPropertyName);
            }
        }

        public const string RandomArtistsPropertyName = "RandomArtists";
        private ObservableCollection<Artist> _randomArtists;
        public ObservableCollection<Artist> RandomArtists
        {
            get { return _randomArtists; }
            set
            {
                if (_randomArtists == value)
                {
                    return;
                }
                _randomArtists = value;
                RaisePropertyChanged(RandomArtistsPropertyName);
            }
        }

        public const string SelecterArtistPropertyName = "SelectedArtist";
        private Artist _selectedArtist;
        public Artist SelectedArtist
        {
            get { return _selectedArtist; }
            set
            {
                if (_settingUpArtists)
                    return;

                _selectedArtist = value;
                RaisePropertyChanged(SelecterArtistPropertyName);

                if (value != null)
                    _navigationService.NavigateTo(ViewModelLocator.ArtistDetailPageUri);
            }
        }

        public const string HeaderTitlePropertyName = "HeaderTitle";
        private string _headerTitle = "//TODO";
        public string HeaderTitle
        {
            get
            {
                return _headerTitle;
            }

            set
            {
                if (_headerTitle == value)
                {
                    return;
                }

                _headerTitle = value;
                RaisePropertyChanged(HeaderTitlePropertyName);
            }
        }


        public RelayCommand<RoutedEventArgs> ShowAllArtistsActionCommand
        {
            get;
            private set;
        }

        public RelayCommand<RoutedEventArgs> ShowScheduleActionCommand
        {
            get;
            private set;
        }

        public const string SelectedArticlePropertyName = "SelectedArticle";
        private News _selectedArticle;
        public News SelectedArticle
        {
            get { return _selectedArticle; }
            set
            {
                if (_selectedArticle == value || _settingUpNews)
                    return;

                _selectedArticle = value;
                RaisePropertyChanged(SelectedArticlePropertyName);

                if (value != null)
                    _navigationService.NavigateTo(ViewModelLocator.ArticleDetailsPageUri);
            }
        }

        #endregion
    }
}