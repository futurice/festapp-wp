using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FestApp.Utils
{
    // "Scoped" async locking, inspired by
    // http://blogs.msdn.com/b/pfxteam/archive/2012/02/12/10266988.aspx
    public class AsyncLock
    {
        private readonly AsyncSemaphore _semaphore;
        private readonly Releaser _releaser;

        public struct Releaser : IDisposable
        {
            private readonly AsyncLock _toRelease;

            internal Releaser(AsyncLock toRelease) { _toRelease = toRelease; }

            public void Dispose()
            {
                if (_toRelease != null)
                    _toRelease._semaphore.Release();
            }
        }

        public AsyncLock()
        {
            _semaphore = new AsyncSemaphore(1);
            _releaser = new Releaser(this);
        }

        public Releaser? TryLock()
        {
            if (_semaphore.TryWait())
            {
                return _releaser;
            }
            return null;
        }

        public async Task<Releaser> LockAsync()
        {
            await _semaphore.WaitAsync();
            return _releaser;
        }
    }
}