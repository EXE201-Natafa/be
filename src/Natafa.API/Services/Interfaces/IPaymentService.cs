using Natafa.Api.Helper;
using Natafa.Api.Models.VnPayModel;
using Natafa.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natafa.Api.Services.Interfaces
{
    public interface IPaymentService
    {
        VnPaymentResponseModel PaymentExecute(IQueryCollection collections);
        Task<MethodResult<string>> CreatePaymentAsync(string email, int orderId, HttpContext httpContext);
        Task<string> ProcessResponseAsync(VnPaymentResponseModel response);
        string GetRedirectUrl();
    }
}
