using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineMarket.Contracts;
using OnlineMarket.DataTransferObjects.Authentication;
using OnlineMarket.Errors;
using OnlineMarket.Models;
using OnlineMarket.Services.Interfaces;

namespace OnlineMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly SignInManager<SystemUser> _signInManager;
        private readonly IMapper _mapper;

        public AuthenticationController(IAuthService authService, SignInManager<SystemUser> signInManager, IMapper mapper)
        {
            _authService = authService;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        /// <summary>
        /// Sign in
        /// </summary>
        /// <param name="authRequest"></param>
        [HttpPost(ApiConstants.AuthRoutes.SignIn)]
        [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIError<ErrorTypes>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignIn([FromBody] AuthRequest authRequest)
        {
            ResponseWrapper<AuthResponse, ErrorTypes> user = await _authService.SignIn(authRequest);

            if (!user.Success)
            {
                return BadRequest(user.Error);
            }

            return Ok(user.Result);
        }


        /// <summary>
        /// Buyer signup
        /// </summary>
        /// <param name="signUpDto"></param>
        [HttpPost(ApiConstants.AuthRoutes.SignUp)]
        [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIError<ErrorTypes>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> BuyerSignUp([FromBody] BuyerSignUpDto signUpDto)
        {
            ErrorBuilder<ErrorTypes> errorBuilder = new ErrorBuilder<ErrorTypes>(ErrorTypes.InvalidRequestBody);
            bool emailValidationResult = _authService.IsValidEmail(signUpDto.email);

            if (!emailValidationResult)
            {
                return BadRequest(errorBuilder.ChangeType(ErrorTypes.InvalidRequestBody).SetMessage("Email address is not valid!").Build());
            }
            SystemUser user = _mapper.Map<SystemUser>(signUpDto);
            ResponseWrapper<AuthResponse, ErrorTypes> result = await _authService.SignUp(user, signUpDto.password, "Buyer");
            if (!result.Success)
            {
                return BadRequest(result.Error);
            }
            await _signInManager.SignInAsync(user, false);
            return Ok(result.Result);
        }
    }
}