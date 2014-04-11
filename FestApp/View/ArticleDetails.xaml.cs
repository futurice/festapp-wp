using System.Windows;
using BF.SL;
using FestApp.ViewModel;

namespace FestApp.View
{
    public partial class ArticleDetails
    {
        public ArticleDetails()
        {
            InitializeComponent();
            Loaded += ArticleDetailsLoaded;
            
        }

        void ArticleDetailsLoaded(object sender, RoutedEventArgs e)
        {
            var viewModelLocator = Application.Current.Resources["Locator"] as ViewModelLocator;
            if (viewModelLocator == null) return;
            var htb = new HtmlToRichTextBox(viewModelLocator.Main.SelectedArticle.content);
            htb.ApplyHtmlToRichTextBox(Content);
        }
    }
}