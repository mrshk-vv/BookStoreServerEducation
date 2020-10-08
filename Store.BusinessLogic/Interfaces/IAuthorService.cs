using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Store.BusinessLogic.Models.Author;

namespace Store.BusinessLogic.Interfaces
{
    public interface IAuthorService
    {
        public Task<AuthorModel> CreateAuthorAsync(AuthorModel model);
        public Task<IEnumerable<AuthorModel>> GetAllAuthorsAsync();
        public Task<AuthorModel> GetAuthorByIdAsync(string id);
        public Task<AuthorModel> UpdateAuthorAsync(AuthorModel model);
        public Task<AuthorModel> RemoveAuthorAsync(string id);
    }
}
