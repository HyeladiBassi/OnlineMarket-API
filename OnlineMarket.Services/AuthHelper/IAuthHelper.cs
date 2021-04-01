using System.Security.Claims;
using System.Threading.Tasks;
using OnlineMarket.Models;
using Microsoft.IdentityModel.Tokens;
using OnlineMarket.DataTransferObjects.Authentication;
using OnlineMarket.Errors;

namespace OnlineMarket.Services.AuthHelper
{
    public interface IAuthHelper
    {
        Task<ResponseWrapper<AuthResponse, ErrorTypes>> GenerateAuthTokenForSignIn(SystemUser user);
        Task<ResponseWrapper<AuthResponse, ErrorTypes>> GenerateAuthTokenForSignUp(SystemUser user);
        ClaimsPrincipal GetPrincipalFromToken(string token);
        bool IsTokenWithValidSecurityAlgorithm(SecurityToken validatedToken);
    }
}