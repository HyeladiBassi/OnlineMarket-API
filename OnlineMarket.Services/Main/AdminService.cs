using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OnlineMarket.DataAccess;
using OnlineMarket.Models;
using OnlineMarket.Services.Interfaces;

namespace OnlineMarket.Services.Main
{
    public class AdminService : IAdminService
    {
        private readonly DataContext _context;
        private readonly UserManager<SystemUser> _userManager;

        public AdminService(DataContext context, UserManager<SystemUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<bool> AddCategory(Category category)
        {
            await _context.Categories.AddAsync(category);
            return await Save();
        }

        public async Task<bool> AddModerator(SystemUser moderator)
        {
            await _userManager.CreateAsync(moderator);
            await _userManager.AddToRoleAsync(moderator, "moderator");
            return await Save();
        }

        public async Task<bool> DeleteModerator(string moderatorId)
        {
            SystemUser user = await _userManager.FindByIdAsync(moderatorId);
            user.IsDeleted = true;
            await _userManager.UpdateAsync(user);
            return await Save();
        }

        private async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}