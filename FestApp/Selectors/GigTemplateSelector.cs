using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace FestApp.Selectors
{
    public class GigTemplateSelector : DataTemplateSelectorBase
    {
        public DataTemplate FirstTemplate { get; set; }
        public DataTemplate RestTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var gig = (ViewModels.MainPage.GigItem)item;

            return (gig.Index == 1) ? FirstTemplate : RestTemplate;
        }
    }
}
