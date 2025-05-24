using Natafa.Api.Helper;
using Natafa.Api.Models;
using Natafa.Api.Models.VoucherModel;
using Natafa.Domain.Paginate;

namespace Natafa.Api.Services.Interfaces
{
    public interface IVoucherService
    {
        Task<MethodResult<VoucherResponse>> GetVoucherByIdAsync(int id);
        Task<MethodResult<IPaginate<VoucherResponse>>> GetVouchersAsync(PaginateRequest request);
        Task<MethodResult<IPaginate<VoucherResponse>>> GetVouchersByUserIdAsync(int userId, PaginateRequest request);
        Task<MethodResult<string>> CreateVoucherAsync(VoucherRequest request);
        Task<MethodResult<string>> UpdateVoucherAsync(int id, VoucherRequest request);
        Task<MethodResult<string>> DeleteVoucherAsync(int id);
        Task<MethodResult<string>> CheckExpiredVouchersAsync();
        Task<MethodResult<string>> TakeVoucherAsync(int userId, int voucherId);
    }


}
