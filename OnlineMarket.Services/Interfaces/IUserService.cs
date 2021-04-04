using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineMarket.DataTransferObjects.SystemUser;
using OnlineMarket.Models;

namespace OnlineMarket.Services.Interfaces
{
    public interface IUserService
    {
         Task<SystemUser> GetUserById(string userId);
         Task<SystemUser> UpdateUser(SystemUserUpdateDto updateDto);
         Task<bool> DeleteUser(string userId);
         Task<bool> UpdateWishList(IEnumerable<Product> products);
    }
}