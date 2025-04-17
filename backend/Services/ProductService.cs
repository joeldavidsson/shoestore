using Backend.Interfaces;
using Backend.Models;
using Backend.Services;

namespace Backend.Services;

public class ProductService : IProductService
{
  private readonly ExternalApiService _externalApiService;

  public ProductService(ExternalApiService externalApiService)
  {

    _externalApiService = externalApiService;
  }

  public async Task<PaginatedResult<Product>> GetAllProductsAsync(int page = 1, int pageSize = 20)
  {
    var allProducts = await _externalApiService.GetProductsFromExternalApi();

    return new PaginatedResult<Product>
    {
      Items = allProducts.Skip((page - 1) * pageSize).Take(pageSize),
      TotalCount = allProducts.Count(),
      CurrentPage = page,
      PageSize = pageSize
    };
  }

  public async Task<Product?> GetProductByIdAsync(int id)
  {
    var products = await _externalApiService.GetProductsFromExternalApi();
    return products.FirstOrDefault(p => p.Id == id);
  }

  public async Task<PaginatedResult<Product>> GetProductsByBrandAsync(string brand, int page = 1, int pageSize = 20)
  {
    var allProducts = await _externalApiService.GetProductsFromExternalApi();
    var filteredProducts = allProducts.Where(p =>
        p.Brand?.Equals(brand, StringComparison.OrdinalIgnoreCase) ?? false);

    return new PaginatedResult<Product>
    {
      Items = filteredProducts.Skip((page - 1) * pageSize).Take(pageSize),
      TotalCount = filteredProducts.Count(),
      CurrentPage = page,
      PageSize = pageSize
    };
  }

  public async Task<PaginatedResult<Product>> SearchProductsAsync(string searchTerm, int page = 1, int pageSize = 20)
  {
    var allProducts = await _externalApiService.GetProductsFromExternalApi();
    var filteredProducts = allProducts.Where(p =>
        (p.Brand?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
        (p.Description?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false)
    );

    return new PaginatedResult<Product>
    {
      Items = filteredProducts.Skip((page - 1) * pageSize).Take(pageSize),
      TotalCount = filteredProducts.Count(),
      CurrentPage = page,
      PageSize = pageSize
    };
  }
}
