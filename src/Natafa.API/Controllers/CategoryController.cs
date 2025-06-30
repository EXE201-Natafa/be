using Natafa.Api.Constants;
using Natafa.Api.Helper;
using Natafa.Api.Models.CategoryModel;
using Natafa.Api.Routes;
using Natafa.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Natafa.Api.Routes.Router;

namespace Natafa.Api.Controllers
{
    /// <summary>
    /// Controller quản lý các danh mục (categories).
    /// </summary>
    public class CategoryController : BaseApiController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Lấy danh sách các danh mục.
        /// </summary>
        /// <remarks>
        /// Không yêu cầu Role.
        /// </remarks>
        /// <returns>Danh sách danh mục.</returns>
        [HttpGet]
        [Route(CategoryRoute.GetCategories)]
        public async Task<ActionResult> GetCategories()
        {
            var result = await _categoryService.GetCategoriesAsync();
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Lấy thông tin chi tiết của một danh mục.
        /// </summary>
        /// <remarks>
        /// Không yêu cầu Role.
        /// </remarks>
        /// <param name="id">ID của danh mục.</param>
        /// <returns>Thông tin chi tiết của danh mục.</returns>
        [HttpGet]
        [Route(CategoryRoute.GetCategory)]
        public async Task<ActionResult> GetCategory(int id)
        {
            var result = await _categoryService.GetCategoryByIdAsync(id);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Tạo mới một danh mục.
        /// </summary>
        /// <remarks>
        /// Yêu cầu Role: Staff.
        /// </remarks>
        /// <param name="request">Thông tin danh mục cần tạo.</param>
        /// <returns>Kết quả tạo danh mục.</returns>
        [HttpPost]
        [Route(CategoryRoute.CreateCategory)]
        [Authorize(Roles = UserConstant.USER_ROLE_STAFF)]
        public async Task<ActionResult> CreateCategory([FromBody] CategoryCreateRequest request)
        {
            var result = await _categoryService.CreateCategoryAsync(request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Cập nhật thông tin danh mục.
        /// </summary>
        /// <remarks>
        /// Yêu cầu Role: Staff.
        /// </remarks>
        /// <param name="id">ID của danh mục cần cập nhật.</param>
        /// <param name="request">Thông tin cập nhật.</param>
        /// <returns>Kết quả cập nhật danh mục.</returns>
        [HttpPut]
        [Route(CategoryRoute.UpdateCategory)]
        [Authorize(Roles = UserConstant.USER_ROLE_STAFF)]
        public async Task<ActionResult> UpdateCategory(int id, [FromBody] CategoryUpdateRequest request)
        {
            var result = await _categoryService.UpdateCategoryAsync(id, request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Xóa danh mục.
        /// </summary>
        /// <remarks>
        /// Yêu cầu Role: Staff.
        /// </remarks>
        /// <param name="id">ID của danh mục cần xóa.</param>
        /// <returns>Kết quả xóa danh mục.</returns>
        [HttpDelete]
        [Route(CategoryRoute.DeleteCategory)]
        [Authorize(Roles = UserConstant.USER_ROLE_STAFF)]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }
    }
}
