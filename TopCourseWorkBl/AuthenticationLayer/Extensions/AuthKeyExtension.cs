﻿using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TopCourseWorkBl.AuthenticationLayer.Extensions
{
    public static class AuthKeyExtension
    {
        public static SymmetricSecurityKey GetSymmetricSecurityKey(this string key)
            => new (Encoding.ASCII.GetBytes(key));
    }
}