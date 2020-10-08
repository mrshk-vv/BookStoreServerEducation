using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using Store.DataAccess.Entities;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IAuthorRepository<T> : IBaseRepository<T> where T : class
    {
        public Task<Author> GetAuthorByNameAsync(string name);
    }
}
