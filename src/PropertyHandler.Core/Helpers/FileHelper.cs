using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PropertyHandler.Core.Helpers
{
    public static class FileHelper
    {
        public static async Task<bool> CreateFile(IFormFile file, string id)
        {
            string fileExtension = GetFileExtension(file.ContentType);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appdata/arquivos", $"{id}.{fileExtension}");

            try
            {
                using var stream = File.Create(path);
                await file.CopyToAsync(stream);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public static string GetFileExtension(string fileType)
        {
            var indexSub = fileType.IndexOf("/") + 1;
            var fileExtension = fileType.Substring(indexSub);
            return fileExtension;
        }
    }
}
