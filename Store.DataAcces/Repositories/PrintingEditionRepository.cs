using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;

namespace Store.DataAccess.Repositories
{
    public class PrintingEditionRepository : BaseEfRepository<PrintingEdition>, IPrintingEditionRepository<PrintingEdition>
    {
        public PrintingEditionRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<IEnumerable<PrintingEdition>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<PrintingEdition> GetEntityAsync(PrintingEdition entity)
        {
            return await _dbSet.FindAsync(entity.Id);
        }

        public async Task<PrintingEdition> GetEntityAsync(string id)
        {
            int curId = int.Parse(id);
            return await _dbSet.FindAsync(curId);
        }

        public async Task<PrintingEdition> CreateAsync(PrintingEdition entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<PrintingEdition> RemoveAsync(PrintingEdition entity)
        {
            entity.IsRemoved = true;
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<PrintingEdition> UpdateAsync(PrintingEdition entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<PrintingEdition> GetEdititonByTitle(string title)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Title == title);
        }
    }
}
