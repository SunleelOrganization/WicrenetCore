using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace System.Linq
{
   public static class WhereIfExtension
    {
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> sourse, bool condition, Expression<Func<T, bool>> predicate)
        {
            return condition ? sourse.Where(predicate) : sourse;
        }
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, bool> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }
    }
}
