using System;
using System.Collections.Generic;

namespace OnlineMarket.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
        public ICollection<Order> Orders { get; set; }
        public int TotalQuantity { get; set; }
        public double TotalPrice { get; set; }
        public SystemUser Buyer { get; set; }
        public Delivery Delivery { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Transaction()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}