using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OnlineMarket.DataAccess;
using OnlineMarket.DataTransferObjects.SystemUser;
using OnlineMarket.Models;
using OnlineMarket.Services.Interfaces;

namespace OnlineMarket.Services.Main
{
    public class UserService : IUserService
    {
        public UserService(UserManager<SystemUser> userManager, DataContext context)
        {
            
        }
        public Task<bool> DeleteUser(string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<SystemUser> GetUserById(string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<ProductReview> ReveiwProduct(int productId, ProductReview productReview)
        {
            throw new System.NotImplementedException();
        }

        public Task<SystemUser> UpdateUser(SystemUserUpdateDto updateDto)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateWishList(IEnumerable<Product> products)
        {
            throw new System.NotImplementedException();
        }
    }
}