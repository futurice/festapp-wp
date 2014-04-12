using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;

namespace FestApp
{
    public partial class ArtistPage : PhoneApplicationPage
    {
        // Constructor
        public ArtistPage()
        {
            InitializeComponent();

        }

        // When page is navigated to set data context to selected item in list
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string selectedIndex = "";
            if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex)) {
                int index = int.Parse(selectedIndex);
                DataContext = App.ViewModel.Items[index];
            }
        }
    }

    class DesignerArtist : ArtistViewModel
    {
        public DesignerArtist()
        {
            Name = "Test artist";
            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras et purus vel diam malesuada blandit eget sit amet lorem. Integer nisi sem, pulvinar id mollis sit amet, ultrices in ligula. Sed adipiscing, lectus vitae ultricies vehicula, eros nunc condimentum ligula, sit amet fermentum lectus massa ullamcorper lorem.";
            Photo = new BitmapImage(new Uri("/DesignData/BadFinance.jpg", UriKind.Relative));
        }
    }
}