using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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

            if (DesignerProperties.IsInDesignTool)
            {
                ImageControl.Source = new BitmapImage(
                    new Uri("/Images/Artist/BadFinance.jpg", UriKind.Relative));
            }
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

        #region DPs

        public static DependencyProperty StretchProperty = DependencyProperty.Register(
            "Stretch", typeof(Stretch), typeof(AsyncImage), new PropertyMetadata(Stretch.Uniform));

        public Stretch Stretch
        {
            get { return ImageControl.Stretch; }
            set { ImageControl.Stretch = value; }
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

        #endregion

        private async void LoadImage()
        {
            if (DesignerProperties.IsInDesignTool) { return; }

            if (Source == null)
            {
                ImageControl.Source = null;
                return;
            }

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
