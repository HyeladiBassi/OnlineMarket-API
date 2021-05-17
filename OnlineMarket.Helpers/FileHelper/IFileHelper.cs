using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OnlineMarket.Helpers.FileHelper
{
    public interface IFileHelper
    {
        Task<SaveFileResult> SaveFile(IFormFile formFile, string fileName = null, string savePath = "imgs");
         
    }
}