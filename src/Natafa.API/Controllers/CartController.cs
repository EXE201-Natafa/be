using Microsoft.AspNetCore.Mvc;
using Natafa.Api.Models;
using Natafa.Api.Services.Interfaces;
using Natafa.Domain.Entities;

namespace Natafa.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(AddToCartModel model)
    {
        var result = await _cartService.AddToCartAsync(model.UserId, model.ProductId, model.ProductDetailId, model.Quantity);
        if (result)
            return Ok(new { message = "Item added to cart successfully" });
        return BadRequest("Failed to add to cart. Please check if the product and product detail exist and match.");
    }

    [HttpDelete("{cartId}")]
    public async Task<IActionResult> RemoveFromCart(int cartId)
    {
        var result = await _cartService.RemoveFromCartAsync(cartId);
        if (result)
            return Ok();
        return NotFound("Cart item not found.");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCart(UpdateCartModel model)
    {
        var result = await _cartService.UpdateCartAsync(model.CartId, model.Quantity);
        if (result)
            return Ok();
        return NotFound("Cart item not found.");
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserCart(int userId)
    {
        var cartItems = await _cartService.GetUserCartAsync(userId);
        return Ok(cartItems);
    }
}

