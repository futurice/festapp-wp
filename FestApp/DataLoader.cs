using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;

namespace FestApp
{
    public class DataLoader
    {
        public async Task<T> Load<T>(string apiPath)
        {
            return await LoadAsTask<T>(apiPath);
        }

        private Task<T> LoadAsTask<T>(string apiPath)
        {
            var task = new TaskCompletionSource<T>();
            WebClient web = new WebClient();

            web.DownloadStringCompleted += (object sender, DownloadStringCompletedEventArgs e) => {
                try
                {
                    string json = e.Result;
                    T result = JsonConvert.DeserializeObject<T>(json);
                    task.TrySetResult(result);
                }
                catch (Exception ex)
                {
                    task.TrySetException(ex);
                }
            };

            string url = Config.ServerUrl + apiPath;
            Debug.WriteLine("Load " + url);
            web.DownloadStringAsync(new Uri(url));
            return task.Task;
        }

        void web_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void test(object sender, OpenReadCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
