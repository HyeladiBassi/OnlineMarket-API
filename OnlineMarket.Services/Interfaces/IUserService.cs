using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineMarket.DataTransferObjects.SystemUser;
using OnlineMarket.Models;

namespace OnlineMarket.Services.Interfaces
{
    public interface IUserService
    {
         Task<SystemUser> GetUserById(string userId);
         Task<SystemUser> UpdateUser(string userId, SystemUserUpdateDto updateDto);
         Task<bool> DeleteUser(string userId);
         Task<ICollection<WishListItem>> GetWishlist(string userId);
         Task<bool> AddToWishList(string userId, int productId);
         Task<bool> RemoveFromWishList(string userId, int productId);
    }
}