using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Backend.Interfaces;
using Backend.Models.DTOs;

namespace Backend.Controllers
{
  [Authorize]
  [ApiController]
  [Route("api/[controller]")]
  public class CartController : ControllerBase
  {
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
      _cartService = cartService;
    }

    private string? GetUserId() => User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    [HttpGet]
    public async Task<ActionResult<CartDto>> GetCart()
    {
      var userId = GetUserId();
      if (userId == null) return Unauthorized();

      var cart = await _cartService.GetCartAsync(userId);
      return Ok(cart);
    }

    [HttpPost("items")]
    public async Task<ActionResult<CartDto>> AddItem([FromBody] CartItemDto dto)
    {
      var userId = GetUserId();
      if (userId == null) return Unauthorized();

      var cart = await _cartService.AddItemToCartAsync(userId, dto);
      return Ok(cart);
    }

    [HttpPut("items/{productId}")]
    public async Task<ActionResult<CartDto>> UpdateItemQuantity(
        int productId,
        [FromBody] int quantity)
    {
      var userId = GetUserId();
      if (userId == null) return Unauthorized();

      var cart = await _cartService.UpdateItemQuantityAsync(userId, productId, quantity);
      return Ok(cart);
    }

    [HttpDelete("items/{productId}")]
    public async Task<IActionResult> RemoveItem(int productId)
    {
      var userId = GetUserId();
      if (userId == null) return Unauthorized();

      await _cartService.RemoveItemAsync(userId, productId);
      return NoContent();
    }

    [HttpDelete("clear")]
    public async Task<IActionResult> ClearCart()
    {
      var userId = GetUserId();
      if (userId == null) return Unauthorized();

      await _cartService.ClearCartAsync(userId);
      return NoContent();
    }
  }
}