using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FestApp.Models
{
    class Program
    {
        public string Title { get; set; }

        [JsonProperty("cover_image")]
        public string CoverImage { get; set; }

        public string Content { get; set; }

        public string Place { get; set; }

        [JsonProperty("_id")]
        public string Id { get; set; }
    }
}
