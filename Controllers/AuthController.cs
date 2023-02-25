
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
using GuacAPI.Services.EmailServices;

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
        private readonly IEmailService _emailService;



        public AuthController(IUserService userService, IJwtUtils jwtUtils, IOptions<AppSettings> appSettings, DataContext context, IEmailService emailService)
        {
            _userService = userService;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
            _context = context;
            _emailService = emailService;

        }
    

        [Authorize]
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
        [Authorize]
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

        [HttpGet("ConfirmEmail")]
        public IActionResult VerifyUserMail(string token,string email) 
        {
            if(token == null || email == null)
            {
                return BadRequest("Invalid client request");
            }
             _userService.VerifyEmail(token, email);
        
            return Ok(new { message = "Email verified successfully" });

        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await _userService.DeleteUser(id);
            if (user == null)
            {
                return BadRequest();
            }

            return Ok(new { message = "User deleted successfully" });
        }


        [HttpPost("Register")]
        public IActionResult Register(RegisterRequest model)
        {
            var response = _userService.Register(model);
            if(response != null) {
                var token = response.VerifyToken;
                var confirmationLink = Url.Action(nameof(VerifyUserMail), "Auth", new { token, email = model.Email }, Request.Scheme);
                var recipientName = model.FirstName + " " + model.LastName;
                var contactEmail = "guacaprocesi@gmail.com";
                var message = new MessageMail(new string[] { model.Email }, "Confirmation de votre compte", model.Username, contactEmail,recipientName, confirmationLink);
                _emailService.SendEmail(message);
                return Ok(new { message = "User registered successfully", response });
            } else
            {
                return BadRequest(new { message = "User already exists" });
            }
        }


        [HttpPost("Login")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            // Supprimer tous les cookies de token
            var response = _userService.Login(model);

            if (response.RefreshToken != null)
            {
                Console.WriteLine("RefreshToken: " + response.AccessToken);
                SetTokenCookie(response.AccessToken, response.Id, response.RefreshToken, response.TokenExpires, response.RefreshExpires);
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
                        if (user.RefreshToken != null && user.RefreshToken.AccessToken != null && user.RefreshToken.AccessTokenExpires > DateTime.UtcNow)
                        {
                            var timeLeft = (int)(user.RefreshToken.AccessTokenExpires - DateTime.UtcNow).TotalSeconds;
                            var expirationTime = DateTimeOffset.UtcNow.AddSeconds(timeLeft);
                            var CookieOptions = new CookieOptions
                            {
                                HttpOnly = true,
                                Expires = expirationTime
                            };
                            return Ok(new { token = user.RefreshToken.AccessToken, expiration = expirationTime.ToUnixTimeSeconds() });
                        }

                        var newRefreshToken = _jwtUtils.GenerateRefreshToken(user);
                        user.RefreshToken.AccessTokenExpires = DateTime.UtcNow.AddHours(1);
                        user.RefreshToken.AccessToken = newRefreshToken.AccessToken;
                        _context.Update(user);
                        _context.SaveChanges();

                        var cookieOptions = new CookieOptions
                        {
                            HttpOnly = true,
                            Expires = DateTime.UtcNow.AddHours(1)
                        };
                        Response.Cookies.Append("AccessToken", newRefreshToken.AccessToken, cookieOptions);

                        return Ok(newRefreshToken.AccessToken);
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

            var users = _userService.GetAll();
            return Ok(users);
        }
        [Authorize]
        [HttpGet("GetUserById/{id}")]
        public IActionResult GetById(int id)
        {
            var byId = _userService.GetById(id);
            return Ok(byId);
        }
        [Authorize]
        [HttpGet("GetRefreshById/{id}/refresh-tokens")]
        public IActionResult GetRefreshTokens(int id)
        {
            var user = _userService.GetById(id);
            return Ok(user.RefreshToken);
        }

        [Authorize]
        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> Update(int id, UpdateRequest model)
        {
            var updatedUser = await _userService.UpdateUser(id, model);
            if (updatedUser == null)
                return BadRequest(new { message = "User not found" });
            return Ok(updatedUser);
        }

        [Authorize]
        [HttpGet("GetUserByToken")]
        public IActionResult GetUserByToken()
        {
            var refreshToken = Request.Cookies["AccessToken"] ?? Request.Cookies["refreshToken"];
            if (refreshToken == null)
                return Unauthorized(new { message = "Unauthorized" });
            var user = CheckToken(refreshToken);
            if (user == null)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }
            return Ok(new { user = user.RefreshToken });

        }

        // helper methods

        private void SetTokenCookie(string token, int id, string refreshToken, DateTime tokenExpires, DateTime newTokenExpires)
        {
            // append cookie with refresh token to the http response

            var cookieOptionsRefresh = new CookieOptions
            {
                HttpOnly = false,
                Secure = true,
                Expires = newTokenExpires
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptionsRefresh);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = false,
                Secure = true,
                Expires = tokenExpires
            };
            Response.Cookies.Append("AccessToken", token, cookieOptions);

        }

        private User CheckToken(string token)
        {

            if (token != null)
            {
                Console.WriteLine("token : " + token);
                var user = _userService.GetUserByRefreshToken(token);
                if (user.RefreshToken.AccessToken != null && user.RefreshToken.AccessTokenExpires > DateTime.UtcNow)
                {
                    var timeLeft = (int)(user.RefreshToken.AccessTokenExpires - DateTime.UtcNow).TotalSeconds;
                    var expirationTime = DateTimeOffset.UtcNow.AddSeconds(timeLeft);
                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Expires = expirationTime
                    };
                    Response.Cookies.Append("AccessToken", user.RefreshToken.AccessToken, cookieOptions);
                    return user;
                }
                else if (user.RefreshToken.AccessToken != null && user.RefreshToken.AccessTokenExpires < DateTime.UtcNow && user.RefreshToken.NewTokenExpires > DateTime.UtcNow && user.RefreshToken.NewToken != null)
                {
                    // generate new AccessToken for user 
                    var newAccessToken = _jwtUtils.GenerateAccessToken(user);
                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Expires = DateTime.UtcNow.AddHours(1)
                    };
                    // update user with new AccessToken
                    user.RefreshToken.AccessTokenExpires = DateTime.UtcNow.AddHours(1);
                    user.RefreshToken.AccessToken = newAccessToken.AccessToken;
                    _context.Update(user);
                    _context.SaveChanges();
                    // append cookie with new AccessToken to the http response

                    Response.Cookies.Append("AccessToken", newAccessToken.AccessToken, cookieOptions);
                    return user;
                }
                else
                {
                    throw new UnauthorizedAccessException("Access token not found or expired.");
                }

            }
            throw new UnauthorizedAccessException("Invalid access token.");

        }





      
    }


}