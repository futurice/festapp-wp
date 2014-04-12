using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace FestApp.Utils
{
    public static class LoadingIndicatorHelper
    {
        public static IDisposable StartLoading(string text)
        {
            var handle = new LoadingHandle(text);
            lock (_handles)
            {
                _handles.Add(handle);
            }
            UpdateState();
            return handle;
        }

        public static void Initialize(PhoneApplicationFrame rootFrame)
        {
            _rootFrame = rootFrame;
            rootFrame.Navigated += rootFrame_Navigated;
        }

        private static void rootFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            UpdateState();
        }

        private class LoadingHandle : IDisposable
        {
            public readonly string Text;

            public LoadingHandle(string text)
            {
                Text = text;
            }

            public void Dispose()
            {
                lock (_handles)
                {
                    _handles.Remove(this);
                    LoadingIndicatorHelper.UpdateState();
                }
            }
        }

        private static readonly List<LoadingHandle> _handles = new List<LoadingHandle>();
        private static readonly ProgressIndicator _progressIndicator = new ProgressIndicator();
        private static PhoneApplicationFrame _rootFrame;
        private static LoadingHandle _currentHandle;

        private static async void UpdateState()
        {
            lock (_handles)
            {
                if (_handles.Contains(_currentHandle)) { return; }
                _currentHandle = _handles.LastOrDefault();
            }

            if (_currentHandle == null)
            {
                _progressIndicator.IsVisible = false;
                _progressIndicator.IsIndeterminate = false;
            }
            else
            {
                await EnsureIndicatorSet();
                _progressIndicator.Text = _currentHandle.Text;
                _progressIndicator.IsVisible = true;
                _progressIndicator.IsIndeterminate = true;
            }
        }

        private static async Task EnsureIndicatorSet()
        {
            try
            {
                // Prevent unnecessary exceptions by checking height:
                // Height is 0 when in not-yet-laid-out state
                if (_rootFrame.ActualHeight > 0)
                {
                    SystemTray.ProgressIndicator = _progressIndicator;
                    return;
                }
            }
            catch (InvalidOperationException ex)
            {
                Debug.WriteLine("Error setting progress indicator, retrying: {0}", ex);
            }

            await TaskEx.Delay(50);
            await EnsureIndicatorSet();
        }

    }
}
