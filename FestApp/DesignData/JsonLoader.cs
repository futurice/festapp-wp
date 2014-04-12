using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Resources;
using Newtonsoft.Json;

namespace FestApp.DesignData
{
    public static class JsonLoader
    {
        public static List<Models.Artist> Artists()
        {
            return LoadJson<List<Models.Artist>>("artists");
        }

        public static List<Models.NewsItem> News()
        {
            return LoadJson<List<Models.NewsItem>>("news");
        }

        private static T LoadJson<T>(string filename)
        {
            Uri fileUri = new Uri(string.Format("/FestApp;component/DesignData/{0}.json", filename), UriKind.Relative);
            StreamResourceInfo json = Application.GetResourceStream(fileUri);
            using (StreamReader sr = new StreamReader(json.Stream))
            {
                string fileString = sr.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(fileString);
            }
        }
    }
}
