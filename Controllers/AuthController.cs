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
        public static User.UserReturnDto userReturnDto = new User.UserReturnDto();
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

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(User.UserDtoRegister request)
        {
            if (request.Password == null)
            {
                return BadRequest("Password is required");
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Username = request.Username;
            userReturnDto.FirstName = request.FirstName;
            userReturnDto.LastName = request.LastName;
            userReturnDto.Username = request.Username;

           // await _userService.Register(user);
            return Ok(userReturnDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(User.UserDtoLogin request)
        {
            if (request.Password == null)
            {
                return BadRequest("Password is required");
            }

            if (request.Username != user.Username)
            {
                return BadRequest("Username is incorrect");
            }

            if (user.PasswordSalt != null && user.PasswordHash != null &&
                !VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Password is incorrect");
            }

            string token = CreateToken(user);

            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken);

            return Ok(token);
        }

        [HttpPost("refreshToken")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (user.RefreshToken != null && !user.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid refresh token");
            }
            else if (user.TokenExpires < DateTime.Now)
            {
                return Unauthorized("Token expired");
            }

            string token = CreateToken(user);
            var newRefreshToken = GenerateRefreshToken();
            SetRefreshToken(newRefreshToken);
            return Ok(token);
        }

        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            user.RefreshToken = null;
            user.TokenExpires = DateTime.Now;
            return Ok();
        }


        [HttpDelete("delete")]
        public async Task<ActionResult> Delete()
        {
            user = await _userService.DeleteUser(user.Id) ?? throw new InvalidOperationException();
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
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreatedAt = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;
        }

        // Claims properties are used to store user information anything you want to store in the token
        private string CreateToken(User user)
        {
            if (user.Username != null)
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, "Admin")
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    _configuration.GetSection("AppSettings:Secret").Value ?? throw new InvalidOperationException()));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds);
                var jwt = new JwtSecurityTokenHandler().WriteToken(token);
                return jwt;
            }

            return String.Empty;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }

        // suite du tuto: https://youtu.be/v7q3pEK1EA0?t=618
        // url + /swagger pour la doc api
    }
}