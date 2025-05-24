using AutoMapper;
using Natafa.Api.Constants;
using Natafa.Api.Helper;
using Natafa.Api.Models.AuthenticationModel;
using Natafa.Api.Services.Interfaces;
using Natafa.Api.ViewModels;
using Natafa.Domain.Entities;
using Natafa.Repository.Interfaces;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Security.Principal;

namespace Natafa.Api.Services.Implements
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _uow;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ITokenValidator _tokenValidator;

        public AuthenticationService(IUnitOfWork uow, 
            ITokenGenerator tokenGenerator, 
            IMapper mapper, 
            IEmailService emailService, 
            ITokenValidator tokenValidator)
        {
            _uow = uow;
            _tokenGenerator = tokenGenerator;
            _mapper = mapper;
            _emailService = emailService;
            _tokenValidator = tokenValidator;
        }

        public async Task<MethodResult<AccessToken>> AuthenticateAsync(User user)
        {
            AccessToken accessToken = await _tokenGenerator.GenerateAccessToken(user);
            string refreshToken = await _tokenGenerator.GenerateRefreshToken();

            var refreshTokensInDb = await _uow.GetRepository<RefreshToken>().GetListAsync(
                predicate: p => p.UserId == user.UserId
            );
            _uow.GetRepository<RefreshToken>().DeleteRangeAsync(refreshTokensInDb);

            RefreshToken refreshTokenDTO = new()
            {
                Token = refreshToken,
                UserId = user.UserId
            };            
            await _uow.GetRepository<RefreshToken>().InsertAsync(refreshTokenDTO);

            await _uow.CommitAsync();
            return new MethodResult<AccessToken>.Success(accessToken);
        }

        public async Task<MethodResult<string>> SignUpAsync(SignupRequest request)
        {
            try
            {
                var dupeEmailUser = await GetUserByEmailAsync(request.Email);
                if (dupeEmailUser != null)
                {
                    return new MethodResult<string>.Failure("Email already in use", StatusCodes.Status400BadRequest);
                }

                var dupePhoneUser = await _uow.GetRepository<User>().SingleOrDefaultAsync(
                    predicate: p => p.PhoneNumber == request.PhoneNumber
                );
                if (dupePhoneUser != null)
                {
                    return new MethodResult<string>.Failure("Phone number already in use", StatusCodes.Status400BadRequest);
                }

                var user = _mapper.Map<User>(request);
                await _uow.GetRepository<User>().InsertAsync(user);
                await _uow.CommitAsync();

                return await _emailService.SendAccountVerificationEmailAsync(user);
            }
            catch (Exception e)
            {
                return new MethodResult<string>.Failure(e.ToString(), StatusCodes.Status500InternalServerError);
            }

        }

        public async Task<MethodResult<AccessToken>> SigninAsync(LoginRequest request)
        {
            var user = await GetUserByEmailAsync(request.Email);
            if (user == null)
            {
                return new MethodResult<AccessToken>.Failure("Invalid email", StatusCodes.Status400BadRequest);
            }

            var correctedPassword = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
            if (!correctedPassword)
            {
                return new MethodResult<AccessToken>.Failure("Password is not correct", 400);
            }

            if (user.Status == UserConstant.USER_STATUS_INACTIVE)
            {
                return new MethodResult<AccessToken>.Failure("Your account has been inactivated", 400);
            }

            return await AuthenticateAsync(user);
        }

        public async Task<MethodResult<string>> VerifyEmailAsync(string token)
        {
            try
            {
                var email = await _tokenValidator.ValidateEmailVerificationToken(token);
                var user = await GetUserByEmailAsync(email);
                user.ConfirmedEmail = UserConstant.USER_CONFIRMED_EMAIL_ACTIVE;
                _uow.GetRepository<User>().UpdateAsync(user);
                await _uow.CommitAsync();
                return new MethodResult<string>.Success("Verify email successfully");
            }
            catch (Exception e)
            {
                return new MethodResult<string>.Failure(e.ToString(), StatusCodes.Status500InternalServerError);
            }
        }

        private async Task<User> GetUserByEmailAsync(string email)
        {
            return await _uow.GetRepository<User>().SingleOrDefaultAsync(
                predicate: p => p.Email == email
            );
        }
    }
}
