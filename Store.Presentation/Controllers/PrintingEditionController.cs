using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
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
    public class PrintingEditionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPrintingEditionService _editionService;
        private readonly IUriService _uriService;

        public PrintingEditionController(IPrintingEditionService editionService, IMapper mapper, IUriService uriService)
        {
            _editionService = editionService;
            _mapper = mapper;
            _uriService = uriService;
        }

        [HttpPost(Constants.Routes.EDITION_CREATE_ROUTE)]
        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> CreateEdition([FromBody]PrintingEditionModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(new { edition = await _editionService.CreateEditionAsync(model) });
            }

            return BadRequest();
        }

        [HttpGet(Constants.Routes.EDITION_GET_ROUTE)]
        public async Task<IActionResult> GetEdition(string id)
        {
            if (id is null)
            {
                return NotFound();
            }

            return Ok(new { edition = await _editionService.GetEditionAsync(id) });
        }

        [HttpGet(Constants.Routes.EDITIONS_GET_ALL_ROUTE)]
        public async Task<IActionResult> GetEditions([FromQuery]PaginationQuery paginationQuery = null, [FromQuery]PrintingEditionFilter filter = null)
        {
            var editionsResponse = await _editionService.GetAllEditionsAsync();

            if (paginationQuery is null || paginationQuery.PageSize < 1 || paginationQuery.PageNumber < 1)
            {
                return Ok(new PagedResponse<PrintingEditionModel>(editionsResponse));
            }

            editionsResponse = await _editionService.GetAllEditionsAsync(paginationQuery, filter);

            var paginationResponse = PaginationProvider.CreatePaginatedResponse(_uriService,
                $"api/{ControllerContext.ActionDescriptor.ControllerName}/{Constants.Routes.EDITIONS_GET_ALL_ROUTE}",
                paginationQuery,filter,
                editionsResponse);

            return Ok(paginationResponse);
        }

        [HttpPost(Constants.Routes.EDITION_UPDATE_ROUTE)]
        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateEdition([FromBody]PrintingEditionModel model)
        {
            if (ModelState.IsValid)
            {
                await _editionService.UpdateEditionAsync(model);
                return Ok(new { edition = await _editionService.UpdateEditionAsync(model) });
            }

            return BadRequest();
        }

        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpPost(Constants.Routes.EDITION_REMOVE_ROUTE)]
        public async Task<IActionResult> RemoveEdition(string id)
        {
            if (id is null)
            {
                return NotFound();
            }

            return Ok(new { edition = await _editionService.RemoveEditionAsync(id) });
        }
    }
}