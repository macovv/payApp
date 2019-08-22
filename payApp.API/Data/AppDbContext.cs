using System.Buffers.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using payApp.API.Models;

namespace payApp.API.Data
{
    public class AppDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, 
    IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public AppDbContext(DbContextOptions opt) : base(opt)
        {
        }

        public DbSet<Wish> Wishes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // builder.Entity<UserRole>(userRole => {

            //     userRole.HasKey(ur => new {ur.UserId, ur.RoleId});

            //     userRole.HasOne(ur => ur.Role)
            //         .WithMany(r => r.UserRoles)
            //         .HasForeignKey(ur => ur.RoleId)
            //         .IsRequired();

            //     userRole.HasOne(ur => ur.User)
            //         .WithMany(r => r.UserRoles)
            //         .HasForeignKey(ur => ur.UserId)
            //         .IsRequired();
            // });
        }

    }
}