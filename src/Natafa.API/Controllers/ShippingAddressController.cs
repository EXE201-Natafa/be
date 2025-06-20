using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Tnef;
using Natafa.Api.Models.ShippingAddressModel;
using Natafa.Api.Services.Interfaces;
using System.Security.Claims;
using static Natafa.Api.Routes.Router;

namespace Natafa.Api.Controllers
{
    public class ShippingAddressController : BaseApiController
    {
        private readonly IShippingAddressService _shippingAddressService;

        public ShippingAddressController(IShippingAddressService shippingAddressService)
        {
            _shippingAddressService = shippingAddressService;
        }

        [HttpGet]
        [Route(ShippingAddressRoute.GetShippingAddresses)]
        [Authorize]
        public async Task<IActionResult> GetAllByUserId()
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            var result = await _shippingAddressService.GetAllByUserIdAsync(userId);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpGet]
        [Route(ShippingAddressRoute.GetUpdateDelete)]
        [Authorize]
        public async Task<IActionResult> GetOne(int id)
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            var result = await _shippingAddressService.GetAllByUserIdAsync(userId);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpPost]
        [Route(ShippingAddressRoute.CreateShippingAddress)]
        [Authorize]
        public async Task<IActionResult> CreateShippingAddress([FromBody] ShippingAddressRequest request)
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            var result = await _shippingAddressService.CreateOneAsync(userId, request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpPut]
        [Route(ShippingAddressRoute.GetUpdateDelete)]
        [Authorize]
        public async Task<IActionResult> UpdateShippingAddress(int id, [FromBody] ShippingAddressRequest request)
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            var result = await _shippingAddressService.UpdateOneAsync(userId, id, request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpDelete]
        [Route(ShippingAddressRoute.GetUpdateDelete)]
        [Authorize]
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
