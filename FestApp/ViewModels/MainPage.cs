using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using FestApp.Utils;
using System.Threading.Tasks;
using System.Diagnostics;

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

        public class GigItem
        {
            public string Annotation { get; private set; }
            public string Artist { get; private set; }
            public string Stage { get; private set; }

            public string StartingIn { get; private set; }

            public GigItem(Models.Artist artist, string annotation)
            {
                Artist = artist.Name;
                Annotation = annotation;
                StartingIn = MakeStartString(artist.TimeStart);
                Stage = artist.Stage;
            }

            private static string MakeStartString(DateTimeOffset startTime)
            {
                var timeUntilStart = startTime - DateTimeOffset.Now;

                if (timeUntilStart.Hours > 0)
                {
                    return " in " + (int)timeUntilStart.TotalHours + " hours " + timeUntilStart.Minutes + " minutes";
                }
                else
                {
                    return " in " + timeUntilStart.Minutes + " minutes";
                }
                
            }
        }

        public async Task LoadData()
        {
            try
            {
                await API.News.UseCachedThenFreshData(result => PopulateNewsFromList(result.Data));
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error loading news, HANDLE! {0}", e);
            }

            SetVMProperty(
                () => NextGigs,
 
                new List<GigItem>()
                {
                    new GigItem(new Models.Artist()
                    {
                        Name="Lily Allen",
                        TimeStart=DateTimeOffset.Now + TimeSpan.FromMinutes(15), 
                        Stage="Stallman Stage",
                    }, "Next Up: "),

                    new GigItem(new Models.Artist()
                    {
                        Name="Anna Abreu",
                        TimeStart=DateTimeOffset.Now + TimeSpan.FromMinutes(90),
                        Stage="Group Stage",
                    }, "")
                });


        }

        protected void PopulateNewsFromList(List<Models.NewsItem> newsItemsList)
        {
            var vmList = newsItemsList.
                Select(x => new NewsItem(x)).
                Take(2).
                ToList();

            SetVMProperty(() => LatestNews, vmList);

        }

        public List<GigItem> NextGigs { get; set; }

        public List<NewsItem> LatestNews { get; set; }
    }

    class DesignerMainPage : MainPage
    {
        public DesignerMainPage()
        {

            NextGigs = new List<GigItem>()
            {
                new GigItem(new Models.Artist()
                {
                    Name="Lily Allen",
                    TimeStart=DateTimeOffset.Now + TimeSpan.FromMinutes(15), 
                    Stage="Stallman Stage",
                }, "Next Up: "),

                new GigItem(new Models.Artist()
                {
                    Name="Anna Abreu",
                    TimeStart=DateTimeOffset.Now + TimeSpan.FromMinutes(90),
                    Stage="Group Stage",
                }, "")
            };

            PopulateNewsFromList(DesignData.JsonLoader.News());
            


        }

        public GigItem TestItem { get { return NextGigs[0]; } }
    }
}
