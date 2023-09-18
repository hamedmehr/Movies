using Microsoft.AspNetCore.Http;

namespace Helpers
{
    public static class FileHelper
    {
        public static string SaveFile(this IFormFile file, string url)
        {
            if (file.Length > 0)
            {
                string filePath = Path.Combine(url, file.FileName);

                if (!Directory.Exists(url))
                    Directory.CreateDirectory(url);

                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                return filePath;
            }
            return string.Empty;
        }
    }
}