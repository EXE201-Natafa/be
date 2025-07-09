using Natafa.Api.Services.Interfaces;
using Natafa.Api.ViewModels;
using Natafa.Domain.Entities;
using Natafa.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Natafa.Api.Services.Implements
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CartService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> AddToCartAsync(int userId, int productId, int productDetailId, int quantity)
        {
            try
            {
                var cartRepo = _unitOfWork.GetRepository<Cart>();
                var productDetailRepo = _unitOfWork.GetRepository<ProductDetail>();
                var productRepo = _unitOfWork.GetRepository<Product>();
                
                // Check if product and product detail exist and match
                var product = await productRepo.SingleOrDefaultAsync(predicate: p => p.ProductId == productId);
                if (product == null)
                    return false;
                
                var productDetail = await productDetailRepo.SingleOrDefaultAsync(predicate: p => p.ProductDetailId == productDetailId && p.ProductId == productId);
                if (productDetail == null)
                    return false;

                // Check if item already exists in cart
                var existingCartItem = await cartRepo.SingleOrDefaultAsync(predicate: c => c.UserId == userId && c.ProductDetailId == productDetailId);
                
                if (existingCartItem != null)
                {
                    // Update quantity if item already exists
                    existingCartItem.Quantity += quantity;
                    existingCartItem.UpdatedDate = DateTime.Now;
                    cartRepo.UpdateAsync(existingCartItem);
                }
                else
                {
                    // Add new item to cart
                    var cartItem = new Cart
                    {
                        UserId = userId,
                        ProductDetailId = productDetailId,
                        Quantity = quantity,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    };
                    await cartRepo.InsertAsync(cartItem);
                }

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RemoveFromCartAsync(int cartId)
        {
            try
            {
                var cartRepo = _unitOfWork.GetRepository<Cart>();
                var cartItem = await cartRepo.SingleOrDefaultAsync(predicate: c => c.CartId == cartId);
                
                if (cartItem == null)
                    return false;

                cartRepo.DeleteAsync(cartItem);
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateCartAsync(int cartId, int quantity)
        {
            try
            {
                var cartRepo = _unitOfWork.GetRepository<Cart>();
                var cartItem = await cartRepo.SingleOrDefaultAsync(predicate: c => c.CartId == cartId);
                
                if (cartItem == null)
                    return false;

                cartItem.Quantity = quantity;
                cartItem.UpdatedDate = DateTime.Now;
                cartRepo.UpdateAsync(cartItem);
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<CartResponse>> GetUserCartAsync(int userId)
        {
            var cartRepo = _unitOfWork.GetRepository<Cart>();
            var cartItems = await cartRepo.GetListAsync(
                predicate: c => c.UserId == userId, 
                include: source => source
                    .Include(c => c.ProductDetail)
                    .ThenInclude(pd => pd.Product)
                    .ThenInclude(p => p.Category)
                    .Include(c => c.ProductDetail)
                    .ThenInclude(pd => pd.Product)
                    .ThenInclude(p => p.ProductImages)
            );
            
            return _mapper.Map<IEnumerable<CartResponse>>(cartItems);
        }
    }
}

