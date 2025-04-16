using Backend.Data;
using Backend.Models;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class CartRepository : ICartRepository
{
  private readonly ApplicationDbContext _context;

  public CartRepository(ApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<Cart> GetCartByUserIdAsync(string userId)
  {
    return await _context.Carts
        .Include(c => c.Items)
        .FirstOrDefaultAsync(c => c.UserId == userId) ?? throw new InvalidOperationException("Cart not found.");
  }

  public async Task<Cart> AddToCartAsync(string userId, CartItem item)
  {
    var cart = await GetCartByUserIdAsync(userId);
    if (cart == null)
    {
      cart = new Cart { UserId = userId };
      _context.Carts.Add(cart);
    }

    cart.Items.Add(item);
    await _context.SaveChangesAsync();
    return cart;
  }

  public async Task<bool> RemoveFromCartAsync(string userId, int productId)
  {
    var cart = await GetCartByUserIdAsync(userId);
    var item = cart?.Items.FirstOrDefault(i => i.ProductId == productId);

    if (item == null) return false;

    cart?.Items.Remove(item);
    await _context.SaveChangesAsync();
    return true;
  }
}