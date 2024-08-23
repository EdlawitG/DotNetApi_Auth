using authApi.DTos;
using authApi.Models;
using authApi.Repository;
using AutoMapper;
namespace authApi.Service
{
    public class AuthService(AuthRepository authRepository)
    {
        private readonly AuthRepository _authRepository = authRepository ?? throw new ArgumentNullException(nameof(authRepository));

        public async Task Register(RegisterRequest request)
        {
            await _authRepository.RegisterAsync(request);
        }

        public async Task Login(LoginRequest request)
        {
            
            await _authRepository.LoginAsync(request);
        }
        public async Task LogOut()
        {
            await _authRepository.LogoutAsync();
        }
    }
}
