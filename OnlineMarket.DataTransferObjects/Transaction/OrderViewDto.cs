using OnlineMarket.DataTransferObjects.Product;

namespace OnlineMarket.DataTransferObjects.Transaction
{
    public class OrderViewDto
    {
        public ProductViewDto Product { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}