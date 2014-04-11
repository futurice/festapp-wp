using System.Collections;
using System.Collections.ObjectModel;
using FestApp.Helpers;
using FestApp.Model.Json;

namespace FestApp.Model
{
    public class PerformanceDay : IEnumerable 
    {
        public ObservableCollection<Artist> Performances { get; set; }
        public string Name { get { return _name; }
            set { _name = StringHelpers.DayNumberToDay(value); }
        }
        private string _name;
        public IEnumerator GetEnumerator()
        {
            return Performances.GetEnumerator();
        }
    }
}
