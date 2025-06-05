using Natafa.Api.Helper;
using Natafa.Api.Models;
using Natafa.Api.ViewModels;
using Natafa.Domain.Paginate;

namespace Natafa.Api.Services.Interfaces
{
    public interface IWishListService
    {
        Task<MethodResult<IPaginate<ProductResponse>>> GetWishListAsync(int userId, PaginateRequest request);
        Task<MethodResult<string>> AddToWishListAsync(int userId, int productId);
    }
}
