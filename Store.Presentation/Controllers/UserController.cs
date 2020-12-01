using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Store.BusinessLogic.Interfaces;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services;
using Store.Presentation.Providers.Pagination;
using Store.Shared.Constants;
using Store.Shared.Filters;
using Store.Shared.Pagination;

namespace Store.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IUriService _uriService;
        private readonly EmailProvider _emailProvider;
        private readonly IConfiguration _configuration;
        public UserController(IAccountService accountService, IUriService uriService, EmailProvider emailProvider, IConfiguration configuration)
        {
            _accountService = accountService;
            _uriService = uriService;
            _emailProvider = emailProvider;
            _configuration = configuration;
        }

        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpGet(Constants.Routes.USERS_GET_ALL_ROUTE)]
        public async Task<IActionResult> GetAllUsers([FromQuery]PaginationQuery paginationQuery,[FromQuery] UsersFilter filter)
        {
            var usersResponse = await _accountService.GetUsersAsync();
        
            if (paginationQuery is null || paginationQuery.PageSize < 1 || paginationQuery.PageNumber < 1)
            {
                return Ok(new PagedResponse<UserModel>(usersResponse));
            }

            usersResponse = await _accountService.GetUsersAsync(paginationQuery, filter);

            var paginationResponse = PaginationProvider.CreatePaginatedResponse(_uriService,
                $"api/{ControllerContext.ActionDescriptor.ControllerName}/{Constants.Routes.USERS_GET_ALL_ROUTE}",
                paginationQuery,filter,usersResponse);
        
            return Ok(paginationResponse);
        
        }

        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpGet(Constants.Routes.USER_GET_ROUTE)]
        public async Task<IActionResult> GetUser([FromQuery]string id)
        {
            if (id is null)
            {
                var userModel = await _accountService.GetUserByIdAsync(id);
                return Ok(userModel);
            }

            return NotFound();
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost(Constants.Routes.USER_UPDATE_ROUTE)]
        public async Task<IActionResult> UpdateUser([FromBody]UserSingUpModel model)
        {
            if (ModelState.IsValid)
            {
                var userModel = await _accountService.UpdateUserAsync(model);
                return Ok(userModel);
            }

            return BadRequest();
        }

        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpPost(Constants.Routes.USER_DELETE_ROUTE)]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            await _accountService.DeleteUserAsync(id);
            return Ok();
        }

        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpPost(Constants.Routes.USER_BLOCK_ROUTE)]
        public async Task<IActionResult> ChangeUserBlockStatus(string id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            var userModel = await _accountService.ChangeUserBlockStatusAsync(id);
            return Ok(userModel);
        }
    }
}