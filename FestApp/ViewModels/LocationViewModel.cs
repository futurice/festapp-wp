using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestApp.ViewModels
{
    public class LocationViewModel : ViewModelBase
    {
        public string Name { get; set; }
        public List<EventViewModel> Events { get; set; }

        public LocationViewModel(string name, List<EventViewModel> events)
        {
            Name = name;
            Events = events;
        }
    }
}
