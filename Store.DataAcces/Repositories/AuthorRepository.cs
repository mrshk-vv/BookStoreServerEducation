using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;

namespace Store.DataAccess.Repositories
{
    public class AuthorRepository : BaseEfRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Author>> GetAuthorsAsync()
        {
            return await GetAllAsync();
        }

        public async Task<Author> GetAuthorByIdAsync(string id)
        {
            var curId = int.Parse(id);
            return await GetByIdAsync(curId);
        }


        public async Task<Author> GetAuthorByNameAsync(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(a => a.Name == name);
        }

        public async Task<Author> CreateAuthorAsync(Author entity)
        {
            return await CreateAsync(entity);
        }

        public async Task<Author> RemoveAuthorAsync(Author entity)
        {
            entity.IsRemoved = true;
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAuthorAsync(Author entity)
        {
           await DeleteAsync(entity);
        }

        public async Task<Author> UpdateAuthorAsync(Author entity)
        {
            return await UpdateAsync(entity);
        }

    }
}
