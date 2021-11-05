﻿using System;

namespace TopCourseWorkBl.AuthenticationLayer.Exceptions
{
    public class TokenExpiredException : Exception
    {
        public TokenExpiredException(string message) : base(message)
        {
        }
    }
}