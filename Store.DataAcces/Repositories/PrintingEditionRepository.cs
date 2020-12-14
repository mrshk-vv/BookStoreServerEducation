using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;
using Store.Shared.Filters;

namespace Store.DataAccess.Repositories
{
    public class PrintingEditionRepository : BaseEfRepository<PrintingEdition>, IPrintingEditionRepository
    {
        public PrintingEditionRepository(ApplicationContext context) : base(context)
        {
        }
        public async Task<IEnumerable<PrintingEdition>> GetEditionsAsync()
        {
            return await  _dbSet.Include("AuthorInPrintingEditions.Author")
                                .ToListAsync();
        }

        public async Task<IEnumerable<PrintingEdition>> GetEditionsAsync(int skip, int pageSize)
        {
            return await _dbSet.Include("AuthorInPrintingEditions.Author")
                               .Skip(skip)
                               .Take(pageSize)
                               .ToListAsync();
        }

        public async Task<IEnumerable<PrintingEdition>> GetEditionsAsync(int skip, int pageSize, PrintingEditionFilter filter)
        {
            var printingEditions = _dbSet.Include("AuthorInPrintingEditions.Author");

            if (!string.IsNullOrWhiteSpace(filter.SearchString))
            {
                printingEditions = printingEditions.Where(pe => pe.Title.ToLower().Contains(filter.SearchString.ToLower()) ||
                                             pe.Description.ToLower().Contains(filter.SearchString.ToLower()));
            }

            if (filter.Types != null)
            {
                printingEditions = printingEditions.Where(pe => filter.Types.Contains(pe.EditionType));
            }

            if (filter.MaxPrice > 0)
            {
                printingEditions =
                    printingEditions.Where(pe => pe.Price >= filter.MinPrice && pe.Price <= filter.MaxPrice);
            }

            if (filter.Currency != null)
            {
                printingEditions = printingEditions.Where(pe => pe.EditionCurrency == filter.Currency);
            }

            if (filter.Sort != null)
            {
                if ((bool) filter.Sort)
                {
                    printingEditions = printingEditions.OrderByDescending(pe => pe.Price);
                }
                else
                {
                    printingEditions = printingEditions.OrderBy(pe => pe.Price);
                }
            }

            printingEditions = printingEditions.Skip(skip).Take(pageSize);

            return await printingEditions.ToListAsync();
        }
        
        public async Task<PrintingEdition> GetEditionByIdAsync(string id)
        {
            int curId = int.Parse(id);

            return await _dbSet.Include("AuthorInPrintingEditions.Author")
                .SingleOrDefaultAsync(e => e.Id == curId);
        }

        public async Task<PrintingEdition> CreateEditionAsync(PrintingEdition entity)
        {
            return await CreateAsync(entity);
        }

        public async Task<PrintingEdition> RemoveEditionAsync(PrintingEdition entity)
        {
            entity.IsRemoved = true;
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteEditionAsync(PrintingEdition entity)
        {
            await DeleteAsync(entity);
        }

        public async Task<PrintingEdition> UpdateEditionAsync(PrintingEdition entity)
        {
            return await UpdateAsync(entity);
        }

        public async Task<PrintingEdition> GetEditionByTitle(string title)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Title.Equals(title));
        }

        public async Task<PrintingEdition> GetEditionByTitle(string title, int peId)
        {
            return await _dbSet.Include("AuthorInPrintingEditions.Author").FirstOrDefaultAsync(e => e.Title.Equals(title) && e.Id != peId);
        }
    }
}
