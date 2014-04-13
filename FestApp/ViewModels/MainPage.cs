using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using FestApp.Utils;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Media.Imaging;
using System.Windows;

namespace FestApp.ViewModels
{
    public class MainPage : ViewModelBase
    {
        public class NewsItem
        {
            public string Title { get; private set; }
            public string Time { get; private set; }

            public NewsItem(Models.NewsItem newsItem)
            {
                Title = newsItem.Title;
                Time = MakeTimeString(newsItem.Time);
            }

            private static string MakeTimeString(DateTimeOffset timestamp)
            {
                var timeDifference = DateTimeOffset.Now - timestamp;

                if (timeDifference.Days == 0)
                {
                    return "Today " + timestamp.ToString("hh:mm");
                }
                else if (timeDifference.Days == 1)
                {
                    return "Yesterday " + timestamp.ToString("hh:mm");
                }
                else
                {
                    return timestamp.ToString("dd.MM hh:mm");
                }

            }
        }

        public class EventVM : ViewModelBase
        {
            public string Title { get; private set; }
            public string Stage { get; private set; }
            public string StartingIn { get; private set; }
            public string ArtistImage { get; private set; }

            public readonly Models.Event Event;

            public EventVM(Models.Event eventInfo)
            {
                Event = eventInfo;

                Title = String.Format("Next up: {0} in {1}",
                    String.Join(", ", eventInfo.Artists),
                    MakeStartString(eventInfo.StartTime));
                Stage = eventInfo.Location;

                StartingIn = MakeStartString(eventInfo.StartTime);
            }

            public async Task LoadArtistImage()
            {
                await API.Artists.UseCachedThenFreshData(data =>
                    {
                        var artist = data.Data.
                            Where(a => Event.Artists.Any(aName => aName == a.Name)).
                            First();
                        SetVMProperty(() => ArtistImage, artist.Picture);
                    });
            }

            private static string MakeStartString(DateTimeOffset startTime)
            {
                var timeUntilStart = startTime - DateTimeOffset.Now;

                if (timeUntilStart.Hours > 0)
                {
                    return (int)timeUntilStart.TotalHours + " hours " + timeUntilStart.Minutes + " minutes";
                }
                else
                {
                    return timeUntilStart.Minutes + " minutes";
                }
            }
        }

        public async Task LoadData()
        {
            await Task.WhenAll(new[] { LoadEvents(), LoadNews() });
        }

        private async Task LoadNews()
        {
            await API.News.UseCachedThenFreshData(result => PopulateNewsFromList(result.Data));
        }

        private async Task LoadEvents()
        {
            await API.Events.UseCachedThenFreshData(result => PopulateEventsFromList(result.Data));
        }

        protected void PopulateNewsFromList(List<Models.NewsItem> newsItemsList)
        {
            var vmList = newsItemsList.
                Select(x => new NewsItem(x)).
                Take(1).
                ToList();

            SetVMProperty(() => LatestNews, vmList);
        }

        protected async void PopulateEventsFromList(List<Models.Event> events)
        {
            var vm = events.
                OrderBy(e => e.StartTime).
                Where(e => e.StartTime > DateTimeOffset.Now).
                Select(e => new EventVM(e)).
                FirstOrDefault();

            SetVMProperty(() => NextGig, vm);

            try
            {
                await vm.LoadArtistImage();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error loading artist image: {0}", e);
            }
        }

        public EventVM NextGig { get; set; }

        public List<NewsItem> LatestNews { get; set; }
    }

    class DesignerMainPage : MainPage
    {
        public DesignerMainPage()
        {
            PopulateNewsFromList(DesignData.JsonLoader.News());

            var events = DesignData.JsonLoader.Events();
            events[0].StartTime = DateTimeOffset.Now + TimeSpan.FromMinutes(27);
            PopulateEventsFromList(events);
        }
    }
}
