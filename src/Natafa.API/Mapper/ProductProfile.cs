using AutoMapper;
using Natafa.Api.Models.CategoryModel;
using Natafa.Api.Models.ProductModel;
using Natafa.Api.ViewModels;
using Natafa.Domain.Entities;

namespace Natafa.Api.Mapper
{
    public class ProductProfile : Profile
    {        
        public ProductProfile()
        {
            CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.ProductImages.FirstOrDefault().Url)); ;
            CreateMap<ProductDetail, DetailProduct>();
            CreateMap<Product, ProductDetailResponse>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.ProductImages.Select(f => f.Url))); ;
                //.ForMember(dest => dest.ProductDetails, opt => opt.MapFrom(src => src.ProductDetails))
                //.ForMember(dest => dest.Subcategory, opt => opt.MapFrom(src => src.Subcategory));

            CreateMap<ProductCreateRequest, Product>();
            CreateMap<ProductDetailCreateRequest, ProductDetail>();

            CreateMap<ProductUpdateRequest, Product>();
            CreateMap<ProductDetailUpdateRequest, ProductDetail>();
        }      
    }
}
