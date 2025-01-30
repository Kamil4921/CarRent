using System.Security.Claims;
using System.Text;
using CarRent.Application.Abstractions.Authentication;
using CarRent.Domain.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace CarRent.Infrastructure.Authentication;

public class TokenProvider(IConfiguration configuration) : ITokenProvider
{
    public string Create(User user)
    {
        var secretKey = configuration["JWT:SecretKey"]!;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            ]),
            Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("JWT:ExpirationInMinutes")),
            SigningCredentials = credentials,
            Issuer = configuration["JWT:Issuer"],
            Audience = configuration["JWT:Audience"]
        };
        
        var handler = new JsonWebTokenHandler();
        var token = handler.CreateToken(tokenDescriptor);
        
        return token;
    }
}