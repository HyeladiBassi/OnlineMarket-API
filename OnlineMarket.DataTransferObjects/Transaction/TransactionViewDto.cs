using System;
using System.Collections.Generic;
using OnlineMarket.DataTransferObjects.Deliver;
using OnlineMarket.DataTransferObjects.Product;
using OnlineMarket.DataTransferObjects.SystemUser;

namespace OnlineMarket.DataTransferObjects.Transaction
{
    public class TransactionViewDto
    {
        public int Id { get; set; }
        public string Currency { get; set; }
        public int TotalQuantity { get; set; }
        public double TotalPrice { get; set; }
        public string Status { get; set; }
        public ICollection<OrderViewDto> Orders { get; set; }
        public SystemUserViewDto Buyer { get; set; }
        public DeliveryViewDto Delivery { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}