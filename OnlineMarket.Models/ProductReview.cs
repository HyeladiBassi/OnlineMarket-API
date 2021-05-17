using System;

namespace OnlineMarket.Models
{
    public class ProductReview
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Review { get; set; }
        public int Rating { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public SystemUser Reviewer { get; set; }
        public bool IsDeleted { get; set; }
        public ProductReview()
        {
            DateCreated = DateTime.Now;
            DateUpdated = DateTime.Now;
            IsDeleted = false;
        }
    }
}