using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Store.BusinessLogic.Interfaces;
using Store.BusinessLogic.Models.Author;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Interfaces;
using Store.Shared.Common;
using Store.Shared.Constants;
using Store.Shared.Enums;

namespace Store.BusinessLogic.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IMapper _mapper;
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IMapper mapper, IAuthorRepository authorRepository)
        {
            _mapper = mapper;
            _authorRepository = authorRepository;
        }
        
        public async Task<AuthorModel> CreateAuthorAsync(AuthorModel model)
        {
            if (model is null)
            {
                throw new ServerException(Constants.Errors.AUTHOR_EMPTY, Enums.Errors.BadRequest);
            }

            var author = await _authorRepository.GetAuthorByNameAsync(model.Name);

            if (!(author is null))
            {
                throw new ServerException(Constants.Errors.AUTHOR_ALREADY_EXIST, Enums.Errors.BadRequest);
            }

            var authorToAdd = _mapper.Map<AuthorModel, Author>(model);
            var authorModel = _mapper.Map<Author,AuthorModel>(await _authorRepository.CreateAuthorAsync(authorToAdd));

            return authorModel;
        }

        public async Task<IEnumerable<AuthorModel>> GetAllAuthorsAsync()
        {
            var authors = _mapper.Map<IEnumerable<Author>, IEnumerable<AuthorModel>>(await _authorRepository.GetAuthorsAsync());
            
            return authors;
        }

        public async Task<AuthorModel> GetAuthorByIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ServerException(Constants.Errors.AUTHOR_ID_NOT_EXIST, Enums.Errors.BadRequest);
            }

            var authorModel = _mapper.Map<Author, AuthorModel>(await _authorRepository.GetAuthorByIdAsync(id));

            return authorModel;
        }

        public async Task<AuthorModel> UpdateAuthorAsync(AuthorModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                throw new ServerException(Constants.Errors.AUTHOR_EMPTY, Enums.Errors.BadRequest);
            }

            var author = await _authorRepository.GetAuthorByNameAsync(model.Name);

            if (!(author is null))
            {
                throw new ServerException(Constants.Errors.AUTHOR_ALREADY_EXIST,Enums.Errors.BadRequest);
            }

            var authorToUpdate = _mapper.Map<AuthorModel, Author>(model);

            var authorModel = _mapper.Map<Author, AuthorModel>(await _authorRepository.UpdateAuthorAsync(authorToUpdate));

            return authorModel;
        }

        public async Task<AuthorModel> RemoveAuthorAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ServerException(Constants.Errors.AUTHOR_ID_NOT_EXIST, Enums.Errors.BadRequest);
            }

            var author = await _authorRepository.GetAuthorByIdAsync(id);

            if (author is null)
            {
                throw new ServerException(Constants.Errors.AUTHOR_NOT_FOUND, Enums.Errors.NotFound);
            }

            var authorModel = _mapper.Map<Author,AuthorModel>(await _authorRepository.RemoveAuthorAsync(author));

            return authorModel;
        }
    }
}
