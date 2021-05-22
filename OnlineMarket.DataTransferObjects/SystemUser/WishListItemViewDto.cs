using System.Collections.Generic;
using OnlineMarket.DataTransferObjects.Media;

namespace OnlineMarket.DataTransferObjects.SystemUser
{
    public class WishListItemViewDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Currency { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
        public string WarehouseLocation { get; set; }
        public double AverageRating { get; set; }
        public ICollection<MediaViewDto> Images { get; set; }
    }
}