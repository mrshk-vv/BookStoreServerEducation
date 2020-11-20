using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.BusinessLogic.Interfaces;
using Store.BusinessLogic.Models.Author;
using Store.Presentation.Providers.Pagination;
using Store.Shared.Constants;
using Store.Shared.Filters;
using Store.Shared.Pagination;

namespace Store.Presentation.Controllers
{
    [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
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
        public async Task<IActionResult> CreateAuthor([FromBody]AuthorItemModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _authorService.CreateAuthorAsync(model));
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
            return Ok(await _authorService.GetAuthorByIdAsync(id));
        }

        [HttpGet(Constants.Routes.AUTHORS_GET_ALL_ROUTE)]
        public async Task<IActionResult> GetAuthors([FromQuery]PaginationQuery paginationQuery, [FromQuery]AuthorFilter filter)
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

        [HttpPost(Constants.Routes.AUTHOR_UPDATE_ROUTE)]
        public async Task<IActionResult> UpdateAuthor([FromBody]AuthorItemModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _authorService.UpdateAuthorAsync(model));
            }

            return BadRequest();
        }

        [HttpGet(Constants.Routes.AUTHORS_GET_LIST_ROUTE)]
        public async Task<IActionResult> GetListAuthors()
        {
            return Ok(await _authorService.GetAuthorsAsync());
        }

        [HttpPost(Constants.Routes.AUTHOR_DELETE_ROUTE)]
        public async Task<IActionResult> DeleteAuthor(string id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            await _authorService.DeleteAuthorAsync(id);

            return Ok();
        }

        [HttpPost(Constants.Routes.AUTHOR_REMOVE_ROUTE)]
        public async Task<IActionResult> RemoveAuthor(string id)
        {
            if (id is null)
            {
                return NotFound();
            }

            return Ok(await _authorService.RemoveAuthorAsync(id));
        }
    }
}