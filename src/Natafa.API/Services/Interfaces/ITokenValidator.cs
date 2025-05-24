using Natafa.Api.Helper;

namespace Natafa.Api.Services.Interfaces
{
    public interface ITokenValidator
    {        
        MethodResult<string> GetEmailFromExpiredAccessToken(string token);
        bool ValidateRefreshToken(string token);
        Task<string> ValidateEmailVerificationToken(string token);
    }
}
