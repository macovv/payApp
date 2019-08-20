using System.Net;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using payApp.API.Data;
using payApp.API.Dtos;
using payApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;

namespace payApp.API.Controllers
{
    // [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _ctx;
        private readonly IMapper _mapper;
        private readonly IUserRepository _repo;

        public UserController(AppDbContext ctx, IMapper mapper, IUserRepository repo)
        {
            _ctx = ctx;
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> getUsers()
        {
            var users = await _repo.GetUsers();
            if(users != null)
                return Ok(users);
            return BadRequest("Problem with getting all users");
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> getUser(string name) 
        {
            var user = await _repo.GetUser(name);
            if(user != null)
                return Ok(user);
            else
                return BadRequest();
        }

        [Authorize]
        [HttpPut("{name}")]
        public async Task<IActionResult> updateUser(UserForUpdatedDto userForUpdatedDto, string name)
        {
            var user = await _repo.GetUser(name.ToLower());
            if (user.UserName.ToString() != User.FindFirst(ClaimTypes.NameIdentifier).Value)
            {
                return Unauthorized();
            }
            _mapper.Map(userForUpdatedDto, user);
            /*
                W ANGULARZE SKONFIGURUJ TO TAK ZEBY AUTOMATYCZNIE WYSYAŁAŁO DOMYŚLNE CZYLI JUZ NADANE
             */
            if(await _repo.SaveAll())
                return Ok(user);
            return BadRequest("Problem with updating user");
        }

    }
}
