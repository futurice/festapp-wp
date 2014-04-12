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
        CACHE,
        STATIC_CACHE
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
                    return await LoadFromNet<T>(apiPath);
                case LoadSource.CACHE:
                    return await LoadFromCache<T>(apiPath);
                case LoadSource.STATIC_CACHE:
                    return await LoadFromStaticCache<T>(apiPath);
                default:
                    throw new Exception("Unknown source");
            }
        }

        public async Task<T> LoadFromCache<T>(string apiPath)
        {
            try
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                StorageFolder folder = await local.GetFolderAsync(cacheFolder);
                StorageFile cacheFile = await folder.GetFileAsync(ApiPathToFilename(apiPath));

                using (var stream = await cacheFile.OpenStreamForReadAsync())
                using (var reader = new StreamReader(stream))
                using (JsonReader jsonReader = new JsonTextReader(reader))
                {
                    var serializer = JsonSerializer.Create();
                    return await Task.Run(() => serializer.Deserialize<T>(jsonReader)); // TODO remove await
                }
            }
            catch (FileNotFoundException) { }

            Debug.WriteLine("Not cached: " + apiPath);
            return await LoadFromStaticCache<T>(apiPath);
        }

        public async Task<T> LoadFromStaticCache<T>(string apiPath)
        {
            try
            {
                var cachedResource = Application.GetResourceStream(new Uri("Cache/" + ApiPathToFilename(apiPath), UriKind.Relative));

                using (StreamReader reader = new StreamReader(cachedResource.Stream))
                using (JsonReader jsonReader = new JsonTextReader(reader))
                {
                    var serializer = JsonSerializer.Create();
                    return await Task.Run(() => serializer.Deserialize<T>(jsonReader)); // TODO remove await
                }
            }
            catch (IOException) { }

            Debug.WriteLine("Warning: No static cache for " + apiPath);
            return default(T);
        }

        private string ApiPathToFilename(string apiPath)
        {
            return apiPath.Replace('/', '_');
        }

        public async Task<T> LoadFromNet<T>(string apiPath)
        {
            string url = Config.ServerUrl + Config.ServerApiPath + apiPath;

            Debug.WriteLine("Loading " + url);

            string json = await httpClient.GetStringAsync(url);
            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
            StorageFolder folder = await local.CreateFolderAsync(cacheFolder, CreationCollisionOption.OpenIfExists);
            StorageFile cacheFile = await folder.CreateFileAsync(ApiPathToFilename(apiPath),
                CreationCollisionOption.ReplaceExisting);

            using (var stream = await cacheFile.OpenStreamForWriteAsync())
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(json);
            }

            Debug.WriteLine("Cached " + apiPath);

            return JsonConvert.DeserializeObject<T>(json);

            /*
            using (Stream stream = await httpClient.GetStreamAsync(url))
            using (StreamReader reader = new StreamReader(stream))
            using (JsonReader jsonReader = new JsonTextReader(reader))
            {
                var serializer = JsonSerializer.Create();
                return await Task.Run(() => serializer.Deserialize<T>(jsonReader)); // TODO remove await
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
