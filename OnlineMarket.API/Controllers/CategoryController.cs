using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineMarket.Contracts;
using OnlineMarket.DataTransferObjects.Product;
using OnlineMarket.Models;
using OnlineMarket.Services.Interfaces;
using OnlineMarket.Services.Main;

namespace OnlineMarket.API.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        ///<summary>
        /// Get list of categories
        ///</summary>
        [HttpGet(ApiConstants.CategoryRoutes.GetCategories)]
        [ProducesResponseType(typeof(ICollection<Category>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategories()
        {
            ICollection<Category> categories = await _categoryService.GetCategories();
            return Ok(categories);
        }

        ///<summary>
        /// Add new category
        ///</summary>
        [HttpPost(ApiConstants.CategoryRoutes.CreateCategory)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateCategory(CategoryCreateDto createDto)
        {
            Category newCategory = new Category {
                Name = createDto.Name,
                ParentId = createDto.ParentId
            };
            bool result = await _categoryService.AddCategory(newCategory);
            return Ok(result);
        }

        ///<summary>
        /// Update existing category
        ///</summary>
        [HttpPut(ApiConstants.CategoryRoutes.UpdateCategory)]
        [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCategory([FromParameter("{categoryId}")] int categoryId, CategoryUpdateDto updateDto)
        {
            Category category = await _categoryService.UpdateCategory(categoryId,updateDto);
            return Ok(category);
        }

        ///<summary>
        /// Delete existing category
        ///</summary>
        [HttpDelete(ApiConstants.CategoryRoutes.DeleteCategory)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteCategory([FromParameter("{categoryId}")] int categoryId)
        {
            bool result = await _categoryService.DeleteCategory(categoryId);
            return Ok(result);
        }
    }
}