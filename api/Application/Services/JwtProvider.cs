using Common;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application;

public class JwtProvider : IJwtProvider
{
    private readonly AppOptions _options;

    public JwtProvider(IOptions<AppOptions> options)
    {
        _options = options.Value;
    }

    public JwtResponse GetJwt(JwtRequest request)
    {
        var signingCredentials = GetSigningCredentials();

        var claims = GetClaims(request);

        var expiry = DateTime.UtcNow.AddMinutes(_options.JwtExpiry);

        var jwtSecurityToken = new JwtSecurityToken
        (
            issuer: _options.JwtIssuer,
            audience: _options.JwtAudience,
            claims,
            expires: expiry,
            signingCredentials: signingCredentials
        );

        return new JwtResponse
        {
            UserId = request.UserId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            EmailAddress = request.EmailAddress,
            Secret = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Expiry = expiry
        };
    }

    private static IEnumerable<Claim> GetClaims(JwtRequest request)
    {
        return new Claim[]
        {
            new (ClaimNames.UserId, request.UserId.ToString()),
            new (ClaimNames.FirstName, request.FirstName),
            new (ClaimNames.LastName, request.LastName),
            new (ClaimNames.EmailAddress, request.EmailAddress),
        };
    }

    private SigningCredentials GetSigningCredentials()
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.JwtKey));

        return new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    }
}