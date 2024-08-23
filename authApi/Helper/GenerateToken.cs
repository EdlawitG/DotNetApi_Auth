using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
namespace authApi.Helper
{
    class GenerateToken
    {
        public static void GenerateAccessToken(HttpContext httpContext, string Id)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, Id)
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(1),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes("averylongsecretkeythatisrequiredtobeused")),
                    SecurityAlgorithms.HmacSha256)
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            
            string tokenString = tokenHandler.WriteToken(token);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddDays(7),
                SameSite = SameSiteMode.None
            };

            httpContext.Response.Cookies.Append("AuthToken", tokenString, cookieOptions);
        }
    }

}