using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace OnlineMarket.Services.Extensions
{
    public static class TokenExtension
    {
        public static string GetUserIdFromToken(this HttpContext httpContext)
        {
            JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();
            KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues> authHeader = httpContext.Request.Headers.Where(x => x.Key == "Authorization").FirstOrDefault();
            string token = authHeader.Value.FirstOrDefault();

            if (token == null)
            {
                return string.Empty;
            }
            
            JwtSecurityToken securityToken = jwtHandler.ReadJwtToken(token.Split(' ').LastOrDefault());
            
            if (httpContext.User == null)
            {
                return string.Empty;
            }

            System.Security.Claims.Claim userIdClaim = securityToken.Claims.Where(x => x.Type == "id").FirstOrDefault();
            return userIdClaim.Value;
        }

        // public static string GetUserId(this HttpContext httpContext)
        // {
        //     if (httpContext.User == null)
        //     {
        //         return string.Empty;
        //     }
        //     return httpContext.User.Claims.Single(x => x.Type == "id").Value;
        // }
    }
}