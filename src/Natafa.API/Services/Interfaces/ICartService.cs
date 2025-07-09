using Natafa.Api.ViewModels;
using Natafa.Domain.Entities;

namespace Natafa.Api.Services.Interfaces
{
    public interface ICartService
    {
        Task<bool> AddToCartAsync(int userId, int productId, int productDetailId, int quantity);
        Task<bool> RemoveFromCartAsync(int cartId);
        Task<bool> UpdateCartAsync(int cartId, int quantity);
        Task<IEnumerable<CartResponse>> GetUserCartAsync(int userId);
    }
}

