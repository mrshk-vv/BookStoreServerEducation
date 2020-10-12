using System.Collections.Generic;
using System.Threading.Tasks;
using Store.BusinessLogic.Models.AuthorInPrintingEdition;

namespace Store.BusinessLogic.Interfaces
{
    public interface IAuthorInPEService
    {
        public Task<IEnumerable<AuthorInPEModel>> GetAuthorsInPE();
        public Task<IEnumerable<AuthorInPEModel>> GetAuthorInPE(int skip, int pageSize);
        public Task AddAuthorToPE(AuthorInPEModel model);
        public Task RemoveAuthorFromPE(AuthorInPEModel model);
        public Task UpdateAuthorsInPE(AuthorInPEModel model);
    }
}
