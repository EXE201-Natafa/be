using Natafa.Api.Models.AuthenticationModel;
using Natafa.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Natafa.Api.Services.Interfaces
{
    public interface ITokenGenerator
    {
        Task<AccessToken> GenerateAccessToken(User user);
        Task<string> GenerateRefreshToken();
        Task<string> GenerateEmailVerificationToken(string email);
    }
}
