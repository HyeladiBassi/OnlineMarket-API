using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineMarket.DataAccess;
using OnlineMarket.DataTransferObjects.Authentication;
using OnlineMarket.Errors;
using OnlineMarket.Models;
using OnlineMarket.Services.AuthHelper;
using OnlineMarket.Services.Interfaces;

namespace OnlineMarket.Services.Main
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<SystemUser> _userManager;
        private readonly DataContext _context;
        private readonly IAuthHelper _authHelper;

        public AuthService(
            UserManager<SystemUser> userManager,
            DataContext context,
            IAuthHelper authHelper
            )
        {
            _userManager = userManager;
            _context = context;
            _authHelper = authHelper;

        }
        public bool IsValidEmail(string email)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                 @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                 @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            Regex regex = new Regex(strRegex);
            if (regex.IsMatch(email))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<ResponseWrapper<AuthResponse, ErrorTypes>> RefreshToken(string token, string refreshToken)
        {
            ErrorBuilder<ErrorTypes> errorBuilder = new ErrorBuilder<ErrorTypes>(ErrorTypes.WrongCredentials);

            System.Security.Claims.ClaimsPrincipal validatedToken = _authHelper.GetPrincipalFromToken(token);

            if (validatedToken == null)
            {
                return new ResponseWrapper<AuthResponse, ErrorTypes>(false)
                {
                    Error = errorBuilder.ChangeType(ErrorTypes.WrongCredentials).SetMessage("Invalid Token!").Build()
                };
            }

            long expiryDateUnix = long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

            DateTime expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(expiryDateUnix);

            if (expiryDateTimeUtc > DateTime.Now)
            {
                return new ResponseWrapper<AuthResponse, ErrorTypes>(false)
                {
                    Error = errorBuilder.ChangeType(ErrorTypes.WrongCredentials).SetMessage("Token has not expired yet!").Build()
                };
            }

            string jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            RefreshToken storedToken = await _context.RefreshTokens.SingleOrDefaultAsync(x => x.Token == refreshToken);

            if (storedToken == null)
            {
                return new ResponseWrapper<AuthResponse, ErrorTypes>(false)
                {
                    Error = errorBuilder.ChangeType(ErrorTypes.WrongCredentials).SetMessage("Refresh token does not exist").Build()
                };
            }

            if (DateTime.Now > storedToken.ExpiryDate)
            {
                return new ResponseWrapper<AuthResponse, ErrorTypes>(false)
                {
                    Error = errorBuilder.ChangeType(ErrorTypes.WrongCredentials).SetMessage("Token has expired").Build()
                };
            }

            if (storedToken.Invalidated)
            {
                return new ResponseWrapper<AuthResponse, ErrorTypes>(false)
                {
                    Error = errorBuilder.ChangeType(ErrorTypes.WrongCredentials)
                    .SetMessage("Refresh Token has been invalidated!").Build()
                };
            }

            if (storedToken.Used)
            {
                return new ResponseWrapper<AuthResponse, ErrorTypes>(false)
                {
                    Error = errorBuilder.ChangeType(ErrorTypes.WrongCredentials)
                    .SetMessage("Refresh Token has been used!").Build()
                };
            }

            if (storedToken.JwtId != jti)
            {
                return new ResponseWrapper<AuthResponse, ErrorTypes>(false)
                {
                    Error = errorBuilder.ChangeType(ErrorTypes.WrongCredentials)
                    .SetMessage("Refresh Token does not match JWT!").Build()
                };
            }

            storedToken.Used = true;
            // TODO Store/update refresh token when validated
            _context.RefreshTokens.Update(storedToken);
            await SaveAll();

            SystemUser user = await _userManager.FindByIdAsync(validatedToken.Claims.Single(x => x.Type == "id").Value);
            return await _authHelper.GenerateAuthTokenForSignIn(user);
        }

        public async Task<ResponseWrapper<AuthResponse, ErrorTypes>> SignIn(AuthRequest authRequest)
        {
            ErrorBuilder<ErrorTypes> errorBuilder = new ErrorBuilder<ErrorTypes>(ErrorTypes.WrongCredentials);
            SystemUser userByUsername = await _userManager.FindByNameAsync(authRequest.access);
            SystemUser userByEmail = await _userManager.FindByEmailAsync(authRequest.access);


            if (IsValidEmail(authRequest.access) && userByEmail != null)
            {
                bool authWithEmail = await _userManager.CheckPasswordAsync(userByEmail, authRequest.password);
                if (!authWithEmail)
                {
                    return new ResponseWrapper<AuthResponse, ErrorTypes>(false)
                    {
                        Error = errorBuilder.ChangeType(ErrorTypes.WrongCredentials).SetMessage("Email/Password Combination is wrong!").Build()
                    };
                }
                return await _authHelper.GenerateAuthTokenForSignIn(userByEmail);
            }

            if (userByUsername == null)
            {
                return new ResponseWrapper<AuthResponse, ErrorTypes>(false)
                {
                    Error = errorBuilder.ChangeType(ErrorTypes.WrongCredentials).SetMessage("User not found !").Build()
                };
            }

            bool result = await _userManager.CheckPasswordAsync(userByUsername, authRequest.password);

            if (!result)
            {
                return new ResponseWrapper<AuthResponse, ErrorTypes>(false)
                {
                    Error = errorBuilder.ChangeType(ErrorTypes.WrongCredentials).SetMessage("Email/Password Combination is wrong!").Build()
                };
            }
            return await _authHelper.GenerateAuthTokenForSignIn(userByUsername);

        }

        public async Task<ResponseWrapper<AuthResponse, ErrorTypes>> SignUp(SystemUser systemUser, string password, string role)
        {
            ErrorBuilder<ErrorTypes> errorBuilder = new ErrorBuilder<ErrorTypes>(ErrorTypes.WrongCredentials);

            SystemUser email = await _userManager.FindByEmailAsync(systemUser.Email);

            if (email != null)
            {
                return new ResponseWrapper<AuthResponse, ErrorTypes>(false)
                {
                    Error = errorBuilder.ChangeType(ErrorTypes.InvalidRequestBody).SetMessage("Account with email exists!").Build()
                };
            }

            SystemUser existingUser = await _userManager.FindByNameAsync(systemUser.UserName);

            if (existingUser != null)
            {
                return new ResponseWrapper<AuthResponse, ErrorTypes>(false)
                {
                    Error = errorBuilder.ChangeType(ErrorTypes.InvalidRequestBody).SetMessage("Username is taken!").Build()
                };
            }

            if (!IsValidEmail(systemUser.Email))
            {
                return new ResponseWrapper<AuthResponse, ErrorTypes>(false)
                {
                    Error = errorBuilder.ChangeType(ErrorTypes.InvalidRequestBody).SetMessage("Invalid email address").Build()
                };
            }

            IdentityResult result = await _userManager.CreateAsync(systemUser, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(systemUser, role);
                return await _authHelper.GenerateAuthTokenForSignUp(systemUser);
            }
            else
            {
                string errors = string.Join(',', result.Errors.Select(x => x.Description));
                return new ResponseWrapper<AuthResponse, ErrorTypes>(false)
                {
                    Error = errorBuilder.ChangeType(ErrorTypes.WrongCredentials).SetMessage(errors).Build()
                };
            }
        }

        private async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}