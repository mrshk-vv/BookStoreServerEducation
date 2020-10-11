using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Store.BusinessLogic.Interfaces;
using Store.Shared.Pagination;
using System.Linq;
using System.Web;

namespace Store.Presentation.Providers.Pagination
{
    public class PaginationProvider
    {
        public static PagedResponse<T> CreatePaginatedResponse<T>(IUriService uriService, string route,
            PaginationQuery pagination,object filter, IEnumerable<T> collection)
        {
            string filtrationMode = null;

            if (filter != null)
            {
                filtrationMode = GetQueryString(filter);
            }

            var nextPage = pagination.PageNumber >= 1
                ? uriService.GetPagesUri(route, filtrationMode, new PaginationQuery(pagination.PageNumber + 1,
                        pagination.PageSize))
                    .ToString()
                : null;

            var previousPage = pagination.PageNumber -1 >= 1
                ? uriService.GetPagesUri(route, filtrationMode, new PaginationQuery(pagination.PageNumber - 1,
                        pagination.PageSize))
                    .ToString()
                : null;

            return new PagedResponse<T>
            {
                Data = collection,
                PageNumber = pagination.PageNumber >= 1 ? pagination.PageNumber : (int?)null,
                PageSize = pagination.PageSize >= 1 ? pagination.PageSize : (int?)null,
                NextPage = collection.Any() ? nextPage : null,
                PreviousPage = previousPage
            };
        }
        
        private static string GetQueryString(object obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                where p.GetValue(obj, null) != null
                select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return String.Join("&", properties.ToArray());
        }
    }
}
