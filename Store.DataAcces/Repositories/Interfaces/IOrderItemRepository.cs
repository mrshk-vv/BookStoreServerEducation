using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Store.DataAccess.Entities;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IOrderItemRepository
    {
        public Task<IEnumerable<OrderItem>> GetItemsFromOrderAsync(int id);
        public Task<OrderItem> GetOrderItem(string id);
        public Task<OrderItem> CreateOrderItemAsync(Order order, PrintingEdition edition);
        public Task<OrderItem> AddItemToOrder(OrderItem model);
        public Task<OrderItem> UpdateItemInOrder(OrderItem model);
        public Task RemoveItemFromOrder(OrderItem model);
    }
}
