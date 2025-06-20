using AutoMapper;
using Natafa.Api.Models.AuthenticationModel;
using Natafa.Api.Models.OrderModel;
using Natafa.Api.ViewModels;
using Natafa.Domain.Entities;

namespace Natafa.Api.Mapper
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderCreateRequest, Order>()
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => 0));

            CreateMap<Order, OrderResponse>()
                .ForMember(dest => dest.Transaction, opt => opt.MapFrom(src => src.Transactions.FirstOrDefault()));
            CreateMap<OrderTracking, OrderTrackingResponse>();
            CreateMap<OrderDetail, OrderDetailResponse>();
        }
    }
}
