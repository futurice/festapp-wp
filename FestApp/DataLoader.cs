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
using FestApp.Utils;

namespace FestApp
{
    public enum LoadSource
    {
        NETWORK,
        CACHE,
        STATIC_CACHE
    }

    public static class DataLoader
    {
        private static HttpClient httpClient = new HttpClient();

        private static Dictionary<string, LazyAsync<BitmapImage>> _images = new Dictionary<string, LazyAsync<BitmapImage>>();

        public static async Task<BitmapImage> LoadImage(string path, CancellationToken? ct = null)
        {
            LazyAsync<BitmapImage> la;
            lock (_images)
            {
                if (!_images.TryGetValue(path, out la))
                {
                    la = new LazyAsync<BitmapImage>(_ => LoadImageRaw(path, _));
                    _images[path] = la;
                }
            }

            var bitmap = await la.Get(ct);

            // Don't keep bitmaps in memory (a weak ref would be better...)
            lock (_images)
            {
                _images.Remove(path);
            }

            return bitmap;
        }

        private static async Task<BitmapImage> LoadImageRaw(string path, CancellationToken ct)
        {
            string url = Config.ServerUrl + path;
            Debug.WriteLine("Loading image " + url);

            Stream stream;
            while ((stream = await Cache.OpenFileForReadIfExtists(path)) == null)
            {
                using (var response = await httpClient.GetAsync(url, ct))
                using (var diskStream = await Cache.OpenFileForWrite(path))
                {
                    await response.Content.CopyToAsync(diskStream);
                }                
            }

            return await Utils.AsyncBitmapLoader.LoadFromStreamAsync(stream);
        }

        public static async Task<T> Load<T>(string apiPath, LoadSource source)
        {
            var path = Config.ServerApiPath + apiPath;

            switch (source)
            {
                case LoadSource.NETWORK:
                    return await LoadFromNet<T>(path);
                case LoadSource.CACHE:
                    return await LoadFromCache<T>(path);
                case LoadSource.STATIC_CACHE:
                    return await LoadFromStaticCache<T>(path);
                default:
                    throw new Exception("Unknown source");
            }
        }

        private static async Task<T> LoadFromCache<T>(string path)
        {
            var cacheStream = await Cache.OpenFileForReadIfExtists(path);
            if (cacheStream == null)
            {
                Debug.WriteLine("Not cached: " + path);
                return await LoadFromStaticCache<T>(path);
            }

            using (cacheStream)
            {
                return LoadJson<T>(cacheStream);
            }
        }

        private static Task<T> LoadFromStaticCache<T>(string path)
        {
            throw new NotImplementedException("No static cache");

            // TODO implement
            Debug.WriteLine("Warning: No static cache for " + path);
            return Task.FromResult(default(T));
        }

        // Load from net and store to cache
        private static async Task<T> LoadFromNet<T>(string path)
        {
            string url = Config.ServerUrl + path;

            Debug.WriteLine("Loading " + url);

            using (var netStream = await httpClient.GetStreamAsync(url))
            using (var diskStream = await Cache.OpenFileForWrite(path))
            {
                await netStream.CopyToAsync(diskStream);
            }

            using (var cacheStream = await Cache.OpenFileForReadIfExtists(path))
            {
                return LoadJson<T>(cacheStream);
            }
        }

        private static T LoadJson<T>(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            using (JsonReader jsonReader = new JsonTextReader(reader))
            {
                var serializer = JsonSerializer.Create();
                return serializer.Deserialize<T>(jsonReader);
            }
        }
    }
}
