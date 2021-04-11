using System.Collections.Generic;

namespace OnlineMarket.DataTransferObjects
{
    public class Paginate<T>
    {
        public IEnumerable<T> items { get; set; }
        public PagingDto pagingInfo { get; set; }
    }
}