using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OnlineMarket.DataTransferObjects.Product;
using OnlineMarket.Models;

namespace OnlineMarket.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ICollection<Category>> GetCategories();
        Task<Category> GetCategoryById(int categoryId);
        Task<Category> GetCategoryByName(string name);
        Task<Category> UpdateCategory(int categoryId, CategoryUpdateDto updateDto);
        Task<bool> AddCategory(Category createdCategory);
        Task<bool> DeleteCategory(int categoryId);
    }
}