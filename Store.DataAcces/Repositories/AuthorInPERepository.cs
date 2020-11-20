using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;

namespace Store.DataAccess.Repositories
{
    public class AuthorInPERepository : BaseEfRepository<AuthorInPrintingEdition>,IAuthorInPERepository
    {
        public AuthorInPERepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<IEnumerable<AuthorInPrintingEdition>> GetAuthorsInPE(string id)
        {
            var curId = int.Parse(id);

            return await _dbSet.Where(a => a.AuthorId == curId).ToListAsync();
        }

        public async Task<IEnumerable<AuthorInPrintingEdition>> GetAuthorsInPEs(int skip, int pageSize)
        {
            return await _dbSet.Skip(skip).Take(pageSize).ToListAsync();
        }

        public async Task<AuthorInPrintingEdition> AddAuthorToPE(AuthorInPrintingEdition model)
        {
            await CreateAsync(model);

            return model;
        }

        public async Task AddAuthorsToPE(IEnumerable<AuthorInPrintingEdition> authors)
        {
            await AddRangeAsync(authors);
        }

        public async Task RemoveAuthorFromPE(AuthorInPrintingEdition model)
        {
            await DeleteAsync(model);
        }
        public async Task RemoveAuthorsFromPE(IEnumerable<AuthorInPrintingEdition> list)
        {
            await DeleteRangeAsync(list);
        }

        public async Task<AuthorInPrintingEdition> UpdateAuthorInPE(AuthorInPrintingEdition model)
        {
            await UpdateAsync(model);

            return model;
        }

        public async Task<List<AuthorInPrintingEdition>> UpdateAuthorsInPE(List<AuthorInPrintingEdition> list)
        {
            await UpdateRangeAsync(list);

            return list;
        }
    }
}
