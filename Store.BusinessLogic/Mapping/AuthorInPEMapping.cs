using AutoMapper;
using Store.BusinessLogic.Models.AuthorInPrintingEdition;
using Store.DataAccess.Entities;

namespace Store.BusinessLogic.Mapping
{
    class AuthorInPEMapping : Profile
    {
        public AuthorInPEMapping()
        {
            CreateMap<AuthorInPEModel, AuthorInPrintingEdition>();
            CreateMap<AuthorInPrintingEdition, AuthorInPEModel>();
        }
    }
}
