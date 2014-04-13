using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.ObjectModel;

namespace FestApp.ViewModels
{
    public delegate void NewsItemLoadedListener(NewsViewModel.NewsItem item);

    public class NewsViewModel : ViewModelBase
    {
        // TODO merge this class with MainPage's NewsItem?
        public class NewsItem
        {
            public Models.NewsItem Model { get; private set; }

            public NewsItem() { }

            public NewsItem(Models.NewsItem newsItem)
            {
                Model = newsItem;
                Title = newsItem.Title;
                Time = MakeTimeString(newsItem.Time);
                Content = newsItem.Content;
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

            public string Title { get; set; }
            public string Time { get; set; }
            public string Content { get; set; }
            public int ListIndex { get; set; }

            public static async Task LoadSingle(string id, NewsItemLoadedListener listener)
            {
                try
                {
                    await API.News.UseCachedThenFreshData(result =>
                    {
                        foreach (Models.NewsItem item in result.Data)
                        {
                            if (item.Id == id)
                            {
                                listener(new NewsItem(item));
                            }
                        }
                    });
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Error loading artists, HANDLE! {0}", e);
                }
            }
        }

        public ObservableCollection<NewsItem> News { get; set; }

        public NewsViewModel()
        {
            News = new ObservableCollection<NewsItem>();
        }

        public async Task LoadData()
        {
            try
            {
                await API.News.UseCachedThenFreshData(result => PopulateNewsFromList(result.Data));
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error loading artists, HANDLE! {0}", e);
            }
        }

        private void PopulateNewsFromList(List<Models.NewsItem> list)
        {
            News.Clear();

            for (int i = 0; i < list.Count; i++)
            {
                News.Add(new NewsItem(list[i])
                {
                    ListIndex = i
                });
            }
        }
    }
}
