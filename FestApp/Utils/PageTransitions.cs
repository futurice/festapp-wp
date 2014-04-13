using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Phone.Controls;

namespace FestApp.Utils
{
    public static class PageTransitions
    {
        private static NavigationInTransition _turnstileInTransition = new NavigationInTransition()
        {
            Backward = new TurnstileTransition() { Mode = TurnstileTransitionMode.BackwardIn },
            Forward = new TurnstileTransition() { Mode = TurnstileTransitionMode.ForwardIn }
        };

        private static NavigationOutTransition _turnstileOutTransition = new NavigationOutTransition()
        {
            Backward = new TurnstileTransition() { Mode = TurnstileTransitionMode.BackwardOut },
            Forward = new TurnstileTransition() { Mode = TurnstileTransitionMode.ForwardOut }
        };

        public static void EnableTransitions(this PhoneApplicationPage page)
        {
            TransitionService.SetNavigationInTransition(page, _turnstileInTransition);
            TransitionService.SetNavigationOutTransition(page, _turnstileOutTransition);
        }
    }
}
