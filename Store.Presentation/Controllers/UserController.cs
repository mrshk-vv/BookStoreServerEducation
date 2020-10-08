using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Store.BusinessLogic.Interfaces;
using Store.BusinessLogic.Models.Users;
using Store.Shared.Constants;

namespace Store.Presentation.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public UserController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet(Constants.Routes.USERS_GET_ALL_ROUTE)]
        public async Task<IEnumerable<UserModel>> GetAllUsers()
        {
            return await _accountService.GetUsers();
        }

        [HttpGet(Constants.Routes.USER_GET_ROUTE)]
        public async Task<IActionResult> GetUser(string id)
        {
            var userModel = await _accountService.GetUserByIdAsync(id);
            return Ok(userModel);
        }

        [HttpPost(Constants.Routes.USER_DELETE_ROUTE)]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _accountService.DeleteUserAsync(id);
            return Ok("User has been deleted");
        }

        [HttpPost(Constants.Routes.USER_BLOCK_ROUTE)]
        public async Task<IActionResult> BlockUser(string id)
        {
            await _accountService.BlockUserAsync(id);
            return Ok("User has been blocked");
        }
    }
}