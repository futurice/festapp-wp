using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FestApp.Models
{
    public class NewsItem
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string TeaserText { get; set; }
        public string Content { get; set; }
        public string Time { get; set; }
        public string Status { get; set; }
    }
}
