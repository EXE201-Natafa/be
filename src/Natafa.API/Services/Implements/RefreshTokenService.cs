using Natafa.Api.Helper;
using Natafa.Api.Models.AuthenticationModel;
using Natafa.Api.Services.Interfaces;
using Natafa.Domain.Entities;
using Natafa.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natafa.Api.Services.Implements
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IUnitOfWork _uow;
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;
        private readonly ITokenValidator _tokenValidator;
        public RefreshTokenService( IUnitOfWork uow,
            IAuthenticationService authenticationService,
            IUserService userService,
            ITokenValidator tokenValidator)
        {
            _uow = uow;
            _authenticationService = authenticationService;
            _userService = userService;
            _tokenValidator = tokenValidator;
        }
        public async Task<MethodResult<AccessToken>> RefreshTokenAsync(string accessToken) => await
            _tokenValidator.GetEmailFromExpiredAccessToken(accessToken)
            .Bind(email => _userService.GetByEmailAsync(email)).Result
            .Bind(user => GetByUserIdAsync(user.UserId).Result.Bind(async refreshToken =>
            {
                var isValid = _tokenValidator.ValidateRefreshToken(refreshToken.Token);
                if (!isValid)
                {
                    return new MethodResult<AccessToken>.Failure("Invalid token", 401);
                }
                return await _authenticationService.AuthenticateAsync(user);
            }));

        public async Task<MethodResult<RefreshToken>> GetByUserIdAsync(int userId)
        {
            var refreshToken = await _uow.GetRepository<RefreshToken>().SingleOrDefaultAsync(
                predicate: p => p.UserId == userId
            );
            if (refreshToken == null)
            {
                return new MethodResult<RefreshToken>.Failure("No refresh token found for this user", 400);
            }
            return new MethodResult<RefreshToken>.Success(refreshToken);
        }
    }
}
