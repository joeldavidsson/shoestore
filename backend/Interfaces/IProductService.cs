using Backend.Models;

namespace Backend.Interfaces;

public interface IProductService
{
  Task<PaginatedResult<Product>> GetAllProductsAsync(int page = 1, int pageSize = 20);
  Task<Product?> GetProductByIdAsync(int id);
  Task<PaginatedResult<Product>> GetProductsByBrandAsync(string brand, int page = 1, int pageSize = 20);
  Task<PaginatedResult<Product>> SearchProductsAsync(string searchTerm, int page = 1, int pageSize = 20);
}
