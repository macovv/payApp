using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using payApp.API.Models;

namespace payApp.API.Data
{
    public interface IUserRepository
    {
        Task<User> GetUser(string name);
        IQueryable<User> GetUsers(); // list?
        Task<bool> SaveAll();
    }
}