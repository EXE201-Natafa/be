using Natafa.Api.Models.AuthenticationModel;
using Natafa.Api.Routes;
using Natafa.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static Natafa.Api.Routes.Router;

namespace Natafa.Api.Controllers
{
    /// <summary>
    /// Controller quản lý các thao tác xác thực và đăng nhập.
    /// </summary>
    public class AuthenticationController : BaseApiController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IRefreshTokenService _refreshTokenService;

        public AuthenticationController(IAuthenticationService authenticationService,
            IRefreshTokenService refreshTokenService)
        {
            _authenticationService = authenticationService;
            _refreshTokenService = refreshTokenService;
        }

        /// <summary>
        /// Đăng ký tài khoản mới.
        /// </summary>
        /// <remarks>
        /// Không yêu cầu Role.
        /// </remarks>
        /// <param name="request">Thông tin đăng ký của người dùng.</param>
        /// <returns>Kết quả đăng ký.</returns>
        [HttpPost]
        [Route(AtuthenticationRoute.Register)]
        public async Task<IActionResult> Register([FromBody] SignupRequest request)
        {
            var result = await _authenticationService.SignUpAsync(request);
            return result.Match(
                (errorMessage, statusCode) => Problem(detail: errorMessage, statusCode: statusCode),
                Ok
            );
        }

        /// <summary>
        /// Đăng nhập.
        /// </summary>
        /// <remarks>
        /// Không yêu cầu Role.
        /// </remarks>
        /// <param name="request">Thông tin đăng nhập của người dùng.</param>
        /// <returns>Token nếu đăng nhập thành công.</returns>
        [HttpPost]
        [Route(AtuthenticationRoute.Login)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authenticationService.SigninAsync(request);
            return result.Match(
                (errorMessage, statusCode) => Problem(detail: errorMessage, statusCode: statusCode),
                Ok
            );
        }

        /// <summary>
        /// Làm mới token.
        /// </summary>
        /// <remarks>
        /// Yêu cầu token hợp lệ trong Authorization Header.
        /// </remarks>
        /// <returns>Token mới nếu thành công.</returns>
        [HttpPut]
        [Route(AtuthenticationRoute.RefreshToken)]
        public async Task<IActionResult> Refresh()
        {
            var token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }
            var result = await _refreshTokenService.RefreshTokenAsync(token);
            return result.Match(
                (errorMessage, statusCode) => Problem(detail: errorMessage, statusCode: statusCode),
                Ok
            );
        }

        /// <summary>
        /// Xác minh email.
        /// </summary>
        /// <remarks>
        /// Yêu cầu token xác minh được gửi qua email.
        /// </remarks>
        /// <param name="token">Token xác minh email.</param>
        /// <returns>Kết quả xác minh email.</returns>
        [HttpPatch]
        [Route(AtuthenticationRoute.VerifyEmail)]
        public async Task<IActionResult> VerifyAccount(string token)
        {
            var result = await _authenticationService.VerifyEmailAsync(token);
            return result.Match(
                (errorMessage, statusCode) => Problem(detail: errorMessage, statusCode: statusCode),
                successMessage => Ok(new { message = successMessage })
            );
        }
    }
}
