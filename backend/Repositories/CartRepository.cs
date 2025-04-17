using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;
using Backend.Interfaces;

namespace Backend.Repositories
{
  public class CartRepository : ICartRepository
  {
    private readonly ApplicationDbContext _context;

    public CartRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<Cart> GetByUserIdAsync(string userId)
    {
      var cart = await _context.Carts
          .Include(c => c.Items)
          .FirstOrDefaultAsync(c => c.UserId == userId);

      if (cart == null)
      {
        cart = new Cart { UserId = userId };
        _context.Carts.Add(cart);
        await _context.SaveChangesAsync();
      }

      return cart;
    }

    public async Task<Cart> AddItemAsync(string userId, CartItem item)
    {
      var cart = await GetByUserIdAsync(userId);
      var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == item.ProductId);

      if (existingItem != null)
      {
        existingItem.Quantity += item.Quantity;
      }
      else
      {
        cart.Items.Add(item);
      }

      cart.LastModified = DateTime.UtcNow;
      await _context.SaveChangesAsync();
      return cart;
    }

    public async Task<Cart> UpdateItemQuantityAsync(string userId, int productId, int quantity)
    {
      var cart = await GetByUserIdAsync(userId);
      var item = cart.Items.FirstOrDefault(i => i.ProductId == productId)
          ?? throw new KeyNotFoundException("Item not found in cart");

      item.Quantity = quantity;
      cart.LastModified = DateTime.UtcNow;
      await _context.SaveChangesAsync();
      return cart;
    }

    public async Task RemoveItemAsync(string userId, int productId)
    {
      var cart = await GetByUserIdAsync(userId);
      var item = cart.Items.FirstOrDefault(i => i.ProductId == productId)
          ?? throw new KeyNotFoundException("Item not found in cart");

      cart.Items.Remove(item);
      cart.LastModified = DateTime.UtcNow;
      await _context.SaveChangesAsync();
    }

    public async Task ClearAsync(string userId)
    {
      var cart = await GetByUserIdAsync(userId);
      cart.Items.Clear();
      cart.LastModified = DateTime.UtcNow;
      await _context.SaveChangesAsync();
    }
  }
}