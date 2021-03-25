namespace OnlineMarket.Helpers.ResourceParameters
{
    public class ResourceParameters
    {
        private const int maxPageSize = 100;
        public int pageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int pageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
        public string searchQuery { get; set; }

    }
}