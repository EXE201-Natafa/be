using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Natafa.Api.Constants;
using Natafa.Api.Models;
using Natafa.Api.Models.ProductModel;
using Natafa.Api.Services.Implements;
using Natafa.Api.Services.Interfaces;
using static Natafa.Api.Routes.Router;

namespace Natafa.Api.Controllers
{
    /// <summary>
    /// Controller quản lý các API liên quan đến sản phẩm.
    /// </summary>
    public class ProductController : BaseApiController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Lấy danh sách sản phẩm.
        /// </summary>
        /// <param name="request">Thông tin phân trang.</param>
        /// <param name="categoryId">ID danh mục (tùy chọn).</param>
        /// <param name="minPrice">Giá tối thiểu (tùy chọn).</param>
        /// <param name="maxPrice">Giá tối đa (tùy chọn).</param>
        /// <returns>Danh sách sản phẩm theo bộ lọc.</returns>
        [HttpGet]
        [Route(ProductRoute.GetProducts)]
        public async Task<ActionResult> GetProducts([FromQuery] PaginateRequest request, int? categoryId, decimal? minPrice, decimal? maxPrice)
        {
            var result = await _productService.GetProductsAsync(request, categoryId, minPrice, maxPrice);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Lấy danh sách sản phẩm bán chạy.
        /// </summary>
        /// <returns>Danh sách sản phẩm bán chạy.</returns>
        [HttpGet]
        [Route(ProductRoute.GetBestSellerProducts)]
        public async Task<ActionResult> GetBestSellerProducts()
        {
            var result = await _productService.GetBestSellerProductsAsync();
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Lấy chi tiết sản phẩm.
        /// </summary>
        /// <param name="id">ID sản phẩm.</param>
        /// <returns>Thông tin chi tiết của sản phẩm.</returns>
        [HttpGet]
        [Route(ProductRoute.GetProductDetail)]
        public async Task<ActionResult> GetProductDetail(int id)
        {
            var result = await _productService.GetProductDetailByIdAsync(id);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Tạo mới sản phẩm.
        /// </summary>
        /// <remarks>
        /// Yêu cầu Role: Staff.
        /// </remarks>
        /// <param name="request">Thông tin sản phẩm cần tạo.</param>
        /// <returns>Kết quả tạo sản phẩm.</returns>
        [HttpPost]
        [Route(ProductRoute.CreateProduct)]
        [Authorize(Roles = UserConstant.USER_ROLE_STAFF)] 
        public async Task<ActionResult> CreateProduct(ProductCreateRequest request)
        {
            var result = await _productService.CreateProductAsync(request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Cập nhật thông tin sản phẩm.
        /// </summary>
        /// <remarks>
        /// Yêu cầu Role: Staff.
        /// </remarks>
        /// <param name="id">ID sản phẩm cần cập nhật.</param>
        /// <param name="request">Thông tin sản phẩm mới.</param>
        /// <returns>Kết quả cập nhật sản phẩm.</returns>
        [HttpPut]
        [Route(ProductRoute.UpdateProduct)]
        [Authorize(Roles = UserConstant.USER_ROLE_STAFF)] 
        public async Task<ActionResult> UpdateProduct(int id, ProductUpdateRequest request)
        {
            var result = await _productService.UpdateProductAsync(id, request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Xóa sản phẩm.
        /// </summary>
        /// <remarks>
        /// Yêu cầu Role: Staff.
        /// </remarks>
        /// <param name="id">ID sản phẩm cần xóa.</param>
        /// <param name="ProductImages">Danh sách ảnh của sản phẩm.</param>
        /// <returns>Kết quả xóa sản phẩm.</returns>
        [HttpDelete]
        [Route(ProductRoute.DeleteProduct)]
        [Authorize(Roles = UserConstant.USER_ROLE_STAFF)]
        public async Task<ActionResult> DeleteProduct(int id, List<IFormFile> ProductImages)
        {
            var result = await _productService.DeleteProductAsync(id);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }
    }
}
