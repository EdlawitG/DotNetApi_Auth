using System.ComponentModel.DataAnnotations;

namespace authApi.DTos
{
    public class RegisterRequest
    {
        [Required]
        [StringLength(30)]
        public string? Username { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}