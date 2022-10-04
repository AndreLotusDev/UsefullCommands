using System.Collections.Generic;
using System.Linq;

namespace Helpers
{
    public static class PaginationHelper
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int? index, int? offset)
        {
            if (index != null)
            {
                query = query.Skip(index.Value);
            }
            if (offset != null)
            {
                query = query.Take(offset.Value);
            }

            return query;
        }

        public static IEnumerable<T> Paginate<T>(this IEnumerable<T> list, int? index, int? offset)
        {
            if (index != null)
            {
                list = list.Skip(index.Value);
            }
            if (offset != null)
            {
                list = list.Take(offset.Value);
            }

            return list;
        }
    }
}
