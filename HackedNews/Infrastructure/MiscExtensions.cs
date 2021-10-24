using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackedNews.Infrastructure
{
    public static class MiscExtensions
    {
        public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int N)
        {
            return source.Skip(Math.Max(0, source.Count() - N));
        }
    }
}
