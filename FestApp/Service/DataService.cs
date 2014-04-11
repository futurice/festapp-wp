using System;
using System.Collections.ObjectModel;
using System.Net;
using Newtonsoft.Json;
using FestApp.Model.Json;

namespace FestApp.Service
{
    public class DataService
    {
        private const string _baseUrl = "http://festapp-server.herokuapp.com/api/";
        
        public async void GetArtists(Action<ObservableCollection<Artist>, Exception> callback)
        {
            const string artistDataUrl = _baseUrl + "artists";
            var request = (HttpWebRequest)WebRequest.Create(artistDataUrl);
            try
            {
                var response = (HttpWebResponse) await request.GetResponseAsync();
                // Read the response into a Stream object.
                var responseStream = response.GetResponseStream();
                string data;
                using (var reader = new System.IO.StreamReader(responseStream))
                {
                    data = reader.ReadToEnd();
                }
                responseStream.Close();
                callback(JsonConvert.DeserializeObject<ObservableCollection<Artist>>(data), null);
            }
            catch (Exception ex)
            {
                callback(null, ex);
            }
        }

        public async void GetNews(Action<ObservableCollection<News>, Exception> callback)
        {
            const string newsDataUrl = _baseUrl + "news";
            var request = (HttpWebRequest)WebRequest.Create(newsDataUrl);
            try
            {
                var response = (HttpWebResponse)await request.GetResponseAsync();
                // Read the response into a Stream object.
                var responseStream = response.GetResponseStream();
                string data;
                using (var reader = new System.IO.StreamReader(responseStream))
                {
                    data = reader.ReadToEnd();
                }
                responseStream.Close();
                callback(JsonConvert.DeserializeObject<ObservableCollection<News>>(data), null);
            }
            catch (Exception ex)
            {
                callback(null, ex);
            }
        }
    }
}
