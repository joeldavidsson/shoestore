using Backend.Models;

namespace Backend.Interfaces
{
  public interface ICartRepository
  {
    Task<Cart> GetByUserIdAsync(string userId);
    Task<Cart> AddItemAsync(string userId, CartItem item);
    Task<Cart> UpdateItemQuantityAsync(string userId, int productId, int quantity);
    Task RemoveItemAsync(string userId, int productId);
    Task ClearAsync(string userId);
  }
}