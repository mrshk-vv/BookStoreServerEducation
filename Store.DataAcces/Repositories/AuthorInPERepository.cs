using System;
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

        public async Task AddAuthorToPrintingEditionAsync(AuthorInPrintingEdition model)
        {
            await CreateAsync(model);
        }

        public async Task AddAuthorsToPrintingEditionAsync(IEnumerable<AuthorInPrintingEdition> list)
        {
            await _dbSet.AddRangeAsync(list);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAuthorsInPrintingEditionAsync(IEnumerable<AuthorInPrintingEdition> list, int printingEditionId)
        {
            _dbSet.RemoveRange(_dbSet.Where(ap => ap.PrintingEditionId == printingEditionId));
            await AddAuthorsToPrintingEditionAsync(list);
        }

        public async Task UpdateInPrintingEditionByAuthorsAsync(IEnumerable<AuthorInPrintingEdition> list, int authorId)
        {
            _dbSet.RemoveRange(_dbSet.Where(ap => ap.AuthorId == authorId));
            if (list is null)
            {
                await _context.SaveChangesAsync();
                return;
            }
            await AddAuthorsToPrintingEditionAsync(list);
        }

        public async Task UpdateAuthorInPrintingEditionAsync(AuthorInPrintingEdition model)
        {
            await UpdateAsync(model);
        }

        public async Task RemoveInPrintingEditionByAuthorsAsync(int authorId)
        {
            _dbSet.RemoveRange(_dbSet.Where(ap => ap.AuthorId == authorId));
            await _context.SaveChangesAsync();
        }
    }
}
