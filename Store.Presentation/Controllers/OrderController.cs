using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.BusinessLogic.Interfaces;
using Store.Shared.Constants;

namespace Store.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;

        public OrderController(IOrderService orderService, ICartService cartService)
        {
            _orderService = orderService;
            _cartService = cartService;
        }

        [HttpGet(Constants.Routes.ORDERS_GET_ALL_ROUTE)]
        public async Task<IActionResult> GetOrders()
        {
            return Ok(await _orderService.GetOrdersAsync());
        }

        [Authorize(Roles = "Client", AuthenticationSchemes = "Bearer")]
        [HttpPost(Constants.Routes.ADD_ITEM_TO_ORDER)]
        public async Task<IActionResult> AddItemToOrder([FromQuery]string id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            return Ok(_cartService.AddItemToOrder(id));
        }

    }
}