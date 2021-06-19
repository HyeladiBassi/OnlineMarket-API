using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineMarket.Contracts;
using OnlineMarket.DataTransferObjects.Authentication;
using OnlineMarket.DataTransferObjects.SystemUser;
using OnlineMarket.Errors;
using OnlineMarket.Models;
using OnlineMarket.Services.Extensions;
using OnlineMarket.Services.Interfaces;

namespace OnlineMarket.API.Controllers
{
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public AdminController(IAdminService adminService, IMapper mapper, IUserService userService)
        {
            _adminService = adminService;
            _mapper = mapper;
            _userService = userService;
        }


        /// <summary>
        /// Gets list of users
        /// </summary>
        [HttpGet(ApiConstants.AdminRoutes.GetUsers)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ICollection<SystemUserViewDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIError<ErrorTypes>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(APIError<ErrorTypes>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUsers(string role)
        {
            ErrorBuilder<ErrorTypes> errorBuilder = new ErrorBuilder<ErrorTypes>(ErrorTypes.InvalidRequestBody);
            string userId = HttpContext.GetUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Forbid();
            }
            if (role != "seller" && role != "buyer")
            {
                return BadRequest(errorBuilder.ChangeType(ErrorTypes.InvalidRequestBody).AddField("Role", "Role must be seller or buyer").Build());
            }
            ICollection<SystemUser> users = await _adminService.GetAllUsers(role);
            ICollection<SystemUserViewDto> mappedUsers = _mapper.Map<ICollection<SystemUserViewDto>>(users);
            return Ok(mappedUsers);
        }


        /// <summary>
        /// Gets list of moderators
        /// </summary>
        [HttpGet(ApiConstants.AdminRoutes.GetModerators)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ICollection<SystemUserViewDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetModerators()
        {
            ErrorBuilder<ErrorTypes> errorBuilder = new ErrorBuilder<ErrorTypes>(ErrorTypes.InvalidRequestBody);
            string userId = HttpContext.GetUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Forbid();
            }
            ICollection<SystemUser> users = await _adminService.GetAllModerators();
            ICollection<SystemUserViewDto> mappedUsers = _mapper.Map<ICollection<SystemUserViewDto>>(users);
            return Ok(mappedUsers);
        }


        /// <summary>
        /// Add Moderator
        /// </summary>
        [HttpPost(ApiConstants.AdminRoutes.AddModerator)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(SystemUserViewDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIError<ErrorTypes>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddModerator(ModeratorCreateDto createDto)
        {
            ErrorBuilder<ErrorTypes> errorBuilder = new ErrorBuilder<ErrorTypes>(ErrorTypes.InvalidRequestBody);
            string userId = HttpContext.GetUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Forbid();
            }

            SystemUser newModerator = _mapper.Map<SystemUser>(createDto);

            SystemUser result = await _adminService.AddModerator(newModerator);

            if (result != null)
            {
                SystemUserViewDto newUser = _mapper.Map<SystemUserViewDto>(result);
                return Ok(newUser);
            }

            return BadRequest("Something went wrong!");
        }


        /// <summary>
        /// Delete Moderator
        /// </summary>
        [HttpDelete(ApiConstants.AdminRoutes.AddModerator)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIError<ErrorTypes>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteModerator([FromParameter("moderatorId")] string moderatorId)
        {
            ErrorBuilder<ErrorTypes> errorBuilder = new ErrorBuilder<ErrorTypes>(ErrorTypes.InvalidRequestBody);
            string userId = HttpContext.GetUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Forbid();
            }

            if (!await _adminService.ModeratorExists(moderatorId))
            {
                return BadRequest(errorBuilder.ChangeType(ErrorTypes.InvalidRequestBody).SetMessage("The user is not a moderator").Build());
            }

            bool result = await _adminService.DeleteModerator(moderatorId);

            if (result)
            {
                return Ok(result);
            }

            return BadRequest("Something went wrong!");
        }
    }
}