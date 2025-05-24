using Natafa.Api.Helper;
using Natafa.Api.Models.CategoryModel;
using Natafa.Api.ViewModels;

namespace Natafa.Api.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<MethodResult<CategoryResponse>> GetCategoryByIdAsync(int id);
        Task<MethodResult<IEnumerable<CategoryResponse>>> GetCategoriesAsync();
        Task<MethodResult<string>> CreateCategoryAsync(CategoryCreateRequest request);
        Task<MethodResult<string>> UpdateCategoryAsync(int id, CategoryUpdateRequest request);
        Task<MethodResult<string>> DeleteCategoryAsync(int id);
    }
}
