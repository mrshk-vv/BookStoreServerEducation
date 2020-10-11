using System.Linq;
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
                .ForMember(p => p.AuthorName,
                    a => a.MapFrom(n => n.AuthorInPrintingEditions.SingleOrDefault(ap => ap.PrintingEditionId == n.Id).Author.Name));

                        CreateMap<PrintingEditionModel, PrintingEdition>();
        }
    }
}
