using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Store.BusinessLogic.Interfaces;
using Store.BusinessLogic.Models.Orders;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Interfaces;
using Store.Shared.Common;
using Store.Shared.Enums;
using Store.Shared.Filters;
using Store.Shared.Pagination;
using Stripe;
using Order = Store.DataAccess.Entities.Order;
using OrderItem = Store.DataAccess.Entities.OrderItem;

namespace Store.BusinessLogic.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;

        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper, IHttpContextAccessor contextAccessor, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _contextAccessor = contextAccessor;
            _mapper = mapper;

        }

        public async Task<IEnumerable<OrderModel>> GetOrdersAsync()
        {
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderModel>>(await _orderRepository.GetOrdersAsync());
        }

        public async Task<IEnumerable<OrderModel>> GetOrdersAsync(PaginationQuery paginationFilter, OrderFilter filter)
        {
            int skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
            if (filter is null)
            {
                var orderNoFilter =
                    _mapper.Map<IEnumerable<Order>, IEnumerable<OrderModel>>(
                        await _orderRepository.GetOrdersAsync(skip, paginationFilter.PageSize));

                return orderNoFilter;
            }

            var orderFilter =
                _mapper.Map<IEnumerable<Order>, IEnumerable<OrderModel>>(
                    await _orderRepository.GetOrdersAsync(skip, paginationFilter.PageSize, filter));

            return orderFilter;
        }

        public async Task PayOrderAsync(OrderModel order, string token)
        {
            string userEmail = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            var paidOrder = _mapper.Map<OrderModel, Order>(order);

            var customer = await new CustomerService()
                .CreateAsync(new CustomerCreateOptions
                {
                    Email = userEmail
                });

            var charges = new ChargeService();

            var charge = await charges.CreateAsync(new ChargeCreateOptions
            {
                Amount = (long) (order.TotalAmount * 100),
                Currency = "usd",
                Source = token
            });

            if (charge.Status == "succeeded")
            {
                paidOrder.Status = Enums.Status.Paid;
                await _orderRepository.UpdateOrder(paidOrder);
            }
            else
            {
                throw new ServerException("Order payment error", Enums.Errors.InternalServerError);
            }
        }

        public async Task<int> CreateOrderAsync(List<OrderItemModel> cart)
        {
            string userEmail = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var user = await _userRepository.GetUserByEmailAsync(userEmail);

            decimal totalAmount = 0;

            for (int i = 0; i < cart.Count; i++)
            {
                totalAmount += cart[i].Amount * cart[i].PrintingEdition.Price;
            }

            var payment = new Payment()
            {
                TransactionId = Guid.Empty
            };

            Order order = new Order
            {
                UserId = user.Id,
                Date = DateTime.Now,
                TotalAmount = totalAmount,
                Status = Enums.Status.Unpaid
            };

            order = await _orderRepository.CreateOrderAsync(order, payment);

            var orderItems = _mapper.Map<List<OrderItemModel>, List<OrderItem>>(cart);

            for (int i = 0; i < orderItems.Count; i++)
            {
                orderItems[i].OrderId = order.Id;
            }

            await _orderRepository.AddItemsToOrder(orderItems);

            return order.Id;
        }

        public async Task<OrderModel> GetOrderAsync(int id)
        {
            var order = _mapper.Map<Order, OrderModel>(await _orderRepository.GetOrderById(id));
            if (order is null)
            {
                throw new ServerException("Order not exist", Enums.Errors.NotFound);
            }

            return order;
        }

        public async Task<IEnumerable<OrderModel>> GetUserOrdersAsync()
        {
            string userEmail = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var user = await _userRepository.GetUserByEmailAsync(userEmail);

            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderModel>>(await _orderRepository.GetUserOrders(user.Id));
        }

        public async Task RemoveOrderAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
