using Natafa.Api.Constants;
using Natafa.Api.Models.OrderModel;
using Natafa.Api.Routes;
using Natafa.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Natafa.Api.Models;
using static Natafa.Api.Routes.Router;

namespace Natafa.Api.Controllers
{
    /// <summary>
    /// Controller quản lý các API liên quan đến đơn hàng.
    /// </summary>
    public class OrderController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly ICloudinaryService _cloud;

        public OrderController(IOrderService orderService, ICloudinaryService cloud)
        {
            _orderService = orderService;
            _cloud = cloud;
        }

        /// <summary>
        /// Tạo mới đơn hàng.
        /// </summary>
        /// <remarks>
        /// Yêu cầu Role: Customer.
        /// </remarks>
        /// <param name="voucherId">Mã giảm giá (nếu có).</param>
        /// <param name="request">Chi tiết đơn hàng.</param>
        /// <returns>Kết quả tạo đơn hàng.</returns>
        [HttpPost]
        [Route(OrderRoute.CreateOrder)]
        [Authorize(Roles = UserConstant.USER_ROLE_CUSTOMER)]
        public async Task<ActionResult> CreateOrder(int? voucherId, [FromBody] OrderCreateRequest request)
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            var result = await _orderService.CreateOrderAsync(userId, voucherId, request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Xác nhận đơn hàng.
        /// </summary>
        /// <remarks>
        /// Yêu cầu Role: Staff.
        /// </remarks>
        /// <param name="orderId">ID đơn hàng cần xác nhận.</param>
        /// <returns>Kết quả xác nhận đơn hàng.</returns>
        [HttpPost]
        [Route(OrderRoute.ConfirmOrder)]
        [Authorize(Roles = UserConstant.USER_ROLE_STAFF)]
        public async Task<ActionResult> ConfirmOrder(int orderId)
        {
            var result = await _orderService.ConfirmOrderAsync(orderId);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Hoàn thành đơn hàng.
        /// </summary>
        /// <remarks>
        /// Yêu cầu Role: Customer hoặc Staff.
        /// </remarks>
        /// <param name="orderId">ID đơn hàng cần hoàn thành.</param>
        /// <returns>Kết quả hoàn thành đơn hàng.</returns>
        [HttpPost]
        [Route(OrderRoute.CompleteOrder)]
        [Authorize(Roles = $"{UserConstant.USER_ROLE_CUSTOMER}, {UserConstant.USER_ROLE_STAFF}")]
        public async Task<ActionResult> CompleteOrder(int orderId)
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            var role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
            var result = await _orderService.CompleteOrderAsync(userId, orderId, role);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Hủy đơn hàng.
        /// </summary>
        /// <remarks>
        /// Yêu cầu Role: Customer hoặc Staff.
        /// </remarks>
        /// <param name="orderId">ID đơn hàng cần hủy.</param>
        /// <returns>Kết quả hủy đơn hàng.</returns>
        [HttpPost]
        [Route(OrderRoute.CancelOrder)]
        [Authorize(Roles = $"{UserConstant.USER_ROLE_CUSTOMER}, {UserConstant.USER_ROLE_STAFF}")]
        public async Task<ActionResult> CancelOrder(int orderId)
        {
            var role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
            var userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);

            var result = await _orderService.CancelOrderAsync(role, userId, orderId);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// trả đơn hàng.
        /// </summary>
        /// <remarks>
        /// Yêu cầu Role: Customer hoặc Staff.
        /// </remarks>
        /// <param name="orderId">ID đơn hàng cần hủy.</param>
        /// <returns>Kết quả trả đơn hàng.</returns>
        [HttpPost]
        [Route(OrderRoute.ReturnOrder)]
        [Authorize(Roles = $"{UserConstant.USER_ROLE_CUSTOMER}, {UserConstant.USER_ROLE_STAFF}")]
        public async Task<ActionResult> ReturnOrder(int orderId)
        {
            var role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
            var userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);

            var result = await _orderService.ReturnOrderAsync(role, userId, orderId);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Lấy danh sách đơn hàng của người dùng.
        /// </summary>
        /// <remarks>
        /// Yêu cầu Role: Customer.
        /// </remarks>
        /// <param name="request">Thông tin phân trang.</param>
        /// <param name="minAmount">Giá trị đơn hàng tối thiểu (tùy chọn).</param>
        /// <param name="maxAmount">Giá trị đơn hàng tối đa (tùy chọn).</param>
        /// <returns>Danh sách đơn hàng.</returns>
        [HttpGet]
        [Route(OrderRoute.GetUserOrder)]
        [Authorize(Roles = UserConstant.USER_ROLE_CUSTOMER)]
        public async Task<ActionResult> GetOrdersByUser([FromQuery] PaginateRequest request, decimal? minAmount, decimal? maxAmount)
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            var result = await _orderService.GetOrdersByUserAsync(userId, request, minAmount, maxAmount);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Lấy tất cả đơn hàng.
        /// </summary>
        /// <remarks>
        /// Yêu cầu Role: Staff.
        /// </remarks>
        /// <param name="request">Thông tin phân trang.</param>
        /// <param name="minAmount">Giá trị đơn hàng tối thiểu (tùy chọn).</param>
        /// <param name="maxAmount">Giá trị đơn hàng tối đa (tùy chọn).</param>
        /// <returns>Danh sách tất cả đơn hàng.</returns>
        [HttpGet]
        [Route(OrderRoute.GetAllOrder)]
        [Authorize(Roles = UserConstant.USER_ROLE_STAFF)]
        public async Task<ActionResult> GetAllOrders([FromQuery] PaginateRequest request, decimal? minAmount, decimal? maxAmount)
        {
            var result = await _orderService.GetAllOrdersAsync(request, minAmount, maxAmount);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Tính phí vận chuyển.
        /// </summary>
        /// <param name="inRegion">True nếu giao hàng trong khu vực, False nếu ngoài khu vực.</param>
        /// <param name="request">Chi tiết sản phẩm trong đơn hàng.</param>
        /// <returns>Chi phí vận chuyển.</returns>
        [HttpPost]
        [Route(OrderRoute.GetShippingCost)]
        public async Task<IActionResult> GetShippingCost(bool inRegion, List<OrderDetailCreateRequest> request)
        {
            var result = await _orderService.GetShippingCostAsync(inRegion, request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }
    }
}
