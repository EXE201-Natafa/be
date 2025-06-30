using Natafa.Api.Constants;
using Natafa.Api.Models.FeedbackModel;
using Natafa.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Natafa.Api.Routes.Router;
using Natafa.Api.Models;

namespace Natafa.Api.Controllers
{
    /// <summary>
    /// Controller xử lý các thao tác liên quan đến Feedback.
    /// </summary>
    public class FeedbackController : BaseApiController
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        /// <summary>
        /// Tạo mới Feedback.
        /// </summary>
        /// <remarks>
        /// Yêu cầu Role: Customer
        /// </remarks>
        [HttpPost(FeedbackRoute.CreateFeedback)]
        [Authorize(Roles = UserConstant.USER_ROLE_CUSTOMER)]
        public async Task<IActionResult> CreateFeedback(FeedbackRequest request)
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            var result = await _feedbackService.CreateFeedbackAsync(userId, request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Lấy danh sách Feedback theo sản phẩm.
        /// </summary>
        /// <remarks>
        /// Không yêu cầu Role.
        /// </remarks>
        [HttpGet(FeedbackRoute.GetFeedbackByProduct)]
        public async Task<IActionResult> GetFeedbackByProduct(int productId, [FromQuery] PaginateRequest request, int? rating)
        {
            var result = await _feedbackService.GetFeedbackByProductAsync(productId, request, rating);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Lấy tất cả Feedback.
        /// </summary>
        /// <remarks>
        /// Yêu cầu Role: Staff hoặc Customer
        /// </remarks>
        [HttpGet(FeedbackRoute.GetAllFeedbacks)]
        [Authorize(Roles = $"{UserConstant.USER_ROLE_STAFF}, {UserConstant.USER_ROLE_CUSTOMER}")]
        public async Task<IActionResult> GetAllFeedbacks([FromQuery] PaginateRequest request, int? rating)
        {
            var result = await _feedbackService.GetAllFeedbacksAsync(request, rating);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Cập nhật trạng thái Feedback.
        /// </summary>
        /// <remarks>
        /// Yêu cầu Role: Staff
        /// </remarks>
        [HttpPut(FeedbackRoute.GetUpdateDelete)]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> UpdateFeedbackStatus(int id, [FromBody] UpdateFeedbackStatusRequest request)
        {
            var result = await _feedbackService.UpdateFeedbackStatusAsync(id, request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Xóa Feedback.
        /// </summary>
        /// <remarks>
        /// Yêu cầu Role: Bất kỳ Role nào được ủy quyền.
        /// </remarks>
        [HttpDelete]
        [Route(FeedbackRoute.GetUpdateDelete)]
        [Authorize]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            var result = await _feedbackService.DeleteFeedbackByIdAsync(userId, id);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }
    }
}
