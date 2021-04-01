using System.Threading.Tasks;
using OnlineMarket.DataTransferObjects.Authentication;
using OnlineMarket.Errors;
using OnlineMarket.Models;

namespace OnlineMarket.Services.Interfaces
{
    public interface IAuthService
    {
        bool IsValidEmail(string email);
        Task<ResponseWrapper<AuthResponse, ErrorTypes>> SignIn(AuthRequest authRequest);
        Task<ResponseWrapper<AuthResponse, ErrorTypes>> SignUp(SystemUser systemUser, string password, string role);        
        Task<ResponseWrapper<AuthResponse, ErrorTypes>> RefreshToken(string token, string refreshToken);
        
    }
}