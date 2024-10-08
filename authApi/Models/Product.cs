namespace authApi.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? Category { get; set; }
        public decimal? Price { get; set; }
    }
}