using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Store.BusinessLogic.Interfaces;
using Store.BusinessLogic.Models.Orders;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Interfaces;
using Store.Shared.Enums;
using Store.Shared.Filters;
using Store.Shared.Pagination;

namespace Store.BusinessLogic.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPaymentRepository _paymentRepository;

        private readonly IHttpContextAccessor _contextAccessor;
        private readonly string _userEmail;
        private readonly IMapper _mapper;
        public OrderService(IOrderRepository orderRepository, IMapper mapper, IHttpContextAccessor contextAccessor, IUserRepository userRepository, IPaymentRepository paymentRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _paymentRepository = paymentRepository;
            _mapper = mapper;
            _userEmail = contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _orderRepository.GetOrders();
        }

        public async Task<IEnumerable<OrderModel>> GetOrdersAsync(PaginationQuery paginationFilter, OrderFilter filter)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Order> CreateOrderAsync()
        {
            var user = await _userRepository.GetUserByEmailAsync(_userEmail);

            var paymentId = await _paymentRepository.CreatePayment();

            Order order = new Order
            {
                UserId = user.Id,
                Date = DateTime.Now,
                PaymentId = paymentId,
                Status = Enums.Status.Unpaid
            };

            return await _orderRepository.CreateOrderAsync(order);

        }

        public async Task<bool> CheckUnpaidOrderExist()
        {
            var orders = await GetUserOrders();

            foreach (var order in orders)
            {
                if (order.Status == Enums.Status.Unpaid)
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<Order> GetLastUnpaidUserOrder()
        {
            var orders = await GetUserOrders();

            foreach (var order in orders)
            {
                if (order.Status == Enums.Status.Unpaid)
                {
                    return order;
                }
            }

            return null;
        }

        public async Task<OrderModel> GetOrderAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetUserOrders()
        {
            var user = await _userRepository.GetUserByEmailAsync(_userEmail);

            return await _orderRepository.GetUserOrdersById(user.Id);
        }

        public async Task RemoveOrderAsync(string id)
        {
            
        }

        public async Task<OrderModel> UpdateOrderAsync(OrderModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}
