using Backend.Models;
using Backend.Repositories;

namespace Backend.Interfaces;

public interface ICartRepository
{
  Task<Cart> GetCartByUserIdAsync(string userId);
  Task<Cart> AddToCartAsync(string userId, CartItem item);
  Task<bool> RemoveFromCartAsync(string userId, int productId);
}