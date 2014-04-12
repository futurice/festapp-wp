using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FestApp.Models
{
    class Event
    {
        [JsonProperty("start_time")]
        public DateTimeOffset StartTime { get; set; }

        [JsonProperty("end_time")]
        public DateTimeOffset EndTime { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public int StarredCount { get; set; }

        [JsonProperty("_id")]
        public string Id { get; set; }

        public List<string> Artists { get; set; }

        public string Title { get; set; }
    }
}
