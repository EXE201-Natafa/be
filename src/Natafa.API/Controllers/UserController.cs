using Natafa.Api.Constants;
using Natafa.Api.Models.AuthenticationModel;
using Natafa.Api.Models.UserModel;
using Natafa.Api.Routes;
using Natafa.Api.Services.Implements;
using Natafa.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Natafa.Api.Routes.Router;
using Natafa.Api.Models;

namespace Natafa.Api.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route(UserRoute.GetUpdateDeleteProfile)]
        public async Task<IActionResult> GetProfile()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null) return Unauthorized();

            var result = await _userService.GetProfileAsync(email);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

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

        [HttpPost]
        [Route(UserRoute.CreateUser)]
        [Authorize(Roles = UserConstant.USER_ROLE_ADMIN + "," + UserConstant.USER_ROLE_STAFF)]
        public async Task<IActionResult> CreateUser([FromBody] UserRequest request)
        {
            var result = await _userService.CreateUserAsync(request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }



        // Cập nhật thông tin user
        [HttpPut]
        [Route(UserRoute.GetUpdateDelete)]
        [Authorize(Roles = UserConstant.USER_ROLE_ADMIN + "," + UserConstant.USER_ROLE_STAFF)]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserRequest request)
        {
            var result = await _userService.UpdateUserAsync(id, request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        // Cập nhật trạng thái user
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
