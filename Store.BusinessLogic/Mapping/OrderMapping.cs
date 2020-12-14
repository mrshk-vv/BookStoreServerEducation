using System.Collections.Generic;
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
            CreateMap<OrderModel, Order>()
                .ForMember(o => o.Status, opt => opt.MapFrom(m => m.Status))
                .ForMember(o => o.Date, opt => opt.MapFrom(m => m.Date))
                .ForMember(o => o.Description, opt => opt.MapFrom(m => m.Description))
                .ForMember(o => o.PaymentId, opt => opt.MapFrom(m => m.PaymentId))
                .ForMember(o => o.TotalAmount, opt => opt.MapFrom(m => m.TotalAmount))
                .ForMember(o => o.UserId, opt => opt.MapFrom(m => m.UserId))
                .ForMember(o => o.OrderItems, opt => opt.Ignore())
                .ForMember(o => o.User, opt => opt.Ignore())
                .ForMember(o => o.Payment, opt => opt.Ignore());
                
            CreateMap<OrderItem, OrderItemModel>();
            CreateMap<OrderItemModel, OrderItem>()
                .ForMember(o => o.PrintingEditionId, opt => opt.MapFrom(m => m.PrintingEditionId))
                .ForMember(o => o.Amount, opt => opt.MapFrom(m => m.Amount))
                .ForMember(o => o.Currency, opt => opt.MapFrom(m => m.Currency))
                .ForMember(o => o.PrintingEdition, opt => opt.Ignore());
        }
    }
}
