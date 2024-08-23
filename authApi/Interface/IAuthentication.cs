
using authApi.DTos;

namespace authApi.Interface
{
    public interface IAuthentication
    {
        Task<string> Register(RegisterRequest request);
        Task<string> Login(LoginRequest request);
        Task <string> LogOut();
    }
}