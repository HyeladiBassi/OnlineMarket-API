using System.Collections.Generic;
using OnlineMarket.DataTransferObjects.Deliver;
using OnlineMarket.DataTransferObjects.Product;

namespace OnlineMarket.DataTransferObjects.Transaction
{
    public class TransactionUpdateDto
    {
        public string Description { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        public ICollection<ProductUpdateDto> Products { get; set; }
        public DeliveryUpdateDto Delivery { get; set; }
    }
}