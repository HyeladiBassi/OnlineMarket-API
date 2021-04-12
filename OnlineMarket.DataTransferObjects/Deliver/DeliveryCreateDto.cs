namespace OnlineMarket.DataTransferObjects.Deliver
{
    public class DeliveryCreateDto
    {
        public string PaymentMethod { get; set; }
        public string Region { get; set; }
        public string Address { get; set; }
        public string ExtraDetails { get; set; }
        public double Price { get; set; }
        public string Type { get; set; }
    }
}