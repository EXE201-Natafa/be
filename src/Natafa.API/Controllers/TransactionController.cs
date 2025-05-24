using Natafa.Api.Constants;
using Natafa.Api.Routes;
using Natafa.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Natafa.Api.Models;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Natafa.Api.Controllers
{
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

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
