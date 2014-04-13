using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestApp.ViewModels
{
    public class InfoViewModel : ViewModelBase
    {
        public class InfoItem
        {
            public InfoItem(Models.Info info, int listIndex)
            {
                Title = info.Title;
                ListIndex = listIndex;
            }
            public string Title { get; set; }
            public int ListIndex { get; set; }
        }

        public void PopulateInfo(List<Models.Info> models) 
        {
            var vmlist = models.
                Select((m, i) => new InfoItem(m, i)).
                ToList();

            SetVMProperty(() => Info, vmlist);
        }

        public async Task LoadData()
        {
            await API.Info.UseCachedThenFreshData((data) => PopulateInfo(data.Data));
        }

        public List<InfoItem> Info { get; set; }
    }
}
