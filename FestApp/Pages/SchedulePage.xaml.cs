using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using FestApp.ViewModels;
using System.Diagnostics;
using System.Windows.Data;

namespace FestApp.Pages
{
    public partial class SchedulePage : PhoneApplicationPage
    {
        private const int HOUR_WIDTH = 300;

        private ScheduleViewModel _vm;

        private DependencyProperty SelectedDayDP = DependencyProperty.Register("SelectedDay", typeof(ViewModels.ScheduleDayViewModel), typeof(SchedulePage), new PropertyMetadata(new PropertyChangedCallback((o, a) => {
            Debug.WriteLine("New selectedDay");
        })));

        public SchedulePage()
        {
            InitializeComponent();

            TimeToMarginConverter.HourWidth = HOUR_WIDTH;
            DurationToWidthConverter.HourWidth = HOUR_WIDTH;

            Loaded += SchedulePage_Loaded;

            DataContext = _vm = new ScheduleViewModel();
        }

        async void SchedulePage_Loaded(object sender, RoutedEventArgs e)
        {
            await _vm.LoadData();

            SelectDay(_vm.Days.First());
        }

        public static Uri GetPageUri()
        {
            return new Uri(string.Format("/Pages/SchedulePage.xaml"), UriKind.Relative);
        }

        private void Day_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var button = sender as Button;
            var dayVM = button.DataContext as ScheduleDayViewModel;

            SelectDay(dayVM);
        }

        private void SelectDay(ScheduleDayViewModel day)
        {
            TimeToMarginConverter.FirstHour = day.FirstHour;
            EventItemsControl.Width = day.NumOfHours * HOUR_WIDTH;

            _vm.SelectedDay = day;

            EventScrollViewer.ScrollToHorizontalOffset(0);
        }
    }

    public class TimeToMarginConverter : IValueConverter
    {
        public static int FirstHour;
        public static int HourWidth;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var time = (DateTimeOffset)value;
            var leftMargin = ((time.Hour + 24 - FirstHour) % 24 + (float)time.Minute/60) * HourWidth;

            return new Thickness(leftMargin, 0, 0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DurationToWidthConverter : IValueConverter
    {
        public static int HourWidth;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var duration = (double)value;
            return duration * HourWidth;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}