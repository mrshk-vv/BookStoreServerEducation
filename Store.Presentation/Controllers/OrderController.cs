using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.BusinessLogic.Interfaces;
using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Models.Payment;
using Store.Presentation.Providers.Pagination;
using Store.Shared.Constants;
using Store.Shared.Filters;
using Store.Shared.Pagination;

namespace Store.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IUriService _uriService;

        public OrderController(IOrderService orderService, IUriService uriService)
        {
            _orderService = orderService;
            _uriService = uriService;
        }

        [Authorize(Roles = "Admin",AuthenticationSchemes = "Bearer")]
        [HttpGet(Constants.Routes.ORDERS_GET_ALL_ROUTE)]
        public async Task<IActionResult> GetOrders([FromQuery]PaginationQuery paginationQuery, [FromQuery]OrderFilter filter)
        {
            var editionsResponse = await _orderService.GetOrdersAsync();

            if (paginationQuery is null || paginationQuery.PageSize < 1 || paginationQuery.PageNumber < 1)
            {
                return Ok(new PagedResponse<OrderModel>(editionsResponse));
            }

            editionsResponse = await _orderService.GetOrdersAsync(paginationQuery, filter);

            var paginationResponse = PaginationProvider.CreatePaginatedResponse(_uriService,
                $"api/{ControllerContext.ActionDescriptor.ControllerName}/{Constants.Routes.ORDERS_GET_ALL_ROUTE}",
                paginationQuery, filter,
                editionsResponse);


            return Ok(paginationResponse);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet(Constants.Routes.ORDER_GET_ROUTE)]
        public async Task<IActionResult> GetOrder(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            return Ok(await _orderService.GetOrderAsync((int)id));
        }

        [Authorize(Roles="Client", AuthenticationSchemes = "Bearer")]
        [HttpGet(Constants.Routes.ORDERS_CLIENT_GET)]
        public async Task<IActionResult> GetClientOrders()
        {
            return Ok(await _orderService.GetUserOrdersAsync());
        }

        [Authorize(Roles = "Client", AuthenticationSchemes = "Bearer")]
        [HttpPost(Constants.Routes.ORDER_CREATE_ROUTE)]
        public async Task<IActionResult> CreateOrder(List<OrderItemModel> cart)
        {
            return Ok(await _orderService.CreateOrderAsync(cart));
        }

        [Authorize(Roles = "Client", AuthenticationSchemes = "Bearer")]
        [HttpPost(Constants.Routes.ORDER_PAY)]
        public async Task<IActionResult> PayOrder([FromBody] PaymentModel model)
        {
            if (model is null)
            {
                return BadRequest();
            }

            await _orderService.PayOrderAsync(model.Order, model.Token);
            return Ok();
        }

    }
}