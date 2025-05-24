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
            CreateMap<Category, CategoryResponse>();
            CreateMap<Subcategory, SubcategoryResponse>();

            CreateMap<CategoryCreateRequest, Category>();
            CreateMap<SubcategoryCreateRequest, Subcategory>();

            CreateMap<CategoryUpdateRequest, Category>();
            CreateMap<SubcategoryUpdateRequest, Subcategory>();
        }
    }
}
