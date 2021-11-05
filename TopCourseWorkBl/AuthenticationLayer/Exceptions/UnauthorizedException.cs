using System;

namespace TopCourseWorkBl.AuthenticationLayer.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base(message)
        {
        }
    }
}