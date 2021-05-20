using Microsoft.AspNetCore.Http;

namespace OnlineMarket.DataTransferObjects.Product
{
    // ! Add collection for images
    
    public class ProductUpdateDto
    {
        public string Name { get; set; }
        public string Currency { get; set; }
        public double? Price { get; set; }
        public int? Stock { get; set; }
        public string Description { get; set; }
        public string WarehouseLocation { get; set; }
        public string Category { get; set; }
        public IFormFileCollection imageFiles { get; set; }
    }
}