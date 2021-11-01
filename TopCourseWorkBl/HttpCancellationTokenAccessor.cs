using System.Threading;
using Microsoft.AspNetCore.Http;

namespace TopCourseWorkBl
{
    public class HttpCancellationTokenAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpCancellationTokenAccessor(IHttpContextAccessor httpContextAccessor)
            => _httpContextAccessor = httpContextAccessor;

        public CancellationToken Token
            => _httpContextAccessor.HttpContext?.RequestAborted ?? CancellationToken.None;
    }
}