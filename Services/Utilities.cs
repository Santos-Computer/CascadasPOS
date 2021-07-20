using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http.Headers;

namespace CascadasPOS.Services
{
    public static class Utilities
    {
        public static string SaveImage(IFormFile formFile)
        {
            var webRoot = Directory.GetCurrentDirectory() + "\\wwwroot\\Images\\Uploads\\";



            if (formFile == null)
            {
                return null;
            }

            var filename = ContentDispositionHeaderValue
                                    .Parse(formFile.ContentDisposition)
                                    .FileName
                                    .Trim('"');

            filename = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\Uploads\\" + filename);

            if (Directory.Exists(webRoot) == false)
            {
                Directory.CreateDirectory(webRoot);
            }

            using FileStream fs = File.Create(filename);
            formFile.CopyTo(fs);
            fs.Flush();

            return $"~/Images/Uploads/{formFile.FileName}";
        }
    }
}
