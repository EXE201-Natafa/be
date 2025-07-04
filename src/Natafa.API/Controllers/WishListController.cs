using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Natafa.Api.Constants;
using Natafa.Api.Models;
using Natafa.Api.Services.Interfaces;
using System.Security.Claims;
using static Natafa.Api.Routes.Router;

namespace Natafa.Api.Controllers
{
    /// <summary>
    /// Controller quản lý danh sách yêu thích của người dùng.
    /// </summary>
    public class WishListController : BaseApiController
    {
        private readonly IWishListService _wishListService;

        /// <summary>
        /// Khởi tạo controller WishList.
        /// </summary>
        /// <param name="wishListService">Dịch vụ quản lý danh sách yêu thích.</param>
        public WishListController(IWishListService wishListService)
        {
            _wishListService = wishListService;
        }

        /// <summary>
        /// Lấy danh sách yêu thích của người dùng.
        /// </summary>
        /// <param name="request">Thông tin phân trang.</param>
        /// <returns>Danh sách sản phẩm trong danh sách yêu thích.</returns>
        [HttpGet]
        [Route(WishListRoute.GetWishList)]
        [Authorize(Roles = UserConstant.USER_ROLE_CUSTOMER)]
        public async Task<ActionResult> GetWishList([FromQuery] PaginateRequest request)
        {
            // Lấy ID người dùng từ Claims.
            var userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);

            // Gọi dịch vụ để lấy danh sách yêu thích.
            var result = await _wishListService.GetWishListAsync(userId, request);

            // Xử lý kết quả trả về.
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Thêm sản phẩm vào danh sách yêu thích của người dùng.
        /// </summary>
        /// <param name="id">ID sản phẩm cần thêm.</param>
        /// <returns>Kết quả thêm sản phẩm vào danh sách yêu thích.</returns>
        [HttpPost]
        [Route(WishListRoute.AddToWishList)]
        [Authorize(Roles = UserConstant.USER_ROLE_CUSTOMER)]
        public async Task<ActionResult> AddToWishList(int id)
        {
            // Lấy ID người dùng từ Claims.
            var userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);

            // Gọi dịch vụ để thêm sản phẩm vào danh sách yêu thích.
            var result = await _wishListService.AddToWishListAsync(userId, id);

            // Xử lý kết quả trả về.
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Xóa sản phẩm khỏi danh sách yêu thích của người dùng.
        /// </summary>
        /// <param name="id">ID sản phẩm cần xóa.</param>
        /// <returns>Kết quả xóa sản phẩm vào danh sách yêu thích.</returns>
        [HttpDelete]
        [Route(WishListRoute.DeleteFromWishList)]
        [Authorize(Roles = UserConstant.USER_ROLE_CUSTOMER)]
        public async Task<ActionResult> DeleteFromWishList(int id)
        {
            // Lấy ID người dùng từ Claims.
            var userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);

            // Gọi dịch vụ để thêm sản phẩm vào danh sách yêu thích.
            var result = await _wishListService.DeleteFromWishListAsync(userId, id);

            // Xử lý kết quả trả về.
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }
    }
}
