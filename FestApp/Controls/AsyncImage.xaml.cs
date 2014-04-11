using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace FestApp.Controls
{
    public partial class AsyncImage : UserControl
    {
        public static Func<object, CancellationToken, Task<ImageSource>> ImageFactory;

        private CancellationTokenSource _cts;

        public AsyncImage()
        {
            InitializeComponent();

            Loaded += AsyncImage_Loaded;
            Unloaded += AsyncImage_Unloaded;
        }

        void AsyncImage_Loaded(object sender, RoutedEventArgs e)
        {
            LoadImage();
        }

        void AsyncImage_Unloaded(object sender, RoutedEventArgs e)
        {
            CancelLoad();
            ImageControl.Source = null;
        }

        private void CancelLoad()
        {
            if (_cts != null) { _cts.Cancel(); }
        }

        public static DependencyProperty SourceProperty = DependencyProperty.Register(
            "Source", typeof(object), typeof(AsyncImage), new PropertyMetadata(null, SourceChanged));

        public object Source
        {
            get { return GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        private static void SourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((AsyncImage)d).SourceChanged();
        }

        private void SourceChanged()
        {
            CancelLoad();
            LoadImage();
        }

        private async void LoadImage()
        {
            try
            {
                _cts = new CancellationTokenSource();
                var token = _cts.Token;
                var image = await ImageFactory(Source, token);

                if (!token.IsCancellationRequested)
                {
                    ImageControl.Source = image;
                }
            }
            catch (TaskCanceledException) { }
            catch (Exception e)
            {
                Debug.WriteLine("Image loading failed: {0}", e);
            }
        }


    }
}
