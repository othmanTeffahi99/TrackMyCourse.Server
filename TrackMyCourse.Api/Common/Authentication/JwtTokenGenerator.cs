using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace TrackMyCourseApi.Common.Authentication;

public class JwtTokenGenerator(IOptions<JwtSettings> jwtOpts) : IJwtTokenGenerator
{
    private JwtSettings JwtSettings => jwtOpts.Value;

    public string GenerateToken(Guid userId, string firstName, string lastName)
    {
        var signingCreadintials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.SecretKey)),
            SecurityAlgorithms.HmacSha256Signature
        );

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, firstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, lastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var securityToken = new JwtSecurityToken(issuer: JwtSettings.Issuer, expires: DateTime.Now.AddDays(
            jwtOpts.Value.ExpiryMinutes),audience: JwtSettings.Audience,
            claims: claims, signingCredentials: signingCreadintials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}