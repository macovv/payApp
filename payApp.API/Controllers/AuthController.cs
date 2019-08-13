using System.Net;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using payApp.API.Data;
using payApp.API.Dtos;
using payApp.API.Models;

namespace payApp.API.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous] //?
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _ctx;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private  IConfiguration _config;

        public AuthController(AppDbContext ctx, UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration config)
        {
            _ctx = ctx;
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(UserForRegisterDto user)
        {   
            var newUser = new User() 
            {
                UserName = user.UserName.ToLower(),
                Email = user.Email
            };
            var result = await _userManager.CreateAsync(newUser, user.Password);
            if(result.Succeeded)
            {
                await _signInManager.SignInAsync(newUser, false);
                return Ok(); // correct return!
            }
            return BadRequest();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto user) // here can be something wrong
        {
            if(ModelState.IsValid) //dunnno it should be here
            {
                var result = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, user.RememberMe, false);
                if(result.Succeeded)
                {
                    var userToReturn = _ctx.Users.Include(x => x.UserWishes).Where(u => u.UserName == user.UserName).FirstOrDefault();
                    if (userToReturn != null)
                    {
                        var tokenString = GenerateJSONWebToken(userToReturn);
                        return Ok(new { token = tokenString, user = userToReturn });
                    }
                }
            }
            return Unauthorized();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}