
using GuacAPI.Authorization;
using GuacAPI.Context;
using GuacAPI.Entities;
using GuacAPI.Helpers;
using GuacAPI.Models;
using GuacAPI.Models.Users;
using GuacAPI.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace guacapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtUtils _jwtUtils;
          private readonly AppSettings _appSettings;
        private readonly DataContext _context;
        public AuthController(IUserService userService, IJwtUtils jwtUtils, IOptions<AppSettings> appSettings, DataContext context)
        {
            _userService = userService;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
            _context = context;
          
        }



        [HttpGet]
        [Route("GetUserByUsername/{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (refreshToken != null)
            {
                var infos = await _userService.GetUserByUsername(username);
                if (infos == null)
                {
                    return BadRequest();
                }

                return Ok(infos);
            }

            return Unauthorized(new { message = "Unauthorized" });
        }

        [HttpGet]
        [Route("GetUserByEmail/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (refreshToken != null)
            {
                var infos = await _userService.GetUserByEmail(email);
                if (infos == null)
                {
                    return BadRequest();
                }

                return Ok(infos);
            }

            return Unauthorized(new { message = "Unauthorized" });
        }


        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete()
        {
            var username = User.Identity?.Name;
            if (username != null)
            {
                var user = await _userService.GetUserByUsername(username);
                if (user == null)
                {
                    return BadRequest();
                }

                await _userService.DeleteUser(user.UserId);
            }

            return Ok();
        }


        [HttpPost("Register")]
        public IActionResult Register(RegisterRequest model)
        {
            _userService.Register(model);
            return StatusCode(201, "User created successfully");
        }


        [HttpPost("Login")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            // Supprimer tous les cookies de token
            Response.Cookies.Delete("refreshToken");
            Response.Cookies.Delete("FirstToken");

            var response = _userService.Login(model);
        
           if (response.RefreshToken != null) {
        SetTokenCookie(response.JwtToken, response.Id, response.RefreshToken, response.TokenExpires, response.RefreshExpires);
           }
           
          
           
            return StatusCode(200, response);
        
        }


        [HttpPost("refresh-token")]
  public IActionResult RefreshToken(int userId)
{
    var refreshToken = Request.Cookies["refreshToken"] ?? Request.Cookies["FirstToken"];

    if (refreshToken != null)
    {
        try
        {
        
            var principal = _jwtUtils.GetPrincipalFromToken(refreshToken, _appSettings.Secret);
            var userIdClaim = principal.Claims.FirstOrDefault(c => c.Type ==  "id" );
            if (userIdClaim != null)
            {
                var userIdFromToken = int.Parse(userIdClaim.Value);
                if (userIdFromToken == userId)
                {
                    var user = _userService.GetById(userId);
                    if (user != null)
                    {
                        var refreshTokenFromDb = user.RefreshTokens.FirstOrDefault(rt => rt.Token == refreshToken);
                        if (refreshTokenFromDb != null)
                        {
                            if (!refreshTokenFromDb.IsActive)
                            {
                                return BadRequest(new { message = "Refresh token is no longer active" });
                            }

                            var newAccessToken = _jwtUtils.GenerateAccessToken(user);
                            var newRefreshToken = _jwtUtils.GenerateRefreshToken(user);
                            refreshTokenFromDb.TokenExpires = DateTime.UtcNow.AddDays(7);
                            refreshTokenFromDb.newTokenExpires = DateTime.UtcNow.AddMinutes(15);
                            refreshTokenFromDb.newToken = newRefreshToken.newToken;
                            _context.Update(refreshTokenFromDb);
                            _context.SaveChanges();

                            var cookieOptions = new CookieOptions
                            {
                                HttpOnly = true,
                                Expires = DateTime.UtcNow.AddDays(7)
                            };
                            Response.Cookies.Append("refreshToken", newRefreshToken.newToken, cookieOptions);

                            return StatusCode(201, new
                            {
                                access_token = newAccessToken,
                                refresh_token = newRefreshToken
                            });
                        }
                    }
                }
            }
        }
        catch (SecurityTokenException)
        {
            return BadRequest(new { message = "Invalid refresh token" });
        }
    }

    return BadRequest(new { message = "No refresh token found" });
}







        [HttpGet("GetAllUsers")]
        public IActionResult GetAll()
        {
            var refreshToken = Request.Cookies["refreshToken"] ?? Request.Cookies["FirstToken"];
            if (refreshToken == null)
                return Unauthorized(new { message = "Unauthorized" });
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("GetUserById/{id}")]
        public IActionResult GetById(int id)
        {
            var byId = _userService.GetById(id);
            return Ok(byId);
        }

        [HttpGet("GetRefreshById/{id}/refresh-tokens")]
        public IActionResult GetRefreshTokens(int id)
        {
            var user = _userService.GetById(id);
            return Ok(user.RefreshTokens);
        }

        [HttpPut("UpdateUser/{id}")]
        public IActionResult Update(int id, UpdateRequest model)
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (refreshToken != null)
                return Unauthorized(new { message = "Unauthorized" });

            _userService.Update(id, model);
            return Ok();
        }

        // helper methods

      private void SetTokenCookie(string token, int id, string refreshToken, DateTime tokenExpires, DateTime newTokenExpires)
{
    // append cookie with refresh token to the http response
    var user = _userService.GetById(id);
        
    var cookieOptionsRefresh = new CookieOptions
    {
        HttpOnly = true,
        Expires = newTokenExpires
    };
    Response.Cookies.Append("refreshToken", refreshToken, cookieOptionsRefresh);

    var cookieOptions = new CookieOptions
    {
        HttpOnly = true,
        Expires = tokenExpires
    };
    Response.Cookies.Append("FirstToken", token, cookieOptions);
}




        private IActionResult CheckTokenToBDD(string token, int id)
        {
            var user = _userService.GetById(id);
            var tokenId = user.RefreshTokens.Find(x => x.Token == token);
            if (tokenId.TokenExpires < DateTime.UtcNow && tokenId.Token != null)
            {
                user.RefreshTokens.Remove(user.RefreshTokens.Find(x => x.Token == token));

                return Unauthorized(new { message = "Unauthorized" });
            }

            if (tokenId == null)
            {
                if (user.RefreshTokens.Find(x => x.newToken == token).newToken != null)
                {
                    if (user.RefreshTokens.Find(x => x.newToken == token).newTokenExpires < DateTime.UtcNow)
                    {
                        user.RefreshTokens.Remove(user.RefreshTokens.Find(x => x.newToken == token));

                        return Unauthorized(new { message = "Unauthorized" });
                    }
                    else
                    {
                        return StatusCode(201, user.RefreshTokens.Find(x => x.newToken == token));
                    }
                }
                else
                {
                    return Unauthorized(new { message = "Unauthorized" });
                }
            };
            return StatusCode(201, user.RefreshTokens.Find(x => x.Token == token));
        }

        private string checkTokenHeader()
        {
            var token = Request.Cookies["refreshToken"] ?? Request.Cookies["FirstToken"];
            if (token != null)
            {
                return token;
            }
            throw new Exception("Refresh token is missing");
        }

        private string IpAddress()
        {
            // get source ip address for the current request
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"]!;
            else
                return HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString()!;
        }

        private RefreshToken GetRefreshToken(string ipAddress)
        {
            var users = _userService.GetAll();

            foreach (var user in users)
            {
                //     var refreshToken = user.RefreshTokens.FirstOrDefault(x => x.IpAddress == ipAddress);

                //     if (refreshToken != null)
                //     {
                //         return refreshToken;
                //     }
            }

            return null;
        }
    }
}