using System.Collections.Generic;
using payApp.API.Models;

namespace payApp.API.Dtos
{
    public class UserForListDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public ICollection<Wish> UserWishes { get; set; }


    }
}