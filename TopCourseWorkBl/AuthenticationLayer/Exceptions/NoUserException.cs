using System;

namespace TopCourseWorkBl.AuthenticationLayer.Exceptions
{
    public class NoUserException : Exception
    {
        public NoUserException(string message) : base(message)
        {
        }
    }
}