using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Store.BusinessLogic.Models.Author;
using Store.Shared.Filters;
using Store.Shared.Pagination;

namespace Store.BusinessLogic.Interfaces
{
    public interface IAuthorService
    {
        public Task<IEnumerable<AuthorModel>> GetAuthorsAsync();
        public Task<IEnumerable<AuthorModel>> GetAuthorsAsync(PaginationQuery paginationFilter, AuthorFilter filter);
        public Task<AuthorModel> CreateAuthorAsync(AuthorModel model);
        public Task<AuthorModel> GetAuthorByIdAsync(string id);
        public Task<AuthorModel> UpdateAuthorAsync(AuthorModel model);
        public Task<AuthorModel> RemoveAuthorAsync(string id);
    }
}
