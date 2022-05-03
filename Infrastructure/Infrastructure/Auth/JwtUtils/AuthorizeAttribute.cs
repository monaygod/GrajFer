using System;
using System.Linq;
using System.Security.Authentication;
using Infrastructure.Auth.Model;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Auth.JwtUtils
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeRouteAttribute : Attribute, IAuthorizationFilter
    {
        public string scopeNeeded { get; set; }
        public AuthorizeRouteAttribute(string scope = "")
        {
            scopeNeeded = scope;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // skip authorization if action is decorated with [AllowAnonymous] attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            // authorization
            var accessToken = (AccessToken)context.HttpContext.Items["User"];
            if (accessToken == null)
            {
                throw new AuthenticationException("Unauthorized!");
            }

            if (scopeNeeded != "" && !accessToken.Scopes.Contains(scopeNeeded))
                throw new UnauthorizedAccessException("Access denied!");
            if (accessToken.ExpTime < DateTime.UtcNow)
                throw new SecurityTokenExpiredException("Token Expired");

        }
    }
}