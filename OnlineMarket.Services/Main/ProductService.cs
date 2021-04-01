using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineMarket.DataTransferObjects.Product;
using OnlineMarket.Helpers;
using OnlineMarket.Helpers.ResourceParameters;
using OnlineMarket.Models;
using OnlineMarket.Services.Interfaces;

namespace OnlineMarket.Services.Main
{
    public class ProductService : IProductService
    {
        public async Task<Product> DeleteProduct(string productId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<PagedList<Product>> GetPagedProductList(ResourceParameters resourceParameters)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Product> GetProductById(string productId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ICollection<Product>> GetProductList(ResourceParameters resourceParameters)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ICollection<Product>> GetProductListByUserId(string userId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Product> UpdateProduct(string productId, ProductUpdateDto updatedProduct)
        {
            throw new System.NotImplementedException();
        }
    }
}