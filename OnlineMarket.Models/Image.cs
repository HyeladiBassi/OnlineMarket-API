namespace OnlineMarket.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string MimeType { get; set; }
        public string Link { get; set; }
        public bool IsMain { get; set; }
        public Image()
        {
            IsMain = false;
        }
    }
}