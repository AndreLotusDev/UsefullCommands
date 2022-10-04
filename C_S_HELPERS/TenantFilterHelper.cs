using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WH.Gateway.Domain.Entities;

namespace Helpers
{
    public static class TenantFilterHelper
    {
        /// <summary>
        /// Enforces the inclusion of a child property which contains the relevant information to determine whether the parent entity is allowed to be retrieved.
        /// A global query filter shall be applied to the ITenantSharable object (which is being included by this method).
        /// </summary>
        /// <typeparam name="T">Base Entity type</typeparam>
        public static IQueryable<T> IncludeTenantFilterableProperty<T>(this IQueryable<T> query, System.Linq.Expressions.Expression<Func<T, ITenantSharable>> filter) where T : class
        {
            return query.Include(filter);
        }
    }
}
