﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;
using Store.Shared.Filters;

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

        public async Task<IEnumerable<Author>> GetAuthorsAsync(int skip, int pageSize)
        {
            return await _dbSet.Include("AuthorInPrintingEditions.PrintingEdition")
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Author>> GetAuthorsAsync(int skip, int pageSize, AuthorFilter filter)
        {
            var authorList = await _dbSet.Include("AuthorInPrintingEditions.PrintingEdition")
                .Where(a => a.Name.ToLower().Contains(filter.Name.ToLower()))
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            return authorList;
        }

        public async Task<Author> GetAuthorByIdAsync(string id)
        {
            var curId = int.Parse(id);
            return await _dbSet.Include("AuthorInPrintingEditions.PrintingEdition")
                .SingleOrDefaultAsync(a => a.Id == curId);
        }

        public async Task<Author> GetAuthorByNameAsync(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(a => a.Name.Equals(name));
        }

        public async Task<Author> GetAuthorByNameAsync(string name, int authorId)
        {
            return await _dbSet.FirstOrDefaultAsync(a => a.Name == name && a.Id != authorId);
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
