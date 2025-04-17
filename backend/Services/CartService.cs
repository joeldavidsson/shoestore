using Backend.Interfaces;
using Backend.Models;
using Backend.Models.DTOs;

namespace Backend.Services
{
  public class CartService : ICartService
  {
    private readonly ICartRepository _cartRepository;

    public CartService(ICartRepository cartRepository)
    {
      _cartRepository = cartRepository;
    }

    public async Task<CartDto> GetCartAsync(string userId)
    {
      var cart = await _cartRepository.GetByUserIdAsync(userId);
      return MapToDto(cart);
    }

    public async Task<CartDto> AddItemToCartAsync(string userId, CartItemDto dto)
    {
      if (dto.Quantity < 1)
        throw new InvalidOperationException("Quantity must be greater than 0");

      var cartItem = new CartItem
      {
        ProductId = dto.ProductId,
        Name = dto.Name,
        Description = dto.Description,
        Brand = dto.Brand,
        Price = dto.Price,
        ImageUrl = dto.ImageUrl,
        Quantity = dto.Quantity
      };

      var cart = await _cartRepository.AddItemAsync(userId, cartItem);
      return MapToDto(cart);
    }

    public async Task<CartDto> UpdateItemQuantityAsync(string userId, int productId, int quantity)
    {
      if (quantity < 1)
        throw new InvalidOperationException("Quantity must be greater than 0");

      var cart = await _cartRepository.UpdateItemQuantityAsync(userId, productId, quantity);
      return MapToDto(cart);
    }

    public async Task RemoveItemAsync(string userId, int productId)
    {
      await _cartRepository.RemoveItemAsync(userId, productId);
    }

    public async Task ClearCartAsync(string userId)
    {
      await _cartRepository.ClearAsync(userId);
    }

    private static CartDto MapToDto(Cart cart)
    {
      return new CartDto
      {
        Id = cart.Id,
        UserId = cart.UserId!,
        Items = cart.Items.Select(i => new CartItemDto
        {
          Id = i.Id,
          ProductId = i.ProductId,
          Name = i.Name!,
          Description = i.Description,
          Brand = i.Brand,
          Price = i.Price!,
          ImageUrl = i.ImageUrl,
          Quantity = i.Quantity
        }).ToList(),
        CreatedAt = cart.CreatedAt,
        LastModified = cart.LastModified,
        TotalAmount = cart.TotalAmount
      };
    }
  }
}