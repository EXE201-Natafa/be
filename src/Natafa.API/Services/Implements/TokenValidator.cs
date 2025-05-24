using Natafa.Api.Helper;
using Natafa.Api.Models.Configurations;
using Natafa.Api.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;
using System.Text;


namespace Natafa.Api.Services.Implements
{
    public class TokenValidator : ITokenValidator
    {
        private readonly AuthenticationConfiguration _configuration;

        public TokenValidator(IOptions<AuthenticationConfiguration> options)
        {
            _configuration = options.Value;
        }

        public ClaimsPrincipal? Validate(string secretKey, string token, bool validateLifeTime)
        {
            TokenValidationParameters validationParameters = new()
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                ValidIssuer = _configuration.Issuer,
                ValidAudience = _configuration.Audience,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = validateLifeTime,
                ClockSkew = TimeSpan.Zero
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var cp = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                return cp;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public MethodResult<string> GetEmailFromExpiredAccessToken(string token)
        {
            var cp = Validate(_configuration.AccessTokenSecret, token, false);
            var email = cp?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (email == null)
            {
                return new MethodResult<string>.Failure("Invalid token", 400);
            }
            return new MethodResult<string>.Success(email);
        }

        public bool ValidateRefreshToken(string token)
        {
            var cp = Validate(_configuration.RefreshTokenSecret, token, true);
            if (cp == null)
            {
                return false;
            }
            return true;
        }

        public async Task<string> ValidateEmailVerificationToken(string token)
        {
            var cp = Validate(_configuration.EmailVerificationSecret, token, true);
            var email = cp?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            //if (email == null)
            //{
            //    throw new Exception();
            //}
            return email;
        }
    }

}
