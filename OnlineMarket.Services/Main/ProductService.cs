using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineMarket.DataAccess;
using OnlineMarket.DataTransferObjects.Product;
using OnlineMarket.DataTransferObjects.ProductReview;
using OnlineMarket.Helpers;
using OnlineMarket.Models;
using OnlineMarket.Services.Extensions;
using OnlineMarket.Services.Interfaces;

namespace OnlineMarket.Services.Main
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
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
                .Where(x => x.Status == resourceParameters.status)
                .Where(x => !x.IsDeleted)
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
                .AsNoTracking()
                .Include(x => x.Seller)
                .Include(x => x.Reviews)
                .Include(x => x.Images)
                .Where(x => !x.IsDeleted)
                .Where(x => x.Id == productId)
                .FirstOrDefaultAsync();

            return product;
        }

        public async Task<IEnumerable<Product>> GetProductList(ProductResourceParameters resourceParameters)
        {
            List<Product> productList = await _context.Products
                .AsQueryable()
                .Include(x => x.Seller)
                .Where(x => x.Status == "approved")
                .ToListAsync();

            return productList;
        }

        public async Task<IEnumerable<Product>> GetRejectedProductList(ProductResourceParameters resourceParameters)
        {
            List<Product> productList = await _context.Products
                .AsQueryable()
                .Include(x => x.Seller)
                .Where(x => x.Status == "rejected")
                .ToListAsync();

            return productList;
        }

        public async Task<IEnumerable<Product>> GetUnapprovedProductList(ProductResourceParameters resourceParameters)
        {
            List<Product> productList = await _context.Products
                .AsQueryable()
                .Include(x => x.Seller)
                .Where(x => x.Status == "pending")
                .ToListAsync();

            return productList;
        }

        public async Task<PagedList<Product>> GetPagedProductListByUserId(string userId, ProductResourceParameters resourceParameters)
        {
            PagedList<Product> productList = await _context.Products
                .AsQueryable()
                .Where(x => x.Seller.Id == userId)
                .Where(x => !x.IsDeleted)
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
                product.Category = updatedProduct.Category;
            }

            if (updatedProduct.Description != null)
            {
                product.Description = updatedProduct.Description;
            }

            if (updatedProduct.Name != null)
            {
                product.Name = updatedProduct.Name;
            }

            if (updatedProduct.PaymentMethod != null)
            {
                product.PaymentMethod = updatedProduct.PaymentMethod;
            }

            if (updatedProduct.Price != product.Price)
            {
                product.Price = updatedProduct.Price;
            }

            if (updatedProduct.Stock != product.Stock)
            {
                product.Stock = updatedProduct.Stock;
            }

            _context.Products.Update(product);

            if (await Save())
            {
                return product;
            }
            return null;
        }

        private async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
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
            if (product != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> ApproveProduct(int productId, bool approval)
        {
            Product product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
            if (approval)
            {
                product.Status = "approved";
            } else {
                product.Status  = "rejected";
            }
            _context.Products.Update(product);
            return await Save();
        }
    }
}