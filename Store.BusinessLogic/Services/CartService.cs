using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Store.BusinessLogic.Interfaces;
using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Models.PrintingEdition;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Interfaces;
using Store.Shared.Common;
using Store.Shared.Constants;
using Store.Shared.Enums;

namespace Store.BusinessLogic.Services
{
    public class CartService: ICartService
    {
        private readonly IOrderService _orderService;
        private readonly IOrderItemRepository _itemRepository;
        private readonly IPrintingEditionRepository _editionRepository;
        private readonly IMapper _mapper;

        public CartService(IOrderService orderService, IOrderItemRepository itemRepository, IMapper mapper, IPrintingEditionRepository editionRepository)
        {
            _orderService = orderService;
            _itemRepository = itemRepository;
            _mapper = mapper;
            _editionRepository = editionRepository;
        }

        public async Task<OrderItemModel> AddItemToOrder(string id)
        {
            var edition = await _editionRepository.GetEditionByIdAsync(id);

            if (edition is null)
            {
                throw new ServerException(Constants.Errors.EDITION_NOT_FOUND, Enums.Errors.NotFound);
            }

            if (await _orderService.CheckUnpaidOrderExist())
            {
                var order = await _orderService.CreateOrderAsync();
                var newOrderItem = await _itemRepository.CreateOrderItemAsync(order, edition);
                return _mapper.Map<OrderItem, OrderItemModel>(await _itemRepository.AddItemToOrder(newOrderItem));
            }

            var userOrder = await _orderService.GetLastUnpaidUserOrder();
            var userOrderItems = await _itemRepository.GetItemsFromOrderAsync(userOrder.Id);

            var orderItem = await ItemContainsInOrder(userOrderItems, edition.Id);
            if (orderItem is null)
            {
                orderItem = await _itemRepository.CreateOrderItemAsync(userOrder, edition);
                return _mapper.Map<OrderItem, OrderItemModel>(await _itemRepository.AddItemToOrder(orderItem));
            }

            orderItem.Amount += 1;
            return _mapper.Map<OrderItem, OrderItemModel>(await _itemRepository.UpdateItemInOrder(orderItem));

        }

        public async Task<OrderItemModel> UpdateItemInOrder(OrderItem model)
        {
            return _mapper.Map<OrderItem, OrderItemModel>(await _itemRepository.UpdateItemInOrder(model));
        }

        public async Task RemoveItemFromOrder(string id)
        {
            var orderItem = _itemRepository.GetOrderItem(id);
            if (orderItem is null)
            {
                throw new ServerException(Constants.Errors.ORDER_ITEM_EMPTY, Enums.Errors.BadRequest);
            }
        }

        private async Task<OrderItem> ItemContainsInOrder(IEnumerable<OrderItem> list, int id)
        {
            foreach (var item in list)
            {
                if (item.PrintingEditionId == id)
                {
                    return item;
                }
            }

            return null;
        }
    }
}
