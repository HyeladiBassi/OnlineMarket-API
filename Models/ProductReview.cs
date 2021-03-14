namespace OnlineMarket.API.Models
{
    public class ProductReview
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public string Review { get; set; }
        public int Rating { get; set; }
        public SystemUser Reviewer { get; set; }
    }
}