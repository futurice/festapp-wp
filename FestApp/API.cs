using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FestApp.Utils;

namespace FestApp
{
    static class API
    {
        public static readonly Refreshable<List<Models.Artist>> Artists =
            new Refreshable<List<Models.Artist>>("artists");

        public static readonly Refreshable<List<Models.NewsItem>> News =
            new Refreshable<List<Models.NewsItem>>("news");

        public static readonly Refreshable<List<Models.Event>> Events =
            new Refreshable<List<Models.Event>>("events");

        public static readonly Refreshable<List<Models.Location>> Locations =
            new Refreshable<List<Models.Location>>("locations");

        public static readonly Refreshable<List<Models.Info>> Info =
            new Refreshable<List<Models.Info>>("info");

    }
}
