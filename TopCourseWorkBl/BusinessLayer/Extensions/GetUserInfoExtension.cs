using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace TopCourseWorkBl.BusinessLayer.Extensions
{
    public static class GetUserInfoExtension
    {
        public static long GetUserIdFromHttpContext(this HttpContext context)
            => long.Parse(context.User.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}