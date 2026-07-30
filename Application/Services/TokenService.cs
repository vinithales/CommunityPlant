using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CommunityPlant.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CommunityPlant.Application.Services;

public interface ITokenService
{
    string CreateToken(User user);
}

public sealed class TokenService(IConfiguration configuration) : ITokenService
{
    public string CreateToken(User user)
    {
        var settings = configuration.GetSection("Jwt");
        var key = settings["Key"] ?? throw new InvalidOperationException("Jwt:Key não configurada.");
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, user.TypeUser.ToString())
        };
        var credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(settings["Issuer"], settings["Audience"], claims,
            expires: DateTime.UtcNow.AddMinutes(int.TryParse(settings["ExpirationMinutes"], out var minutes) ? minutes : 60),
            signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
