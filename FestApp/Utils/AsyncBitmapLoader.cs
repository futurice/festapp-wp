using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace FestApp.Utils
{
    class AsyncBitmapLoader
    {
        public static Task<BitmapImage> LoadFromStreamAsync(Stream stream)
        {
            return new AsyncBitmapLoader().ImageForStreamAsync(stream);
        }

        private readonly TaskCompletionSource<bool> _tcs = new TaskCompletionSource<bool>();

        private async Task<BitmapImage> ImageForStreamAsync(Stream stream)
        {
            var image = await SmartDispatcher.InvokeAsync(() =>
                {
                    var i = new BitmapImage() { CreateOptions = BitmapCreateOptions.BackgroundCreation };
                    i.ImageOpened += ImageOpened;
                    i.ImageFailed += ImageFailed;
                    i.SetSource(stream);
                    return i;
                });

            // Sometimes the events don't fire, so lets poll the height periodically
            while (_tcs.Task.Wait(TimeSpan.FromMilliseconds(50)))
            {
                var height = await SmartDispatcher.InvokeAsync(() => image.PixelHeight);
                if (height > 0) { break; }
            }

            return image;
        }

        private void Unregister(object sender)
        {
            var image = (BitmapImage)sender;
            image.ImageOpened -= ImageOpened;
            image.ImageFailed -= ImageFailed;
        }

        private void ImageFailed(object sender, System.Windows.ExceptionRoutedEventArgs e)
        {
            if (e.ErrorException != null)
            {
                // Don't check against null, because it's really an error situation
                _tcs.SetException(e.ErrorException);
            }
            else
            {
                _tcs.SetException(new Exception("Image failed to load for unknown reason"));
            }
            Unregister(sender);
        }

        private void ImageOpened(object sender, System.Windows.RoutedEventArgs e)
        {
            // Don't check against null, because it's really an error situation
            _tcs.SetResult(true);
            Unregister(sender);
        }
    }
}
