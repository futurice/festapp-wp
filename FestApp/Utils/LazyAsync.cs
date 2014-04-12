using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestApp.Utils
{
    class LazyAsync<TValue> where TValue : class
    {
        private TValue _val = null;
        private readonly AsyncLock _lock = new AsyncLock();
        private readonly Func<Task<TValue>> _factory;

        public LazyAsync(Func<Task<TValue>> factory)
        {
            _factory = factory;
        }

        public async Task<TValue> Get()
        {
            using (await _lock.LockAsync())
            {
                if (_val != null) { return _val; }
                _val = await _factory();
            }

            return _val;
        }
    }
}
