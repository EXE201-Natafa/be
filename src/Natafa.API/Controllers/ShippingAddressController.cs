using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Natafa.Api.Models.ShippingAddressModel;
using Natafa.Api.Services.Interfaces;
using System.Security.Claims;
using static Natafa.Api.Routes.Router;

namespace Natafa.Api.Controllers
{
    /// <summary>
    /// Controller quản lý các địa chỉ giao hàng.
    /// </summary>
    [Authorize]
    public class ShippingAddressController : BaseApiController
    {
        private readonly IShippingAddressService _shippingAddressService;

        public ShippingAddressController(IShippingAddressService shippingAddressService)
        {
            _shippingAddressService = shippingAddressService;
        }

        /// <summary>
        /// Lấy tất cả địa chỉ giao hàng của người dùng.
        /// </summary>
        /// <returns>Danh sách địa chỉ giao hàng.</returns>
        [HttpGet]
        [Route(ShippingAddressRoute.GetShippingAddresses)]
        public async Task<IActionResult> GetAllByUserId()
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            var result = await _shippingAddressService.GetAllByUserIdAsync(userId);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Lấy thông tin chi tiết một địa chỉ giao hàng.
        /// </summary>
        /// <param name="id">ID của địa chỉ giao hàng.</param>
        /// <returns>Thông tin chi tiết địa chỉ giao hàng.</returns>
        [HttpGet]
        [Route(ShippingAddressRoute.GetUpdateDelete)]
        public async Task<IActionResult> GetOne(int id)
        {
            var result = await _shippingAddressService.GetOneByIdAsync(id); 
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Tạo mới một địa chỉ giao hàng.
        /// </summary>
        /// <param name="request">Thông tin địa chỉ giao hàng cần tạo.</param>
        /// <returns>Kết quả tạo mới.</returns>
        [HttpPost]
        [Route(ShippingAddressRoute.CreateShippingAddress)]
        public async Task<IActionResult> CreateShippingAddress([FromBody] ShippingAddressRequest request)
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            var result = await _shippingAddressService.CreateOneAsync(userId, request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Cập nhật thông tin một địa chỉ giao hàng.
        /// </summary>
        /// <param name="id">ID của địa chỉ giao hàng.</param>
        /// <param name="request">Thông tin mới của địa chỉ giao hàng.</param>
        /// <returns>Kết quả cập nhật.</returns>
        [HttpPut]
        [Route(ShippingAddressRoute.GetUpdateDelete)]
        public async Task<IActionResult> UpdateShippingAddress(int id, [FromBody] ShippingAddressRequest request)
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            var result = await _shippingAddressService.UpdateOneAsync(userId, id, request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Xóa một địa chỉ giao hàng.
        /// </summary>
        /// <param name="id">ID của địa chỉ giao hàng cần xóa.</param>
        /// <returns>Kết quả xóa.</returns>
        [HttpDelete]
        [Route(ShippingAddressRoute.GetUpdateDelete)]
        public async Task<IActionResult> DeleteShippingAddress(int id)
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            var result = await _shippingAddressService.DeleteOneAsync(userId, id);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }
    }
}
