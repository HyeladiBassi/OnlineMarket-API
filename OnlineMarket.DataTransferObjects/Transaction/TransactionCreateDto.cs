using System.Collections.Generic;
using OnlineMarket.DataTransferObjects.Deliver;
using OnlineMarket.DataTransferObjects.Product;

namespace OnlineMarket.DataTransferObjects.Transaction
{
    public class TransactionCreateDto
    {
        public string Description { get; set; }
        public string Status { get; set; }
        public IEnumerable<ProductCreateDto> Products { get; set; }
        public DeliveryCreateDto Delivery { get; set; }
    }

    public class Purchases
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Currency { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string PaymentMethod { get; set; }
    }
}