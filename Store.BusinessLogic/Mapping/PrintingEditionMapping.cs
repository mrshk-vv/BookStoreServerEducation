using System.Linq;
using AutoMapper;
using Store.BusinessLogic.Models.AuthorInPrintingEdition;
using Store.BusinessLogic.Models.PrintingEdition;
using Store.DataAccess.Entities;

namespace Store.BusinessLogic.Mapping
{
    public class PrintingEditionMapping : Profile
    {
        public PrintingEditionMapping()
        {
            CreateMap<PrintingEdition, PrintingEditionModel>()
                .ForMember(p => p.Authors,
                    a => a.MapFrom(n =>
                        n.AuthorInPrintingEditions.Where(p => p.PrintingEditionId == n.Id)
                        ));

            CreateMap<PrintingEditionModel, PrintingEdition>()
                .ForMember(pe => pe.Title, e => e.MapFrom(pem => pem.Title))
                .ForMember(pe => pe.Currency, e => e.MapFrom(pem => pem.Currency))
                .ForMember(pe => pe.Description, e => e.MapFrom(pem => pem.Description))
                .ForMember(pe => pe.Type, e => e.MapFrom(pem => pem.Type))
                .ForMember(pe => pe.Price, e => e.MapFrom(pem => pem.Price))
                .ForMember(pe => pe.IsRemoved, e => e.MapFrom(pem => pem.IsRemoved));

            // CreateMap<PrintingEditionModel, AuthorInPEModel>()
            //     .ForMember(a => a.AuthorId, ap => ap.MapFrom(pem => pem.AuthorId))
            //     .ForMember(pe => pe.PrintingEditionId, ap => ap.MapFrom(pem => pem.Id));
        }
    }
}
