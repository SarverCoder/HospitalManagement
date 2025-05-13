using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HospitalManagement.JwtConfiguration.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HospitalManagement.JwtConfiguration.Services
{
    public class AuthService(IOptions<JwtSettings> settings) : IAuthService
    {
        private readonly IOptions<JwtSettings> _options = settings;
        private readonly JwtSecurityTokenHandler _handler = new();

        public string GetToken(string username)
        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                "cafe.uz",
                "cafe.uz",
                claims: [
                    new Claim(JwtRegisteredClaimNames.Email, ""),
                    new Claim("user_id", "1")
                ],
                expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)),
                signingCredentials: credentials
            );

            return _handler.WriteToken(token);

        }
    }
}
