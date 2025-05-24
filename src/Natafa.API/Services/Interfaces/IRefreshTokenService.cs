using Natafa.Api.Helper;
using Natafa.Api.Models.AuthenticationModel;
using Natafa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natafa.Api.Services.Interfaces
{
    public interface IRefreshTokenService
    {
        Task<MethodResult<AccessToken>> RefreshTokenAsync(string token);
        Task<MethodResult<RefreshToken>> GetByUserIdAsync(int userId);
    }
}
