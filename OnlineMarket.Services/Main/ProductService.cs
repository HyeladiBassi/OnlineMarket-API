using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineMarket.DataAccess;
using OnlineMarket.DataTransferObjects.Product;
using OnlineMarket.DataTransferObjects.ProductReview;
using OnlineMarket.Helpers;
using OnlineMarket.Helpers.FileHelper;
using OnlineMarket.Models;
using OnlineMarket.Services.Extensions;
using OnlineMarket.Services.Interfaces;

namespace OnlineMarket.Services.Main
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;
        private readonly IFileHelper _fileHelper;
        private readonly UserManager<SystemUser> _userManager;

        public ProductService(DataContext context, IFileHelper fileHelper, UserManager<SystemUser> userManager)
        {
            _context = context;
            _fileHelper = fileHelper;
            _userManager = userManager;
        }

        public async Task<bool> CreateProduct(Product createdProduct)
        {

            await _context.Products.AddAsync(createdProduct);

            return await Save();
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            Product product = await _context.Products
                .FirstOrDefaultAsync(x => x.Id == productId);

            product.IsDeleted = true;
            _context.Update(product);

            return await Save();
        }

        public async Task<PagedList<Product>> GetPagedProductList(ProductResourceParameters resourceParameters)
        {
            PagedList<Product> products = await _context.Products
                .Include(x => x.Seller)
                .Include(x => x.Images)
                .Include(x => x.Category)
                .AsSingleQuery()
                .Where(x => x.Status == "approved")
                .Where(x => !x.IsDeleted)
                .Where(x => x.Name.ToLower().Contains(resourceParameters.searchQuery))
                .WhereGtEq(x => x.Price, resourceParameters.priceGt)
                .WhereLtEq(x => x.Price, resourceParameters.priceLt)
                .WhereGtEq(x => x.Stock, resourceParameters.stockGt)
                .WhereLtEq(x => x.Stock, resourceParameters.stockLt)
                .ToPagedListAsync(resourceParameters.pageNumber, resourceParameters.pageSize);

            return products;
        }

        public async Task<PagedList<Product>> GetPagedProductListFromRegion(ProductResourceParameters resourceParameters)
        {
            PagedList<Product> products = await _context.Products
                .Include(x => x.Seller)
                .Include(x => x.Category)
                .Include(x => x.Images)
                .AsSingleQuery()
                .Where(x => x.Status == "approved")
                .Where(x => !x.IsDeleted)
                .Where(x => x.WarehouseLocation == resourceParameters.region)
                .Where(x => x.Name.ToLower().Contains(resourceParameters.searchQuery))
                .WhereGtEq(x => x.Price, resourceParameters.priceGt)
                .WhereLtEq(x => x.Price, resourceParameters.priceLt)
                .WhereGtEq(x => x.Stock, resourceParameters.stockGt)
                .WhereLtEq(x => x.Stock, resourceParameters.stockLt)
                .ToPagedListAsync(resourceParameters.pageNumber, resourceParameters.pageSize);
            return products;
        }

        public async Task<Product> GetProductById(int productId)
        {
            Product product = await _context.Products
                .Include(x => x.Seller)
                .Include(x => x.Reviews)
                .Include(x => x.Images)
                .Include(x => x.Category)
                .AsSingleQuery()
                .Where(x => !x.IsDeleted)
                .Where(x => x.Id == productId)
                .FirstOrDefaultAsync();

            return product;
        }

        public async Task<IEnumerable<Product>> GetProductList(ProductResourceParameters resourceParameters)
        {
            List<Product> productList = await _context.Products
                .Include(x => x.Seller)
                .Include(x => x.Images)
                .Include(x => x.Category)
                .AsSingleQuery()
                .Where(x => x.Status == "approved")
                .Where(x => x.Name.ToLower().Contains(resourceParameters.searchQuery))
                .ToListAsync();

            return productList;
        }

        public async Task<PagedList<Product>> GetRejectedProductList(ProductResourceParameters resourceParameters)
        {
            PagedList<Product> productList = await _context.Products
                .Include(x => x.Seller)
                .Include(x => x.Images)
                .Include(x => x.Category)
                .AsSingleQuery()
                .Where(x => !x.IsDeleted)
                .Where(x => x.Status == "rejected")
                .ToPagedListAsync(resourceParameters.pageNumber, resourceParameters.pageSize);

            return productList;
        }

        public async Task<PagedList<Product>> GetUnapprovedProductList(ProductResourceParameters resourceParameters)
        {
            PagedList<Product> productList = await _context.Products
                .Include(x => x.Seller)
                .Include(x => x.Images)
                .Include(x => x.Category)
                .AsSingleQuery()
                .Where(x => !x.IsDeleted)
                .Where(x => x.Status == "pending")
                .ToPagedListAsync(resourceParameters.pageNumber, resourceParameters.pageSize);

            return productList;
        }

        public async Task<PagedList<Product>> GetPagedProductListByUserId(string userId, ProductResourceParameters resourceParameters)
        {
            PagedList<Product> productList = await _context.Products
                .Include(x => x.Images)
                .Include(x => x.Category)
                .AsSingleQuery()
                .Where(x => x.Seller.Id == userId)
                .Where(x => !x.IsDeleted)
                .Where(x => x.Name.ToLower().Contains(resourceParameters.searchQuery))
                .WhereGtEq(x => x.Price, resourceParameters.priceGt)
                .WhereLtEq(x => x.Price, resourceParameters.priceLt)
                .WhereGtEq(x => x.Stock, resourceParameters.stockGt)
                .WhereLtEq(x => x.Stock, resourceParameters.stockLt)
                .ToPagedListAsync(resourceParameters.pageNumber, resourceParameters.pageSize);

            return productList;
        }

        public async Task<IEnumerable<Product>> GetProductListByUserId(string userId)
        {
            List<Product> productList = await _context.Products
                .AsQueryable()
                .Where(x => x.Seller.Id == userId)
                .ToListAsync();

            return productList;
        }

        public async Task<Product> UpdateProduct(int productId, ProductUpdateDto updatedProduct)
        {
            Product product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);

            if (updatedProduct.Category != null)
            {
                Category category = await _context.Categories.FirstOrDefaultAsync(x => x.Name == updatedProduct.Category);
                product.Category = category;
            }

            if (updatedProduct.Description != null)
            {
                product.Description = updatedProduct.Description;
            }

            if (updatedProduct.Name != null)
            {
                product.Name = updatedProduct.Name;
            }

            if (updatedProduct.Price != null)
            {
                product.Price = (double)updatedProduct.Price;
            }

            if (updatedProduct.Stock != null)
            {
                product.Stock = (int)updatedProduct.Stock;
            }

            if (updatedProduct.WarehouseLocation != null)
            {
                product.WarehouseLocation = updatedProduct.WarehouseLocation;
            }

            if (updatedProduct.imageFiles != null && updatedProduct.imageFiles.Count() > 0)
            {
                SaveFileResult[] mediaFiles = await Task.WhenAll(updatedProduct.imageFiles.Select(x => _fileHelper.SaveFile(x)));
                List<Image> images = mediaFiles.Select(x => new Image() { Link = $"/{x.fileName.Replace('\\', '/')}", Type = x.type, MimeType = x.mimeType }).ToList();
                product.Images = images;
            }

            _context.Products.Update(product);

            if (await Save())
            {
                return product;
            }
            return null;
        }

        public async Task<bool> BuyProduct(int productId, int quantity)
        {
            Product product = await _context.Products
                .Where(x => !x.IsDeleted)
                .FirstOrDefaultAsync(x => x.Id == productId);

            product.Stock = product.Stock - quantity;

            _context.Products.Update(product);
            return await Save();
        }

        public async Task<bool> CheckAvailability(int productId, int quantity)
        {
            Product product = await _context.Products
                .AsNoTracking().
                FirstOrDefaultAsync(x => x.Id == productId);
            if (product.Stock > quantity)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> ProductExists(int productId)
        {
            Product product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
            if (product != null && product.IsDeleted == false)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> ImageExists(int imageId)
        {
            Image image = await _context.Images.FirstOrDefaultAsync(x => x.Id == imageId);
            if (image != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteImage(int productId, int imageId)
        {
            Product product = await _context.Products.Include(x => x.Images).FirstOrDefaultAsync(x => x.Id == productId);
            Image imageToDelete = product.Images.FirstOrDefault(x => x.Id == imageId);
            _context.Images.Remove(imageToDelete);
            return await Save();
        }

        public async Task<bool> MakeMain(int productId, int imageId)
        {
            Product product = await _context.Products.Include(x => x.Images).FirstOrDefaultAsync(x => x.Id == productId);
            foreach (Image image in product.Images)
            {
                if (image.Id == imageId)
                {
                    image.IsMain = true;
                }
                else
                {
                    image.IsMain = false;
                }
            }
            _context.Products.Update(product);
            return await Save();
        }

        public async Task<bool> ApproveProduct(int productId, bool approval, string userId)
        {
            Product product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
            if (approval)
            {
                product.Status = "approved";
            }
            else
            {
                product.Status = "rejected";
            }
            SystemUser moderator = await _userManager.FindByIdAsync(userId);
            product.ModeratedBy = moderator;
            _context.Products.Update(product);
            return await Save();
        }
        private async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}