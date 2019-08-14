using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace payApp.API.Models
{
    public class User : IdentityUser<int>
    {
        public int Income { get; set; }
        public int Costs { get; set; }
        public int Saldo { get; set; }
        public ICollection<Wish> UserWishes { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        //ICollection of products which client want to buy(Wishes MODEL)
    }
}