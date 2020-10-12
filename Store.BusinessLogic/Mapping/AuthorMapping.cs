using System.Linq;
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
            
            CreateMap<Author, AuthorModel>()
                .ForMember(p => p.PrintingEditions,
                    a => a.MapFrom(n =>
                        n.AuthorInPrintingEditions.Where(p => p.AuthorId == n.Id)
                    ));
        }
    }
}
