using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Natafa.Api.Helper;
using Natafa.Api.Models;
using Natafa.Api.Services.Interfaces;
using Natafa.Api.ViewModels;
using Natafa.Domain.Entities;
using Natafa.Domain.Paginate;
using Natafa.Repository.Interfaces;
using System.Linq.Expressions;

namespace Natafa.Api.Services.Implements
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public TransactionService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<MethodResult<IPaginate<TransactionResponse>>> GetAllTransactionsAsync(PaginateRequest request, decimal? minAmount, decimal? maxAmount)
        {
            int page = request.page > 0 ? request.page : 1;
            int size = request.size > 0 ? request.size : 10;
            string search = request.search?.ToLower() ?? string.Empty;
            string filter = request.filter?.ToLower() ?? string.Empty;

            Expression<Func<Transaction, bool>> predicate = p =>
                (string.IsNullOrEmpty(search) || p.Description.ToLower().Contains(search) || p.TransactionId.ToString().Contains(search)) &&
                //(string.IsNullOrEmpty(filter) || filter.Contains(p.Type.ToLower())) &&
                (minAmount == null || p.Amount >= minAmount) &&
                (maxAmount == null || p.Amount <= maxAmount);


            var result = await _uow.GetRepository<Transaction>().GetPagingListAsync<TransactionResponse>(
                    selector: s => _mapper.Map<TransactionResponse>(s),
                    predicate: predicate,
                    orderBy: BuildOrderBy(request.sortBy),
                    page: page,
                    size: size
                );

            return new MethodResult<IPaginate<TransactionResponse>>.Success(result);
        }

        public async Task<MethodResult<IPaginate<TransactionResponse>>> GetUserTransactionsAsync(int userId, PaginateRequest request, decimal? minAmount, decimal? maxAmount)
        {
            int page = request.page > 0 ? request.page : 1;
            int size = request.size > 0 ? request.size : 10;
            string search = request.search?.ToLower() ?? string.Empty;
            string filter = request.filter?.ToLower() ?? string.Empty;

            Expression<Func<Transaction, bool>> predicate = p =>
                (string.IsNullOrEmpty(search) || p.Description.ToLower().Contains(search) || p.TransactionId.ToString().Contains(search)) &&
                //(string.IsNullOrEmpty(filter) || filter.Contains(p.Type.ToLower())) &&
                (minAmount == null || p.Amount >= minAmount) &&
                (maxAmount == null || p.Amount <= maxAmount) &&
                p.Order.UserId == userId;


            var result = await _uow.GetRepository<Transaction>().GetPagingListAsync<TransactionResponse>(
                    selector: s => _mapper.Map<TransactionResponse>(s),
                    predicate: predicate,
                    include: i => i.Include(t => t.Order),
                    orderBy: BuildOrderBy(request.sortBy),
                    page: page,
                    size: size
                );

            return new MethodResult<IPaginate<TransactionResponse>>.Success(result);
        }

        private Func<IQueryable<Transaction>, IOrderedQueryable<Transaction>> BuildOrderBy(string sortBy)
        {
            if (string.IsNullOrEmpty(sortBy)) return null;

            return sortBy.ToLower() switch
            {
                "amount" => q => q.OrderBy(p => p.Amount),
                "amount_desc" => q => q.OrderByDescending(p => p.Amount),
                "date" => q => q.OrderBy(p => p.CreatedDate),
                "date_desc" => q => q.OrderByDescending(p => p.CreatedDate),
                _ => q => q.OrderByDescending(p => p.TransactionId) // Default sort
            };
        }

        public async Task<MethodResult<TransactionResponse>> GetTransactionByIdAsync(int transactionId)
        {
            var transaction = await _uow.GetRepository<Transaction>().SingleOrDefaultAsync(
                selector: s => _mapper.Map<TransactionResponse>(s),
                predicate: p => p.TransactionId == transactionId
            );
            if (transaction == null)
            {
                return new MethodResult<TransactionResponse>.Failure("Transaction not found", 404);
            }
            return new MethodResult<TransactionResponse>.Success(transaction);
        }
    }
}

