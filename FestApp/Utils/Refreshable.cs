using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace FestApp.Utils
{
    public class Refreshable<T> where T : class
    {
        private readonly AsyncLock _lock = new AsyncLock();
        private readonly LazyAsync<T> _cache;
        private readonly LazyAsync<T> _network;

        public Refreshable(string apiPath)
        {
            _cache = new LazyAsync<T>(() => DataLoader.Load<T>(apiPath, LoadSource.CACHE));
            _network = new LazyAsync<T>(() => DataLoader.Load<T>(apiPath, LoadSource.NETWORK));
        }

        public class DataResult
        {
            public readonly bool IsFresh;
            public readonly T Data;

            public DataResult(bool isFresh, T data)
            {
                IsFresh = isFresh;
                Data = data;
            }
        }

        public async Task<DataResult> GetCached()
        {
            using (await _lock.LockAsync())
            {
                if (_network.HasValue)
                {
                    return new DataResult(true, await _network.Get());
                }
                else
                {
                    return new DataResult(false, await _cache.Get());
                }
            }
        }

        public async Task<DataResult> GetLatest()
        {
            using (await _lock.LockAsync())
            {
                return new DataResult(true, await _network.Get());
            }
        }

        public Task UseCachedThenFreshData(Action<DataResult> action)
        {
            return UseCachedThenFreshData(r =>
                {
                    action(r);
                    return Task.FromResult(0);
                });
        }

        // Does heavy stuff in thread pool, then dispatches to UI
        public async Task UseCachedThenFreshData(Func<DataResult, Task> action)
        {
            await Task.Run(async () =>
            {
                DataResult result;
                try
                {
                    result = await GetCached();
                    await SmartDispatcher.InvokeAsync(() => action(result));
                    if (result.IsFresh) { return; }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Error loading from cache: {0}", e);
                }

                result = await GetLatest();
                await SmartDispatcher.InvokeAsync(() => action(result));
            });
        }
    }
}
