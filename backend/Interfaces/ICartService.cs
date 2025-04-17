using Backend.Models;
using Backend.Models.DTOs;

namespace Backend.Interfaces
{
  public interface ICartService
  {
    Task<CartDto> GetCartAsync(string userId);
    Task<CartDto> AddItemToCartAsync(string userId, CartItemDto item);
    Task<CartDto> UpdateItemQuantityAsync(string userId, int productId, int quantity);
    Task RemoveItemAsync(string userId, int productId);
    Task ClearCartAsync(string userId);
  }
}