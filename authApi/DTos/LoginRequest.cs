using System.ComponentModel.DataAnnotations;

namespace authApi.DTos
{
    public class LoginRequest
    {
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}