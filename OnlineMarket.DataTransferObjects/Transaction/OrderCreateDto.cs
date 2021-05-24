namespace OnlineMarket.DataTransferObjects.Transaction
{
    public class OrderCreateDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}