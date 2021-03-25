namespace OnlineMarket.DataTransferObjects.Product
{
    // ! Add collection for images

    public class ProductCreateDto
    {
        public string Name { get; set; }
        public string Currency { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string PaymentMethod { get; set; }
        public string SellerId { get; set; }
    }
}