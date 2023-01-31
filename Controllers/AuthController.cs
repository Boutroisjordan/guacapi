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

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(User.UserDtoRegister request)
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

            if(checkUsername == false) {
                return BadRequest("Username is already use, take another username");
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Username = request.Username;
            user.Email = request.Email;
            user.FirstName = request.FirstName;
            user.Phone = request.Phone;
            user.Role = "Admin";
            user.LastName = request.LastName;
            userReturnDto.FirstName = request.FirstName;
            userReturnDto.LastName = request.LastName;
            userReturnDto.Username = request.Username;
            userReturnDto.Role = "Admin";

            user = await _userService.Register(user) ?? throw new InvalidOperationException();
            return Ok(userReturnDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(User.UserDtoLogin request)
        {
             if (request.Password == null)
             {
                 return BadRequest("Password is required");
             }

             if (request.Username == null)
             {
                 return BadRequest("Username is required");
             }

            // if (request.Username != user.Username)
            // {
            //     return BadRequest("Username is incorrect");
            // }

            // if (user.PasswordSalt != null && user.PasswordHash != null &&
            //     !VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            // {
            //     return BadRequest("Password is incorrect");
            // }





        //Todo : check all username pour pas pouvoir créer un usernam qui existe déjà pour pouvoir récupérer le user par son username.  
        // Récupérer le password salt appelle verifyPassWordHash

            

             var result = await _userService.Login(request.Username, request.Password);

              if(result is false) {
                 return BadRequest("Bad credentials");
              }


            //  string token = CreateToken(user);

            //Refresh token en attente 
            //  var refreshToken = GenerateRefreshToken();
            //  SetRefreshToken(refreshToken);
            //  user.TokenExpires = DateTime.Now.AddMinutes(5);
            //  user.TokenCreatedAt = DateTime.Now;

            //  refresh token expires in 7 days
             var upToken = await _userService.updateToken(request);
            return Ok(upToken);
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
            user.TokenExpires = DateTime.Now.AddDays(7);
            user.TokenCreatedAt = DateTime.Now;
            user.RefreshToken = newRefreshToken.Token;
            user = await _userService.Register(user) ?? throw new InvalidOperationException();
            return Ok(token);
        }


        [HttpPost("logout")]
        public ActionResult Logout()
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

            if (newRefreshToken.Token != null)
            {
                Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
                user.RefreshToken = newRefreshToken.Token;
                user.TokenCreatedAt = newRefreshToken.Created;
                user.TokenExpires = newRefreshToken.Expires;
            }


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


    }
}