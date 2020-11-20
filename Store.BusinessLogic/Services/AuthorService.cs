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
using Store.Shared.Filters;
using Store.Shared.Pagination;

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

        public async Task<AuthorModel> CreateAuthorAsync(AuthorItemModel model)
        {
            var author = await _authorRepository.GetAuthorByNameAsync(model.Name);

            if (author is null)
            {
                var authorToAdd = _mapper.Map<AuthorItemModel, Author>(model);
                var authorModel = _mapper.Map<Author, AuthorModel>(await _authorRepository.CreateAuthorAsync(authorToAdd));

                return authorModel;
            }

            throw new ServerException(Constants.Errors.AUTHOR_ALREADY_EXIST, Enums.Errors.BadRequest);
        }

        public async Task<IEnumerable<AuthorModel>> GetAuthorsAsync()
        {
            var authors =
                _mapper.Map<IEnumerable<Author>, IEnumerable<AuthorModel>>(await _authorRepository.GetAuthorsAsync());

            return authors;
        }

        public async Task<IEnumerable<AuthorModel>> GetAuthorsAsync(PaginationQuery paginationFilter = null, AuthorFilter filter = null)
        {
            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;

            if (filter.Name == null)
            {
                var authorsNoFilter = _mapper.Map<IEnumerable<Author>, IEnumerable<AuthorModel>>
                    (await _authorRepository.GetAuthorsAsync(skip, paginationFilter.PageSize));

                return authorsNoFilter;

            }

            var authorsFilter = _mapper.Map<IEnumerable<Author>, IEnumerable<AuthorModel>>
                (await _authorRepository.GetAuthorsAsync(skip, paginationFilter.PageSize));

            return authorsFilter;
        }

        public async Task<AuthorModel> GetAuthorByIdAsync(string id)
        {
            var author = _mapper.Map<Author, AuthorModel>(await _authorRepository.GetAuthorByIdAsync(id));
            return author;
        }

        public async Task<AuthorModel> UpdateAuthorAsync(AuthorItemModel model)
        {
            var author = await _authorRepository.GetAuthorByNameAsync(model.Name);

            if (author != null)
            {
                throw new ServerException(Constants.Errors.AUTHOR_ALREADY_EXIST,Enums.Errors.BadRequest);
            }

            var authorToUpdate = _mapper.Map<AuthorItemModel, Author>(model);

            var authorUpdated = 
                _mapper.Map<Author, AuthorModel>(await _authorRepository.UpdateAuthorAsync(authorToUpdate));

            return authorUpdated;
        }

        public async Task<AuthorModel> RemoveAuthorAsync(string id)
        {
            var author = await _authorRepository.GetAuthorByIdAsync(id);

            if (author is null)
            {
                throw new ServerException(Constants.Errors.AUTHOR_NOT_FOUND, Enums.Errors.NotFound);
            }

            var authorRemoved = _mapper.Map<Author,AuthorModel>(await _authorRepository.RemoveAuthorAsync(author));

            return authorRemoved;
        }

        public async Task DeleteAuthorAsync(string id)
        {
            var author = await _authorRepository.GetAuthorByIdAsync(id);

            if (author is null)
            {
                throw new ServerException(Constants.Errors.AUTHOR_NOT_FOUND, Enums.Errors.NotFound);
            }

            await _authorRepository.DeleteAuthorAsync(author);
        }
    }
}
