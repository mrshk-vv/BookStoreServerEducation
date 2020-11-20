using System.Collections.Generic;
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
    public class OrderRepository : BaseEfRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {

            return await GetAllAsync();
            //return await _dbSet.Include(o => o.OrderItems)
            //    .ThenInclude(pe => pe.Order)
            //    .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrders(int skip, int pageSize)
        {
            return await GetAllAsync();
            //return await _dbSet
            //    .Include(o => o.OrderItems)
            //    .ThenInclude(oi => oi.Order).Skip(skip)
            //    .Take(pageSize)
            //    .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrders(int skip, int pageSize, OrderFilter filter)
        {
            return await GetAllAsync();
            //return await _dbSet
            //    .Include(o => o.OrderItems)
            //    .ThenInclude(oi => oi.Order)
            //    .Where(o => filter.Status.Any(s => s == o.Status.ToString()))
            //    .Skip(skip)
            //    .Take(pageSize)
            //    .ToListAsync();
        }

        public async Task<Order> GetOrderById(string id)
        {
            var curId = int.Parse(id);
            return await GetByIdAsync(curId);
        }

        public async Task<Order> CreateOrderAsync(Order model)
        {
            return await CreateAsync(model);
        }

        public async Task<Order> UpdateOrderAsync(Order model)
        {
            return await UpdateAsync(model);
        }

        public async Task RemoveOrderAsync(string id)
        {
            var order = await GetOrderById(id);
            await DeleteAsync(order);
        }

        public async Task<IEnumerable<Order>> GetUserOrdersById(string id)
        {
            return await _dbSet.AsNoTracking().AsQueryable().Where(o => o.UserId == id).ToListAsync();
        }
    }
}
