using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineMarket.Helpers;
using OnlineMarket.Models;

namespace OnlineMarket.Services.Interfaces
{
    public interface IAdminService
    {
         Task<SystemUser> AddModerator(SystemUser moderator);
         Task<bool> DeleteModerator(string moderatorId);
         Task<ICollection<SystemUser>> GetAllUsers(string role);
         Task<ICollection<SystemUser>> GetAllModerators();
         Task<bool> ModeratorExists(string id);
    }
}