using System;
using Store.Shared.Pagination;

namespace Store.BusinessLogic.Interfaces
{
    public interface IUriService
    {
        public Uri GetPagesUri(string route,string filter, PaginationQuery paginationQuery = null);
    }
}
