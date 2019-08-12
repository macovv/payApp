using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using payApp.API.Models;

namespace payApp.API.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions opt) : base(opt)
        {
        }

        public DbSet<Wish>  Wishes { get; set; }
    }
}