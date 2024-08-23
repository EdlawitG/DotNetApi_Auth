using System.ComponentModel.DataAnnotations;

namespace authApi.Dtos
{
    public class UpdateProRequest
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(20)]
        public string? ProductName { get; set; }
        [Required]
        [StringLength(100)]
        public string? Description { get; set; }
        [Required]
        public IFormFile? ImageUrl { get; set; }
        public string? Category { get; set; }
        public decimal? Price { get; set; }

    }
}