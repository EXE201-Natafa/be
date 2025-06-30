using Natafa.Api.Constants;
using Natafa.Api.Routes;
using Natafa.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Natafa.Api.Models;

namespace Natafa.Api.Controllers
{
    /// <summary>
    /// Controller quản lý các giao dịch.
    /// </summary>
    public class TransactionController : BaseApiController
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        /// <summary>
        /// Lấy tất cả giao dịch (chỉ dành cho Admin).
        /// </summary>
        /// <param name="request">Thông tin phân trang.</param>
        /// <param name="minAmount">Số tiền tối thiểu.</param>
        /// <param name="maxAmount">Số tiền tối đa.</param>
        /// <returns>Danh sách giao dịch.</returns>
        [HttpGet]
        [Route(Router.TransactionRoute.GetAllTransactions)]
        [Authorize(Roles = UserConstant.USER_ROLE_ADMIN)]
        public async Task<ActionResult> GetAllTransactions([FromQuery] PaginateRequest request, decimal? minAmount, decimal? maxAmount)
        {
            var result = await _transactionService.GetAllTransactionsAsync(request, minAmount, maxAmount);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok);
        }

        /// <summary>
        /// Lấy danh sách giao dịch của người dùng (chỉ dành cho khách hàng).
        /// </summary>
        /// <param name="request">Thông tin phân trang.</param>
        /// <param name="minAmount">Số tiền tối thiểu.</param>
        /// <param name="maxAmount">Số tiền tối đa.</param>
        /// <returns>Danh sách giao dịch của người dùng.</returns>
        [HttpGet]
        [Route(Router.TransactionRoute.GetUserTransactions)]
        [Authorize(Roles = UserConstant.USER_ROLE_CUSTOMER)]
        public async Task<ActionResult> GetUserTransactions([FromQuery] PaginateRequest request, decimal? minAmount, decimal? maxAmount)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.Sid).Value);
            var result = await _transactionService.GetUserTransactionsAsync(userId, request, minAmount, maxAmount);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok);
        }

        /// <summary>
        /// Lấy thông tin chi tiết giao dịch.
        /// </summary>
        /// <param name="id">ID của giao dịch.</param>
        /// <returns>Thông tin giao dịch.</returns>
        [HttpGet]
        [Route(Router.TransactionRoute.GetTransaction)]
        public async Task<ActionResult> GetTransaction(int id)
        {
            var result = await _transactionService.GetTransactionByIdAsync(id);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok);
        }
    }
}
