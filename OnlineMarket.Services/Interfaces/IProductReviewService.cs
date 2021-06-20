using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineMarket.DataTransferObjects.ProductReview;
using OnlineMarket.Models;

namespace OnlineMarket.Services.Interfaces
{
    public interface IProductReviewService
    {
        Task<IEnumerable<ProductReview>> GetProductReviewsByProductId(int productId);
        Task<IEnumerable<ProductReview>> GetProductReviewsByUserId(string userId);
        Task<IEnumerable<ProductReview>> GetProductReviews();
        Task<ProductReview> GetProductReviewById(int reviewId);
        Task<bool> CanReview(string userId, int productId);
        Task<bool> AddProductReview(int productId, ProductReview review);
        Task<bool> UpdateProductReview(int reviewId, ProductReviewUpdateDto review);
        Task<bool> DeleteProductReview(int reveiwId);
    }
}