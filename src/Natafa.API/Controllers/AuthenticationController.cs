using Natafa.Api.Models.AuthenticationModel;
using Natafa.Api.Routes;
using Natafa.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Natafa.Api.Services.Implements;
using static Natafa.Api.Routes.Router;

namespace Natafa.Api.Controllers
{
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
