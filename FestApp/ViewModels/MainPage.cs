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

            public NewsItem(string title, string time)
            {
                Title = title;
                Time = time;
            }
        }

        public class GigItem
        {
            public string Title { get; private set; }
            public int Index { get; private set; }

            public GigItem(string title, int index)
            {
                Title = title;
                Index = index;
            }
        }

        public ObservableCollection<GigItem> NextGigs { get; set; }

        public ObservableCollection<NewsItem> LatestNews { get; set; }
    }

    class DesignerMainPage : MainPage
    {
        public DesignerMainPage()
        {
            var gigs = new[]
                {
                    "Artist 1 in 15 minutets",
                    "Artist 2 in 30 minutets",
                };

            NextGigs = new ObservableCollection<GigItem>(gigs.
                Zip(EnumerableExtensions.Count(1), (g, i) => new GigItem(g, i)));
        }

        public GigItem TestItem { get { return NextGigs[0]; } }
    }
}
