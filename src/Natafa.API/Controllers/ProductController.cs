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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route(ProductRoute.GetProducts)]
        public async Task<ActionResult> GetCategories(PaginateRequest request, int? subcategoryId, decimal? minPrice, decimal? maxPrice)
        {
            var result = await _productService.GetProductsAsync(request, subcategoryId, minPrice, maxPrice);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

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

        [HttpPost]
        [Route(ProductRoute.CreateProduct)]
        [Authorize(Roles = UserConstant.USER_ROLE_ADMIN)]
        public async Task<ActionResult> CreateProduct([FromBody] ProductCreateRequest request)
        {
            var result = await _productService.CreateProductAsync(request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpPut]
        [Route(ProductRoute.UpdateProduct)]
        [Authorize(Roles = UserConstant.USER_ROLE_ADMIN)]
        public async Task<ActionResult> UpdateProduct(int id, [FromBody] ProductUpdateRequest request)
        {
            var result = await _productService.UpdateProductAsync(id, request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpDelete]
        [Route(ProductRoute.DeleteProduct)]
        [Authorize(Roles = UserConstant.USER_ROLE_ADMIN)]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }
    }
}
