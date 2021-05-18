using Microsoft.AspNetCore.Http;

namespace OnlineMarket.DataTransferObjects.ProductReview
{
    public class ProductReviewCreateDto
    {
        public string Review { get; set; }
        public int Rating { get; set; }
        public IFormFileCollection imageFiles { get; set; }
    }
}