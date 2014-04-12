using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FestApp.Utils
{
    public static class EnumerableExtensions
    {
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
