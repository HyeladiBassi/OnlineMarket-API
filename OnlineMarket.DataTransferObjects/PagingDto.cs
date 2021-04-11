namespace OnlineMarket.DataTransferObjects
{
    public class PagingDto
    {
        public int totalCount { get; set; }
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
        public int totalPages { get; set; }

    }
}