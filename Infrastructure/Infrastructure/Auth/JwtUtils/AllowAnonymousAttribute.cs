using System;

namespace Infrastructure.Auth.JwtUtils
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllowAnonymousAttribute : Attribute
    { }
}