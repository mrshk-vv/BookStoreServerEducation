using System.Collections.Generic;
using System.Threading.Tasks;
using Store.DataAccess.Entities;
using Store.Shared.Filters;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        public Task<IEnumerable<Order>> GetOrdersAsync();
        public Task<IEnumerable<Order>> GetOrdersAsync(int skip, int pageSize);
        public Task<IEnumerable<Order>> GetOrdersAsync(int skip, int pageSize,OrderFilter filter);
        public Task<IEnumerable<Order>> GetUserOrders(string id);
        public Task<Order> GetOrderById(int id);
        public Task<Order> CreateOrderAsync(Order model, Payment payment);
        public Task UpdatePaymentTransactionId(int paymentId, string transactionId);
        public Task<Order> UpdateOrder(Order order);
        public Task AddItemsToOrder(List<OrderItem> items);
        public Task RemoveOrderAsync(int id);



    }
}
