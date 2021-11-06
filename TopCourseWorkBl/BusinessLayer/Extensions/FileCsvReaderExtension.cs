using System.IO;
using Microsoft.AspNetCore.Http;

namespace TopCourseWorkBl.BusinessLayer.Extensions
{
    public static class FileCsvReaderExtension
    {
        public static void CreateTempFile(this IFormFile file, string path)
        {
            using var fileStream = File.Create(path);
            file.CopyTo(fileStream);
            fileStream.Flush();
            fileStream.Dispose();
        }
    }
}