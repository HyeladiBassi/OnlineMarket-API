using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineMarket.DataAccess;
using OnlineMarket.DataTransferObjects.Product;
using OnlineMarket.Models;
using OnlineMarket.Services.Interfaces;

namespace OnlineMarket.Services.Main
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _context;

        public CategoryService(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> AddCategory(Category createdCategory)
        {
            await _context.Categories.AddAsync(createdCategory);
            return await Save();
        }

        public async Task<bool> DeleteCategory(int categoryId)
        {
            Category category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);
            _context.Categories.Remove(category);
            return await Save();
        }

        public async Task<ICollection<Category>> GetCategories()
        {
            List<Category> categories = await _context.Categories.ToListAsync();
            return categories;
        }

        public async Task<Category> GetCategoryById(int categoryId)
        {
            Category category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);
            return category;
        }

        public async Task<Category> GetCategoryByName(string name)
        {
            Category category = await _context.Categories.FirstOrDefaultAsync(x => x.Name == name);
            return category;
        }

        public async Task<Category> UpdateCategory(int categoryId, CategoryUpdateDto updateDto)
        {
            Category existingCategory = await _context.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);
            if (updateDto.Name != null)
            {
                existingCategory.Name = updateDto.Name;
            }
            if (updateDto.ParentId != 0)
            {
                existingCategory.ParentId = updateDto.ParentId;
            }
            _context.Categories.Update(existingCategory);
            await Save();
            return existingCategory;
        }

        private async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}