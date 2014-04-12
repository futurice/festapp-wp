using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FestApp.Models
{
    class Location
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string Type { get; set; }

        [JsonProperty("_id")]
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
