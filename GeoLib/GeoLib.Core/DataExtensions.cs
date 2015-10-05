using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;

namespace GeoLib.Core
{
    public static class DataExtensions
    {
        public static IEnumerable<T> ToFullyLoaded<T>(this IQueryable<T> query)
        {
            return query.ToArray().ToList();
        }

        public static IEnumerable<T> ToFullyLoaded<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.ToList();
        }

        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params Expression<Func<T,object>>[] includeProperties)
        {
            if (includeProperties == null) return query;

            foreach(var include in includeProperties)
            {
                query = query.Include(include);
            }

            return query;
        }
    }
}
