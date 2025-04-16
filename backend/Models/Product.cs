namespace Backend.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Brand { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Price { get; set; }
    }
}