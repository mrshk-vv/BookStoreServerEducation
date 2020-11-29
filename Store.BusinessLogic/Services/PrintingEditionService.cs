using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Store.BusinessLogic.Interfaces;
using Store.BusinessLogic.Models.Author;
using Store.BusinessLogic.Models.PrintingEdition;
using Store.BusinessLogic.Providers;
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
        private readonly IAuthorInPERepository _authorInPeRepository;
        public PrintingEditionService(IPrintingEditionRepository editionRepository, IMapper mapper, IAuthorInPERepository authorInPeRepository)
        {
            _editionRepository = editionRepository;
            _mapper = mapper;
            _authorInPeRepository = authorInPeRepository;
        }

        public async Task<PrintingEditionModel> CreateEditionAsync(PrintingEditionItemModel model)
        {
            var printingEdition = await _editionRepository.GetEditionByTitle(model.Title);

            if (printingEdition is null)
            {
                var editionToAdd = _mapper.Map<PrintingEditionItemModel, PrintingEdition>(model);
                var editionAdded = await _editionRepository.CreateEditionAsync(editionToAdd);

                if (model.Authors.Count > 1)
                {
                    var authorInPrintingEditions =
                        AuthorInPrintingEditionProvider.GetAuthorInPrintingEditionList(model.Authors, editionAdded.Id);

                    await _authorInPeRepository.AddAuthorsToPrintingEditionAsync(authorInPrintingEditions);
                }
                else
                {
                    var authorInPrintingEdition =
                        AuthorInPrintingEditionProvider.GetAuthorInPrintingEdition(editionAdded.Id, model.Authors[0]);

                    await _authorInPeRepository.AddAuthorToPrintingEditionAsync(authorInPrintingEdition);
                }

                printingEdition = await _editionRepository.GetEditionByIdAsync(editionAdded.Id.ToString());
                var printingEditionModel = _mapper.Map<PrintingEdition, PrintingEditionModel>(printingEdition);

                return printingEditionModel;
            }

            throw new ServerException(Constants.Errors.EDITION_ALREADY_EXIST, Enums.Errors.BadRequest);
        }

        public async Task<IEnumerable<PrintingEditionModel>> GetAllEditionsAsync()
        {
            var printingEditions =
                _mapper.Map<IEnumerable<PrintingEdition>, IEnumerable<PrintingEditionModel>>(
                    await _editionRepository.GetEditionsAsync());

            return printingEditions;
        }

        public async Task<IEnumerable<PrintingEditionModel>> GetAllEditionsAsync(PaginationQuery paginationFilter = null,
                                                                                 PrintingEditionFilter filter = null)
        {
            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;

            if (filter.MaxPrice == Decimal.MaxValue && filter.MinPrice == 0 && filter.Type == null)
            {
                var printingEditionsNoFilter =
                    _mapper.Map<IEnumerable<PrintingEdition>, IEnumerable<PrintingEditionModel>>(
                        await _editionRepository.GetEditionsAsync(skip, paginationFilter.PageSize));

                return printingEditionsNoFilter;
            }

            var printingEditionsFilter = _mapper.Map<IEnumerable<PrintingEdition>, IEnumerable<PrintingEditionModel>>(
                await _editionRepository.GetEditionsAsync(skip,
                    paginationFilter.PageSize,
                    filter));
 

            switch (filter.PriceSorting)
            {
                case "ASC":
                    printingEditionsFilter = printingEditionsFilter.OrderBy(pe => pe.Price);
                    break;
                case "DESC":
                    printingEditionsFilter = printingEditionsFilter.OrderByDescending(pe => pe.Price);
                    break;
                default:
                    break;
            }

            return printingEditionsFilter;

        }

        public async Task<PrintingEditionModel> GetEditionAsync(string id)
        {
            var printingEdition =
                _mapper.Map<PrintingEdition, PrintingEditionModel>(await _editionRepository.GetEditionByIdAsync(id));

            return printingEdition;
        }

        public async Task<PrintingEditionModel> UpdateEditionAsync(PrintingEditionItemModel model)
        {
            var printingEdition = await _editionRepository.GetEditionByTitle(model.Title, model.Id);

            if (printingEdition is null)
            {
                var editionToUpdate = _mapper.Map<PrintingEditionItemModel, PrintingEdition>(model);
                var editionUpdated = await _editionRepository.UpdateEditionAsync(editionToUpdate);

                var authorsInPrintingEdition =
                    AuthorInPrintingEditionProvider.GetAuthorInPrintingEditionList(model.Authors,
                        editionUpdated.Id);

                await _authorInPeRepository.UpdateAuthorsInPrintingEditionAsync(authorsInPrintingEdition,
                    editionUpdated.Id);

                printingEdition = await _editionRepository.GetEditionByIdAsync(editionUpdated.Id.ToString());
                var printingEditionModel = _mapper.Map<PrintingEdition, PrintingEditionModel>(printingEdition);

                return printingEditionModel;

            }

            throw new ServerException(Constants.Errors.EDITION_ALREADY_EXIST, Enums.Errors.BadRequest);
        }

        public async Task<PrintingEditionModel> RemoveEditionAsync(string id)
        {
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
            var editionToDelete = await _editionRepository.GetEditionByIdAsync(id);

            if (editionToDelete is null)
            {
                throw new ServerException(Constants.Errors.EDITION_NOT_FOUND, Enums.Errors.NotFound);
            }

            await _editionRepository.DeleteEditionAsync(editionToDelete);
        }
    }
}
