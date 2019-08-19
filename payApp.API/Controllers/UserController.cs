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

        [Authorize]
        [HttpPost("{name}/addWish")]
        public async Task<IActionResult> addWish(WishForAddDto wish, string name)
        {
            var user = await _repo.GetUser(name);
            if (user.UserName.ToString() != User.FindFirst(ClaimTypes.NameIdentifier).Value)
            {
                return Unauthorized();
            }
            Wish wishToAdd = new Wish() {
                UrlToShop = wish.UrlToShop,
                Name = wish.Name,
                Cost = wish.Cost,
                User = user
            };
            user.UserWishes.Add(wishToAdd);
            _ctx.Wishes.Add(wishToAdd); // to do repo
            if(await _repo.SaveAll())
                return Ok(user);
            return BadRequest("Problem with adding new wish!");
        }

        [Authorize]
        [HttpPost("{name}/{wishId}/payForWish")]
        public async Task<IActionResult> payForWish(string name, int wishId)
        {               
            var user = await _repo.GetUser(name);
            var wish = user.UserWishes.Where(i => i.Id == wishId).FirstOrDefault();
            var loggedUsser = _ctx.Users.Include(w => w.UserWishes).Where(u => u.UserName == User.FindFirst(ClaimTypes.NameIdentifier).Value).FirstOrDefault();
            loggedUsser.Saldo -= wish.Cost;
            if(loggedUsser.Saldo > 0)
                return Ok("Ok" + wish);
            else
                return BadRequest("You can not afford this " + wish);
        }

    }
}
