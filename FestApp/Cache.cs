using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace FestApp
{
    public static class Cache
    {
        private const string _cacheFolder = "Cache";

        public static async Task<Stream> OpenFileForReadIfExtists(string path)
        {
            try
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                StorageFolder folder = await local.GetFolderAsync(_cacheFolder);
                StorageFile cacheFile = await folder.GetFileAsync(PathToFilename(path));
                return await cacheFile.OpenStreamForReadAsync();
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }

        public static async Task<Stream> OpenFileForWrite(string path)
        {
            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
            StorageFolder folder = await local.CreateFolderAsync(_cacheFolder, CreationCollisionOption.OpenIfExists);
            StorageFile cacheFile = await folder.CreateFileAsync(PathToFilename(path),
                CreationCollisionOption.ReplaceExisting);
            return await cacheFile.OpenStreamForWriteAsync();
        }

        private static string PathToFilename(string path)
        {
            return path.Replace('/', '_');
        }
    }
}
