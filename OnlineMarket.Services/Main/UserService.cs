using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineMarket.DataAccess;
using OnlineMarket.DataTransferObjects.SystemUser;
using OnlineMarket.Models;
using OnlineMarket.Services.Interfaces;

namespace OnlineMarket.Services.Main
{
    public class UserService : IUserService
    {
        private readonly UserManager<SystemUser> _userManager;
        private readonly DataContext _context;

        public UserService(UserManager<SystemUser> userManager, DataContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<bool> AddToWishList(string userId, int productId)
        {
            Product product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
            WishListItem wishList = new WishListItem {
                Product = product,
                UserId = userId
            };
            await _context.WishList.AddAsync(wishList);
            return await Save();
        }

        public async Task<bool> DeleteUser(string userId)
        {
            SystemUser user = await _userManager.FindByIdAsync(userId);
            user.IsDeleted = true;
            await _userManager.UpdateAsync(user);
            return await Save();
        }

        public async Task<SystemUser> GetUserById(string userId)
        {
            SystemUser user = await _userManager.FindByIdAsync(userId);
            return user;
        }

        public async Task<ICollection<WishListItem>> GetWishlist(string userId)
        {
            var wishlist = await _context.WishList
                .Include(x => x.Product)
                .Include(x => x.Product.Images)
                .AsSingleQuery()
                .Where(x => x.UserId == userId)
                .ToListAsync();
            return wishlist;
        }

        public async Task<bool> RemoveFromWishList(string userId, int productId)
        {
            Product product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
            WishListItem wishListItem = await _context.WishList.FirstOrDefaultAsync(x => x.Product.Id == productId);
            _context.WishList.Remove(wishListItem);
            return await Save();
        }

        public async Task<SystemUser> UpdateUser(string userId, SystemUserUpdateDto updateDto)
        {
            SystemUser existingUser = await _userManager.FindByIdAsync(userId);

            if (updateDto.FirstName != null)
            {
                existingUser.FirstName = updateDto.FirstName;
            }
            if (updateDto.LastName != null)
            {
                existingUser.LastName = updateDto.LastName;
            }
            if (updateDto.Address != null)
            {
                existingUser.Address = updateDto.Address;
            }
            if (updateDto.Area != null)
            {
                existingUser.Area = updateDto.Area;
            }
            if (updateDto.City != null)
            {
                existingUser.City = updateDto.City;
            }
            if (updateDto.Email != null)
            {
                existingUser.Email = updateDto.Email;
            }
            if (updateDto.ExtraDetails != null)
            {
                existingUser.ExtraDetails = updateDto.ExtraDetails;
            }
            if (updateDto.IBAN != null)
            {
                existingUser.IBAN = updateDto.IBAN;
            }
            if (updateDto.Bank != null)
            {
                existingUser.BankName = updateDto.Bank;
            }
            await _userManager.UpdateAsync(existingUser);
            return existingUser;
        }
        
        private async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}