using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartLibrary.Helpers
{
    public static class PaginationHelper
    {
        public static IQueryable<T> ApplySearch<T>(IQueryable<T> query, string search, Func<T, bool> searchCondition)
        {
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(item => searchCondition(item)).AsQueryable();
            }
            return query;
        }

        public static IQueryable<T> ApplySorting<T>(IQueryable<T> query, string sortOrder, Func<IQueryable<T>, string, IQueryable<T>> sortExpression)
        {
            // Ensure sorting is applied correctly
            return sortExpression(query, sortOrder);
        }


        public static IQueryable<T> ApplyPagination<T>(IQueryable<T> query, int page, int pageSize)
        {
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}