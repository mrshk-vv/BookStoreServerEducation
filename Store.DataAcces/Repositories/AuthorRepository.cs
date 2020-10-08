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
    public class AuthorRepository : BaseEfRepository<Author>, IAuthorRepository<Author>
    {
        public AuthorRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<Author> GetEntityAsync(Author entity)
        {
            return await _dbSet.FindAsync(entity.Id);
        }

        public async Task<Author> GetEntityAsync(string id)
        {
            int curId = int.Parse(id);
            return await _dbSet.FindAsync(curId);
        }

        public async Task<Author> CreateAsync(Author entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Author> RemoveAsync(Author entity)
        {
            entity.IsRemoved = true;
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Author> UpdateAsync(Author entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Author> GetAuthorByNameAsync(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(a => a.Name == name);
        }
    }
}
