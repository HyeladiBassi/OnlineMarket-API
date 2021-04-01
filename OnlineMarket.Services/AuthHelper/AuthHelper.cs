using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using OnlineMarket.DataAccess;
using OnlineMarket.DataTransferObjects.Authentication;
using OnlineMarket.Errors;
using OnlineMarket.Helpers;
using OnlineMarket.Models;

namespace OnlineMarket.Services.AuthHelper
{
    public class AuthHelper : IAuthHelper
    {
        private readonly JwtSettings _jwtSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly UserManager<SystemUser> _userManager;
        private readonly DataContext _context;

        public AuthHelper(
            DataContext context,
            JwtSettings jwtSettings,
            UserManager<SystemUser> userManager,
            TokenValidationParameters tokenValidationParameters
            )
        {
            _context = context;
            _jwtSettings = jwtSettings;
            _userManager = userManager;
            _tokenValidationParameters = tokenValidationParameters;
        }

        public async Task<ResponseWrapper<AuthResponse, ErrorTypes>> GenerateAuthTokenForSignIn(SystemUser user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            IList<string> userRoles = await _userManager.GetRolesAsync(user);
            List<Claim> claims = new List<Claim>() {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("id", user.Id),
                new Claim("name", user.FirstName),
                new Claim("role", userRoles.FirstOrDefault())
            };

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.Add(_jwtSettings.TokenLifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            RefreshToken refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                JwtId = token.Id,
                UserId = user.Id.ToString(),
                CreationDate = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(30)
            };

            await _context.RefreshTokens.AddAsync(refreshToken);
            await SaveAll();

            return new ResponseWrapper<AuthResponse, ErrorTypes>(false)
            {
                Success = true,
                Result = new AuthResponse
                {
                    token = tokenHandler.WriteToken(token),
                    refreshToken = refreshToken.Token
                }
            };
        }

        public async Task<ResponseWrapper<AuthResponse, ErrorTypes>> GenerateAuthTokenForSignUp(SystemUser user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            IList<string> userRoles = await _userManager.GetRolesAsync(user);
            List<Claim> claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("id", user.Id),
                new Claim("name", user.FirstName),
                new Claim("role", userRoles.FirstOrDefault())
            };

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.Add(_jwtSettings.TokenLifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            RefreshToken refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                JwtId = token.Id,
                UserId = user.Id.ToString(),
                CreationDate = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(30)
            };

            return new ResponseWrapper<AuthResponse, ErrorTypes>(false)
            {
                Success = true,
                Result = new AuthResponse
                {
                    token = tokenHandler.WriteToken(token),
                    refreshToken = refreshToken.Token
                }
            };

        }

        public ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                TokenValidationParameters tokenValidationParameters = _tokenValidationParameters.Clone();
                tokenValidationParameters.ValidateLifetime = false;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token.ToString(), tokenValidationParameters, out SecurityToken validatedToken);
                if (!IsTokenWithValidSecurityAlgorithm(validatedToken))
                {
                    return null;
                }

                return principal;
            }
            catch (System.Exception)
            {

                return null;
            }
        }

        public bool IsTokenWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken JwtSecurityToken) && JwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature, StringComparison.InvariantCultureIgnoreCase);
        }

        private async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}