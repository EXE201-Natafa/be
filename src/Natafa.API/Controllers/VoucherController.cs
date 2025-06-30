using Natafa.Api.Models.VoucherModel;
using Natafa.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Natafa.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Natafa.Api.Constants;
using System.Security.Claims;
using static Natafa.Api.Routes.Router;
using Natafa.Domain.Paginate;
using Natafa.Api.Models;

namespace Natafa.Api.Controllers
{
    /// <summary>
    /// Controller quản lý voucher.
    /// </summary>
    public class VoucherController : BaseApiController
    {
        private readonly IVoucherService _voucherService;

        public VoucherController(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }

        /// <summary>
        /// Lấy thông tin voucher theo ID.
        /// </summary>
        /// <param name="id">ID của voucher.</param>
        /// <returns>Thông tin chi tiết của voucher.</returns>
        [HttpGet]
        [Route(VoucherRoute.GetUpdateDelete)]
        public async Task<IActionResult> GetVoucherById(int id)
        {
            var result = await _voucherService.GetVoucherByIdAsync(id);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Lấy danh sách tất cả các voucher.
        /// </summary>
        /// <param name="request">Thông tin phân trang.</param>
        /// <returns>Danh sách voucher.</returns>
        [HttpGet]
        [Route(VoucherRoute.GetVouchers)]
        public async Task<IActionResult> GetAllVouchers([FromQuery] PaginateRequest request)
        {
            var result = await _voucherService.GetVouchersAsync(request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Lấy danh sách voucher của người dùng hiện tại (khách hàng).
        /// </summary>
        /// <param name="request">Thông tin phân trang.</param>
        /// <returns>Danh sách voucher.</returns>
        [HttpGet]
        [Route(VoucherRoute.GetMyVouchers)]
        [Authorize(Roles = UserConstant.USER_ROLE_CUSTOMER)]
        public async Task<IActionResult> GetMyVouchers([FromQuery] PaginateRequest request)
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            var result = await _voucherService.GetVouchersByUserIdAsync(userId, request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Lấy danh sách voucher theo ID người dùng (dành cho Staff).
        /// </summary>
        /// <param name="userId">ID người dùng.</param>
        /// <param name="request">Thông tin phân trang.</param>
        /// <returns>Danh sách voucher của người dùng.</returns>
        [HttpGet]
        [Route(VoucherRoute.GetVouchersByUserId)]
        [Authorize(Roles = UserConstant.USER_ROLE_STAFF)]
        public async Task<IActionResult> GetVouchersByUserId(int userId, [FromQuery] PaginateRequest request)
        {
            var result = await _voucherService.GetVouchersByUserIdAsync(userId, request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Tạo mới một voucher (dành cho Staff).
        /// </summary>
        /// <param name="request">Thông tin voucher cần tạo.</param>
        /// <returns>Kết quả tạo mới voucher.</returns>
        [HttpPost]
        [Route(VoucherRoute.CreateVoucher)]
        [Authorize(Roles = UserConstant.USER_ROLE_STAFF)]
        public async Task<IActionResult> CreateVoucher([FromBody] VoucherRequest request)
        {
            var result = await _voucherService.CreateVoucherAsync(request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Cập nhật thông tin voucher (dành cho Staff).
        /// </summary>
        /// <param name="id">ID voucher.</param>
        /// <param name="request">Thông tin cần cập nhật.</param>
        /// <returns>Kết quả cập nhật.</returns>
        [HttpPut]
        [Route(VoucherRoute.GetUpdateDelete)]
        [Authorize(Roles = UserConstant.USER_ROLE_STAFF)]
        public async Task<IActionResult> UpdateVoucher(int id, [FromBody] VoucherRequest request)
        {
            var result = await _voucherService.UpdateVoucherAsync(id, request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Xóa một voucher (dành cho Staff).
        /// </summary>
        /// <param name="id">ID voucher cần xóa.</param>
        /// <returns>Kết quả xóa.</returns>
        [HttpDelete]
        [Route(VoucherRoute.GetUpdateDelete)]
        [Authorize(Roles = UserConstant.USER_ROLE_STAFF)]
        public async Task<IActionResult> DeleteVoucher(int id)
        {
            var result = await _voucherService.DeleteVoucherAsync(id);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Kiểm tra và cập nhật trạng thái voucher đã hết hạn (dành cho Staff).
        /// </summary>
        /// <returns>Kết quả kiểm tra và cập nhật.</returns>
        [HttpPatch]
        [Route(VoucherRoute.ExpiredVoucher)]
        [Authorize(Roles = UserConstant.USER_ROLE_STAFF)]
        public async Task<IActionResult> CheckExpiredVouchers()
        {
            var result = await _voucherService.CheckExpiredVouchersAsync();
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Khách hàng nhận voucher.
        /// </summary>
        /// <param name="id">ID của voucher cần nhận.</param>
        /// <returns>Kết quả nhận voucher.</returns>
        [HttpPost]
        [Route(VoucherRoute.TakeVoucher)]
        [Authorize(Roles = UserConstant.USER_ROLE_CUSTOMER)]
        public async Task<IActionResult> TakeVoucher(int id)
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            var result = await _voucherService.TakeVoucherAsync(userId, id);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }
    }
}
