using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Auth.JwtUtils;
using Infrastructure.Auth.Model;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;


        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;

        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var claims = JwtUtils.ValidateJwtToken(token);
            if (claims != null)
            {
                context.Items["User"] = new AccessToken(claims);
            }
            else
            {
                context.Items["User"] = null;
            }
            await _next(context);
        }
    }
}