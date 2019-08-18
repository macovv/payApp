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

        public UserController(AppDbContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }


        [HttpGet("{name}")]
        public IActionResult getUser(string name) 
        {
            var user = _ctx.Users.Where(u => u.UserName == name).Include(i => i.UserWishes).FirstOrDefault();
            if(user != null)
                return Ok(user);
            else
                return BadRequest();
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> updateUser(UserForUpdatedDto userForUpdatedDto)
        {
            var user = _ctx.Users.Include(w => w.UserWishes).Where(u => u.UserName == userForUpdatedDto.UserName).FirstOrDefault();
            if (user.UserName.ToString() != User.FindFirst(ClaimTypes.NameIdentifier).Value)
            {
                return Unauthorized();
            }
            _mapper.Map(userForUpdatedDto, user);
            /*
                W ANGULARZE SKONFIGURUJ TO TAK ZEBY AUTOMATYCZNIE WYSYAŁAŁO DOMYŚLNE CZYLI JUZ NADANE
             */
            await _ctx.SaveChangesAsync();
            return Ok(user);
        }

        [Authorize]
        [HttpPost("{name}/addWish")]
        public async Task<IActionResult> addWish(WishForAddDto wish, string name)
        {
            var user = _ctx.Users.Include(w => w.UserWishes).Where(u => u.UserName == name).FirstOrDefault();
            // Console.WriteLine
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
            _ctx.Wishes.Add(wishToAdd);
            await _ctx.SaveChangesAsync();
            return Ok(user);
        }

        [Authorize]
        [HttpPost("{name}/{wishId}/payForWish")]
        public IActionResult payForWish(string name, int wishId)
        {               
            Console.WriteLine("---------------------Test");
            var user = _ctx.Users.Include(w => w.UserWishes).Where(u => u.UserName == name).FirstOrDefault();
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
