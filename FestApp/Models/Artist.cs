using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace FestApp.Models
{
    public class Artist
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Picture { get; set; }

        public string Quote { get; set; }

        public string Content { get; set; }

        public string Featured { get; set; }

        public string Status { get; set; }

        public string Stage { get; set; }

        public string Day { get; set; }

        [JsonProperty("time_start")]
        public DateTimeOffset TimeStart { get; set; }

        [JsonProperty("time_stop")]
        public DateTimeOffset TimeStop { get; set; }

        public string Founded { get; set; }

        public string Genre { get; set; }

        public List<string> Members { get; set; }

        public List<string> Albums { get; set; }

        public string Highlights { get; set; }

        public string Youtube { get; set; }

        public string Spotify { get; set; }

        public string ContactInfo { get; set; }

        public string PressImage { get; set; }

        public string Credits { get; set; }

        public string Place { get; set; }
    }
}
