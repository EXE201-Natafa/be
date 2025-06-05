using AutoMapper;
using Natafa.Api.Models.CategoryModel;
using Natafa.Api.ViewModels;
using Natafa.Domain.Entities;

namespace Natafa.Api.Mapper
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryResponse>()
                .ForMember(dest => dest.Subcategories, opt => opt.MapFrom(src => src.InverseParentCategory));
            CreateMap<Category, SubcategoryResponse>()
                .ForMember(dest => dest.SubcategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.SubcategoryName, opt => opt.MapFrom(src => src.CategoryName));

            CreateMap<CategoryCreateRequest, Category>();

            CreateMap<CategoryUpdateRequest, Category>();
        }
    }
}
