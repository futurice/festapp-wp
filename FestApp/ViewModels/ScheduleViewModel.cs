using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestApp.ViewModels
{
    public class ScheduleViewModel : ViewModelBase
    {
        public List<ScheduleDayViewModel> Days { get; set; }

        private ScheduleDayViewModel _selectedDay;
        public ScheduleDayViewModel SelectedDay
        {
            get { return _selectedDay; }
            set { 
                _selectedDay = value;

                foreach (var day in Days) {
                    day.Selected = (day == _selectedDay);
                }

                NotifyChanged(() => SelectedDay);
            }
        }

        public async Task LoadData()
        {
            try {
                await API.Events.UseCachedThenFreshData(result => PopulateDays(result.Data));
            } catch (Exception e) {
                Debug.WriteLine("Error loading events, HANDLE! {0}", e);
            }

            /*var evs = new List<Models.Event>() {
                CreateMockEvent("2014-04-18 22:15", "Eka artisti", "Eka stage"),
                CreateMockEvent("2014-04-18 22:30", "Toka artisti", "Toka stage"),
                CreateMockEvent("2014-04-18 23:15", "Kolmas artisti", "Eka stage"),
                CreateMockEvent("2014-04-19 01:30", "Neljäs artisti", "Eka stage"),
                CreateMockEvent("2014-04-19 21:15", "Viides artisti", "Eka stage"),
                CreateMockEvent("2014-04-19 23:15", "Kuudes artisti", "Kolmas stage"),
            };

            PopulateDays(evs);*/
        }

        /*private Models.Event CreateMockEvent(string time, string artistName, string stage)
        {
            return new Models.Event() {
                StartTime = DateTime.Parse(time),
                EndTime = DateTime.Parse(time).AddMinutes(45),
                Artists = new List<string>() { artistName },
                Location = stage
            };
        }*/

        private void PopulateDays(List<Models.Event> list)
        {
            Days = list.Select(ev => new EventViewModel(ev))
                       .GroupBy(vm => vm.Date)
                       .Select(g => new ScheduleDayViewModel(g.Key, g.ToList()))
                       .ToList();

            NotifyChanged(() => Days);
        }
    }
}
