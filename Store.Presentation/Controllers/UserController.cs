using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Store.BusinessLogic.Interfaces;
using Store.BusinessLogic.Models.Users;
using Store.Presentation.Providers.Pagination;
using Store.Shared.Constants;
using Store.Shared.Filters;
using Store.Shared.Pagination;

namespace Store.Presentation.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IUriService _uriService;
        private readonly IMapper _mapper;
        public UserController(IAccountService accountService, IMapper mapper, IUriService uriService)
        {
            _accountService = accountService;
            _mapper = mapper;
            _uriService = uriService;
        }

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

        [HttpGet(Constants.Routes.USER_GET_ROUTE)]
        public async Task<IActionResult> GetUser(string id)
        {
            if (id is null)
            {
                var userModel = await _accountService.GetUserByIdAsync(id);
                return Ok(new {user = userModel});
            }

            return NotFound();
        }

        [HttpPost(Constants.Routes.USER_DELETE_ROUTE)]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (id is null)
            {
                await _accountService.DeleteUserAsync(id);
                return Ok("User has been deleted");
            }

            return NotFound();
        }

        [HttpPost(Constants.Routes.USER_BLOCK_ROUTE)]
        public async Task<IActionResult> BlockUser(string id)
        {
            await _accountService.BlockUserAsync(id);
            return Ok("User has been blocked");
        }
    }
}