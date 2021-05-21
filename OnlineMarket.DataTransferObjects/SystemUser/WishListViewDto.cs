using System.Collections.Generic;
using OnlineMarket.DataTransferObjects.Product;

namespace OnlineMarket.DataTransferObjects.SystemUser
{
    public class WishListViewDto
    {
        public int Id { get; set; }
        public WishListItemViewDto Product { get; set; }
        public string UserId { get; set; }
    }
}