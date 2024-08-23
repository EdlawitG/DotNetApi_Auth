using authApi.DTos;
using authApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace authApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(AuthService service) : Controller
    {
        private readonly AuthService _service = service;

        [HttpPost("/register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _service.Register(request);
                return Ok(new { message = "User registered successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the user", error = ex.Message });
            }
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _service.Login(request);
                return Ok(new { message = "Logged In" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while loggingin", error = ex.Message });

            }
        }
        [HttpPost("/logout")]
        public async Task<IActionResult> Logout()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _service.LogOut();
                return Ok(new { message = "Logged Out" });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "An error occurred while LoggingOut", error = ex.Message });

            }
        }
    }
}
