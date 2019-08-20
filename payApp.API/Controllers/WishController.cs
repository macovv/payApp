using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using payApp.API.Data;
using payApp.API.Dtos;
using payApp.API.Models;

namespace payApp.API.Controllers
{
    [Route("api/user/{username}/[controller]")]
    [ApiController]
    public class WishController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;

        public WishController(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> getWishes(string username)
        {
            var wishes = await _repo.GetUserWishes(username);
            if(wishes != null)
                return Ok(wishes);
            return BadRequest("Problem with getting user wishes");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getWish(int id)
        {
            var wish = await _repo.GetUserWish(id);
            if(wish != null)
                return Ok(wish);
            return BadRequest("Problem with getting user wish");

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> addWish(WishForAddDto wish, string username)
        {
            var user = await _repo.GetUser(username);
            if (user.UserName.ToString() != User.FindFirst(ClaimTypes.NameIdentifier).Value)
            {
                return Unauthorized();
            }
            var wishToAdd = _mapper.Map<Wish>(wish);
            user.UserWishes.Add(wishToAdd);
            if(await _repo.SaveAll())
                return Ok(user);
            return BadRequest("Problem with adding new wish!");
        }

        [Authorize]
        [HttpPost("{wishId}/payForWish")]
        public async Task<IActionResult> payForWish(string name, int wishId)
        {               
            var user = await _repo.GetUser(name);
            var wish = user.UserWishes.Where(i => i.Id == wishId).FirstOrDefault();  // to do 
            var loggedUsser = await _repo.GetUser(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            loggedUsser.Saldo -= wish.Cost; // if less than 0 return to previous saldo
            if(loggedUsser.Saldo > 0)
                return Ok(wish);
            else
                return BadRequest("You can not afford this " + wish);
        }


    }
}