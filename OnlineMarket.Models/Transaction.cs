using System;
using System.Collections.Generic;

namespace OnlineMarket.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Currency { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        public ICollection<Product> Products { get; set; }
        public SystemUser Buyer { get; set; }
        public Delivery Delivery { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Transaction()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}