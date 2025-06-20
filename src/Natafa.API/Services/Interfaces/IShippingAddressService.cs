using Natafa.Api.Helper;
using Natafa.Api.Models.ShippingAddressModel;
using Natafa.Api.ViewModels;
using Natafa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natafa.Api.Services.Interfaces
{
    public interface IShippingAddressService
    {

        Task<MethodResult<ShippingAddressResponse>> GetOneByIdAsync(int shippingAddressId);
        Task<MethodResult<IEnumerable<ShippingAddressResponse>>> GetAllByUserIdAsync(int customerId);
        Task<MethodResult<string>> CreateOneAsync(int userId, ShippingAddressRequest request);
        Task<MethodResult<string>> UpdateOneAsync(int userId, int shippingAddressId, ShippingAddressRequest request);
        Task<MethodResult<string>> DeleteOneAsync(int userId, int shippingAddressId);
    }
}
