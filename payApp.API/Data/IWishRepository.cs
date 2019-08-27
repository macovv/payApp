using System.Collections.Generic;
using System.Threading.Tasks;
using payApp.API.Models;

namespace payApp.API.Data
{
    public interface IWishRepository
    {
        Task<bool> SaveAll();
        Task<IList<Wish>> GetWishes();
        Task<Wish> GetWish(int id);
        Task<IList<Wish>> GetUserWishes(string userName);
        void AddWish(User user, Wish wishToAdd);
        void RemoveWish(Wish wishToRemove);
    }
}