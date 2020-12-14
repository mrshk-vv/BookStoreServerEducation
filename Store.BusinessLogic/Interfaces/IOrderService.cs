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
        public Task<IEnumerable<OrderModel>> GetOrdersAsync();
        public Task<IEnumerable<OrderModel>> GetOrdersAsync(PaginationQuery paginationFilter, OrderFilter filter);
        public Task<OrderModel> GetOrderAsync(int id);
        public Task<IEnumerable<OrderModel>> GetUserOrdersAsync();
        public Task PayOrderAsync(OrderModel order, string token);
        public Task<int> CreateOrderAsync(List<OrderItemModel> cart);
        public Task RemoveOrderAsync(string id);
    }
}
