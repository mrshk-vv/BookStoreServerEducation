using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.BusinessLogic.Interfaces;
using Store.BusinessLogic.Models.Author;
using Store.Shared.Constants;

namespace Store.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpPost(Constants.Routes.AUTHOR_CREATE_ROUTE)]
        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> CreateAuthor([FromBody]AuthorModel model)
        {
            return Ok(new { author = await _authorService.CreateAuthorAsync(model) });
        }

        [HttpGet(Constants.Routes.AUTHOR_GET_ROUTE)]
        public async Task<IActionResult> GetAuthor(string id)
        {
            return Ok(new { author = await _authorService.GetAuthorByIdAsync(id) });
        }

        [HttpGet(Constants.Routes.AUTHORS_GET_ALL_ROUTE)]
        public async Task<IActionResult> GetAllAuthors()
        {
            return Ok(new { authorList = await _authorService.GetAllAuthorsAsync() });
        }

        [HttpPost(Constants.Routes.AUTHOR_REMOVE_ROUTE)]
        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> RemoveAuthor(string id)
        {
            return Ok(new { author = await _authorService.RemoveAuthorAsync(id) });
        }

        [HttpPost(Constants.Routes.AUTHOR_UPDATE_ROUTE)]
        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateAuthor([FromBody]AuthorModel model)
        {
            return Ok(new { author = await _authorService.UpdateAuthorAsync(model) });
        }
}
}