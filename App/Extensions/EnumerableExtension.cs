using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.Extensions
{
    public static class EnumerableExtension
    {
        #region ForEach()
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            var i = 0;

            foreach (var item in source)
            {
                action(item, i++);
            }

            return source;
        }
        #endregion

        #region None()
        public static bool None<T>(this IEnumerable<T> source)
        {
            return !source.Any();
        }
        #endregion
    }
}
