using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using payApp.API.Models;

namespace payApp.API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _ctx;

        public UserRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<User> GetUser(string name)
        {
            var user = await _ctx.Users.Include(w => w.UserWishes).Where(u => u.UserName == name).FirstOrDefaultAsync();
            return user;
        }

        public async Task<IList<User>> GetUsers()
        {
            return await _ctx.Users.Include(w => w.UserWishes).ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await _ctx.SaveChangesAsync() > 0;
        }
    }
}