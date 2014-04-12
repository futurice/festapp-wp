using System.ComponentModel;
using System.Threading.Tasks;

namespace System.Windows.Threading
{
    /// <summary>
    /// A smart dispatcher system for routing actions to the user interface
    /// thread.
    /// </summary>
    public static class SmartDispatcher
    {
        /// <summary>
        /// A single Dispatcher instance to marshall actions to the user
        /// interface thread.
        /// </summary>
        private static Dispatcher _instance = Deployment.Current.Dispatcher;

        /// <summary>
        /// Backing field for a value indicating whether this is a design-time
        /// environment.
        /// </summary>
        private static bool _designer = DesignerProperties.IsInDesignTool;

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public static bool CheckAccess()
        {
            return _instance.CheckAccess();
        }

        /// <summary>
        /// Executes the specified delegate asynchronously on the user interface
        /// thread. If the current thread is the user interface thread, the
        /// dispatcher if not used and the operation happens immediately.
        /// </summary>
        /// <param name="a">A delegate to a method that takes no arguments and
        /// does not return a value, which is either pushed onto the Dispatcher
        /// event queue or immediately run, depending on the current thread.</param>
        public static void BeginInvoke(Action a)
        {
            // If the current thread is the user interface thread, skip the
            // dispatcher and directly invoke the Action.
            if (_instance.CheckAccess() || _designer == true)
            {
                a();
            }
            else
            {
                _instance.BeginInvoke(a);
            }
        }

        public static async Task InvokeAsync(Action a)
        {
            // If the current thread is the user interface thread, skip the
            // dispatcher and directly invoke the Action.
            if (_instance.CheckAccess() || _designer == true)
            {
                a();
            }
            else
            {
                await _instance.InvokeAsync(a);
            }
        }

        public static async Task<T> InvokeAsync<T>(Func<T> a)
        {
            // If the current thread is the user interface thread, skip the
            // dispatcher and directly invoke the Action.
            if (_instance.CheckAccess() || _designer == true)
            {
                return a();
            }
            else
            {
                return await _instance.InvokeAsync(a);
            }
        }
    }
}