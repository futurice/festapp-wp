using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace FestApp.Models
{
    public class NewsItem
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        public string Title { get; set; }

        public string Image { get; set; }

        [JsonProperty("teaser_text")]
        public string TeaserText { get; set; }

        public string Content { get; set; }

        public DateTimeOffset Time { get; set; }

        public string Status { get; set; }
    }
}
