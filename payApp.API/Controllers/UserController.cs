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

namespace payApp.API.Controllers
{
    // [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _ctx;

        public UserController(AppDbContext ctx)
        {
            _ctx = ctx;
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

        [HttpPost("{name}/addWish")]
        public async Task<IActionResult> addWish(WishForAddDto wish, string name)
        {
            var user = _ctx.Users.Include(w => w.UserWishes).Where(u => u.UserName == name).FirstOrDefault();
            Wish wishToAdd = new Wish() {
                UrlToShop = wish.UrlToShop,
                Name = wish.Name,
                Cost = wish.Cost,
                User = user
            };
            user.UserWishes.Add(wishToAdd);
            _ctx.Wishes.Add(wishToAdd);
            await _ctx.SaveChangesAsync();
            return Ok("Wish Added");
        }
    }
}
