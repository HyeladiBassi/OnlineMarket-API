using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineMarket.Contracts;
using OnlineMarket.DataTransferObjects.ProductReview;
using OnlineMarket.Errors;
using OnlineMarket.Helpers.FileHelper;
using OnlineMarket.Models;
using OnlineMarket.Services.Extensions;
using OnlineMarket.Services.Interfaces;

namespace OnlineMarket.API.Controllers
{
    [ApiController]
    public class ProductReviewController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<SystemUser> _userManager;
        private readonly IProductReviewService _reviewService;
        private readonly IFileHelper _fileHelper;

        public ProductReviewController(IProductReviewService reviewService, IMapper mapper, UserManager<SystemUser> userManager, IFileHelper fileHelper)
        {
            _mapper = mapper;
            _userManager = userManager;
            _reviewService = reviewService;
            _fileHelper = fileHelper;
        }

        /// <summary>
        /// Get list of reviews by product Id
        /// </summary>
        [HttpGet(ApiConstants.ProductReviewRoutes.GetReviewsByProductId)]
        [ProducesResponseType(typeof(IEnumerable<ProductReviewViewDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetReviewsByProductId([FromParameter("id")] int id)
        {

            IEnumerable<ProductReview> reviews = await _reviewService.GetProductReviewsByProductId(id);
            IEnumerable<ProductReviewViewDto> mappedReviews = _mapper.Map<IEnumerable<ProductReviewViewDto>>(reviews);
            return Ok(mappedReviews);
        }

        /// <summary>
        /// Get list of reviews by user Id
        /// </summary>
        [HttpGet(ApiConstants.ProductReviewRoutes.GetReviewsByUserId)]
        [ProducesResponseType(typeof(IEnumerable<ProductReviewViewDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetReviewsByUserId([FromParameter("userId")] string id)
        {

            IEnumerable<ProductReview> reviews = await _reviewService.GetProductReviewsByUserId(id);
            IEnumerable<ProductReviewViewDto> mappedReviews = _mapper.Map<IEnumerable<ProductReviewViewDto>>(reviews);
            return Ok(mappedReviews);
        }

        /// <summary>
        /// Create a product review
        /// </summary>
        [HttpPost(ApiConstants.ProductReviewRoutes.CreateProductReview)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIError<ErrorTypes>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProductReview([FromParameter("id")] int id,[FromForm] ProductReviewCreateDto createDto)
        {
            ErrorBuilder<ErrorTypes> errorBuilder = new ErrorBuilder<ErrorTypes>(ErrorTypes.InvalidRequestBody);

            string userId = HttpContext.GetUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return StatusCode(403);
            }
            SystemUser user = await _userManager.FindByIdAsync(userId);
            ProductReview productReview = _mapper.Map<ProductReview>(createDto);            

            if (createDto.imageFiles != null && createDto.imageFiles.Count() > 0)
            {
                SaveFileResult[] mediaFiles = await Task.WhenAll(createDto.imageFiles.Select(x => _fileHelper.SaveFile(x)));
                List<Image> images = mediaFiles.Select(x => new Image() { Link = $"/{x.fileName.Replace('\\', '/')}", Type = x.type, MimeType = x.mimeType }).ToList();
                images[0].IsMain = true;
                productReview.Images = images;
            }

            productReview.Reviewer = user;
            bool result = await _reviewService.AddProductReview(id, productReview);
            if (result)
            {
                return Ok();
            }
            return BadRequest("Something went wrong");
        }

        /// <summary>
        /// Update product review using review id
        /// </summary>
        [HttpPut(ApiConstants.ProductReviewRoutes.UpdateProductReview)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIError<ErrorTypes>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProductReview([FromParameter("id")] int id, ProductReviewUpdateDto updateDto)
        {
            ErrorBuilder<ErrorTypes> errorBuilder = new ErrorBuilder<ErrorTypes>(ErrorTypes.InvalidRequestBody);

            string userId = HttpContext.GetUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return StatusCode(403);
            }
            ProductReview review = await _reviewService.GetProductReviewById(id);
            if (review.Reviewer.Id != userId)
            {
                return BadRequest(errorBuilder
                    .ChangeType(ErrorTypes.InvalidObjectId)
                    .SetMessage("You can only edit your reviews!")
                    .Build());
            }
            bool result = await _reviewService.UpdateProductReview(id, updateDto);
            if (result)
            {
                return Ok();
            }
            return BadRequest("Something went wrong");
        }

        /// <summary>
        /// Delete product review using review id
        /// </summary>
        [HttpDelete(ApiConstants.ProductReviewRoutes.DeleteProductReview)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIError<ErrorTypes>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteProductReview([FromParameter("id")] int id)
        {
            ErrorBuilder<ErrorTypes> errorBuilder = new ErrorBuilder<ErrorTypes>(ErrorTypes.InvalidRequestBody);

            string userId = HttpContext.GetUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return StatusCode(403);
            }
            ProductReview review = await _reviewService.GetProductReviewById(id);
            if (review.Reviewer.Id != userId)
            {
                return BadRequest(errorBuilder
                    .ChangeType(ErrorTypes.InvalidObjectId)
                    .SetMessage("You can only delete your reviews!")
                    .Build());
            }
            bool result = await _reviewService.DeleteProductReview(id);
            if (result)
            {
                return Ok();
            }
            return BadRequest("Something went wrong");
        }
    }
}