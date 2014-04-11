using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FestApp.Utils
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<TResult> Zip<T1, T2, TResult>(this IEnumerable<T1> fst, IEnumerable<T2> snd, Func<T1, T2, TResult> func)
        {
            using (var it1 = fst.GetEnumerator())
            using (var it2 = snd.GetEnumerator())
            {
                while (it1.MoveNext() && it2.MoveNext())
                {
                    yield return func(it1.Current, it2.Current);
                }
            }
        }

        public static IEnumerable<int> Count(int start)
        {
            var i = start;
            while (true)
            {
                yield return i;
                ++i;
            }
        }
    }
}
