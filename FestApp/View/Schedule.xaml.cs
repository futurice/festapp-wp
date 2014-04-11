using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using FestApp.Model.Json;
using FestApp.ViewModel;

namespace FestApp.View
{
    public partial class Schedule
    {
        public Schedule()
        {
            InitializeComponent();                        
        }

        private void LongListSelector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModelLocator = Application.Current.Resources["Locator"] as ViewModelLocator;
            if (viewModelLocator == null) {
                return;
            }

            if (e.AddedItems.Count == 0) {
                return;
            }

            var item = e.AddedItems[0] as LongListSelectorItem;
            if (item == null) return;
            viewModelLocator.Main.SelectedArtist = item.Item as Artist;
            ((LongListSelector)sender).SelectedItem = null;
        }
    }
}