using System.Collections.Generic;
using System.Threading.Tasks;
using Store.BusinessLogic.Models.Orders;
using Store.DataAccess.Entities;

namespace Store.BusinessLogic.Interfaces
{
    public interface ICartService
    {
        public Task<OrderItemModel> AddItemToOrder(string id);
        public Task<OrderItemModel> UpdateItemInOrder(OrderItem model);
        public Task RemoveItemFromOrder(string id);
    }
}
