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
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetailRequests));
            CreateMap<OrderDetailCreateRequest, OrderDetail>();

            CreateMap<Order, OrderResponse>()
                .ForMember(dest => dest.Transaction, opt => opt.MapFrom(src => src.Transactions.FirstOrDefault()));
            CreateMap<PaymentMethod, PaymentMethodResponse>();
            CreateMap<OrderTracking, OrderTrackingResponse>()
                .ForMember(dest => dest.UpdateTime, opt => opt.MapFrom(src => src.UpdatedDate));
            CreateMap<OrderDetail, OrderDetailResponse>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductDetail.Product.ProductName))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.ProductDetail.Product.ProductImages.FirstOrDefault().Url));
        }
    }
}
