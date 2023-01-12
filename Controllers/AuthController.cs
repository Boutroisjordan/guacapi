using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using GuacAPI.Models;
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

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
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
            userReturnDto.FirstName = user.Username;
            userReturnDto.LastName = user.Username;
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
            return Ok(token);
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