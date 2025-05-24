using Natafa.Api.Helper;
using Natafa.Api.Models;
using Natafa.Api.ViewModels;
using Natafa.Domain.Paginate;

namespace Natafa.Api.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<MethodResult<IPaginate<TransactionResponse>>> GetAllTransactionsAsync(PaginateRequest request, decimal? minAmount, decimal? maxAmount);
        Task<MethodResult<IPaginate<TransactionResponse>>> GetUserTransactionsAsync(int userId, PaginateRequest request, decimal? minAmount, decimal? maxAmount);
        Task<MethodResult<TransactionResponse>> GetTransactionByIdAsync(int transactionId);
    }
}
