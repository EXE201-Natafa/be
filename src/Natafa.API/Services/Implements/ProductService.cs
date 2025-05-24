using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Natafa.Api.Helper;
using Natafa.Api.Models;
using Natafa.Api.Models.ProductModel;
using Natafa.Api.Services.Interfaces;
using Natafa.Api.ViewModels;
using Natafa.Domain.Entities;
using Natafa.Domain.Paginate;
using Natafa.Repository.Interfaces;
using System.Linq.Expressions;

namespace Natafa.Api.Services.Implements
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;

        public ProductService(IUnitOfWork uow, IMapper mapper, ICloudinaryService cloudinaryService)
        {
            _uow = uow;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<MethodResult<IPaginate<ProductResponse>>> GetProductsAsync(PaginateRequest request, int? subcategoryId, decimal? minPrice, decimal? maxPrice)
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
                     p.ProductName.ToLower().Contains(search) &&
                    (string.IsNullOrEmpty(filter) ||
                     (filter.Contains("active") && p.Status) ||
                     (filter.Contains("inactive") && !p.Status)) &&
                    (subcategoryId == null || subcategoryId == p.SubcategoryId) &&
                    (minPrice == null || p.ProductDetails.Any(pd => pd.Price * (1 - pd.Discount / 100) >= minPrice)) &&
                    (maxPrice == null || p.ProductDetails.Any(pd => pd.Price * (1 - pd.Discount / 100) <= maxPrice)));

                var result = await _uow.GetRepository<Product>().GetPagingListAsync<ProductResponse>(

                    selector: s => _mapper.Map<ProductResponse>(s),
                    predicate: predicate,
                    orderBy: BuildOrderBy(request.sortBy),
                    include: i => i.Include(x => x.ProductDetails)
                                   .Include(x => x.ProductImages)
                                   .Include(x => x.Subcategory),
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

        public async Task<MethodResult<ProductDetailResponse>> GetProductDetailByIdAsync(int id)
        {
            try
            {                
                var result = await _uow.GetRepository<Product>().SingleOrDefaultAsync<ProductDetailResponse>(

                    selector: s => _mapper.Map<ProductDetailResponse>(s),
                    include: i => i.Include(x => x.ProductDetails)
                                   .Include(x => x.ProductImages)
                                   .Include(x => x.Subcategory)                    
                );
                return new MethodResult<ProductDetailResponse>.Success(result);
            }
            catch (Exception ex)
            {
                return new MethodResult<ProductDetailResponse>.Failure(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<MethodResult<string>> CreateProductAsync(ProductCreateRequest request)
        {
            try
            {
                await _uow.BeginTransactionAsync();
                
                var product = _mapper.Map<Product>(request);

                if (request.ProductImages != null)
                {
                    var imageUrls = await _cloudinaryService.UploadMultipleImagesAsync(request.ProductImages);
                    if (imageUrls.Count != 0)
                    {
                        foreach (var url in imageUrls)
                        {
                            var productImage = new ProductImage
                            {
                                Url = url
                            };

                            product.ProductImages.Add(productImage);
                        }
                    }
                }

                await _uow.GetRepository<Product>().InsertAsync(product);                
                await _uow.CommitAsync();
                await _uow.CommitTransactionAsync();
                return new MethodResult<string>.Success("Product created successfully");
            }
            catch (Exception ex)
            {
                await _uow.RollbackTransactionAsync();
                return new MethodResult<string>.Failure(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<MethodResult<string>> UpdateProductAsync(int id, ProductUpdateRequest request)
        {
            try
            {
                await _uow.BeginTransactionAsync();

                var product = await _uow.GetRepository<Product>().SingleOrDefaultAsync(
                    predicate: p => p.ProductId == id,
                    include: i => i.Include(p => p.ProductImages)
                                 .Include(p => p.ProductDetails)
                );

                if (product == null)
                {
                    return new MethodResult<string>.Failure("Product not found", StatusCodes.Status404NotFound);
                }

                // Upload và quản lý hình ảnh
                if (request.IsUpdateImagePackage)
                {
                    // Xóa ảnh cũ
                    foreach (var image in product.ProductImages.ToList())
                    {
                        _uow.GetRepository<ProductImage>().DeleteAsync(image);
                    }

                    if (request.ProductImages != null && request.ProductImages.Any())
                    {
                        var imageUrls = await _cloudinaryService.UploadMultipleImagesAsync(request.ProductImages);

                        if (imageUrls.Any())
                        {
                            // Thêm ảnh mới
                            foreach (var imageUrl in imageUrls)
                            {
                                await _uow.GetRepository<ProductImage>().InsertAsync(new ProductImage
                                {
                                    ProductId = id,
                                    Url = imageUrl
                                });
                            }
                        }
                    }
                }

                _mapper.Map(request, product);
                _uow.GetRepository<Product>().UpdateAsync(product);

                await _uow.CommitAsync();
                await _uow.CommitTransactionAsync();

                return new MethodResult<string>.Success("Product updated successfully");
            }
            catch (Exception ex)
            {
                await _uow.RollbackTransactionAsync();
                return new MethodResult<string>.Failure(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<MethodResult<string>> DeleteProductAsync(int id)
        {
            try
            {
                var product = await _uow.GetRepository<Product>().SingleOrDefaultAsync(
                    predicate: p => p.ProductId == id,
                    include: i => i.Include(p => p.ProductImages)
                                   .Include(p => p.ProductDetails)
                );
                if (product == null)
                {
                    return new MethodResult<string>.Failure("Product not found", StatusCodes.Status404NotFound);
                }

                product.Status = false;
                _uow.GetRepository<Product>().UpdateAsync(product);
                await _uow.CommitAsync();
                return new MethodResult<string>.Success("Product deleted successfully");
            }
            catch (Exception ex)
            {
                return new MethodResult<string>.Failure(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        //public async Task<MethodResult<string>> DeleteProductAsync(int id)
        //{
        //    try
        //    {
        //        var product = await _uow.GetRepository<Product>().SingleOrDefaultAsync(
        //            predicate: p => p.ProductId == id,
        //            include: i => i.Include(p => p.ProductImages)
        //                           .Include(p => p.ProductDetails)
        //        );
        //        if (product == null)
        //        {
        //            return new MethodResult<string>.Failure("Product not found", StatusCodes.Status404NotFound);
        //        }
                
        //        _uow.GetRepository<Product>().DeleteAsync(product);
        //        await _uow.CommitAsync();
        //        return new MethodResult<string>.Success("Product deleted successfully");
        //    }
        //    catch (Exception ex)
        //    {
        //        return new MethodResult<string>.Failure(ex.Message, StatusCodes.Status500InternalServerError);
        //    }
        //}

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
    }
}
