using Natafa.Api.Helper;
using Natafa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natafa.Api.Services.Interfaces
{
    public interface IEmailService
    {
        Task<MethodResult<string>> SendAccountVerificationEmailAsync(User user);
    }
}
