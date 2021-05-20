using System.Collections.Generic;
using OnlineMarket.DataTransferObjects.Deliver;
using OnlineMarket.DataTransferObjects.Product;

namespace OnlineMarket.DataTransferObjects.Transaction
{
    public class TransactionCreateDto
    {
        public string Description { get; set; }
        public string Status { get; set; }
        public IEnumerable<Purchases> Products { get; set; }
        public DeliveryCreateDto Delivery { get; set; }
    }

    public class Purchases
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
    }
}