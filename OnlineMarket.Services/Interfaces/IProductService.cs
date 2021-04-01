using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineMarket.DataTransferObjects.Product;
using OnlineMarket.Helpers;
using OnlineMarket.Helpers.ResourceParameters;
using OnlineMarket.Models;

namespace OnlineMarket.Services.Interfaces
{
    public interface IProductService
    {
         Task<PagedList<Product>> GetPagedProductList(ResourceParameters resourceParameters);
         Task<ICollection<Product>> GetProductList(ResourceParameters resourceParameters);
         Task<ICollection<Product>> GetProductListByUserId(string userId);
         Task<Product> GetProductById(string productId);
         Task<Product> UpdateProduct(string productId, ProductUpdateDto updatedProduct);
         Task<Product> DeleteProduct(string productId);
    }
}