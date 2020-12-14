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

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _dbSet.Include("OrderItems.PrintingEdition")
                .Include("User")
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(int skip, int pageSize)
        {
            return await _dbSet.Include("OrderItems.PrintingEdition")
                .Include("User")
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(int skip, int pageSize, OrderFilter filter)
        {
            return await _dbSet.Include("OrderItems.PrintingEdition")
                .Include("User")
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetUserOrders(string id)
        {
            return await _dbSet.Include("OrderItems.PrintingEdition")
                .Where(o => o.UserId == id)
                .ToListAsync();
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<Order> CreateOrderAsync(Order model, Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
            model.PaymentId = payment.Id;
            await _dbSet.AddAsync(model);
            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<Order> UpdateOrder(Order order)
        {
            return await UpdateAsync(order);
        }

        public async Task AddItemsToOrder(List<OrderItem> items)
        {
            await _context.OrderItems.AddRangeAsync(items);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveOrderAsync(int id)
        {
            var order = await GetOrderById(id);
            await DeleteAsync(order);
        }

    }
}
