// ✅ AuthService.cs
using Lending.Models;
using Lending.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Lending.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repository;
        private readonly JwtSettings _jwtSettings;

        public AuthService(IAuthRepository repository, IOptions<JwtSettings> jwtOptions)
        {
            _repository = repository;
            _jwtSettings = jwtOptions.Value;
        }

        public LoginResponseViewModel Login(LoginViewModel login)
        {
            var response = _repository.Login(login);

            if (response.IsSuccess)
            {
                response.Token = GenerateToken(response.User);
            }

            return response;
        }

        private string GenerateToken(User user)
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);
            var secretKey = new SymmetricSecurityKey(key);
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim("MyClaim", user.UserPhone ?? string.Empty)
            };

            var tokenOptions = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
    }
}
