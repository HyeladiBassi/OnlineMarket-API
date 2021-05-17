using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OnlineMarket.Helpers.FileHelper
{
    public enum SaveFileResultTypes
    {
        Saved,
        FailedToParseExtension,
    }

    public class SaveFileResult
    {
        public SaveFileResultTypes result { get; set; }
        public string fileName { get; set; }
        public string mimeType { get; set; }
        public string type { get; set; }
        public string message { get; set; }
    }

    public class FileHelper : IFileHelper
    {
        public async Task<SaveFileResult> SaveFile(IFormFile formFile, string fileName = null, string savePath = "imgs")
        {
            string extension = null;
            string mimeType = MimeMapping.MimeUtility.GetMimeMapping(formFile.FileName);
            try
            {
                extension = Path.GetExtension(formFile.FileName);
            }
            catch (ArgumentException exception)
            {
                return new SaveFileResult { result = SaveFileResultTypes.FailedToParseExtension, message = exception.Message };
            }
            if (fileName == null)
            {
                fileName = Guid.NewGuid().ToString() + extension;
            }
            else
            {
                fileName += extension;
            }

            if (!Directory.Exists("wwwroot"))
            {
                Directory.CreateDirectory("wwwroot");
            }

            if (!Directory.Exists(Path.Combine("wwwroot", savePath)))
            {
                Directory.CreateDirectory(Path.Combine("wwwroot", savePath));
            }

            if (formFile.Length > 0)
            {
                using (FileStream stream = new FileStream(Path.Combine("wwwroot", savePath, fileName), FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
            }

            return new SaveFileResult
            {
                result = SaveFileResultTypes.Saved,
                fileName = Path.Combine(savePath, fileName),
                mimeType = mimeType,
                type = mimeType.Split("/")[0]
            };
        }
    }
}