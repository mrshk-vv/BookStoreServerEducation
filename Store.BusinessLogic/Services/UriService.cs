using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Store.BusinessLogic.Interfaces;
using Store.Shared.Pagination;

namespace Store.BusinessLogic.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetPagesUri(string route,string filter,PaginationQuery paginationQuery = null)
        {
            var uri = new Uri($"{_baseUri}{route}");

            if (paginationQuery is null)
            {
                return uri;
            }

            if (filter is null)
            {
                return uri;
            }


            var modifiedUri =
                QueryHelpers.AddQueryString(uri.ToString(),"pageNumber", paginationQuery.PageNumber.ToString());

            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", paginationQuery.PageSize.ToString());

            modifiedUri = $"{modifiedUri}&{filter}";

            return new Uri(modifiedUri);
        }
    }
}
