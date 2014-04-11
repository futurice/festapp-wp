/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:FestApp"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using System;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using FestApp.Service;

namespace FestApp.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        public static readonly Uri MainPageUri = new Uri("/View/MainPage.xaml", UriKind.Relative);
        public static readonly Uri SchedulePageUri = new Uri("/View/Schedule.xaml", UriKind.Relative);
        public static readonly Uri ArtistDetailPageUri = new Uri("/View/ArtistDetails.xaml", UriKind.Relative);
        public static readonly Uri ArtistsPageUri = new Uri("/View/Artists.xaml", UriKind.Relative);
        public static readonly Uri ArticleDetailsPageUri = new Uri("/View/ArticleDetails.xaml", UriKind.Relative);
        public static readonly Uri AboutPageUri = new Uri("/View/About.xaml", UriKind.Relative);
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            // Create run time view services and models
            SimpleIoc.Default.Register<DataService, DataService>();
            SimpleIoc.Default.Register<MainViewModel>();
       }
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
    }
}