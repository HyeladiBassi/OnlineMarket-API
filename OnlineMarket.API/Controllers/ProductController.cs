using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineMarket.API.Extensions;
using OnlineMarket.Contracts;
using OnlineMarket.DataTransferObjects;
using OnlineMarket.DataTransferObjects.Product;
using OnlineMarket.Errors;
using OnlineMarket.Helpers;
using OnlineMarket.Helpers.FileHelper;
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
        private readonly IFileHelper _fileHelper;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper, UserManager<SystemUser> userManager, IFileHelper fileHelper)
        {
            _productService = productService;
            _mapper = mapper;
            _userManager = userManager;
            _fileHelper = fileHelper;
        }


        /// <summary>
        /// Get paged list of products
        /// </summary>
        [HttpGet(ApiConstants.ProductRoutes.GetAllProducts)]
        [ProducesResponseType(typeof(Paginate<ProductViewDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductList([FromQuery] ProductResourceParameters parameters)
        {

            switch (parameters.region)
            {
                case null:
                    {
                        PagedList<Product> pagedProducts = await _productService.GetPagedProductList(parameters);
                        PagingDto paging = pagedProducts.ExtractPaging();

                        Paginate<ProductViewDto> result = new Paginate<ProductViewDto>
                        {
                            items = _mapper.Map<IEnumerable<ProductViewDto>>(pagedProducts),
                            pagingInfo = paging
                        };
                        return Ok(result);
                    }
                default:
                    {
                        PagedList<Product> pagedProducts = await _productService.GetPagedProductListFromRegion(parameters);
                        PagingDto paging = pagedProducts.ExtractPaging();

                        Paginate<ProductViewDto> result = new Paginate<ProductViewDto>
                        {
                            items = _mapper.Map<IEnumerable<ProductViewDto>>(pagedProducts),
                            pagingInfo = paging
                        };
                        return Ok(result);
                    }
            }
        }

        /// <summary>
        /// Get paged list of products by seller Id
        /// </summary>
        [HttpGet(ApiConstants.ProductRoutes.GetUserProducts)]
        [ProducesResponseType(typeof(Paginate<ProductViewDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductListByUserId([FromParameter("id")] string id, [FromQuery] ProductResourceParameters parameters)
        {
            PagedList<Product> pagedProducts = await _productService.GetPagedProductListByUserId(id, parameters);
            PagingDto paging = pagedProducts.ExtractPaging();

            Paginate<ProductViewDto> result = new Paginate<ProductViewDto>
            {
                items = _mapper.Map<IEnumerable<ProductViewDto>>(pagedProducts),
                pagingInfo = paging
            };
            return Ok(result);
        }


        /// <summary>
        /// Get list of unapproved products
        /// </summary>
        [HttpGet(ApiConstants.ProductRoutes.UnapprovedProducts)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(IEnumerable<ProductViewDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUnapprovedProducts([FromQuery] ProductResourceParameters parameters)
        {
            string userId = HttpContext.GetUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Forbid();
            }

            IEnumerable<Product> products = await _productService.GetUnapprovedProductList(parameters);
            IEnumerable<ProductViewDto> productsView = _mapper.Map<IEnumerable<ProductViewDto>>(products);

            PagedList<Product> pagedProducts = await _productService.GetPagedProductList(parameters);
            PagingDto paging = pagedProducts.ExtractPaging();

            Paginate<ProductViewDto> result = new Paginate<ProductViewDto>
            {
                items = _mapper.Map<IEnumerable<ProductViewDto>>(pagedProducts),
                pagingInfo = paging
            };

            return Ok(productsView);
        }


        /// <summary>
        /// Get list of rejected products
        /// </summary>
        [HttpGet(ApiConstants.ProductRoutes.RejectedProducts)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(IEnumerable<ProductViewDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRejectedProducts([FromQuery] ProductResourceParameters parameters)
        {
            string userId = HttpContext.GetUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Forbid();
            }

            IEnumerable<Product> products = await _productService.GetRejectedProductList(parameters);
            IEnumerable<ProductViewDto> productsView = _mapper.Map<IEnumerable<ProductViewDto>>(products);

            PagedList<Product> pagedProducts = await _productService.GetPagedProductList(parameters);
            PagingDto paging = pagedProducts.ExtractPaging();

            Paginate<ProductViewDto> result = new Paginate<ProductViewDto>
            {
                items = _mapper.Map<IEnumerable<ProductViewDto>>(pagedProducts),
                pagingInfo = paging
            };

            return Ok(productsView);
        }


        /// <summary>
        /// Get product by Id
        /// </summary>
        [HttpGet(ApiConstants.ProductRoutes.GetProductById)]
        [ProducesResponseType(typeof(ProductViewDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIError<ErrorTypes>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductById([FromParameter("id")] int id)
        {
            ErrorBuilder<ErrorTypes> errorBuilder = new ErrorBuilder<ErrorTypes>(ErrorTypes.InvalidRequestBody);
            if (!await _productService.ProductExists(id))
            {
                return NotFound(errorBuilder
                    .ChangeType(ErrorTypes.InvalidObjectId)
                    .SetMessage("Product does not exist!")
                    .Build());
            }

            Product product = await _productService.GetProductById(id);
            ProductViewDto productView = _mapper.Map<ProductViewDto>(product);
            return Ok(productView);
        }


        /// <summary>
        /// Create new product
        /// </summary>
        [HttpPost(ApiConstants.ProductRoutes.CreateProduct)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProductViewDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIError<ErrorTypes>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProduct([FromForm] ProductCreateDto createProductDto)
        {

            string userId = HttpContext.GetUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Forbid("Unauthorized");
            }

            Product product = _mapper.Map<Product>(createProductDto);

            if (createProductDto.imageFiles != null && createProductDto.imageFiles.Count() > 0)
            {
                SaveFileResult[] mediaFiles = await Task.WhenAll(createProductDto.imageFiles.Select(x => _fileHelper.SaveFile(x)));
                List<Image> images = mediaFiles.Select(x => new Image() { Link = $"/{x.fileName.Replace('\\', '/')}", Type = x.type, MimeType = x.mimeType }).ToList();
                images[0].IsMain = true;
                product.Images = images;
            }

            product.Seller = await _userManager.FindByIdAsync(userId);
            bool result = await _productService.CreateProduct(product);
            ProductViewDto productView = _mapper.Map<ProductViewDto>(product);
            if (result)
            {
                return Ok(productView);
            }
            return BadRequest("Something went wrong!");
        }


        /// <summary>
        /// Puchase product
        /// </summary>
        [HttpPost(ApiConstants.ProductRoutes.GetProductById)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIError<ErrorTypes>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(APIError<ErrorTypes>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PurchaseProduct([FromParameter("id")] int id, [FromQuery] int quantity)
        {
            ErrorBuilder<ErrorTypes> errorBuilder = new ErrorBuilder<ErrorTypes>(ErrorTypes.InvalidRequestBody);
            string userId = HttpContext.GetUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Forbid();
            }

            if (!await _productService.ProductExists(id))
            {
                return NotFound(errorBuilder
                    .ChangeType(ErrorTypes.InvalidObjectId)
                    .SetMessage("Product does not exist")
                    .Build());
            }

            if (!await _productService.CheckAvailability(id, quantity))
            {
                return BadRequest(errorBuilder
                    .ChangeType(ErrorTypes.InvalidObjectId)
                    .SetMessage("Not enough stock to complete purchase")
                    .Build());
            }

            if (await _productService.BuyProduct(id, quantity))
            {
                return Ok("Successfully purchased product");
            }

            return BadRequest("Something went wrong!");
        }


        /// <summary>
        /// Update product
        /// </summary>
        [HttpPut(ApiConstants.ProductRoutes.UpdateProduct)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(APIError<ErrorTypes>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProductViewDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProduct([FromParameter("id")] int id, [FromForm] ProductUpdateDto updatedProductDto)
        {
            ErrorBuilder<ErrorTypes> errorBuilder = new ErrorBuilder<ErrorTypes>(ErrorTypes.InvalidRequestBody);
            string userId = HttpContext.GetUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Forbid();
            }

            if (!await _productService.ProductExists(id))
            {
                return NotFound(errorBuilder
                    .ChangeType(ErrorTypes.InvalidObjectId)
                    .SetMessage("Product does not exist")
                    .Build());
            }

            Product product = await _productService.UpdateProduct(id, updatedProductDto);
            ProductViewDto productView = _mapper.Map<ProductViewDto>(product);
            return Ok(productView);
        }


        /// <summary>
        /// Delete product
        /// </summary>
        [HttpDelete(ApiConstants.ProductRoutes.DeleteProduct)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(APIError<ErrorTypes>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProductViewDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteProduct([FromParameter("id")] int id)
        {
            ErrorBuilder<ErrorTypes> errorBuilder = new ErrorBuilder<ErrorTypes>(ErrorTypes.InvalidRequestBody);
            string userId = HttpContext.GetUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Forbid();
            }

            if (!await _productService.ProductExists(id))
            {
                return NotFound(errorBuilder
                    .ChangeType(ErrorTypes.InvalidObjectId)
                    .SetMessage("Product does not exist")
                    .Build());
            }

            bool result = await _productService.DeleteProduct(id);
            if (!result)
            {
                return BadRequest(errorBuilder
                    .ChangeType(ErrorTypes.InvalidObjectId)
                    .SetMessage("Unable to delete product, try again later")
                    .Build());
            }
            return Ok();
        }


        /// <summary>
        /// Approve product
        /// </summary>
        [HttpPut(ApiConstants.ProductRoutes.ApproveProduct)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(APIError<ErrorTypes>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> ApproveProduct([FromParameter("id")] int id, [FromQuery] bool approval)
        {
            ErrorBuilder<ErrorTypes> errorBuilder = new ErrorBuilder<ErrorTypes>(ErrorTypes.InvalidRequestBody);
            string userId = HttpContext.GetUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Forbid();
            }

            if (!await _productService.ProductExists(id))
            {
                return NotFound(errorBuilder
                    .ChangeType(ErrorTypes.InvalidObjectId)
                    .SetMessage("Product does not exist")
                    .Build());
            }

            var product = await _productService.ApproveProduct(id, approval);
            return Ok(product);
        }


        /// <summary>
        /// Change image to main
        /// </summary>
        [HttpPut(ApiConstants.ProductRoutes.MakeMain)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(APIError<ErrorTypes>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> MakeMain([FromParameter("id")] int id, [FromParameter("imageId")] int imageId)
        {
            ErrorBuilder<ErrorTypes> errorBuilder = new ErrorBuilder<ErrorTypes>(ErrorTypes.InvalidRequestBody);
            string userId = HttpContext.GetUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Forbid();
            }

            if (!await _productService.ProductExists(id))
            {
                return NotFound(errorBuilder
                    .ChangeType(ErrorTypes.InvalidObjectId)
                    .SetMessage("Product does not exist")
                    .Build());
            }

            bool result = await _productService.MakeMain(id, imageId);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }


        /// <summary>
        /// Delete image
        /// </summary>
        [HttpDelete(ApiConstants.ProductRoutes.DeleteImage)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(APIError<ErrorTypes>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteImage([FromParameter("id")] int id, [FromParameter("imageId")] int imageId)
        {
            ErrorBuilder<ErrorTypes> errorBuilder = new ErrorBuilder<ErrorTypes>(ErrorTypes.InvalidRequestBody);
            string userId = HttpContext.GetUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Forbid();
            }

            if (!await _productService.ProductExists(id))
            {
                return NotFound(errorBuilder
                    .ChangeType(ErrorTypes.InvalidObjectId)
                    .SetMessage("Product does not exist")
                    .Build());
            }

            if (!await _productService.ImageExists(imageId))
            {
                return NotFound(errorBuilder
                    .ChangeType(ErrorTypes.InvalidObjectId)
                    .SetMessage("Image does not exist")
                    .Build());
            }
            bool result = await _productService.DeleteImage(id, imageId);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}