using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FestApp
{
    public class ArtistViewModel : INotifyPropertyChanged
    {
        private string _name;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != _name) {
                    _name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        private string _description;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (value != _description) {
                    _description = value;
                    NotifyPropertyChanged("Description");
                }
            }
        }

        private BitmapImage _photo;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public BitmapImage Photo
        {
            get
            {
                return _photo;
            }
            set
            {
                if (value != _photo) {
                    _photo = value;
                    NotifyPropertyChanged("Photo");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler) {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}