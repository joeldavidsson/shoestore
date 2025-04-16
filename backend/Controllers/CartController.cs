// using Microsoft.AspNetCore.Mvc;
// using Models;

// [ApiController]
// [Route("api/[controller]")]
// public class CartController : ControllerBase
// {
//   private readonly ApplicationDbContext _context;

//   public CartController(ApplicationDbContext context)
//   {
//     _context = context;
//   }

//   [HttpPost("add")]
//   public async Task<IActionResult> AddToCart([FromBody] Product product)
//   {
//     var userId = User.Identity?.Name; 
//     if (userId == null) return Unauthorized();

//     var cart = await _context.Carts.Include(c => c.Products).FirstOrDefaultAsync(c => c.UserId == userId);
//     if (cart == null)
//     {
//       cart = new Cart { UserId = userId, Products = new List<Product>() };
//       _context.Carts.Add(cart);
//     }

//     cart.Products.Add(product);
//     await _context.SaveChangesAsync();

//     return Ok(cart);
//   }

//   [HttpGet]
//   public async Task<IActionResult> GetCart()
//   {
//     var userId = User.Identity?.Name;
//     if (userId == null) return Unauthorized();

//     var cart = await _context.Carts.Include(c => c.Products).FirstOrDefaultAsync(c => c.UserId == userId);
//     return Ok(cart ?? new Cart { Products = new List<Product>() });
//   }
// }