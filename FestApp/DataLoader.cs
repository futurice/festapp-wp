using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace FestApp
{
    public class DataLoader
    {
        public async Task<T> Load<T>(string apiPath)
        {
            return await Load<T>(apiPath);
        }

        private Task<T> LoadAsTask<T>(string apiPath)
        {
            var task = new TaskCompletionSource<T>();
            WebClient web = new WebClient();

            web.DownloadStringCompleted += (object sender, DownloadStringCompletedEventArgs e) => {
                string json = e.Result;
                T result = JsonConvert.DeserializeObject<T>(json);
                task.TrySetResult(result);
            };

            web.DownloadStringAsync(new Uri(Config.ServerUrl + apiPath));
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
