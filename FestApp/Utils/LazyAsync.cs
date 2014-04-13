using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FestApp.Utils
{
    class LazyAsync<TValue> where TValue : class
    {
        private readonly CancellationToken dummyToken = new CancellationToken(false);
        private TValue _val = null;
        private readonly AsyncLock _lock = new AsyncLock();
        private readonly Func<CancellationToken, Task<TValue>> _factory;

        public LazyAsync(Func<CancellationToken, Task<TValue>> factory)
        {
            _factory = factory;
        }

        public LazyAsync(Func<Task<TValue>> factory)
            : this(ct => factory())
        { }

        public bool HasValue { get { return _val != null; } }

        public async Task<TValue> Get(CancellationToken? ct = null)
        {
            using (await _lock.LockAsync())
            {
                if (_val != null) { return _val; }
                _val = await _factory(ct ?? dummyToken);
            }

            return _val;
        }
    }
}
