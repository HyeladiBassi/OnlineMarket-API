using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineMarket.DataAccess;
using OnlineMarket.Helpers;
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

        public async Task<SystemUser> AddModerator(SystemUser moderator)
        {
            await _userManager.CreateAsync(moderator);
            await _userManager.AddToRoleAsync(moderator, "moderator");
            return moderator;
        }

        public async Task<bool> DeleteModerator(string moderatorId)
        {
            SystemUser user = await _userManager.FindByIdAsync(moderatorId);
            IdentityResult result = await _userManager.RemoveFromRoleAsync(user, "moderator");
            IdentityResult res = await _userManager.DeleteAsync(user);
            return res.Succeeded && result.Succeeded;
        }

        public async Task<ICollection<SystemUser>> GetAllModerators()
        {
            IList<SystemUser> moderators = await _userManager.GetUsersInRoleAsync("moderator");
            return moderators;
        }

        public async Task<ICollection<SystemUser>> GetAllUsers(string role)
        {
            IList<SystemUser> users = await _userManager.GetUsersInRoleAsync(role);
            return users;
        }

        public async  Task<bool> ModeratorExists(string id)
        {
            SystemUser user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return false;
            }
            bool result = await _userManager.IsInRoleAsync(user, "moderator");
            return result;
        }

        private async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}