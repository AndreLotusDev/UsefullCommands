using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Helpers
{
    public static class SortingHelper
    {
        private static BindingFlags CaseInsensitiveBindingFlags { get; } = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance;

        public static IQueryable<T> Order<T>(this IQueryable<T> query, string[] orderByProps, bool descending = false)
        {
            if (orderByProps is null) return query;

            IOrderedQueryable<T> orderedQueryable = null;

            foreach (var prop in orderByProps)
            {
                var normalizedProp = prop.Replace("_", string.Empty);

                if (normalizedProp != null && typeof(T).GetProperties().Contains(normalizedProp))
                {
                    var propName = typeof(T).GetProperty(normalizedProp, CaseInsensitiveBindingFlags).Name;

                    if (orderedQueryable == null)
                    {
                        orderedQueryable = descending
                            ? query.OrderByDescending(r => EF.Property<object>(r, propName))
                            : query.OrderBy(r => EF.Property<object>(r, propName));
                    }
                    else
                    {
                        orderedQueryable = descending
                            ? orderedQueryable.ThenByDescending(r => EF.Property<object>(r, propName))
                            : orderedQueryable.ThenBy(r => EF.Property<object>(r, propName));
                    }
                }
            }

            return orderedQueryable ?? query;
        }

        public static IEnumerable<T> Order<T>(this IEnumerable<T> list, string[] orderByProps, bool descending = false) =>
            list.Order(orderByProps, descending);

        private static bool Contains(this IEnumerable<PropertyInfo> props, string propName)
        {
            return props.Select(p => p.Name.ToUpper()).Contains(propName.ToUpper());
        }
    }
}
