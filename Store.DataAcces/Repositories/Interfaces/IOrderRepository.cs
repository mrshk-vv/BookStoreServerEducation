using System.Collections.Generic;
using System.Threading.Tasks;
using Store.DataAccess.Entities;
using Store.Shared.Filters;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        public Task<IEnumerable<Order>> GetOrders();
        public Task<IEnumerable<Order>> GetOrders(int skip, int pageSize);
        public Task<IEnumerable<Order>> GetOrders(int skip, int pageSize,OrderFilter filter);
        public Task<IEnumerable<Order>> GetUserOrdersById(string id);
        public Task<Order> GetOrderById(string id);

        public Task<Order> CreateOrderAsync(Order model);
        public Task<Order> UpdateOrderAsync(Order model);
        public Task RemoveOrderAsync(string id);



    }
}
