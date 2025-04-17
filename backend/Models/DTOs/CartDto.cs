namespace Backend.Models.DTOs
{
  public class CartDto
  {
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public List<CartItemDto> Items { get; set; } = [];
    public DateTime CreatedAt { get; set; }
    public DateTime? LastModified { get; set; }
    public decimal TotalAmount { get; set; }
  }

  public class CartItemDto
  {
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Brand { get; set; }
    public string Price { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public int Quantity { get; set; }
  }

  public class UpdateCartItemQuantityDto
  {
    public int CartItemId { get; set; }
    public int Quantity { get; set; }
  }

  public class AddCartItemDto
  {
    public int ProductId { get; set; }
    public int Quantity { get; set; }
  }
}