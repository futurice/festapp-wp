using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Threading;
using System.Net.Http;
using Windows.Storage;

namespace FestApp
{
    public enum LoadSource
    {
        NETWORK,
        CACHE
    }

    public class DataLoader
    {
        private const string cacheFolder = "Cache";

        private HttpClient httpClient;

        public DataLoader()
        {
            httpClient = new HttpClient();
        }

        public async Task<BitmapImage> LoadImage(string path, CancellationToken? ct = null)
        {
            string url = Config.ServerUrl + path;
            Debug.WriteLine("Loading image " + url);

            using (Stream stream = await httpClient.GetStreamAsync(url))
            {
                BitmapImage bitmapImage = new BitmapImage() { CreateOptions = BitmapCreateOptions.BackgroundCreation };
                bitmapImage.SetSource(stream);
                return bitmapImage;
            }
        }

        public async Task<T> Load<T>(string apiPath, LoadSource source)
        {
            switch (source)
            {
                case LoadSource.NETWORK:
                    return await LoadFromNet<T>(apiPath) ;
                case LoadSource.CACHE:
                    return await LoadFromCache<T>(apiPath);
                default:
                    throw new Exception("Unknown source");
            }
        }

        public async Task<T> LoadFromCache<T>(string apiPath)
        {
            var cachedResource = Application.GetResourceStream(new Uri("Cache/" + apiPath, UriKind.Relative));
            using (StreamReader reader = new StreamReader(cachedResource.Stream))
            {
                string json = await reader.ReadToEndAsync();
                return JsonConvert.DeserializeObject<T>(json);
            }
        }

        public async Task<T> LoadFromNet<T>(string apiPath)
        {
            string url = Config.ServerUrl + Config.ServerApiPath + apiPath;

            Debug.WriteLine("Loading " + url);

            string json = await httpClient.GetStringAsync(url);
            /*
            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
            StorageFolder folder = await local.CreateFolderAsync(cacheFolder, CreationCollisionOption.OpenIfExists);
            StorageFile cacheFile = await local.CreateFileAsync("cache_" + apiPath, CreationCollisionOption.ReplaceExisting);

            using (var stream
            */
            return JsonConvert.DeserializeObject<T>(json);

            /*
            using (Stream stream = await httpClient.GetStreamAsync(url))
            using (StreamReader reader = new StreamReader(stream))
            using (JsonReader jsonReader = new JsonTextReader(reader))
            {
                var serializer = JsonSerializer.Create();
                return await Task.Run(() => serializer.Deserialize<T>(jsonReader));
            }
            */
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
