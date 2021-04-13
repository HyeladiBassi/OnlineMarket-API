using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineMarket.DataTransferObjects.ProductReview;
using OnlineMarket.Models;

namespace OnlineMarket.Services.Interfaces
{
    public interface IProductReviewService
    {
         Task<IEnumerable<ProductReview>> GetProductReviewsById(int productId);
         Task<IEnumerable<ProductReview>> GetProductReviewsByUserId(string userId);
         Task<ProductReview> GetProductReview(int reviewId);
         Task<bool> AddProductReview(int productId, ProductReview review);
         Task<bool> UpdateProductReview(int reviewId,ProductReviewUpdateDto review);
         Task<bool> DeleteProductReview(int productId, int reveiwId);
        //  Task<bool> IsBuyer(int productId, int userId);
    }
}