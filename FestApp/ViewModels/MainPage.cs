using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using FestApp.Utils;

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

                return "Something else";
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
                    return " in " + (int)timeUntilStart.TotalHours + "h " + timeUntilStart.Minutes + "min";
                }
                else
                {
                    return " in " + timeUntilStart.Minutes + " minutes";
                }
                
            }
        }

        public ObservableCollection<GigItem> NextGigs { get; set; }

        public ObservableCollection<NewsItem> LatestNews { get; set; }
    }

    class DesignerMainPage : MainPage
    {
        public DesignerMainPage()
        {

            NextGigs = new ObservableCollection<GigItem>()
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

            LatestNews = new ObservableCollection<NewsItem>()
            {
                new NewsItem(new Models.NewsItem()
                {
                    Title="Everything works perfectly and everyone's happyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy",
                    Time=DateTimeOffset.Now,
                }),

                /*new NewsItem(new Models.NewsItem()
                {
                    Title="Something happened yesterday",
                    Time=DateTimeOffset.Now - TimeSpan.FromDays(1),
                }),

                new NewsItem(new Models.NewsItem()
                {
                    Title="Something happened day before yesterday",
                    Time=DateTimeOffset.Now - TimeSpan.FromDays(2),
                }),*/
            };
        }

        public GigItem TestItem { get { return NextGigs[0]; } }
    }
}
