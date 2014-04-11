using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using FestApp.ViewModel;

namespace FestApp.View
{
    public class BasePage : PhoneApplicationPage
    {
        public BasePage(double systemTrayOpacity = 1)
        {
            SystemTrayOpacity = systemTrayOpacity;

            NavigationInTransition navigateInTransition = new NavigationInTransition();
            navigateInTransition.Backward = new TurnstileTransition { Mode = TurnstileTransitionMode.BackwardIn };
            navigateInTransition.Forward = new TurnstileTransition { Mode = TurnstileTransitionMode.ForwardIn };

            NavigationOutTransition navigateOutTransition = new NavigationOutTransition();
            navigateOutTransition.Backward = new TurnstileTransition { Mode = TurnstileTransitionMode.BackwardOut };
            navigateOutTransition.Forward = new TurnstileTransition { Mode = TurnstileTransitionMode.ForwardOut };
            TransitionService.SetNavigationInTransition(this, navigateInTransition);
            TransitionService.SetNavigationOutTransition(this, navigateOutTransition);

            Loaded += BasePage_Loaded;
        }

        public double SystemTrayOpacity { get; private set; }

        void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!DesignerProperties.IsInDesignTool) {
                var viewModelLocator = Application.Current.Resources["Locator"] as ViewModelLocator;
                SystemTray.Opacity = SystemTrayOpacity;
                if (viewModelLocator != null) {
                    var updateProgressIndicator = SystemTray.ProgressIndicator;
                    if (updateProgressIndicator == null) {
                        updateProgressIndicator = new ProgressIndicator();

                        updateProgressIndicator.IsVisible = true;

                        Binding textBinding = new Binding(MainViewModel.LoadingMessagePropertyName) { Source = viewModelLocator.Main };
                        BindingOperations.SetBinding(updateProgressIndicator, ProgressIndicator.TextProperty, textBinding);

                        SystemTray.SetProgressIndicator(this, updateProgressIndicator);

                        Binding binding = new Binding(MainViewModel.IsLoadingPropertyName) { Source = viewModelLocator.Main };
                        BindingOperations.SetBinding(updateProgressIndicator, ProgressIndicator.IsIndeterminateProperty, binding);
                        SystemTray.IsVisible = true;
                    }
                }
            }
        }
    }
}