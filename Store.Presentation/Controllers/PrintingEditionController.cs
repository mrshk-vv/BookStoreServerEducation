using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.BusinessLogic.Interfaces;
using Store.BusinessLogic.Models.PrintingEdition;
using Store.Shared.Constants;

namespace Store.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrintingEditionController : ControllerBase
    {
        private readonly IPrintingEditionService _editionService;

        public PrintingEditionController(IPrintingEditionService editionService)
        {
            _editionService = editionService;
        }

        [HttpPost(Constants.Routes.EDITION_CREATE_ROUTE)]
        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> CreateEdition([FromBody] PrintingEditionModel model)
        {
            return Ok(new { edition = await _editionService.CreateEditionAsync(model) });
        }

        [HttpGet(Constants.Routes.EDITION_GET_ROUTE)]
        public async Task<IActionResult> GetEdition(string id)
        {
            return Ok(new { edition = await _editionService.GetEditionAsync(id) });
        }

        [HttpGet(Constants.Routes.EDITIONS_GET_ALL_ROUTE)]
        public async Task<IActionResult> GetAllEditions()
        {
            return Ok(new { editionList = await _editionService.GetAllEditionsAsync() });
        }

        [HttpPost(Constants.Routes.EDITION_UPDATE_ROUTE)]
        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateEdition([FromBody]PrintingEditionModel model)
        {
            await _editionService.UpdateEditionAsync(model);

            return Ok(new { edition = await _editionService.UpdateEditionAsync(model) });
        }

        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpPost(Constants.Routes.EDITION_REMOVE_ROUTE)]
        public async Task<IActionResult> RemoveEdition(string id)
        {
            return Ok(new { edition = await _editionService.RemoveEditionAsync(id) });
        }
    }
}