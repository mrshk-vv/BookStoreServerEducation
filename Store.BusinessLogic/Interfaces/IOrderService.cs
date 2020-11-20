using System.Collections.Generic;
using System.Threading.Tasks;
using Store.BusinessLogic.Models.Orders;
using Store.DataAccess.Entities;
using Store.Shared.Filters;
using Store.Shared.Pagination;

namespace Store.BusinessLogic.Interfaces
{
    public interface IOrderService
    {
        public Task<IEnumerable<Order>> GetOrdersAsync();
        public Task<IEnumerable<OrderModel>> GetOrdersAsync(PaginationQuery paginationFilter,OrderFilter filter);
        public Task<OrderModel> GetOrderAsync(string id);

        public Task<Order> CreateOrderAsync();
        public Task<bool> CheckUnpaidOrderExist();
        public Task<Order> GetLastUnpaidUserOrder();
        
        public Task<IEnumerable<Order>> GetUserOrders();
        public Task RemoveOrderAsync(string id);
        public Task<OrderModel> UpdateOrderAsync(OrderModel model);
    }
}
