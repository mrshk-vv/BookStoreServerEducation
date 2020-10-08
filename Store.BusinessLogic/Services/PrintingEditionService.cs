using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Store.BusinessLogic.Interfaces;
using Store.BusinessLogic.Models.PrintingEdition;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Interfaces;
using Store.Shared.Common;
using Store.Shared.Constants;
using Store.Shared.Enums;

namespace Store.BusinessLogic.Services
{
    public class PrintingEditionService : IPrintingEditionService
    {
        private readonly IMapper _mapper;
        private readonly IPrintingEditionRepository<PrintingEdition> _editionRepository;
        public PrintingEditionService(IPrintingEditionRepository<PrintingEdition> editionRepository, IMapper mapper)
        {
            _editionRepository = editionRepository;
            _mapper = mapper;
        }

        public async Task<PrintingEditionModel> CreateEditionAsync(PrintingEditionModel model)
        {
            if (model is null)
            {
                throw new ServerException(Constants.Errors.EDITION_EMPTY,Enums.Errors.BadRequest);
            }

            var printingEdition = await _editionRepository.GetEdititonByTitle(model.Title);

            if (!(printingEdition is null))
            {
                throw new ServerException(Constants.Errors.EDITION_ALREADY_EXIST,Enums.Errors.BadRequest);
            }

            var editionToAdd = _mapper.Map<PrintingEditionModel, PrintingEdition>(model);

            var printingEditionModel =
                _mapper.Map<PrintingEdition, PrintingEditionModel>(await _editionRepository.CreateAsync(editionToAdd));

            return printingEditionModel;
        }

        public async Task<IEnumerable<PrintingEditionModel>> GetAllEditionsAsync()
        {
            var printingEditions =
                _mapper.Map<IEnumerable<PrintingEdition>, IEnumerable<PrintingEditionModel>>(
                    await _editionRepository.GetAllAsync());

            return printingEditions;
        }

        public async Task<PrintingEditionModel> GetEditionAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ServerException(Constants.Errors.EDITION_ID_NOT_EXIST,Enums.Errors.BadRequest);
            }

            var printingEditionModel =
                _mapper.Map<PrintingEdition, PrintingEditionModel>(await _editionRepository.GetEntityAsync(id));

            return printingEditionModel;
        }

        public async Task<PrintingEditionModel> UpdateEditionAsync(PrintingEditionModel model)
        {
            if (model is null)
            {
                throw new ServerException(Constants.Errors.EDITION_EMPTY,Enums.Errors.BadRequest);
            }

            var printingEdition = await _editionRepository.GetEdititonByTitle(model.Title);

            if (!(printingEdition is null))
            {
                throw new ServerException(Constants.Errors.EDITION_ALREADY_EXIST,Enums.Errors.BadRequest);
            }

            var editionToUpdate = _mapper.Map<PrintingEditionModel, PrintingEdition>(model);

            var printingEditionModel =
                _mapper.Map<PrintingEdition, PrintingEditionModel>(
                    await _editionRepository.UpdateAsync(editionToUpdate));

            return printingEditionModel;
        }

        public async Task<PrintingEditionModel> RemoveEditionAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ServerException(Constants.Errors.EDITION_ID_NOT_EXIST,Enums.Errors.BadRequest);
            }

            var editionToRemove = await _editionRepository.GetEntityAsync(id);

            if (editionToRemove is null)
            {
                throw new ServerException(Constants.Errors.EDITION_NOT_FOUND,Enums.Errors.NotFound);
            }

            var printingEditionModel =
                _mapper.Map<PrintingEdition, PrintingEditionModel>(
                    await _editionRepository.RemoveAsync(editionToRemove));

            return printingEditionModel;
        }
    }
}
