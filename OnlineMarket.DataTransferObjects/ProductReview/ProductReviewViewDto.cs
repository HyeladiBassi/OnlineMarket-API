using System;
using System.Collections.Generic;
using OnlineMarket.DataTransferObjects.Media;
using OnlineMarket.DataTransferObjects.Product;
using OnlineMarket.DataTransferObjects.SystemUser;

namespace OnlineMarket.DataTransferObjects.ProductReview
{
    public class ProductReviewViewDto
    {
        public int Id { get; set; }
        public string Review { get; set; }
        public int Rating { get; set; }
        public string ReviewerName { get; set; }
        public DateTime DateCreated { get; set; }
        public IEnumerable<MediaViewDto> Images { get; set; }
    }
}