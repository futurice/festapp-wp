using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace FestApp
{
    public class DataLoader
    {
        public async Task<T> Load<T>(string apiPath)
        {
            WebClient web = new WebClient();
            //web.OpenReadCompleted += new OpenReadCompletedEventHandler(RequestComplete);
            web.OpenReadAsync(new Uri(Config.ServerUrl + apiPath));
        }
    }
}
