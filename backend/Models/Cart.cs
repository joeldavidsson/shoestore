using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
  public class Cart
  {
    public int Id { get; set; }

    [Required]
    public string? UserId { get; set; }

    public ApplicationUser? User { get; set; }

    public List<CartItem> Items { get; set; } = [];

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? LastModified { get; set; }

    [NotMapped]
    public decimal TotalAmount => Items?.Sum(item => item.Quantity * decimal.Parse(item.Price ?? "0")) ?? 0;
  }
}