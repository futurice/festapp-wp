using System.Windows;
using BF.SL;
using FestApp.ViewModel;

namespace FestApp.View
{
    public partial class ArtistDetails
    {
        public ArtistDetails()
        {
            InitializeComponent();
            Loaded += ArtistDetailsLoaded;
        }

        void ArtistDetailsLoaded(object sender, RoutedEventArgs e)
        {
            var viewModelLocator = Application.Current.Resources["Locator"] as ViewModelLocator;
            if (viewModelLocator == null) return;
            var htb = new HtmlToRichTextBox(viewModelLocator.Main.SelectedArtist.Content);
            htb.ApplyHtmlToRichTextBox(RtbArtistDetails);
        }
    }
}