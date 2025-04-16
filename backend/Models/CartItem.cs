using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
  public class CartItem
  {
    public int Id { get; set; }

    [Required]
    public int CartId { get; set; }
    public Cart? Cart { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    public string? ProductName { get; set; }

    [Required]
    public string? Price { get; set; }

    [Required]
    [Range(1, 100)]
    public int Quantity { get; set; }

    public string? Image { get; set; }
  }
}