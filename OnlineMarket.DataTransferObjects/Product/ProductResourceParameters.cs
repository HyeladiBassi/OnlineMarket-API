using OnlineMarket.Helpers.ResourceParameters;

namespace OnlineMarket.DataTransferObjects.Product
{
    public class ProductResourceParameters : ResourceParameters
    {
        public string region { get; set; }
        public string category { get; set; } = "";
        public string status { get; set; }
        public int? stockGt { get; set; }
        public int? stockLt { get; set; }
        public double? priceGt { get; set; }
        public double? priceLt { get; set; }
    }
}