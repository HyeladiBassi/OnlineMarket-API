using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineMarket.Contracts;
using OnlineMarket.DataTransferObjects.SystemUser;
using OnlineMarket.Errors;
using OnlineMarket.Models;
using OnlineMarket.Services.Extensions;
using OnlineMarket.Services.Interfaces;

namespace OnlineMarket.API.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public UserController(IUserService userService, IMapper mapper, IProductService productService)
        {
            _userService = userService;
            _mapper = mapper;
            _productService = productService;
        }

        /// <summary>
        /// Get User by id
        /// </summary>
        [HttpGet(ApiConstants.UserRoutes.GetUserByID)]
        [ProducesResponseType(typeof(SystemUserViewDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserById([FromParameter("userId")] string userId)
        {
            string id = HttpContext.GetUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Forbid();
            }
            
            SystemUser user = await _userService.GetUserById(userId);
            SystemUserViewDto userView = _mapper.Map<SystemUserViewDto>(user);
            return Ok(userView);

        }

        /// <summary>
        /// Update user profile
        /// </summary>
        [HttpPut(ApiConstants.UserRoutes.UpdateUser)]
        [ProducesResponseType(typeof(SystemUserViewDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUser([FromParameter("userId")] string userId, SystemUserUpdateDto updateDto)
        {
            string id = HttpContext.GetUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Forbid();
            }
            SystemUser user = await _userService.UpdateUser(userId, updateDto);
            SystemUserViewDto userView = _mapper.Map<SystemUserViewDto>(user);
            return Ok(userView);

        }

        /// <summary>
        /// Delete user
        /// </summary>
        [HttpDelete(ApiConstants.UserRoutes.DeleteUser)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteUser([FromParameter("userId")] string userId)
        {
            string id = HttpContext.GetUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Forbid();
            }
            
            bool user = await _userService.DeleteUser(userId);
            return Ok(user);

        }

        /// <summary>
        /// Get wishlist by user id
        /// </summary>
        [HttpGet(ApiConstants.UserRoutes.GetWishList)]
        [ProducesResponseType(typeof(ICollection<WishListViewDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWishList([FromParameter("userId")] string userId)
        {
            string id = HttpContext.GetUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Forbid();
            }
            
            ICollection<WishListItem> wishlist = await _userService.GetWishlist(userId);
            ICollection<WishListViewDto> wishlistView = _mapper.Map<ICollection<WishListViewDto>>(wishlist);
            return Ok(wishlistView);

        }

        /// <summary>
        /// Add item to wish list
        /// </summary>
        [HttpPost(ApiConstants.UserRoutes.AddToWishList)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddToWishList([FromParameter("userId")] string userId, int productId)
        {
            ErrorBuilder<ErrorTypes> errorBuilder = new ErrorBuilder<ErrorTypes>(ErrorTypes.InvalidRequestBody);
            string id = HttpContext.GetUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Forbid();
            }

            if (!await _productService.ProductExists(productId))
            {
                return NotFound(errorBuilder
                    .ChangeType(ErrorTypes.InvalidObjectId)
                    .SetMessage("Product does not exist!")
                    .Build());
            }
            
            bool result = await _userService.AddToWishList(userId, productId);
            return Ok(result);

        }

        /// <summary>
        /// Remove item from wishlist
        /// </summary>
        [HttpDelete(ApiConstants.UserRoutes.RemoveFromWishList)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> RemoveFromWishList([FromParameter("userId")] string userId, int productId)
        {
            ErrorBuilder<ErrorTypes> errorBuilder = new ErrorBuilder<ErrorTypes>(ErrorTypes.InvalidRequestBody);
            string id = HttpContext.GetUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Forbid();
            }

            if (!await _productService.ProductExists(productId))
            {
                return NotFound(errorBuilder
                    .ChangeType(ErrorTypes.InvalidObjectId)
                    .SetMessage("Product no longer exists!")
                    .Build());
            }
            
            bool result = await _userService.RemoveFromWishList(userId, productId);
            return Ok(result);

        }
    }
}