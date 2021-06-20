using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Currency { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public string WarehouseLocation { get; set; }
        public string  Status { get; set; }
        public Category Category { get; set; }
        public ICollection<Image> Images { get; set; }
        public SystemUser Seller { get; set; }
        public SystemUser ModeratedBy { get; set; }
        public ICollection<ProductReview> Reviews { get; set; }
        public DateTime ModeratedAt { get; set; }
        public double AverageRating { get; set; }
        public bool IsDeleted { get; set; }
        public Product()
        {
            IsDeleted = false;
            Status = "pending";
        }

    }
}