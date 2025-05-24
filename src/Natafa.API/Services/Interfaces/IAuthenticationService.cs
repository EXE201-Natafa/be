using Natafa.Api.Helper;
using Natafa.Api.Models.AuthenticationModel;
using Natafa.Api.ViewModels;
using Natafa.Domain.Entities;

namespace Natafa.Api.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<MethodResult<AccessToken>> AuthenticateAsync(User user);
        Task<MethodResult<string>> SignUpAsync(SignupRequest request);
        Task<MethodResult<AccessToken>> SigninAsync(LoginRequest request);
        Task<MethodResult<string>> VerifyEmailAsync(string token);
    }
}
