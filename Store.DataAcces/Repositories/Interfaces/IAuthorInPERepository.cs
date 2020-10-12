using System.Collections.Generic;
using System.Threading.Tasks;
using Store.DataAccess.Entities;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IAuthorInPERepository
    {
        public Task<IEnumerable<AuthorInPrintingEdition>> GetAuthorsInPE(string id);
        public Task<IEnumerable<AuthorInPrintingEdition>> GetAuthorsInPEs(int skip, int pageSize);
        public Task<AuthorInPrintingEdition> AddAuthorToPE(AuthorInPrintingEdition model);
        public Task RemoveAuthorFromPE(AuthorInPrintingEdition model);
        public Task<AuthorInPrintingEdition> UpdateAuthorsInPE(AuthorInPrintingEdition model);
    }
}
