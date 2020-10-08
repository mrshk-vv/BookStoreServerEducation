using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> GetEntityAsync(T entity);
        public Task<T> GetEntityAsync(string id);
        public Task<T> CreateAsync(T entity);
        public Task<T> RemoveAsync(T entity);
        public Task<T> UpdateAsync(T entity);
    }
}
