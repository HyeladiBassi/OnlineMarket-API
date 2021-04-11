using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OnlineMarket.DataTransferObjects;
using OnlineMarket.Helpers;
using OnlineMarket.Helpers.ResourceParameters;

namespace OnlineMarket.API.Extensions
{
    public static class PagedListExtensions
    {
        public static PagingDto ExtractPaging<T>(this PagedList<T> source)
        {
            return new PagingDto
            {
                pageNumber = source.CurrentPage,
                pageSize = source.PageSize,
                totalCount = source.TotalCount,
                totalPages = source.TotalPages
            };
            
        }
        
    }
}