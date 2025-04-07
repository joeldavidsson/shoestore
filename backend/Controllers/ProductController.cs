using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
  private readonly ProductApiService _productApiService;

  public ProductController(ProductApiService productApiService)
  {
    _productApiService = productApiService;
  }

  [HttpGet("malefootwear")]
  public async Task<IActionResult> GetMaleFootwear()
  {
    var products = await _productApiService.GetMaleFootwearAsync();
    return Ok(products);
  }
}
