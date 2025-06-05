using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Natafa.Api.Helper;
using Natafa.Api.Models;
using Natafa.Api.Models.VoucherModel;
using Natafa.Api.Services.Interfaces;
using Natafa.Api.ViewModels;
using Natafa.Domain.Entities;
using Natafa.Domain.Paginate;
using Natafa.Repository.Interfaces;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Natafa.Api.Services.Implements
{

    public class VoucherService : IVoucherService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public VoucherService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<MethodResult<VoucherResponse>> GetVoucherByIdAsync(int id)
        {
            var voucher = await _uow.GetRepository<Voucher>().SingleOrDefaultAsync(
                predicate: v => v.VoucherId == id
            );
            if (voucher == null)
                return new MethodResult<VoucherResponse>.Failure("Voucher not found", 404);

            var response = _mapper.Map<VoucherResponse>(voucher);
            return new MethodResult<VoucherResponse>.Success(response);
        }

        public async Task<MethodResult<IPaginate<VoucherResponse>>> GetVouchersAsync(PaginateRequest request)
        {
            int page = request.page > 0 ? request.page : 1;
            int size = request.size > 0 ? request.size : 10;
            string search = request.search?.ToLower() ?? string.Empty;
            string filter = request.filter?.ToLower() ?? string.Empty;

            Expression<Func<Voucher, bool>> predicate = p =>
                (string.IsNullOrEmpty(search) || p.VoucherName.Contains(search) ||
                                                 p.VoucherCode.Contains(search)) &&
                (string.IsNullOrEmpty(filter) ||
                    (filter.Equals("active") && p.Status) ||
                     (filter.Equals("inactive") && !p.Status));

            var result = await _uow.GetRepository<Voucher>().GetPagingListAsync<VoucherResponse>(
                    selector: s => _mapper.Map<VoucherResponse>(s),
                    predicate: predicate,
                    orderBy: BuildOrderBy(request.sortBy),
                    page: page,
                    size: size
                );
            return new MethodResult<IPaginate<VoucherResponse>>.Success(result);
        }

        public async Task<MethodResult<IPaginate<VoucherResponse>>> GetVouchersByUserIdAsync(int userId, PaginateRequest request)
        {
            int page = request.page > 0 ? request.page : 1;
            int size = request.size > 0 ? request.size : 10;
            string search = request.search?.ToLower() ?? string.Empty;
            string filter = request.filter?.ToLower() ?? string.Empty;

            Expression<Func<Voucher, bool>> predicate = p =>
                (string.IsNullOrEmpty(search) || p.VoucherName.Contains(search) ||
                                                 p.VoucherCode.Contains(search)) &&
                (string.IsNullOrEmpty(filter) ||
                    (filter.Equals("active") && p.Status) ||
                     (filter.Equals("inactive") && !p.Status)) &&
                p.UserVouchers.Any(x => x.UserId == userId);

            var result = await _uow.GetRepository<Voucher>().GetPagingListAsync<VoucherResponse>(
                    selector: s => _mapper.Map<VoucherResponse>(s),
                    predicate: predicate,
                    include: i => i.Include(x => x.UserVouchers),
                    orderBy: BuildOrderBy(request.sortBy),
                    page: page,
                    size: size
                );
            return new MethodResult<IPaginate<VoucherResponse>>.Success(result);
        }

        private Func<IQueryable<Voucher>, IOrderedQueryable<Voucher>> BuildOrderBy(string sortBy)
        {
            if (string.IsNullOrEmpty(sortBy)) return null;

            return sortBy.ToLower() switch
            {
                "amount" => q => q.OrderBy(p => p.DiscountAmount),
                "amount_desc" => q => q.OrderByDescending(p => p.DiscountAmount),
                _ => q => q.OrderByDescending(p => p.VoucherId) // Default sort
            };
        }

        public async Task<MethodResult<string>> CreateVoucherAsync(VoucherRequest request)
        {
            var voucher = _mapper.Map<Voucher>(request);
            await _uow.GetRepository<Voucher>().InsertAsync(voucher);
            await _uow.CommitAsync();

            var response = _mapper.Map<VoucherResponse>(voucher);
            return new MethodResult<string>.Success("Voucher created successfully");
        }

        public async Task<MethodResult<string>> UpdateVoucherAsync(int id, VoucherRequest request)
        {
            var voucher = await _uow.GetRepository<Voucher>().SingleOrDefaultAsync(
                predicate: v => v.VoucherId == id
            );
            if (voucher == null)
                return new MethodResult<string>.Failure("Voucher not found", 404);

            _mapper.Map(request, voucher);
            _uow.GetRepository<Voucher>().UpdateAsync(voucher);
            await _uow.CommitAsync();

            return new MethodResult<string>.Success("Voucher updated successfully");
        }

        public async Task<MethodResult<string>> DeleteVoucherAsync(int id)
        {
            var voucher = await _uow.GetRepository<Voucher>().SingleOrDefaultAsync(
                predicate: v => v.VoucherId == id
            );
            if (voucher == null)
                return new MethodResult<string>.Failure("Voucher not found", 404);

            _uow.GetRepository<Voucher>().DeleteAsync(voucher);
            await _uow.CommitAsync();

            return new MethodResult<string>.Success("Voucher deleted successfully");
        }

        public async Task<MethodResult<string>> CheckExpiredVouchersAsync()
        {
            var expiredVouchers = await _uow.GetRepository<Voucher>().GetListAsync(
                predicate: v => v.Status == true && v.EndDate < DateTime.Now,
                selector: v => _mapper.Map<VoucherResponse>(v)
            );

            foreach (var expired in expiredVouchers)
            {
                var entity = await _uow.GetRepository<Voucher>().SingleOrDefaultAsync(
                    predicate: v => v.VoucherId == expired.VoucherId
                );
                entity.Status = false;
                _uow.GetRepository<Voucher>().UpdateAsync(entity);
            }

            await _uow.CommitAsync();
            return new MethodResult<string>.Success("Expired vouchers updated");
        }

        public async Task<MethodResult<string>> TakeVoucherAsync(int userId, int voucherId)
        {
            var voucher = await _uow.GetRepository<Voucher>().SingleOrDefaultAsync(
                predicate: v => v.VoucherId == voucherId
            );
            if (voucher == null)
            {
                return new MethodResult<string>.Failure("Voucher not found", StatusCodes.Status404NotFound);
            }                
            if (voucher.Status == false)
            {
                return new MethodResult<string>.Failure("Invalid voucher", StatusCodes.Status400BadRequest);
            }
            
            var userVouchers = await _uow.GetRepository<UserVoucher>().GetListAsync(
                predicate: v => v.VoucherId == voucherId
            );

            if (voucher.UsageLimit == userVouchers.Count)
            {
                return new MethodResult<string>.Failure("Out of voucher", StatusCodes.Status400BadRequest);
            }
            if (userVouchers.Any(v => v.UserId == userId))
            {
                return new MethodResult<string>.Failure("You have already taken this voucher", StatusCodes.Status400BadRequest);
            }
            var userVoucher = new UserVoucher
            {
                UserId = userId,
                VoucherId = voucherId                
            };

            await _uow.GetRepository<UserVoucher>().InsertAsync(userVoucher);
            await _uow.CommitAsync();
            return new MethodResult<string>.Success("Take voucher successfully");
        }
    }


}
