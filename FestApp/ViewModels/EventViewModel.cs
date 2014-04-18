using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestApp.ViewModels
{
    public class EventViewModel : ViewModelBase
    {
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public double Duration
        {
            get
            {
                return (EndTime - StartTime).TotalHours;
            }
        }

        public EventViewModel(Models.Event ev) {
            Date = ev.StartTime.AddHours(-5).Date; // Events before 5am belong to the previous day
            Location = ev.Location;
            Name = string.Join(", ", ev.Artists);
            StartTime = ev.StartTime;
            EndTime = ev.EndTime;
        }
    }
}
