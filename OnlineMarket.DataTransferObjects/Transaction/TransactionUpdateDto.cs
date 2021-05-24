using System.Collections.Generic;
using OnlineMarket.DataTransferObjects.Deliver;
using OnlineMarket.DataTransferObjects.Product;

namespace OnlineMarket.DataTransferObjects.Transaction
{
    public class TransactionUpdateDto
    {
        public string Currency { get; set; }
        public string Status { get; set; }
        public IEnumerable<OrderCreateDto> Orders { get; set; }
        public DeliveryUpdateDto Delivery { get; set; }
    }
}