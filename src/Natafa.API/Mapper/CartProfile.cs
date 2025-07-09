using AutoMapper;
using Natafa.Api.ViewModels;
using Natafa.Domain.Entities;

namespace Natafa.Api.Mapper
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<Cart, CartResponse>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductDetail.ProductId))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductDetail.Product.ProductName))
                .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.ProductDetail.Product.Summary))
                .ForMember(dest => dest.Material, opt => opt.MapFrom(src => src.ProductDetail.Product.Material))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.ProductDetail.Product.Category.CategoryName))
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.ProductDetail.Size))
                .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.ProductDetail.Weight))
                .ForMember(dest => dest.Height, opt => opt.MapFrom(src => src.ProductDetail.Height))
                .ForMember(dest => dest.Width, opt => opt.MapFrom(src => src.ProductDetail.Width))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.ProductDetail.Price))
                .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.ProductDetail.Discount))
                .ForMember(dest => dest.StockQuantity, opt => opt.MapFrom(src => src.ProductDetail.Quantity))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => 
                    src.ProductDetail.Product.ProductImages.FirstOrDefault() != null 
                        ? src.ProductDetail.Product.ProductImages.FirstOrDefault()!.Url 
                        : ""));
        }
    }
}

