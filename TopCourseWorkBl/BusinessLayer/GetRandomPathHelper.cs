using System;
using System.IO;

namespace TopCourseWorkBl.BusinessLayer
{
    public static class GetRandomPathHelper
    {
        public static string GetRandomPath()
        {
            var random = new Random();
            var tempFilename = random.Next(909090909);
            return Directory.GetCurrentDirectory() + "\\" + tempFilename;
        }
    }
}