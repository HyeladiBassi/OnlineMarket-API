using System.Collections.Generic;

namespace OnlineMarket.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Currency { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public bool IsApproved { get; set; }
        public string Category { get; set; }
        public ICollection<Image> Images { get; set; }
        public string PaymentMethod { get; set; }
        public SystemUser Seller { get; set; }
        // public Delivery Delivery { get; set; }
        public ICollection<ProductReview> Reviews { get; set; }

    }
}