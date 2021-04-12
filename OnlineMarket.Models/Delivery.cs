namespace OnlineMarket.Models
{
    public class Delivery
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string Address { get; set; }
        public string ExtraDetails { get; set; }
        public string PaymentMethod { get; set; }
        public double Price { get; set; }
        public string Type { get; set; }
    }
}