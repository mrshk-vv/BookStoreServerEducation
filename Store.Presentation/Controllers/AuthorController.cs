using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.BusinessLogic.Interfaces;
using Store.BusinessLogic.Models.Author;
using Store.BusinessLogic.Models.PrintingEdition;
using Store.Presentation.Providers.Pagination;
using Store.Shared.Constants;
using Store.Shared.Filters;
using Store.Shared.Pagination;

namespace Store.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IUriService _uriService;
        public AuthorController(IAuthorService authorService, IUriService uriService)
        {
            _authorService = authorService;
            _uriService = uriService;
        }

        [HttpPost(Constants.Routes.AUTHOR_CREATE_ROUTE)]
        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> CreateAuthor([FromBody]AuthorModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(new { author = await _authorService.CreateAuthorAsync(model) });
            }

            return BadRequest();
        }

        [HttpGet(Constants.Routes.AUTHOR_GET_ROUTE)]
        public async Task<IActionResult> GetAuthor(string id)
        {
            if (id is null)
            {
                return NotFound();
            }
            return Ok(new { author = await _authorService.GetAuthorByIdAsync(id) });
        }

        [HttpGet(Constants.Routes.AUTHORS_GET_ALL_ROUTE)]
        public async Task<IActionResult> GetAuthors([FromQuery]PaginationQuery paginationQuery,[FromQuery]AuthorFilter filter)
        {
            var authorResponse = await _authorService.GetAuthorsAsync();

            if (paginationQuery is null || paginationQuery.PageSize < 1 || paginationQuery.PageNumber < 1)
            {
                return Ok(new PagedResponse<AuthorModel>(authorResponse));
            }

            authorResponse = await _authorService.GetAuthorsAsync(paginationQuery, filter);

            var paginationResponse = PaginationProvider.CreatePaginatedResponse(_uriService,
                $"api/{ControllerContext.ActionDescriptor.ControllerName}/{Constants.Routes.AUTHORS_GET_ALL_ROUTE}",
                paginationQuery, filter,
                authorResponse);

            return Ok(paginationResponse);
        }

        [HttpPost(Constants.Routes.AUTHOR_REMOVE_ROUTE)]
        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> RemoveAuthor(string id)
        {
            if (id is null)
            {
                return NotFound();
            }

            return Ok(new { author = await _authorService.RemoveAuthorAsync(id) });
        }

        [HttpPost(Constants.Routes.AUTHOR_UPDATE_ROUTE)]
        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateAuthor([FromBody]AuthorModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(new { author = await _authorService.UpdateAuthorAsync(model) });
            }

            return BadRequest();
        }
}
}