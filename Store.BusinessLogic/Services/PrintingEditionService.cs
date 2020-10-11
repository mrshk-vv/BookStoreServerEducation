using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Store.BusinessLogic.Interfaces;
using Store.BusinessLogic.Models.PrintingEdition;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Interfaces;
using Store.Shared.Common;
using Store.Shared.Constants;
using Store.Shared.Enums;
using Store.Shared.Filters;
using Store.Shared.Pagination;

namespace Store.BusinessLogic.Services
{
    public class PrintingEditionService : IPrintingEditionService
    {
        private readonly IMapper _mapper;
        private readonly IPrintingEditionRepository _editionRepository;
        public PrintingEditionService(IPrintingEditionRepository editionRepository, IMapper mapper)
        {
            _editionRepository = editionRepository;
            _mapper = mapper;
        }

        public async Task<PrintingEditionModel> CreateEditionAsync(PrintingEditionModel model)
        {
            var printingEdition = await _editionRepository.GetEdititonByTitle(model.Title);

            if (printingEdition is null)
            {
                var editionToAdd = _mapper.Map<PrintingEditionModel, PrintingEdition>(model);

                var printingEditionModel =
                    _mapper.Map<PrintingEdition, PrintingEditionModel>(await _editionRepository.CreateEditionAsync(editionToAdd));

                return printingEditionModel;
            }

            throw new ServerException(Constants.Errors.EDITION_ALREADY_EXIST, Enums.Errors.BadRequest);
        }

        public async Task<IEnumerable<PrintingEditionModel>> GetAllEditionsAsync()
        {
            return _mapper.Map<IEnumerable<PrintingEdition>, IEnumerable<PrintingEditionModel>>(
                await _editionRepository.GetEditionsAsync());
        }

        public async Task<IEnumerable<PrintingEditionModel>> GetAllEditionsAsync(PaginationQuery paginationFilter = null, PrintingEditionFilter filter = null)
        {

            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;

            if (filter.MaxPrice == 0 && filter.MinPrice == 0 && filter.Type != null)
            {
                return _mapper.Map<IEnumerable<PrintingEdition>, IEnumerable<PrintingEditionModel>>(
                        await _editionRepository.GetEditionsAsync(skip, paginationFilter.PageSize));
            }

            var printingEditions = _mapper.Map<IEnumerable<PrintingEdition>, IEnumerable<PrintingEditionModel>>(
                await _editionRepository.GetEditionsAsync(skip,
                    paginationFilter.PageSize,
                    filter));

            if (filter.PriceSorting == "ASC")
            {
                printingEditions = printingEditions.OrderBy(pe => pe.Price);
            }

            if (filter.PriceSorting == "DESC")
            {
                printingEditions = printingEditions.OrderByDescending(pe => pe.Price);
            }

            return printingEditions;

        }

        public async Task<PrintingEditionModel> GetEditionAsync(string id)
        {
            var printingEditionModel =
                _mapper.Map<PrintingEdition, PrintingEditionModel>(await _editionRepository.GetEditionByIdAsync(id));

            return printingEditionModel;
        }

        public async Task<PrintingEditionModel> UpdateEditionAsync(PrintingEditionModel model)
        {
            if (model is null)
            {
                throw new ServerException(Constants.Errors.EDITION_EMPTY, Enums.Errors.BadRequest);
            }

            var printingEdition = await _editionRepository.GetEdititonByTitle(model.Title);

            if (printingEdition is null)
            {
                var editionToUpdate = _mapper.Map<PrintingEditionModel, PrintingEdition>(model);

                var printingEditionModel =
                    _mapper.Map<PrintingEdition, PrintingEditionModel>(
                        await _editionRepository.UpdateEditionAsync(editionToUpdate));

                return printingEditionModel;
            }

            throw new ServerException(Constants.Errors.EDITION_ALREADY_EXIST, Enums.Errors.BadRequest);
        }

        public async Task<PrintingEditionModel> RemoveEditionAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ServerException(Constants.Errors.EDITION_ID_NOT_EXIST, Enums.Errors.BadRequest);
            }

            var editionToRemove = await _editionRepository.GetEditionByIdAsync(id);

            if (editionToRemove is null)
            {
                throw new ServerException(Constants.Errors.EDITION_NOT_FOUND, Enums.Errors.NotFound);
            }

            var printingEditionModel =
                _mapper.Map<PrintingEdition, PrintingEditionModel>(
                    await _editionRepository.RemoveEditionAsync(editionToRemove));

            return printingEditionModel;
        }

        public async Task DeleteEditionAsync(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
