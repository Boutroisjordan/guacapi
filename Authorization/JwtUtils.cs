using System.Security.Cryptography;
using GuacAPI.Context;
using GuacAPI.Entities;
using GuacAPI.Helpers;
using GuacAPI.Models;

namespace GuacAPI.Authorization;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public interface IJwtUtils
{
    public string GenerateToken(User user);
    public int? ValidateToken(string token);

    public RefreshToken GenerateRefreshToken(string ipAddress);
    public RefreshToken GenerateToken(string ipAddress);
}

public class JwtUtils : IJwtUtils
{
    private readonly DataContext _context;
    private readonly AppSettings _appSettings;

    public JwtUtils(IOptions<AppSettings> appSettings, DataContext context)
    {
        _context = context;
        _appSettings = appSettings.Value;
    }

    public string GenerateToken(User user)
    {
        // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        if(_appSettings.Secret is null) {
            throw new Exception("secret is null");
        }
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenBdd = new RefreshToken
        {
            Token = tokenHandler.WriteToken(token),
            TokenExpires = DateTime.UtcNow.AddDays(7),
            Created = DateTime.UtcNow,
        };
        return tokenHandler.WriteToken(token);
    }

    public int? ValidateToken(string token)
    {

        var tokenHandler = new JwtSecurityTokenHandler();
        if(_appSettings.Secret is null) {
            throw new Exception("secret is null");
        }
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

            // return user id from JWT token if validation successful
            return userId;
        }
        catch
        {
            // return null if validation fails
            return null;
        }
    }

    public RefreshToken GenerateRefreshToken(string ipAddress)
    {
        var refreshToken = new RefreshToken
        {
            newToken = GetUniqueToken(),
            // token is valid for 7 days
            newTokenExpires = DateTime.UtcNow.AddDays(7),
        };

        return refreshToken;

        string GetUniqueToken()
        {
            // token is a cryptographically strong random sequence of values
            // ensure token is unique by checking against db
            var tokenIsUnique = !_context.Users.Any(u =>
                u.RefreshTokens.Any(t => t.Token == Convert.ToBase64String(RandomNumberGenerator.GetBytes(64))));

            if (!tokenIsUnique)
                return GetUniqueToken();

            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }
    }

       public RefreshToken GenerateToken(string ipAddress)
    {
        var refreshToken = new RefreshToken
        {
            Token = GetUniqueToken(),
            // token is valid for 7 days
            TokenExpires = DateTime.UtcNow.AddMinutes(1),
            Created = DateTime.UtcNow,
        };

        return refreshToken;

        string GetUniqueToken()
        {
            // token is a cryptographically strong random sequence of values
            // ensure token is unique by checking against db
            var tokenIsUnique = !_context.Users.Any(u =>
                u.RefreshTokens.Any(t => t.Token == Convert.ToBase64String(RandomNumberGenerator.GetBytes(64))));

            if (!tokenIsUnique)
                return GetUniqueToken();

            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }
    }
}