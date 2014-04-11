using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;
using FestApp.Helpers;

namespace FestApp.Model.Json
{
    // ReSharper disable InconsistentNaming
    public class Artist
    {

        public string Id { get; set; }
        public string Name { get; set; }
        private string _picture;
        public string Picture { 
            get { return _picture; }
            set 
            { 
                _picture = value;
                var imageUri = new Uri("/Images/Artist/" + value.Substring(15), UriKind.Relative);
                PictureImageSource = new BitmapImage(imageUri);
            } 
        }
        public string Quotet { get; set; }
        public string Content { get; set; }
        public string Featured { get; set; }
        public string Status { get; set; }
        public string Stage { get { return _stage; } set { _stage = StringHelpers.StageNames(value); } }
        private string _stage;
        public string Day { get; set; }
        public ImageSource PictureImageSource { get; set; }

        [JsonProperty("time_start")]
        public double TimeStart { get; set; }

        [JsonProperty("time_stop")]
        public double TimeStop { get; set; }
        public DateTime Start { get { return TimeStamp.UnixTimeStampToDateTime(TimeStart); } }
        public string Start_s {
            get { return Start.ToString("HH:mm"); }
        }
        public string scheduleText {
            get { return Start_s + " - " + Name; }
        }
        public string Founded { get; set; }
        public string Genre { get; set; }
        public string Members { get; set; }
        public string Albums { get; set; }
        public string Highlights { get; set; }
        public string Youtube { get; set; }
        public string Spotify { get; set; }

        [JsonProperty("contact_info")]
        public string ContactInfo { get; set; }

        [JsonProperty("press_image")]
        public string PressImage { get; set; }
        public string Credits { get; set; }
        public string Place { get; set; }
    }
    // ReSharper restore InconsistentNaming
}
