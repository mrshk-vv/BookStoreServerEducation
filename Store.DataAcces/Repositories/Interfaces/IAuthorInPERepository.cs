using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Store.DataAccess.Entities;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IAuthorInPERepository
    {
        public Task AddAuthorToPrintingEditionAsync(AuthorInPrintingEdition model);
        public Task AddAuthorsToPrintingEditionAsync(IEnumerable<AuthorInPrintingEdition> list);
        public Task UpdateAuthorsInPrintingEditionAsync(IEnumerable<AuthorInPrintingEdition> list, int printingEditionId);
        public Task UpdateInPrintingEditionByAuthorsAsync(IEnumerable<AuthorInPrintingEdition> list, int authorId);
        public Task UpdateAuthorInPrintingEditionAsync(AuthorInPrintingEdition model);
        public Task RemoveInPrintingEditionByAuthorsAsync(int authorId);

    }
}
