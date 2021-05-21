using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public class WishListItem
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public string UserId { get; set; }
    }
}