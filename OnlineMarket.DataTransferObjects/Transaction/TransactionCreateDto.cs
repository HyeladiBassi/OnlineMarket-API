using System.Collections.Generic;
using OnlineMarket.DataTransferObjects.Deliver;
using OnlineMarket.DataTransferObjects.Product;

namespace OnlineMarket.DataTransferObjects.Transaction
{
    public class TransactionCreateDto
    {
        public string Currency { get; set; }
        public string Status { get; set; }
        public IEnumerable<OrderCreateDto> Orders { get; set; }
        public DeliveryCreateDto Delivery { get; set; }
    }
}