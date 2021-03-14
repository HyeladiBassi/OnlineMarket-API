using System.Collections.Generic;

namespace OnlineMarket.API.Models
{
    public class WishList
    {
        public int Id { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}