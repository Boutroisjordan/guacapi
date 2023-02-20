
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
            Response.Cookies.Delete("AccessToken");

            var response = _userService.Login(model);

            if (response.RefreshToken != null)
            {
                SetTokenCookie(response.JwtToken, response.Id, response.RefreshToken, response.TokenExpires, response.RefreshExpires);
            }

            return StatusCode(200, response);

        }


        [HttpPost("RefreshToken")]
        public IActionResult RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (refreshToken != null)
            {
                try
                {

                    var user = _userService.GetUserByRefreshToken(refreshToken);

                    if (user != null)
                    {
                        if (user.RefreshToken != null && user.RefreshToken.Token != null && user.RefreshToken.TokenExpires > DateTime.UtcNow)
                        {
                            return Unauthorized(new { message = "Encore valable fdp" });
                        }

                        var newRefreshToken = _jwtUtils.GenerateRefreshToken(user);
                        user.RefreshToken.TokenExpires = DateTime.UtcNow.AddHours(1);
                        user.RefreshToken.Token = newRefreshToken.Token;
                        _context.Update(user);
                        _context.SaveChanges();

                        var cookieOptions = new CookieOptions
                        {
                            HttpOnly = true,
                            Expires = DateTime.UtcNow.AddHours(1)
                        };
                        Response.Cookies.Append("AccessToken", newRefreshToken.Token, cookieOptions);

                        return Ok(newRefreshToken.Token);
                    }

                }
                catch (SecurityTokenException)
                {
                    return Unauthorized(new { message = "Unauthorized" });
                }
            }

            return Unauthorized(new { message = "Unauthorized" });
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
            return Ok(user.RefreshToken);
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

        [HttpGet("GetUserByToken")]
        public IActionResult GetUserByToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (refreshToken != null)
            {
                var user = _userService.GetUserByRefreshToken(refreshToken);
                if (user == null)
                {
                    return BadRequest();
                }

                return Ok(user);
            }

            return Unauthorized(new { message = "Unauthorized" });
        }

        // helper methods

        private void SetTokenCookie(string token, int id, string refreshToken, DateTime tokenExpires, DateTime newTokenExpires)
        {
            // append cookie with refresh token to the http response
            var cookieToken = Request.Cookies["refreshToken"];
            if (cookieToken == null)
            {
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
                Response.Cookies.Append("AccessToken", token, cookieOptions);
            }
            var user = _userService.GetUserByRefreshToken(cookieToken);
            if (user != null && user.RefreshToken.newToken != null && user.RefreshToken.newTokenExpires > DateTime.UtcNow)
            {
                if (user.RefreshToken.TokenExpires < DateTime.UtcNow)
                {
                    var cookieOptionsRefreshAccess = new CookieOptions
                    {
                        HttpOnly = true,
                        Expires = tokenExpires
                    };
                    Response.Cookies.Append("refreshToken", refreshToken, cookieOptionsRefreshAccess);
                }
            }
        }




        // private IActionResult CheckTokenToBDD(string token, int id)
        // {
        // var user = _userService.GetById(id);
        // var tokenId = user.RefreshTokens.Find(x => x.Token == token);
        // if (tokenId.TokenExpires < DateTime.UtcNow && tokenId.Token != null)
        // {
        //     user.RefreshTokens.Remove(user.RefreshTokens.Find(x => x.Token == token));

        //     return Unauthorized(new { message = "Unauthorized" });
        // }

        // if (tokenId == null)
        // {
        //     if (user.RefreshTokens.Find(x => x.newToken == token).newToken != null)
        //     {
        //         if (user.RefreshTokens.Find(x => x.newToken == token).newTokenExpires < DateTime.UtcNow)
        //         {
        //             user.RefreshTokens.Remove(user.RefreshTokens.Find(x => x.newToken == token));

        //             return Unauthorized(new { message = "Unauthorized" });
        //         }
        //         else
        //         {
        //             return StatusCode(201, user.RefreshTokens.Find(x => x.newToken == token));
        //         }
        //     }
        //     else
        //     {
        //         return Unauthorized(new { message = "Unauthorized" });
        //     }
        // };
        // return StatusCode(201, user.RefreshTokens.FirstOr(x => x.Token == token));
    }


}