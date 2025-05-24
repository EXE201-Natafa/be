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
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ICloudinaryService _cloud;

        public OrderController(IOrderService orderService, ICloudinaryService cloud)
        {
            _orderService = orderService;
            _cloud = cloud;
        }

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

        [HttpPost]
        [Route(OrderRoute.CompleteOrder)]
        [Authorize(Roles = $"{UserConstant.USER_ROLE_CUSTOMER}, {UserConstant.USER_ROLE_STAFF}")]
        public async Task<ActionResult> CompleteOrder(int orderId)
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            var role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
            var result = await _orderService.CompleteOrderAsync(userId, orderId , role);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

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
