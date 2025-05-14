using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskFlow.Configurations;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Auth
{
    public class TokenService : ITokenService
    {
        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new(ClaimTypes.Email,user.Email),
            };

            foreach (var permission in user.UserPermissions)
            {
                claims.Add(new Claim("permission", permission.Permission.Name));
            }

            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ApiConfigurations.JwtKey));

            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(8);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer:ApiConfigurations.JwtIssuer,
                audience:ApiConfigurations.JwtAudience,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
