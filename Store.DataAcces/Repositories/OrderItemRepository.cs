using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;
using Store.Shared.Enums;

namespace Store.DataAccess.Repositories
{
    public class OrderItemRepository : BaseEfRepository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<IEnumerable<OrderItem>> GetItemsFromOrderAsync(int id)
        {
            return await _dbSet.Where(oi => oi.OrderId == id).ToListAsync();
        }

        public async Task<OrderItem> GetOrderItem(string id)
        {
            var curId = int.Parse(id);

            return await GetByIdAsync(curId);
        }

        public async Task<OrderItem> CreateOrderItemAsync(Order order, PrintingEdition edition)
        {
            OrderItem orderItem = new OrderItem
            {
                OrderId = order.Id,
                PrintingEditionId = edition.Id,
                Currency = edition.EditionCurrency,
                Amount = 1
            };

            return await CreateAsync(orderItem);
        }

        public async Task<OrderItem> AddItemToOrder(OrderItem model)
        {
            return await CreateAsync(model);
        }

        public async Task<OrderItem> UpdateItemInOrder(OrderItem model)
        {
            return await UpdateAsync(model);
        }

        public async Task RemoveItemFromOrder(OrderItem model)
        {
            throw new System.NotImplementedException();
        }
    }
}
