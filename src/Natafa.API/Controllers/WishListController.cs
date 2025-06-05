using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Natafa.Api.Constants;
using Natafa.Api.Models;
using Natafa.Api.Services.Interfaces;
using System.Security.Claims;
using static Natafa.Api.Routes.Router;

namespace Natafa.Api.Controllers
{
    public class WishListController : BaseApiController
    {
        private readonly IWishListService _wishListService;

        public WishListController(IWishListService wishListService)
        {
            _wishListService = wishListService;
        }

        [HttpGet]
        [Route(WishListRoute.GetWishList)]
        [Authorize(Roles = UserConstant.USER_ROLE_CUSTOMER)]
        public async Task<ActionResult> GetWishList([FromQuery] PaginateRequest request)
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            var result = await _wishListService.GetWishListAsync(userId, request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpPost]
        [Route(WishListRoute.AddToWishList)]
        [Authorize(Roles = UserConstant.USER_ROLE_CUSTOMER)]
        public async Task<ActionResult> AddToWishList(int id)
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            var result = await _wishListService.AddToWishListAsync(userId, id);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }
    }
}
