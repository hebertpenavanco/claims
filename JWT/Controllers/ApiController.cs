using JWT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private IConfiguration _configuration;

        public ApiController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: api/<ApiController>
       

        // GET api/<ApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserLogin user)
        {
            var _user = Authenticate(user);
            if (_user != null)
            {
                var token = Generate(_user);
                return Ok(token);
            }
            else return NotFound("User Not found");
        }

        private string Generate(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256
                );

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Email, user.EmailAddress),
                new Claim(ClaimTypes.GivenName, user.GivenName),
                new Claim(ClaimTypes.Surname, user.Surtname),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token); 
        }

        private UserModel Authenticate(UserLogin user)
        {
            var userModel = UserConstants.Users.Find(o=> o.UserName == user.UserName);

            return userModel;
        }

    }
}
