using System.Threading.Tasks;
using OnlineMarket.Models;

namespace OnlineMarket.Services.Interfaces
{
    public interface IAdminService
    {
         Task<SystemUser> AddModerator(SystemUser moderator);
         
    }
}