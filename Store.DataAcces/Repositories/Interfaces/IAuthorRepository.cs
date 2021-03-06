﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using Store.DataAccess.Entities;
using Store.Shared.Filters;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        public Task<IEnumerable<Author>> GetAuthorsAsync();
        public Task<IEnumerable<Author>> GetAuthorsAsync(int skip, int pageSize);
        public Task<IEnumerable<Author>> GetAuthorsAsync(int skip, int pageSize, AuthorFilter filter);
        public Task<Author> GetAuthorByIdAsync(string id);
        public Task<Author> GetAuthorByNameAsync(string name);
        public Task<Author> GetAuthorByNameAsync(string name, int authorId);
        public Task<Author> CreateAuthorAsync(Author entity);
        public Task<Author> RemoveAuthorAsync(Author entity);
        public Task DeleteAuthorAsync(Author entity);
        public Task<Author> UpdateAuthorAsync(Author entity);
    }
}
