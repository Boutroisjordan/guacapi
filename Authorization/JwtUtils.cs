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
    public RefreshToken GenerateAccessToken(User user);
    public int? ValidateToken(string token);
    public ClaimsPrincipal GetPrincipalFromToken(string token, string signingKey);
    public RefreshToken GenerateRefreshToken(User user);
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
    // generate token that is valid for 7 days
    // var tokenHandler = new JwtSecurityTokenHandler();
    // if(_appSettings.Secret is null) {
    //     throw new Exception("secret is null");
    // }
    // var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
    // var tokenDescriptor = new SecurityTokenDescriptor
    // {
    //     Subject = new ClaimsIdentity(new[] { new Claim("id", user.UserId.ToString()) }),
    //     Expires = DateTime.UtcNow.AddDays(7),
    //     SigningCredentials =
    //         new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    // };
    // var token = tokenHandler.CreateToken(tokenDescriptor);
    // var tokenBdd = new RefreshToken
    // {
    //     Token = tokenHandler.WriteToken(token),
    //     TokenExpires = DateTime.UtcNow.AddDays(7),
    //     Created = DateTime.UtcNow,
    // };
    // return tokenHandler.WriteToken(token);        
    public RefreshToken GenerateAccessToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

        // Crée un token d'accès valide pendant une heure
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.UserId.ToString()) }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var accessToken = tokenHandler.CreateToken(tokenDescriptor);

        var refreshToken = _context.RefreshToken.SingleOrDefault(r => r.UserId == user.UserId);

        if (refreshToken != null && refreshToken.newToken != null && refreshToken.newTokenExpires > DateTime.UtcNow)
        {
            return new RefreshToken
            {
                Token = tokenHandler.WriteToken(accessToken),
                TokenExpires = tokenDescriptor.Expires ?? throw new Exception("Could not create access token"),
                newToken = refreshToken.newToken,
                newTokenExpires = refreshToken.newTokenExpires,
                Created = DateTime.UtcNow,
            };
        }


        // Crée un token de rafraîchissement valide pendant 7 jours
        var refreshTokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.UserId.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var newRefreshToken =  tokenHandler.CreateToken(refreshTokenDescriptor);

        // Stocke le refresh token dans la base de données pour une persistance de connexion
        var refreshTokenBdd = new RefreshToken
        {
            Token = tokenHandler.WriteToken(accessToken),
            TokenExpires = tokenDescriptor.Expires ?? throw new Exception("Could not create access token"),
            newToken = tokenHandler.WriteToken(newRefreshToken),
            newTokenExpires = refreshTokenDescriptor.Expires ?? throw new Exception("Could not create refresh token"),
            Created = DateTime.UtcNow,
            UserId = user.UserId,
        };
        _context.RefreshToken.Add(refreshTokenBdd);

        _context.SaveChanges();

        // Retourne les informations de token pour une utilisation immédiate
        return new RefreshToken
        {
            Token = tokenHandler.WriteToken(accessToken),
            TokenExpires = tokenDescriptor.Expires ?? throw new Exception("Could not create access token"),
            newToken = refreshTokenBdd.Token,
            newTokenExpires = refreshTokenBdd.TokenExpires,
            Created = DateTime.UtcNow,
        };
    }


    public int? ValidateToken(string token)
    {

        var tokenHandler = new JwtSecurityTokenHandler();
        if (_appSettings.Secret is null)
        {
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

    public RefreshToken GenerateRefreshToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

        // Create claims for refresh token
        var claims = new List<Claim>
    {
        new Claim("id", user.UserId.ToString())
    };
        var refreshToken = _context.RefreshToken.SingleOrDefault(r => r.UserId == user.UserId);

        // Generate refresh access token
        var refreshAccessToken = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var newRefreshTokenAccess = tokenHandler.CreateToken(refreshAccessToken);

        var refreshTokenBdd = new RefreshToken
        {
            Token = tokenHandler.WriteToken(newRefreshTokenAccess),
            TokenExpires = refreshAccessToken.Expires ?? throw new Exception("Could not create refresh token"),
            Created = DateTime.UtcNow,
            newToken = refreshToken.newToken,
            newTokenExpires = refreshToken.newTokenExpires,
            UserId = user.UserId
        };
        _context.RefreshToken.Update(refreshTokenBdd);
        return refreshTokenBdd;
    }



    public ClaimsPrincipal GetPrincipalFromToken(string token, string signingKey)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(signingKey);
        Console.WriteLine(key);
        Console.WriteLine(token);
        try
        {
            Console.WriteLine("try");
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = true,
                ClockSkew = TimeSpan.Zero
            };

            var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
            {
                return null;
            }

            return principal;
        }
        catch
        {
            return null;
        }
    }

    private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
    {
        return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
               jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
    }

}
