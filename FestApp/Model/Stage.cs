using System.Collections;
using System.Collections.ObjectModel;

namespace FestApp.Model
{
    public class Stage : IEnumerable 
    {
        public string Name { get; set; }
        public ObservableCollection<PerformanceDay> PerformanceDays { get; set; }
        public IEnumerator GetEnumerator()
        {
            return PerformanceDays.GetEnumerator();
        }
    }
}
