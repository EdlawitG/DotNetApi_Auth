using authApi.Data;
using authApi.DTos;
using authApi.Interface;
using authApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using authApi.Helper;
namespace authApi.Repository
{
    public class AuthRepository(ApplicationDbContext context, ILogger<AuthRepository> logger, IHttpContextAccessor httpContextAccessor, IMapper mapper) : IAuthentication
    {
        private readonly ApplicationDbContext _context = context;
        private readonly ILogger<AuthRepository> _logger = logger;
        private readonly IMapper _mapper = mapper;
        private readonly HttpContext _httpContext = httpContextAccessor.HttpContext ?? throw new InvalidOperationException("HttpContext is not available.");

        public async Task LoginAsync(LoginRequest request)
        {

            try
            {
                var user1 = _mapper.Map<User>(request);
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == user1.Email);

                if (user == null || !BCrypt.Net.BCrypt.Verify(user1.Password, user.Password))
                {
                    throw new UnauthorizedAccessException("Invalid username or password.");
                }

                GenerateToken.GenerateAccessToken(_httpContext, user.Id.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logging in.");
                throw new Exception("An error occurred while logging in.");
            }
        }

        public async Task RegisterAsync(RegisterRequest request)
        {
            try
            {
                var user1 = _mapper.Map<User>(request);
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == user1.Email || u.Username == user1.Username);

                if (existingUser != null)
                {
                    _logger.LogError($"User with email {user1.Email} or username {user1.Username} already exists.");
                    throw new Exception($"User with email {user1.Email} or username {user1.Username} already exists.");
                }

                var user = new User
                {
                    Email = user1.Email,
                    Username = user1.Username,
                    Password = BCrypt.Net.BCrypt.HashPassword(user1.Password)
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating user.");
                throw new Exception("An error occurred while creating user.");
            }
        }

        public async Task LogoutAsync()
        {
            try
            {
                await _httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logging out.");
                throw new Exception("An error occurred while logging out.");
            }
        }

        public Task<string> Register(RegisterRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<string> Login(LoginRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<string> LogOut()
        {
            throw new NotImplementedException();
        }
    }
}
