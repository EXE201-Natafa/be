using Natafa.Api.Helper;
using Natafa.Api.Models;
using Natafa.Api.Models.OrderModel;
using Natafa.Api.ViewModels;
using Natafa.Domain.Paginate;

namespace Natafa.Api.Services.Interfaces
{
    public interface IOrderService
    {
        Task<MethodResult<string>> CreateOrderAsync(int userId, int? voucherId, OrderCreateRequest request);
        Task<MethodResult<IPaginate<OrderResponse>>> GetOrdersByUserAsync(int userId, PaginateRequest request, decimal? minAmount, decimal? maxAmount);
        Task<MethodResult<IPaginate<OrderResponse>>> GetAllOrdersAsync(PaginateRequest request, decimal? minAmount, decimal? maxAmount);
        Task<MethodResult<string>> ConfirmOrderAsync(int orderId);
        Task<MethodResult<string>> DenyOrderAsync(int orderId);
        Task<MethodResult<string>> CompleteOrderAsync(int userId, int orderId, string role);
        Task<MethodResult<string>> CancelOrderAsync(string role, int userId, int orderId);
        Task<MethodResult<string>> ReturnOrderAsync(string role, int userId, int orderId);
        Task<MethodResult<string>> GetShippingCostAsync(bool inRegion, List<OrderDetailCreateRequest> request);
    }
}
