using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineMarket.DataAccess;
using OnlineMarket.DataTransferObjects.ProductReview;
using OnlineMarket.Models;
using OnlineMarket.Services.Interfaces;

namespace OnlineMarket.Services.Main
{
    public class ProductReviewService : IProductReviewService
    {
        private readonly DataContext _context;

        public ProductReviewService(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> AddProductReview(int productId, ProductReview review)
        {
            review.ProductId = productId;
            await _context.Reviews.AddAsync(review);
            return await Save();
        }

        public Task<bool> DeleteProductReview(int productId, int reveiwId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ProductReview> GetProductReview(int reviewId)
        {
            ProductReview review = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);
            return review;
        }

        public async Task<IEnumerable<ProductReview>> GetProductReviewsById(int productId)
        {
            IEnumerable<ProductReview> reviews = await _context.Reviews.Where(x => x.ProductId == productId).ToListAsync();
            return reviews;
        }

        public async Task<IEnumerable<ProductReview>> GetProductReviewsByUserId(string userId)
        {
            IEnumerable<ProductReview> reviews = await _context.Reviews.Where(x => x.Reviewer.Id == userId).ToListAsync();
            return reviews;
        }

        public async Task<bool> UpdateProductReview(int reviewId, ProductReviewUpdateDto updatedReview)
        {
            ProductReview existingReview = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);
            if (updatedReview.Review != existingReview.Review )
            {
                existingReview.Review = updatedReview.Review;
            }

            if (updatedReview.Rating != existingReview.Rating)
            {
                existingReview.Rating = updatedReview.Rating;
            }

            _context.Reviews.Update(existingReview);
            return await Save();
        }

        private async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}