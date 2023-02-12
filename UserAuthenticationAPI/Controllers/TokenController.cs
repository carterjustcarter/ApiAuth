using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserAuthenticationAPI.Model;
using UserAuthenticationAPI.Model.DTO;

namespace UserAuthenticationAPI.Controllers {
    [Route("api/token")]
    [ApiController]
    public class TokenController : ControllerBase {
        public IConfiguration _configuration;

        public TokenController(IConfiguration config) {
            _configuration = config;
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserDTO _user) {
            if (_user != null) {
                var user = Container.GetUserByNameLastNamePassword(_user.Name, _user.LastName, _user.Password);
                if (user != null) {
                    //create claims details based on the user information
                    var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Name", user.Name.ToString()),
                    new Claim("LastName", user.LastName.ToString()),
                    new Claim("AccessLevel", user.AccessLevel.ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(30),
                    signingCredentials: signIn
                );

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                } else {
                    return BadRequest("Invalid credentials");
                }
            }
            return BadRequest("User doesnt exists");
        }
    }
}
