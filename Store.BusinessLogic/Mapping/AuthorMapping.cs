using AutoMapper;
using Store.BusinessLogic.Models.Author;
using Store.DataAccess.Entities;

namespace Store.BusinessLogic.Mapping
{
    public class AuthorMapping : Profile
    {
        public AuthorMapping()
        {
            CreateMap<AuthorItemModel, Author>()
                .ForMember(a => a.Id, opt => opt.MapFrom(am => am.Id))
                .ForMember(a => a.Name, opt => opt.MapFrom(am => am.Name))
                .ForMember(a => a.IsRemoved, opt => opt.MapFrom(am => am.IsRemoved));

            CreateMap<Author, AuthorItemModel>()
                .ForMember(am => am.Id, opt => opt.MapFrom(a => a.Id))
                .ForMember(am => am.Name, opt => opt.MapFrom(a => a.Name))
                .ForMember(am => am.IsRemoved, opt => opt.MapFrom(a => a.IsRemoved));

            CreateMap<AuthorModel, Author>()
                .ForMember(a => a.Id, opt => opt.MapFrom(am => am.Id))
                .ForMember(a => a.Name, opt => opt.MapFrom(am => am.Name))
                .ForMember(a => a.IsRemoved, opt => opt.MapFrom(am => am.IsRemoved))
                .ForMember(a => a.AuthorInPrintingEditions, opt => opt.MapFrom(am => am.AuthorInPrintingEditions));

            CreateMap<Author, AuthorModel>()
                .ForMember(am => am.Id, opt => opt.MapFrom(a => a.Id))
                .ForMember(am => am.Name, opt => opt.MapFrom(a => a.Name))
                .ForMember(am => am.IsRemoved, opt => opt.MapFrom(a => a.IsRemoved))
                .ForMember(am => am.AuthorInPrintingEditions, opt => opt.MapFrom(a => a.AuthorInPrintingEditions));

            CreateMap<AuthorInPrintingEdition, AuthorInPrintingEditionModel>()
                .ForMember(aipem => aipem.AuthorId, opt => opt.MapFrom(aipe => aipe.AuthorId))
                .ForMember(aipem => aipem.Author, opt => opt.MapFrom(aipe => aipe.Author))
                .ForMember(aipem => aipem.PrintingEditionId, opt => opt.MapFrom(aipe => aipe.PrintingEditionId))
                .ForMember(aipem => aipem.PrintingEdition, opt => opt.MapFrom(aipe => aipe.PrintingEdition))
                .ForMember(aipem => aipem.Date, opt => opt.MapFrom(aipe => aipe.Date));

            CreateMap<AuthorInPrintingEditionModel, AuthorInPrintingEdition>()
                .ForMember(aipe => aipe.AuthorId, opt => opt.MapFrom(aipem => aipem.AuthorId))
                .ForMember(aipe => aipe.Author, opt => opt.MapFrom(aipem => aipem.Author))
                .ForMember(aipe => aipe.PrintingEditionId, opt => opt.MapFrom(aipem => aipem.PrintingEditionId))
                .ForMember(aipe => aipe.PrintingEdition, opt => opt.MapFrom(aipem => aipem.PrintingEdition))
                .ForMember(aipe => aipe.Date, opt => opt.MapFrom(aipem => aipem.Date));

            CreateMap<AuthorModel, AuthorInPrintingEdition>()
                .ForMember(aipe => aipe.AuthorId, opt => opt.MapFrom(a => a.Id));

            CreateMap<AuthorInPrintingEdition, AuthorModel>()
                .ForMember(aipe => aipe.Id, opt => opt.MapFrom(a => a.AuthorId));
        }
    }
}
