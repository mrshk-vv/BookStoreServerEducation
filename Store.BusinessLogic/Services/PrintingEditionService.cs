using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Store.BusinessLogic.Interfaces;
using Store.BusinessLogic.Models.Author;
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
        private readonly IAuthorInPERepository _authorInPeRepository;
        private readonly IAuthorRepository _authorRepository;
        public PrintingEditionService(IPrintingEditionRepository editionRepository, IMapper mapper, IAuthorInPERepository authorInPeRepository, IAuthorRepository authorRepository)
        {
            _editionRepository = editionRepository;
            _mapper = mapper;
            _authorInPeRepository = authorInPeRepository;
            _authorRepository = authorRepository;
        }

        public async Task<PrintingEditionModel> CreateEditionAsync(PrintingEditionModel model)
        {
            var printingEdition = await _editionRepository.GetEditionByTitle(model.Title);

            if (printingEdition is null)
            {
                var editionToAdd = _mapper.Map<PrintingEditionModel, PrintingEdition>(model);
                var editionAdded = await _editionRepository.CreateEditionAsync(editionToAdd);
                var authors = _mapper.Map<List<AuthorInPrintingEditionModel>, List<AuthorInPrintingEdition>>(model.AuthorInPrintingEditions);

                if (authors.Count == 1)
                {
                    authors[0].PrintingEditionId = editionAdded.Id;
                    await _authorInPeRepository.AddAuthorToPE(authors[0]);
                    authors[0].Author = await _authorRepository.GetAuthorByIdAsync(authors[0].AuthorId.ToString());
                }
                else
                {
                    for (int i = 0; i < authors.Count; i++)
                    {
                        authors[i].PrintingEditionId = editionAdded.Id;
                    }

                    await _authorInPeRepository.AddAuthorsToPE(authors);

                    for (int i = 0; i < authors.Count; i++)
                    {
                        authors[i].Author = await _authorRepository.GetAuthorByIdAsync(authors[i].AuthorId.ToString());
                    }
                }

                editionAdded.AuthorInPrintingEditions = authors;

                return _mapper.Map<PrintingEdition, PrintingEditionModel>(editionAdded);
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

        public async Task<PrintingEditionModel> UpdateEditionAsync(PrintingEditionModel model)
        {
            var printingEdition = await _editionRepository.GetEditionByTitle(model.Title, model.Id);

            if (printingEdition is null)
            {
                List<AuthorInPrintingEditionModel> authorsInUpdatedEdititon = null;

                printingEdition = await _editionRepository.GetEditionByIdAsync(model.Id.ToString());

                var authors = _mapper.Map<List<AuthorInPrintingEditionModel>, List<AuthorInPrintingEdition>>
                    (model.AuthorInPrintingEditions);

                var editionToUpdate = _mapper.Map<PrintingEditionModel, PrintingEdition>(model);

                var printingEditionModel =
                    _mapper.Map<PrintingEdition, PrintingEditionModel>(
                        await _editionRepository.UpdateEditionAsync(editionToUpdate));

                if (authors.Equals(printingEdition.AuthorInPrintingEditions))
                {
                    authorsInUpdatedEdititon =
                        _mapper.Map<List<AuthorInPrintingEdition>, List<AuthorInPrintingEditionModel>>
                            (authors);

                    printingEditionModel.AuthorInPrintingEditions = authorsInUpdatedEdititon;

                    return printingEditionModel;
                } 

                if (authors.Count == 1)
                {
                    authors[0].PrintingEditionId = printingEditionModel.Id;

                    authorsInUpdatedEdititon =
                        _mapper.Map<List<AuthorInPrintingEdition>, List<AuthorInPrintingEditionModel>>(authors);

                    printingEditionModel.AuthorInPrintingEditions = authorsInUpdatedEdititon;

                    var author =
                        _mapper.Map<AuthorInPrintingEdition, AuthorInPrintingEditionModel>
                            (await _authorInPeRepository.UpdateAuthorInPE(authors[0]));

                    printingEditionModel.AuthorInPrintingEditions.Add(author);

                    return printingEditionModel;
                }

                var authorsInPE = _mapper.Map<List<AuthorInPrintingEdition>, List<AuthorInPrintingEditionModel>>
                    (await _authorInPeRepository.UpdateAuthorsInPE(authors));

                printingEditionModel.AuthorInPrintingEditions = authorsInPE;

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
