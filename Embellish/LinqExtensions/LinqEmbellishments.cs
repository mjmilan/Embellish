using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embellish.LinqExtensions
{
    public static class LinqEmbellishments
    {
        public static IEnumerable<T> FirstNMatchingCondition<T>(this IEnumerable<T> original, int numberOfResults, Func<T, bool> predicate)
        {
            
            var count = original.Count(predicate);
            
            if (numberOfResults > count)
            {
                numberOfResults = count;
            }

            return original.Where(predicate).Take(numberOfResults);
        }

        public static IEnumerable<T> FirstN<T>(this IEnumerable<T> original, int numberOfResults)
        {
            var count = original.Count();

            if (numberOfResults > count)
            {
                numberOfResults = count;
            }

            return original.Take(numberOfResults);
        }

        public static IEnumerable<T> LastNMatchingCondition<T>(this IEnumerable<T> original, int numberOfResults, Func<T, bool> predicate)
        {
            var count = original.Count(predicate);

            if (numberOfResults > count)
            {
                numberOfResults = count;
            }

            int skip = count - numberOfResults;
            return original.Where(predicate).Skip(skip).Take(numberOfResults);
        }

        public static IEnumerable<T> LastN<T>(this IEnumerable<T> original, int numberOfResults)
        {
            var count = original.Count();

            if (numberOfResults > count)
            {
                numberOfResults = count;
            }

            int skip = count - numberOfResults;
            return original.Skip(skip).Take(numberOfResults);
        }


    }
}
