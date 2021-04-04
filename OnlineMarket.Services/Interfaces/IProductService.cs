using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineMarket.DataTransferObjects.Product;
using OnlineMarket.Helpers;
using OnlineMarket.Models;

namespace OnlineMarket.Services.Interfaces
{
    public interface IProductService
    {
        Task<PagedList<Product>> GetPagedProductList(ProductResourceParameters resourceParameters);
        Task<IEnumerable<Product>> GetProductList(ProductResourceParameters resourceParameters);
        Task<PagedList<Product>> GetPagedProductListByUserId(string userId, ProductResourceParameters resourceParameters);
        Task<IEnumerable<Product>> GetProductListByUserId(string userId);
        Task<Product> GetProductById(int productId);
        Task<bool> CreateProduct(Product createdProduct);
        Task<Product> UpdateProduct(int productId, ProductUpdateDto updatedProduct);
        Task<bool> DeleteProduct(int productId);
        Task<bool> BuyProduct(int productId, int quantity);
        Task<bool> CheckAvailability(int productId, int quantity);
        Task<bool> ProductExists(int productId);
    }
}