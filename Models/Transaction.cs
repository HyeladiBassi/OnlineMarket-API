using System.Collections.Generic;

namespace OnlineMarket.API.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Currency { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        // Two methods card(stripe) and payment on delivery
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        public ICollection<Product> Products { get; set; }
        public SystemUser Buyer { get; set; }
        public Delivery Delivery { get; set; }
    }
}