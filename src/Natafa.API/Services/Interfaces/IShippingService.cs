using Natafa.Api.Helper;
using Natafa.Api.Models.OrderModel;

namespace Natafa.Api.Services.Interfaces
{
    public interface IShippingService
    {
        Task<decimal> GetShippingCostAsync(bool inRegion, List<OrderDetailCreateRequest> request);
        //Task<MethodResult<decimal>> GetShippingCostAsync(bool inRegion, List<OrderDetailCreateRequest> request);
    }
}
