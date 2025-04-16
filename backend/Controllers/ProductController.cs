using Microsoft.AspNetCore.Mvc;
using Backend.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
  private readonly IProductService _productService;

  public ProductsController(IProductService productService)
  {
    _productService = productService;
  }

  [HttpGet]
  public async Task<IActionResult> GetProducts([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
  {
    try
    {
      var products = await _productService.GetAllProductsAsync(page, pageSize);
      return Ok(products);
    }
    catch (Exception ex)
    {
      return StatusCode(500, $"Internal server error: {ex.Message}");
    }
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetProduct(int id)
  {
    try
    {
      var product = await _productService.GetProductByIdAsync(id);
      if (product == null)
        return NotFound($"Product with ID {id} not found");

      return Ok(product);
    }
    catch (Exception ex)
    {
      return StatusCode(500, $"Error fetching product: {ex.Message}");
    }
  }

  [HttpGet("brand/{brand}")]
  public async Task<IActionResult> GetProductsByBrand(string brand, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
  {
    try
    {
      var products = await _productService.GetProductsByBrandAsync(brand, page, pageSize);
      return Ok(products);
    }
    catch (Exception ex)
    {
      return StatusCode(500, $"Error fetching products: {ex.Message}");
    }
  }

  [HttpGet("search")]
  public async Task<IActionResult> SearchProducts(
        [FromQuery] string term,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
  {
    try
    {
      if (string.IsNullOrWhiteSpace(term))
      {
        return BadRequest("Search term cannot be empty");
      }

      var results = await _productService.SearchProductsAsync(term, page, pageSize);
      return Ok(results);
    }
    catch (Exception ex)
    {
      return StatusCode(500, $"Error searching products: {ex.Message}");
    }
  }
}
