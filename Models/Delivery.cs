namespace OnlineMarket.API.Models
{
    public class Delivery
    {
        public int Id { get; set; }
        public string Region { get; set; }
        public string Address { get; set; }
        public string ExtraDetails { get; set; }
        public string Currency { get; set; }
        public int Price { get; set; }
        public string Type { get; set; }
    }
}