using AutoMapper;
using Store.BusinessLogic.Models.PrintingEdition;
using Store.DataAccess.Entities;

namespace Store.BusinessLogic.Mapping
{
    public class PrintingEditionMapping : Profile
    {
        public PrintingEditionMapping()
        {
            CreateMap<PrintingEdition, PrintingEditionModel>();
            CreateMap<PrintingEditionModel, PrintingEdition>();
        }
    }
}
