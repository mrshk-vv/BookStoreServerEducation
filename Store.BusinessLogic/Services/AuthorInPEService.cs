using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Store.BusinessLogic.Interfaces;
using Store.BusinessLogic.Models.AuthorInPrintingEdition;
using Store.DataAccess.Repositories.Interfaces;

namespace Store.BusinessLogic.Services
{
    public class AuthorInPEService : IAuthorInPEService
    {
        private readonly IAuthorInPERepository _authorInPeRepository;
        private readonly IMapper _mapper;

        public AuthorInPEService(IAuthorInPERepository authorInPeRepository, IMapper mapper)
        {
            _authorInPeRepository = authorInPeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AuthorInPEModel>> GetAuthorsInPE()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AuthorInPEModel>> GetAuthorInPE(int skip, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task AddAuthorToPE(AuthorInPEModel model)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveAuthorFromPE(AuthorInPEModel model)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAuthorsInPE(AuthorInPEModel model)
        {
            throw new NotImplementedException();
        }
    }
}
