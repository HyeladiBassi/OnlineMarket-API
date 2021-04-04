using System;
using OnlineMarket.Helpers.ResourceParameters;

namespace OnlineMarket.DataTransferObjects.Product
{
    public class TransactionResourceParameters : ResourceParameters
    {
        public double? AmountGt { get; set; }
        public double? AmountLt { get; set; }
        public DateTime? DateAfter { get; set; }
        public DateTime? DateBefore { get; set; }
    }
}