using Natafa.Api.Helper;
using Natafa.Api.Models;
using Natafa.Api.Models.ProductModel;
using Natafa.Api.ViewModels;
using Natafa.Domain.Paginate;

namespace Natafa.Api.Services.Interfaces
{
    public interface IProductService
    {
        Task<MethodResult<IPaginate<ProductResponse>>> GetProductsAsync(PaginateRequest request, int? subcategoryId, decimal? minPrice, decimal? maxPrice);
        Task<MethodResult<ProductDetailResponse>> GetProductDetailByIdAsync(int id);
        Task<MethodResult<string>> CreateProductAsync(ProductCreateRequest request);
        Task<MethodResult<string>> UpdateProductAsync(int id, ProductUpdateRequest request);
        Task<MethodResult<string>> DeleteProductAsync(int id);
    }
}
