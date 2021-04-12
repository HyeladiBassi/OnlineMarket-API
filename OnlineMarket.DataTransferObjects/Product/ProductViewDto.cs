using System.Collections.Generic;
using OnlineMarket.DataTransferObjects.ProductReview;
using OnlineMarket.DataTransferObjects.SystemUser;

namespace OnlineMarket.DataTransferObjects.Product
{
    // ! Add collection for images

    public class ProductViewDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Currency { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string PaymentMethod { get; set; }
        public SystemUserViewDto Seller { get; set; }
    }
}