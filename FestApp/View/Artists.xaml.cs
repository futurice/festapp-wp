using System.Windows;
using FestApp.ViewModel;

namespace FestApp.View
{
    public partial class Artists
    {
        public Artists()
        {
            InitializeComponent();            
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var viewModelLocator = Application.Current.Resources["Locator"] as ViewModelLocator;
            if (viewModelLocator != null) {
                var mainVM = viewModelLocator.Main;
                mainVM.SelectedArtist = null;
            }
        }
    }
}