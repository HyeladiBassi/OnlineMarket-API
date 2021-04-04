using OnlineMarket.DataTransferObjects.Product;
using OnlineMarket.DataTransferObjects.SystemUser;

namespace OnlineMarket.DataTransferObjects.ProductReview
{
    public class ProductReviewViewDto
    {
        public int Id { get; set; }
        public string Review { get; set; }
        public int Rating { get; set; }
        public SystemUserViewDto Reviewer { get; set; }
    }
}