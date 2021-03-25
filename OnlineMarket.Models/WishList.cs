using System.Collections.Generic;

namespace OnlineMarket.Models
{
    public class WishList
    {
        public int Id { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}