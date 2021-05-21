using System;

namespace OnlineMarket.Models
{
    public class Order
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public Order()
        {
            CreatedAt = DateTime.Now;
        }
    }
}