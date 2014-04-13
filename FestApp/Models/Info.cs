using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestApp.Models
{
    public class Info
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
        public int Place { get; set; }
        
        [JsonProperty("_id")]
        public string Id { get; set; }
    }
}
