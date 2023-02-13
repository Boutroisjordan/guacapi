using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using GuacAPI.Entities;
using GuacAPI.Models;
using GuacAPI.Models.Users;
using GuacAPI.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace guacapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
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

                await _userService.DeleteUser(user.Id);
            }

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register(RegisterRequest model)
        {
            _userService.Register(model);
            return StatusCode(201, "User created successfully");
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Login(model, IpAddress());
            if (response.RefreshToken != null) SetTokenCookie(response.RefreshToken, response.Id);
            return StatusCode(200, response);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
    public IActionResult RefreshToken(int id)
{
    var refreshToken = checkTokenHeader();

            if (refreshToken == null)
            {
                var user = _userService.GetById(id);
                if (user == null)
                {
                    return BadRequest();
                }
                // checkToken associated with the userId on refreshTokens
                var tokenUser = user.RefreshTokens.Find(x => x.UserId == id);

                if (tokenUser == null)
                {
                    return BadRequest();
                }
                if(tokenUser.TokenExpires < DateTime.UtcNow){
                    var newToken = _userService.RefreshToken(tokenUser.Token, id);
                    SetTokenCookie(newToken.RefreshToken, id);
                    return StatusCode(201,newToken);
                } else if(tokenUser.TokenExpires > DateTime.UtcNow && tokenUser.Token != null){
                    SetTokenCookie(tokenUser.Token, id);
                    return StatusCode(201,tokenUser);
                }
                
                 else if (tokenUser.newTokenExpires < DateTime.UtcNow && tokenUser.newToken != null)
                {
                    return BadRequest(new { message = "Refresh token expired" });
                }
            }
              var userByToken = _userService.GetUserByRefreshToken(refreshToken);
                if (userByToken == null)
                {
                    return BadRequest();
                } 
            if(userByToken.RefreshTokens.Find(x => x.Token == refreshToken).TokenExpires < DateTime.UtcNow){
                 var newToken = _userService.RefreshToken(userByToken.RefreshTokens.Find(x => x.Token == refreshToken).Token, userByToken.Id);
                SetTokenCookie(newToken.RefreshToken, userByToken.Id);
            } else if (userByToken.RefreshTokens.Find(x => x.Token == refreshToken).newTokenExpires < DateTime.UtcNow && userByToken.RefreshTokens.Find(x => x.Token == refreshToken).newToken != null)
            {
                return BadRequest(new { message = "Refresh token expired" });
            }
            SetTokenCookie(userByToken.RefreshTokens.Find(x => x.Token == refreshToken).Token, userByToken.Id);
            return StatusCode(201,userByToken.RefreshTokens);
}

        [HttpGet("GetAllUsers")]
        public IActionResult GetAll()
        {
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

        private void SetTokenCookie(string token,int id)
        {
            // append cookie with refresh token to the http response
            var user = _userService.GetById(id);
            var tokenId = user.RefreshTokens.Find(x => x.Token == token);
            if(tokenId == null) {
                user.RefreshTokens.Find(x => x.newToken == token);
              var cookieOptionsRefresh = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = user.RefreshTokens.Find(x => x.newToken == token).newTokenExpires
                };
                Response.Cookies.Append("refreshToken", token, cookieOptionsRefresh);
            };
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = user.RefreshTokens.Find(x => x.Token == token).TokenExpires
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

private string checkTokenHeader(){
 var token = Request.Cookies["refreshToken"] ?? Request.Cookies["FirstToken"];
    if (token != null)
    {
        return token;
    }
    return null;
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