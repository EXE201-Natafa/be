using Natafa.Api.Constants;
using Natafa.Api.Models.AuthenticationModel;
using Natafa.Api.Models.UserModel;
using Natafa.Api.Routes;
using Natafa.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Natafa.Api.Routes.Router;
using Natafa.Api.Models;

namespace Natafa.Api.Controllers
{
    /// <summary>
    /// Controller quản lý người dùng.
    /// </summary>
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Trả về thông tin hồ sơ của người dùng hiện đang đăng nhập.
        /// </summary>
        /// <returns>Thông tin hồ sơ của người dùng.</returns>
        [HttpGet]
        [Route(UserRoute.GetMyProfile)]
        [Authorize]
        public async Task<IActionResult> GetMyProfile()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null) return Unauthorized();

            var result = await _userService.GetMyProfileAsync(email);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Trả về thông tin hồ sơ của người dùng theo ID (chỉ dành cho Admin và Staff).
        /// </summary>
        /// <param name="userId">ID của người dùng.</param>
        /// <returns>Thông tin hồ sơ của người dùng.</returns>
        [HttpGet]
        [Route(UserRoute.GetProfileByUserId)]
        [Authorize(Roles = UserConstant.USER_ROLE_ADMIN + "," + UserConstant.USER_ROLE_STAFF)]
        public async Task<IActionResult> GetProfileByUserId(int userId)
        {
            var result = await _userService.GetProfileByUserIdAsync(userId);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Lấy danh sách tất cả người dùng (chỉ dành cho Admin).
        /// </summary>
        /// <param name="request">Thông tin phân trang.</param>
        /// <returns>Danh sách người dùng.</returns>
        [HttpGet]
        [Route(UserRoute.Users)]
        [Authorize(Roles = UserConstant.USER_ROLE_ADMIN)]
        public async Task<IActionResult> GetAllUsers([FromQuery] PaginateRequest request)
        {
            var result = await _userService.GetAllUsersAsync(request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Lấy danh sách người dùng theo ID voucher (chỉ dành cho Admin).
        /// </summary>
        /// <param name="voucherId">ID của voucher.</param>
        /// <param name="request">Thông tin phân trang.</param>
        /// <returns>Danh sách người dùng theo voucher.</returns>
        [HttpGet]
        [Route(UserRoute.GetUsersByVoucherId)]
        [Authorize(Roles = UserConstant.USER_ROLE_ADMIN)]
        public async Task<IActionResult> GetUsersByVoucherId(int voucherId, [FromQuery] PaginateRequest request)
        {
            var result = await _userService.GetUsersByVoucherIdAsync(voucherId, request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Cập nhật hồ sơ người dùng (dành cho khách hàng).
        /// </summary>
        /// <param name="request">Thông tin cần cập nhật.</param>
        /// <returns>Kết quả cập nhật.</returns>
        [HttpPatch]
        [Route(UserRoute.GetUpdateDeleteProfile)]
        [Authorize(Roles = UserConstant.USER_ROLE_CUSTOMER)]
        public async Task<IActionResult> UpdateProfile(UpdateProfileRequest request)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null) return Unauthorized();

            var result = await _userService.UpdateProfileAsync(email, request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Tạo mới một người dùng (chỉ dành cho Admin).
        /// </summary>
        /// <param name="request">Thông tin người dùng.</param>
        /// <returns>Kết quả tạo mới.</returns>
        [HttpPost]
        [Route(UserRoute.CreateUser)]
        [Authorize(Roles = UserConstant.USER_ROLE_ADMIN)]
        public async Task<IActionResult> CreateUser([FromBody] UserRequest request)
        {
            var result = await _userService.CreateUserAsync(request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Cập nhật thông tin người dùng (chỉ dành cho Admin).
        /// </summary>
        /// <param name="id">ID của người dùng.</param>
        /// <param name="request">Thông tin cần cập nhật.</param>
        /// <returns>Kết quả cập nhật.</returns>
        [HttpPut]
        [Route(UserRoute.GetUpdateDelete)]
        [Authorize(Roles = UserConstant.USER_ROLE_ADMIN)]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserRequest request)
        {
            var result = await _userService.UpdateUserAsync(id, request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Cập nhật trạng thái của người dùng (chỉ dành cho Admin).
        /// </summary>
        /// <param name="id">ID của người dùng.</param>
        /// <param name="request">Trạng thái cần cập nhật.</param>
        /// <returns>Kết quả cập nhật.</returns>
        [HttpPatch]
        [Route(UserRoute.UpdateUserStatus)]
        [Authorize(Roles = UserConstant.USER_ROLE_ADMIN)]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusRequest request)
        {
            var result = await _userService.UpdateUserStatusAsync(id, request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }
    }
}
