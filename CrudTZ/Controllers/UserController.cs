using CrudTZ.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CrudTZ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult<List<User>>> AddUser(AddUserDto user)
        {
            return Ok(await _userService.AddUser(user));
        }
        [HttpGet("List")]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsers());
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<User>> GetSingleUser(string email)
        {
            if (Int32.TryParse(email, out int id))
            {
                var resultId = await _userService.GetSingleUserById(id);
                if (resultId is null)
                    return NotFound("user not found");
                return Ok(resultId);
            }
            var result = await _userService.GetSingleUser(email);
            if (result is null)
                return NotFound("user not found");
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<User>>> UpdateUser(int id, AddUserDto request)
        {
            var result = await _userService.UpdateUser(id, request);
            if (result is null)
                return NotFound("user not found");
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<User>>> DeleteUser(int id)
        {
            var result = await _userService.DeleteUser(id);
            if (result is null)
                return NotFound("user not found");
            return Ok(result);
        }
        [HttpGet("token/{username}")]
        public string GetToken(string username)
        {
            var token = CreateToken(username);
            return token;
        }

        private string CreateToken(string username)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha384Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

    }
}
