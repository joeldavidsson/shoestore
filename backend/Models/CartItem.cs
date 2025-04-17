using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
  public class CartItem
  {
    public int Id { get; set; }

    public int CartId { get; set; }
    public Cart Cart { get; set; } = null!;

    [Required]
    public int ProductId { get; set; }

    [Required]
    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Brand { get; set; }

    [Required]
    public string? Price { get; set; }

    public string? ImageUrl { get; set; }

    [Required]
    public int Quantity { get; set; } = 1;
  }
}