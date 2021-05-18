using System;
using System.Collections.Generic;

namespace OnlineMarket.Models
{
    public class ProductReview
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Review { get; set; }
        public double Rating { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public SystemUser Reviewer { get; set; }
        public ICollection<Image> Images { get; set; }
        public bool IsDeleted { get; set; }
        public ProductReview()
        {
            DateCreated = DateTime.Now;
            DateUpdated = DateTime.Now;
            IsDeleted = false;
        }
    }
}