using System.Threading.Tasks;
using OnlineMarket.Models;

namespace OnlineMarket.Services.Interfaces
{
    public interface IAdminService
    {
         Task<bool> AddModerator(SystemUser moderator);
         Task<bool> DeleteModerator(string moderatorId);
         Task<bool> AddCategory(Category category);
        //  Task<bool> EditCategory(Category category);
    }
}