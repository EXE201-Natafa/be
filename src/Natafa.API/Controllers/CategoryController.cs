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
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

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

        [HttpPost]
        [Route(CategoryRoute.CreateCategory)]
        [Authorize(Roles = UserConstant.USER_ROLE_ADMIN)]
        public async Task<ActionResult> CreateCategory([FromBody] CategoryCreateRequest request)
        {
            var result = await _categoryService.CreateCategoryAsync(request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpPut]
        [Route(CategoryRoute.UpdateCategory)]
        [Authorize(Roles = UserConstant.USER_ROLE_ADMIN)]
        public async Task<ActionResult> UpdateCategory(int id, [FromBody] CategoryUpdateRequest request)
        {
            var result = await _categoryService.UpdateCategoryAsync(id, request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpDelete]
        [Route(CategoryRoute.DeleteCategory)]
        [Authorize(Roles = UserConstant.USER_ROLE_ADMIN)]
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