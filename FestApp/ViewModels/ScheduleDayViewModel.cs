using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestApp.ViewModels
{
    public class ScheduleDayViewModel : ViewModelBase
    {
        public DateTime Day { get; set; }
        public List<LocationViewModel> Locations { get; set; }

        public int FirstHour { get; set; }
        public int NumOfHours { get; set; }

        public List<DateTimeOffset> Hours
        {
            get
            {
                return Enumerable.Repeat<int>(0, NumOfHours)
                                 .Select((_, idx) => (idx + FirstHour) % 24)
                                 .Select(hour => DateTimeOffset.Parse(string.Format("{0}:00", hour)))
                                 .ToList();
            }
        }

        private bool _selected;
        public bool Selected {
            get { return _selected; }
            set { _selected = value; NotifyChanged(() => Selected); }
        }

        public ScheduleDayViewModel(DateTime day, List<EventViewModel> events)
        {
            Locations = events.GroupBy(ev => ev.Location)
                              .Select(g => new LocationViewModel(g.Key, g.ToList()))
                              .ToList();

            Day = day;

            var orderedEvents = events.OrderBy(ev => ev.StartTime);

            var firstEvent = orderedEvents.First();
            FirstHour = firstEvent.StartTime.Hour-1;
            if (FirstHour < 0) FirstHour += 24;

            var lastEvent = orderedEvents.Last();
            var lastHour = lastEvent.EndTime.Hour;
            NumOfHours = lastHour - FirstHour;
            if (NumOfHours < 0) NumOfHours += 24;
        }
    }
}
