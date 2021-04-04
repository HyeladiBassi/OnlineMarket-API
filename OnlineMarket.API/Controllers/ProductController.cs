using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineMarket.Contracts;
using OnlineMarket.DataTransferObjects.Product;
using OnlineMarket.Errors;
using OnlineMarket.Models;
using OnlineMarket.Services.Extensions;
using OnlineMarket.Services.Interfaces;

namespace OnlineMarket.API.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly UserManager<SystemUser> _userManager;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper, UserManager<SystemUser> userManager)
        {
            _productService = productService;
            _mapper = mapper;
            _userManager = userManager;
        }


        /// <summary>
        /// Get list of products
        /// </summary>
        /// <param name="parameters"></param>
        [HttpGet(ApiConstants.ProductRoutes.GetAllProducts)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIError<ErrorTypes>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProductList([FromQuery] ProductResourceParameters parameters)
        {
            string userId = HttpContext.GetUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return StatusCode(403);
            }

            IEnumerable<Product> products = await _productService.GetProductList(parameters);
            return Ok(products);
        }


        /// <summary>
        /// Get product by Id
        /// </summary>
        /// <param name="id"></param>
        [HttpGet(ApiConstants.ProductRoutes.GetProductById)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIError<ErrorTypes>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProductById([FromParameter("id")] int id)
        {
            string userId = HttpContext.GetUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return StatusCode(403);
            }

            if (!await _productService.ProductExists(id))
            {
                return BadRequest("Product does not exist!");
            }

            Product product = await _productService.GetProductById(id);
            return Ok(product);
        }


        /// <summary>
        /// Create new product
        /// </summary>
        /// <param name="createProductDto"></param>
        [HttpPost(ApiConstants.ProductRoutes.CreateProduct)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIError<ErrorTypes>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto createProductDto)
        {

            string userId = HttpContext.GetUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return StatusCode(403);
            }

            Product product = _mapper.Map<Product>(createProductDto);
            product.Seller = await _userManager.FindByIdAsync(userId);
            bool result = await _productService.CreateProduct(product);
            if (result)
            {
                return Ok(product);
            }
            return BadRequest("Something went wrong!");
        }


        /// <summary>
        /// Puchase product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="amount"></param>
        [HttpPost(ApiConstants.ProductRoutes.GetProductById)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIError<ErrorTypes>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PurchaseProduct([FromParameter("id")] int id, [FromQuery] int amount)
        {
            string userId = HttpContext.GetUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return StatusCode(403);
            }

            if (!await _productService.ProductExists(id))
            {
                return NotFound("Product does not exist!");
            }

            if (!await _productService.CheckAvailability(id, amount))
            {
                return BadRequest("Not enough stock to complete purchase");
            }

            if (await _productService.BuyProduct(id, amount))
            {
                return Ok();
            }

            return BadRequest("Something went wrong!");
        }


        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedProductDto"></param>
        [HttpPut(ApiConstants.ProductRoutes.UpdateProduct)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProduct([FromParameter("id")] int id, ProductUpdateDto updatedProductDto)
        {
            string userId = HttpContext.GetUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return StatusCode(403);
            }

            if (!await _productService.ProductExists(id))
            {
                return NotFound("Product does not exist!");
            }

            Product product = await _productService.UpdateProduct(id, updatedProductDto);
            return Ok(product);
        }
    }
}