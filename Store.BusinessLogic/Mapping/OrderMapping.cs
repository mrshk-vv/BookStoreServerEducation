using System.Linq;
using AutoMapper;
using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Models.PrintingEdition;
using Store.DataAccess.Entities;

namespace Store.BusinessLogic.Mapping
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            CreateMap<Order, OrderModel>();
            CreateMap<OrderItem, OrderItemModel>();
            CreateMap<PrintingEditionModel, OrderItem>()
                .ForMember(oi => oi.PrintingEditionId, pe => pe.MapFrom(n => n.Id))
                .ForMember(oi => oi.Currency, pe => pe.MapFrom(n => n.EditionCurrency));
        }
    }
}
