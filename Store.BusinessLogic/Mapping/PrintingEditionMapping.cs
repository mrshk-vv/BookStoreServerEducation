using AutoMapper;
using Store.BusinessLogic.Models.PrintingEdition;
using Store.DataAccess.Entities;

namespace Store.BusinessLogic.Mapping
{
    public class PrintingEditionMapping : Profile
    {
        public PrintingEditionMapping()
        {
            CreateMap<PrintingEdition, PrintingEditionModel>()
                .ForMember(pem => pem.Title, opt => opt.MapFrom(pe => pe.Title))
                .ForMember(pem => pem.Description, opt => opt.MapFrom(pe => pe.Description))
                .ForMember(pem => pem.EditionCurrency, opt => opt.MapFrom(pe => pe.EditionCurrency))
                .ForMember(pem => pem.EditionType, opt => opt.MapFrom(pe => pe.EditionType))
                .ForMember(pem => pem.Price, opt => opt.MapFrom(pe => pe.Price))
                .ForMember(pem => pem.IsRemoved, opt => opt.MapFrom(pe => pe.IsRemoved))
                .ForMember(pem => pem.Id, opt => opt.MapFrom(pe => pe.Id));

            CreateMap<PrintingEditionModel, PrintingEdition>()
                .ForMember(pe => pe.Title, opt => opt.MapFrom(pem => pem.Title))
                .ForMember(pe => pe.Description, opt => opt.MapFrom(pem => pem.Description))
                .ForMember(pe => pe.EditionCurrency, opt => opt.MapFrom(pem => pem.EditionCurrency))
                .ForMember(pe => pe.EditionType, opt => opt.MapFrom(pem => pem.EditionType))
                .ForMember(pe => pe.Price, opt => opt.MapFrom(pem => pem.Price))
                .ForMember(pe => pe.IsRemoved, opt => opt.MapFrom(pem => pem.IsRemoved))
                .ForMember(pe => pe.Id, opt => opt.MapFrom(pem => pem.Id));
        }
    }
}
