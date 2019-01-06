using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WhileLearningCzech.Domain.Services.Users;
using WhileLearningCzech.Domain.Services.Users.Dto;
using WhileLearningCzech.Web.Helpers;

namespace WhileLearningCzech.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginInputDto input)
        {
            //TODO: Parse to passwordHash and use as hash
            var user = await GetIdentity(input.Username, input.Password);

            if (user == null)
                return BadRequest(new { message = "Username or Password is incorrect" });

            var key = JwtConfigs.GetSecurityKey();
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(claims: user.Claims, signingCredentials: creds, expires: DateTime.UtcNow + TimeSpan.FromHours(24));
            var tokenResult = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new { token = tokenResult });
        }

        private async Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            var user = await _userService.GetUser(username, password);
            if (user == null)
                return null;

            var identity = new ClaimsIdentity();
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Username));
            return identity;
        }

        [HttpPost("validateToken")]
        [ApiAuthorize]
        public IActionResult ValidateToken()
        {
            return new JsonResult(new { username = User.Identity.Name });
        }
    }
}