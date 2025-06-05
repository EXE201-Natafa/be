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
    public class VoucherController : BaseApiController
    {
        private readonly IVoucherService _voucherService;

        public VoucherController(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }

        // Lấy thông tin voucher theo ID
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

        // Lấy danh sách tất cả các voucher
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

        [HttpGet]
        [Route(VoucherRoute.GetVouchersByUserId)]
        public async Task<IActionResult> GetVouchersByUserId(int userId, [FromQuery] PaginateRequest request)
        {
            var result = await _voucherService.GetVouchersByUserIdAsync(userId, request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        // Tạo mới một voucher
        [HttpPost]
        [Route(VoucherRoute.CreateVoucher)]
        public async Task<IActionResult> CreateVoucher([FromBody] VoucherRequest request)
        {
            var result = await _voucherService.CreateVoucherAsync(request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        // Cập nhật thông tin voucher
        [HttpPut]
        [Route(VoucherRoute.GetUpdateDelete)]
        public async Task<IActionResult> UpdateVoucher(int id, [FromBody] VoucherRequest request)
        {
            var result = await _voucherService.UpdateVoucherAsync(id, request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        // Xóa một voucher
        [HttpDelete]
        [Route(VoucherRoute.GetUpdateDelete)]
        public async Task<IActionResult> DeleteVoucher(int id)
        {
            var result = await _voucherService.DeleteVoucherAsync(id);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        // Kiểm tra và cập nhật các voucher đã hết hạn
        [HttpPatch]
        [Route(VoucherRoute.ExpiredVoucher)]
        public async Task<IActionResult> CheckExpiredVouchers()
        {
            var result = await _voucherService.CheckExpiredVouchersAsync();
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

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
