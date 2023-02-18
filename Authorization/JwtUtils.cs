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
    public RefreshToken GenerateAccessToken(User user, TimeSpan refreshTokenExpires);
    public int? ValidateToken(string token);
    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    public RefreshToken GenerateRefreshToken(User user);
}

public class JwtUtils : IJwtUtils
{
    private readonly DataContext _context;
    private readonly AppSettings _appSettings;
    private readonly Dictionary<string, object> _expiredTokens;

    public JwtUtils(IOptions<AppSettings> appSettings, DataContext context)
    {
        _context = context;
        _appSettings = appSettings.Value;
        _expiredTokens = new Dictionary<string, object>();
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
    public RefreshToken GenerateAccessToken(User user, TimeSpan refreshTokenExpires)
{
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

    // Create claims for both access token and refresh token
    var claims = new List<Claim>
    {
        new Claim("id", user.UserId.ToString())
    };

    // Generate access token
    var accessTokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.UtcNow.AddMinutes(15),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };
    var accessToken = tokenHandler.CreateToken(accessTokenDescriptor);

    var accessTokenBdd = new RefreshToken
    {
        Token = tokenHandler.WriteToken(accessToken),
        TokenExpires = accessTokenDescriptor.Expires.Value,
        Created = DateTime.UtcNow,
        UserId = user.UserId
    };
    _expiredTokens.TryAdd(tokenHandler.WriteToken(accessToken), tokenHandler.ValidateToken(tokenHandler.WriteToken(accessToken), new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
        ClockSkew = TimeSpan.Zero
    }, out SecurityToken validatedToken));

    // Generate refresh token
    var refreshTokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.UtcNow.Add(refreshTokenExpires),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };
    var refreshToken = tokenHandler.CreateToken(refreshTokenDescriptor);

    _expiredTokens.TryAdd(tokenHandler.WriteToken(accessToken), accessTokenBdd);

        accessTokenBdd.Token = tokenHandler.WriteToken(refreshToken);
        accessTokenBdd.TokenExpires = refreshTokenDescriptor.Expires ?? throw new Exception("Could not create refresh token");
        accessTokenBdd.Created = DateTime.UtcNow;
        accessTokenBdd.UserId = user.UserId;


        return accessTokenBdd;
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

    // Generate refresh token
    var refreshTokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.UtcNow.AddDays(7),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };
    var refreshToken = tokenHandler.CreateToken(refreshTokenDescriptor);

    _expiredTokens.Add(tokenHandler.WriteToken(refreshToken), tokenHandler.ValidateToken(tokenHandler.WriteToken(refreshToken), new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
        ClockSkew = TimeSpan.Zero
    }, out SecurityToken validatedToken));
    return new RefreshToken
    {
        Token = tokenHandler.WriteToken(refreshToken),
        TokenExpires = refreshTokenDescriptor.Expires ?? throw new Exception("Could not create refresh token"),
        UserId = user.UserId
    };
}
   

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        foreach (var item in _expiredTokens)
        {
            Console.WriteLine("Key: {0}, Value: {1}", item.Key, item.Value);
        }
        if (_expiredTokens.ContainsKey(token))
        {

            return (ClaimsPrincipal)_expiredTokens[token];
        }

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            RequireExpirationTime = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.Secret)),
            ValidateLifetime = false // Here we are saying that we don't care about the token's expiration date
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        _expiredTokens.Add(token, principal);

        return principal;
    }
}
