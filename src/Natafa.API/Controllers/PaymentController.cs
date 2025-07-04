using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Natafa.Api.Services.Interfaces;
using Natafa.Api.Constants;
using Natafa.Api.Helper;
using Natafa.Api.Routes;

namespace Natafa.Api.Controllers
{
    /// <summary>
    /// Controller quản lý các chức năng liên quan đến thanh toán.
    /// </summary>
    public class PaymentController : BaseApiController
    {
        private readonly IPaymentService _paymentService;

        /// <summary>
        /// Khởi tạo controller Payment.
        /// </summary>
        /// <param name="paymentService">Dịch vụ xử lý thanh toán.</param>
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        /// <summary>
        /// Thực hiện thanh toán cho một đơn hàng.
        /// </summary>
        /// <param name="orderId">ID của đơn hàng cần thanh toán.</param>
        /// <returns>Kết quả thanh toán.</returns>
        [HttpPost]
        [Route(Router.PaymentRoute.MakePayment)]
        [Authorize(Roles = UserConstant.USER_ROLE_CUSTOMER)]
        public async Task<ActionResult> Payment(int orderId)
        {
            // Lấy email của người dùng từ Claims.
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null) return Unauthorized();

            // Tạo yêu cầu thanh toán qua dịch vụ thanh toán.
            var result = await _paymentService.CreatePaymentAsync(email, orderId, HttpContext);

            // Trả về kết quả xử lý.
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Xử lý callback sau khi thanh toán.
        /// </summary>
        /// <returns>Redirect tới URL kết quả thanh toán.</returns>
        [HttpGet]
        [Route(Router.PaymentRoute.PaymentCallBack)]
        public async Task<ActionResult> PaymentCallBack()
        {
            // Xử lý phản hồi thanh toán từ query parameters.
            var response = _paymentService.PaymentExecute(Request.Query);
            if (response == null) return BadRequest();

            // Xử lý phản hồi và cập nhật trạng thái thanh toán.
            var result = await _paymentService.ProcessResponseAsync(response);

            // Lấy URL chuyển hướng sau khi thanh toán hoàn tất.
            var redirectUrl = _paymentService.GetRedirectUrl();

            // Chuyển hướng đến trang kết quả thanh toán.
            return Redirect($"{redirectUrl}{result}");
        }

        /// <summary>
        /// Lấy các phương thức trả tiền.
        /// </summary>
        /// <returns>các phương thức trả tiền.</returns>
        [HttpGet]
        [Route(Router.PaymentRoute.GetPaymentMethod)]
        public async Task<ActionResult> GetPaymentMethod()
        {
            var result = await _paymentService.GetPaymentMethodAsync();
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }
    }
}
