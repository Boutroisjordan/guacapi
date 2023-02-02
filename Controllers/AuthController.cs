using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using GuacAPI.Models;
using GuacAPI.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
 
namespace guacapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        private readonly IConfiguration _configuration;
        public static UserReturnDto userReturnDto = new UserReturnDto();
        private readonly IUserService _userService;

        public AuthController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [HttpGet, Authorize]
        public ActionResult<object> GetUser()
        {
            // on peut aussi faire un FindFirstValue(ClaimTypes.NameIdentifier)

            var infos = _userService.GetUserInfos();
            return Ok(infos);
        }

        [HttpGet, Authorize]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var infos = await _userService.GetAllUsers();
            if (infos == null)
            {
                return BadRequest();
            }

            return Ok(infos);
        }

        [HttpGet, Authorize]
        [Route("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var infos = await _userService.GetUserById(id);
            if (infos == null)
            {
                return BadRequest();
            }

            return Ok(infos);
        }

        [HttpGet, Authorize]
        [Route("GetUserByUsername/{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var infos = await _userService.GetUserByUsername(username);
            if (infos == null)
            {
                return BadRequest();
            }

            return Ok(infos);
        }

        [HttpGet, Authorize]
        [Route("GetUserByEmail/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var infos = await _userService.GetUserByEmail(email);
            if (infos == null)
            {
                return BadRequest();
            }
            return Ok(infos);
        }
        [HttpGet]
        [Route("Getapikkey/")]
        public IActionResult GetApiKey(string key)
        {
            var infos = _userService.CreateApiToken(key);
            if (infos == null)
            {
                return BadRequest();
            }

            return Ok(infos);
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDtoRegister request)
        {
            if (request.Password == null)
            {
                return BadRequest("Password is required");
            }

            if (request.Username == null)
            {
                return BadRequest("Username is required");
            }

            var checkUsername = await _userService.CheckUsernameAvailability(request.Username);

            if (checkUsername == false)
            {
                return BadRequest("Username is already use, take another username");
            }

            user.Email = request.Email;
            user.Username = request.Username;
            user.FirstName = request.FirstName;
            user.Phone = request.Phone;
            user.Role = "Admin";
            user.LastName = request.LastName;
            user.PasswordHash = request.Password;
            userReturnDto.FirstName = request.FirstName;
            userReturnDto.LastName = request.LastName;
            userReturnDto.Username = request.Username;
            userReturnDto.Role = "Admin";

            user = await _userService.Register(user) ?? throw new InvalidOperationException();
            return Ok(userReturnDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserDtoLogin request)

        {
            if (request.Password == null)
            {
                return BadRequest("Password is required");
            }

            if (request.Username == null)
            {
                return BadRequest("Username is required");
            }

            var result = await _userService.Login(request);

            if (result == null)
            {
                return BadRequest("Bad credentials");
            }

            var upToken = await _userService.updateToken(result);
            if (upToken == null)
            {
                return BadRequest("Bad credentials");
            }

            user.RefreshToken = null;
            userReturnDto.Token = upToken.Token;
            return Ok(userReturnDto.Token);
        }

        // [HttpPost("refreshToken")]
        // public ActionResult<string> RefreshToken()
        // {
        //     var refreshToken = Request.Cookies["refreshToken"];
        //     if (user.RefreshToken != null && !user.RefreshToken.Equals(refreshToken))
        //     {
        //         return Unauthorized("Invalid refresh token");
        //     }
        //     else if (user.TokenExpires < DateTime.Now)
        //     {
        //         return Unauthorized("Token expired");
        //     }

        //     string token = CreateToken(user);
        //     var newRefreshToken = GenerateRefreshToken();
        //     SetRefreshToken(newRefreshToken);
        //     user.TokenExpires = DateTime.Now.AddDays(7);
        //     user.RefreshToken = newRefreshToken.Token;
        //     user = await _userService.Register(user) ?? throw new InvalidOperationException();
        //     return Ok(token);
        // }


        [HttpPost("logout")]
        public ActionResult Logout()
        {
            user.RefreshToken = null;
            user.TokenExpires = DateTime.Now;
            return Ok();
        }


        [HttpDelete("delete/{id}")]

        public async Task<ActionResult> Delete(int id)
        {
            user = await _userService.DeleteUser(id) ?? throw new InvalidOperationException();
            return Ok(user);
        }

        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken()
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow
            };
            return refreshToken;
        }

        private void SetRefreshToken(RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                //enlever en https
                HttpOnly = true,
                Expires = newRefreshToken.Expires,
            };

            if (newRefreshToken.Token != null)
            {
                Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
                user.RefreshToken = newRefreshToken.Token;
                user.TokenExpires = newRefreshToken.Expires;
            }
        }

        // Claims properties are used to store user information anything you want to store in the token
    }
}