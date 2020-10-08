using AutoMapper;
using Store.BusinessLogic.Models.Author;
using Store.DataAccess.Entities;

namespace Store.BusinessLogic.Mapping
{
    public class AuthorMapping : Profile
    {
        public AuthorMapping()
        {
            CreateMap<AuthorModel, Author>();
            CreateMap<Author, AuthorModel>();
        }
    }
}
