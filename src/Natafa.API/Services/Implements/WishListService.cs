using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Natafa.Api.Helper;
using Natafa.Api.Models;
using Natafa.Api.Services.Interfaces;
using Natafa.Api.ViewModels;
using Natafa.Domain.Entities;
using Natafa.Domain.Paginate;
using Natafa.Repository.Interfaces;
using System.Linq.Expressions;

namespace Natafa.Api.Services.Implements
{
    public class WishListService : IWishListService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public WishListService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<MethodResult<IPaginate<ProductResponse>>> GetWishListAsync(int userId, PaginateRequest request)
        {
            try
            {
                int page = request.page > 0 ? request.page : 1;
                int size = request.size > 0 ? request.size : 10;
                string search = request.search?.ToLower() ?? string.Empty;
                string filter = request.filter?.ToLower() ?? string.Empty;
                Expression<Func<Product, bool>> predicate = p =>
                    // Search filter
                    (string.IsNullOrEmpty(search) ||
                        p.ProductName.ToLower().Contains(search)) &&
                    (string.IsNullOrEmpty(filter) ||
                        (filter.Equals("active") && p.Status) ||
                        (filter.Equals("inactive") && !p.Status)) &&
                     p.Wishlists.Any(wl => wl.UserId == userId);

                var result = await _uow.GetRepository<Product>().GetPagingListAsync<ProductResponse>(
                    selector: s => _mapper.Map<ProductResponse>(s),
                    predicate: predicate,
                    orderBy: BuildOrderBy(request.sortBy),
                    include: i => i.Include(x => x.ProductImages)
                                   .Include(x => x.Category)
                                   .Include(x => x.Wishlists),
                    page: page,
                    size: size
                );
                return new MethodResult<IPaginate<ProductResponse>>.Success(result);
            }
            catch (Exception ex)
            {
                return new MethodResult<IPaginate<ProductResponse>>.Failure(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        private Func<IQueryable<Product>, IOrderedQueryable<Product>> BuildOrderBy(string sortBy)
        {
            if (string.IsNullOrEmpty(sortBy)) return null;

            return sortBy.ToLower() switch
            {
                "name" => q => q.OrderBy(p => p.ProductName),
                "name_desc" => q => q.OrderByDescending(p => p.ProductName),
                "date" => q => q.OrderBy(p => p.CreatedDate),
                "date_desc" => q => q.OrderByDescending(p => p.CreatedDate),
                _ => q => q.OrderByDescending(p => p.ProductId) // Default sort
            };
        }

        public async Task<MethodResult<string>> AddToWishListAsync(int userId, int productId)
        {
            var product = await _uow.GetRepository<Product>().SingleOrDefaultAsync(
                predicate: p => p.ProductId == productId,
                include: i => i.Include(p => p.Wishlists)
            );

            if (product == null)
            {
                return new MethodResult<string>.Failure("Product not found", StatusCodes.Status404NotFound);
            }

            if (product.Wishlists.Any(wl => wl.UserId == userId))
            {
                return new MethodResult<string>.Failure("Product already in wishlist", StatusCodes.Status400BadRequest);
            }

            var wishlist = new Wishlist
            {
                UserId = userId,
                ProductId = productId
            };

            await _uow.GetRepository<Wishlist>().InsertAsync(wishlist);
            await _uow.CommitAsync();
            return new MethodResult<string>.Success("Product added to wishlist successfully");
        }
    }
}
