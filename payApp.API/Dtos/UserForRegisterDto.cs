using System.Collections.Generic;
using payApp.API.Models;

namespace payApp.API.Dtos
{
    public class UserForRegisterDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Income { get; set; }
        public int Costs { get; set; }
        public string Email { get; set; }
    }
}